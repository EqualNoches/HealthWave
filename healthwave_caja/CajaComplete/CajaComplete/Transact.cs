using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace CajaComplete
{
    public class ConnHandlingTransaction : IDisposable
    {
        private SqlConnection conn;
        public SqlCommand cmd;
        private SqlTransaction tran;
        private bool status = false;
        private bool executed = false;
        private static SemaphoreSlim semaphore = new SemaphoreSlim(1, 1); // Allow only one connection at a time
        private const int Timeout = 30000; // 30 seconds timeout
        private static string logFilePath = "command_log.txt";
        private static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private static Task pollingTask;

        public ConnHandlingTransaction(string connStr)
        {
            // Start the polling task to check server connectivity
            pollingTask = Task.Run(() => PollServerConnectivity(connStr, cancellationTokenSource.Token));

            if (!semaphore.Wait(Timeout))
            {
                throw new TimeoutException("Could not obtain a database connection within the timeout period.");
            }

            try
            {
                conn = new SqlConnection(connStr);
                conn.Open();
            }
            catch (Exception)
            {
                // If connection fails, release the semaphore and rethrow the exception
                semaphore.Release();
                throw;
            }
        }

        private static async Task PollServerConnectivity(string connStr, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    using (var conn = new SqlConnection(connStr))
                    {
                        await conn.OpenAsync(token);
                    }

                    // Server is back online, execute pending commands
                    await ExecutePendingCommands(connStr);
                }
                catch
                {
                    // Server is still down, wait and retry
                    await Task.Delay(5000, token); // Poll every 5 seconds
                }
            }
        }

        private static async Task ExecutePendingCommands(string connStr)
        {
            if (!File.Exists(logFilePath)) return;

            var commandJsons = File.ReadAllLines(logFilePath);
            using (var conn = new SqlConnection(connStr))
            {
                await conn.OpenAsync();
                foreach (var commandJson in commandJsons)
                {
                    var command = JsonConvert.DeserializeObject<Command>(commandJson);
                    using (var cmd = new SqlCommand(command.cmdText, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        foreach (var param in command.cmdParams)
                        {
                            cmd.Parameters.AddWithValue(param.Key, param.Value);
                        }
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }

            File.Delete(logFilePath); // Clear the log file after successful execution
        }

        public bool SetNewTransaction(string storedProc, Dictionary<string, object> parameters = null)
        {
            try
            {
                if (executed)
                {
                    if (!status)
                    {
                        tran.Commit();
                    }
                    tran.Dispose();
                }

                executed = false;
                status = false;
                cmd = new SqlCommand(storedProc, conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                tran = conn.BeginTransaction();
                cmd.Transaction = tran;

                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        cmd.Parameters.AddWithValue(param.Key, param.Value);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"SetNewTransaction Error: {ex.Message}");
                return false;
            }
        }

        public int? ExecuteNonQuery()
        {
            try
            {
                executed = true;
                return cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"ExecuteNonQuery SQL Error: {ex.Message}");
                RollbackTransaction();
                LogCommandToFile("nq", cmd.CommandText, cmd.Parameters);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ExecuteNonQuery Error: {ex.Message}");
                RollbackTransaction();
                LogCommandToFile("nq", cmd.CommandText, cmd.Parameters);
                return null;
            }
        }

        public SqlDataReader ExecuteReader()
        {
            try
            {
                executed = true;
                return cmd.ExecuteReader();
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"ExecuteReader SQL Error: {ex.Message}");
                RollbackTransaction();
                LogCommandToFile("rd", cmd.CommandText, cmd.Parameters);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ExecuteReader Error: {ex.Message}");
                RollbackTransaction();
                LogCommandToFile("rd", cmd.CommandText, cmd.Parameters);
                return null;
            }
        }

        public object ExecuteScalar()
        {
            try
            {
                executed = true;
                return cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"ExecuteScalar SQL Error: {ex.Message}");
                RollbackTransaction();
                LogCommandToFile("sc", cmd.CommandText, cmd.Parameters);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ExecuteScalar Error: {ex.Message}");
                RollbackTransaction();
                LogCommandToFile("sc", cmd.CommandText, cmd.Parameters);
                return null;
            }
        }

        public System.Xml.XmlReader ExecuteXmlReader()
        {
            try
            {
                executed = true;
                return cmd.ExecuteXmlReader();
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"ExecuteXmlReader SQL Error: {ex.Message}");
                RollbackTransaction();
                LogCommandToFile("xr", cmd.CommandText, cmd.Parameters);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ExecuteXmlReader Error: {ex.Message}");
                RollbackTransaction();
                LogCommandToFile("xr", cmd.CommandText, cmd.Parameters);
                return null;
            }
        }

        private void RollbackTransaction()
        {
            try
            {
                tran.Rollback();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Rollback Error: {ex.Message}");
            }
            finally
            {
                tran.Dispose();
                status = true;
            }
        }

        private void LogCommandToFile(string cmdCall, string cmdText, SqlParameterCollection parameters)
        {
            var command = new Command
            {
                cmdCall = cmdCall,
                cmdText = cmdText,
                cmdParams = new Dictionary<string, object>()
            };

            foreach (SqlParameter param in parameters)
            {
                command.cmdParams[param.ParameterName] = param.Value;
            }

            var commandJson = JsonConvert.SerializeObject(command);
            File.AppendAllText(logFilePath, commandJson + Environment.NewLine);
        }

        public void Dispose()
        {
            try
            {
                if (!status && tran != null)
                {
                    tran.Commit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Dispose Commit Error: {ex.Message}");
            }
            finally
            {
                tran?.Dispose();
                cmd?.Dispose();
                conn?.Close();
                semaphore.Release();
            }
        }

        private class Command
        {
            public string cmdCall { get; set; }
            public string cmdText { get; set; }
            public Dictionary<string, object> cmdParams { get; set; }
        }
    }
}
