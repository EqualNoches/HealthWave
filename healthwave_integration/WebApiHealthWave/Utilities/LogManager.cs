using NLog;
using NLog.Config;
using NLog.Targets;

namespace WebApiHealthWave.Utilities
{
    public class LogManager<T>
    {
        // Configuración de Nlog utilizada por la clase LogHandler.
        private static readonly LoggingConfiguration _config = CreateConfiguration();

        // Instancia de Logger utilizada para registrar eventos, configurada para el tipo T.
        private readonly Logger _log = LogManager.GetLogger(typeof(T).FullName);

        /// <summary>
        /// Crea y configura la instancia de LoggingConfiguration para Nlog.
        /// </summary>
        /// <returns>Configuración de registro de eventos.</returns>
        private static LoggingConfiguration CreateConfiguration()
        {
            var config = new LoggingConfiguration();

            var logfile = new FileTarget("logfile") { FileName = "WebApiHealthWave.log" };
            var logconsole = new ConsoleTarget("logconsole");

            config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, logconsole);
            config.AddRule(NLog.LogLevel.Trace, NLog.LogLevel.Fatal, logfile);

            return config;
        }

        /// <summary>
        /// Constructor de la clase LogHandler. Asigna la configuración estática al LogManager.
        /// </summary>
        public LogManager()
        {
            LogManager.Configuration = _config;
        }

        /// <summary>
        /// Registra un mensaje informativo en el registro de eventos.
        /// </summary>
        /// <param name="message">El mensaje informativo a registrar.</param>
        public void LogInfo(string message)
        {
            _log.Info(message);
        }

        /// <summary>
        /// Registra un mensaje de depuración en el registro de eventos.
        /// </summary>
        /// <param name="message">El mensaje de depuración a registrar.</param>
        public void LogDebug(string message)
        {
            _log.Debug(message);
        }

        /// <summary>
        /// Registra un mensaje de error en el registro de eventos, junto con la excepción asociada.
        /// </summary>
        /// <param name="message">El mensaje de error a registrar.</param>
        /// <param name="ex">La excepción asociada al mensaje de error.</param>
        public void LogError(string message, Exception ex)
        {
            _log.Error(ex, message);
        }

        /// <summary>
        /// Registra un mensaje de advertencia en el registro de eventos, junto con la excepción asociada.
        /// </summary>
        /// <param name="message">El mensaje de advertencia a registrar.</param>
        /// <param name="ex">La excepción asociada al mensaje de advertencia.</param>
        public void LogWarn(string message, Exception ex)
        {
            _log.Warn(ex, message);
        }

        /// <summary>
        /// Registra un mensaje de fatalidad en el registro de eventos, junto con la excepción asociada.
        /// </summary>
        /// <param name="message">El mensaje de fatalidad a registrar.</param>
        /// <param name="ex">La excepción asociada al mensaje de fatalidad.</param>
        public void LogFatal(string message, Exception ex)
        {
            _log.Fatal(ex, message);
        }
    }
}
