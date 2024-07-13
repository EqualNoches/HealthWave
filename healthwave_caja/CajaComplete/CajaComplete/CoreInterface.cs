using System;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.Identity.Client;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CajaComplete
{
    public class CoreInterface
    {
        private static HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5042"),
            Timeout = new TimeSpan(0, 0, 10)
        };



        public Usuario login(string documento, string password)
        {
            try
            {
                string jsonString = client.GetStringAsync($"api/Usuario/get{documento}").Result;
                Usuario u = JsonConvert.DeserializeObject<Usuario>(jsonString);
                if (u.usuarioContra == password && u.rol == "C")
                {
                    return u;
                }
                else
                {
                    return null;
                }
            }
            catch (AggregateException ex) {
                MessageBox.Show("Core no esta disponible.", "Error de Conexion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new TimeoutException("Core Timed Out.");
            }
            

        }

        public List<Servicio> GetServicios()
        {
            return JsonConvert.DeserializeObject<List<Servicio>>(client.GetStringAsync("api/Servicios/get").Result);
        }

        public string test()
        {
            return client.GetStringAsync("api/Usuario/get").Result;
        }
    }

    public class Usuario
    {

        public string usuarioCodigo { get; set; }
        public string codigoDocumento { get; set; }
        public string usuarioContra { get; set; }
        public string tipoDocumento { get; set; }
        public string numLicenciaMedica { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string genero { get; set; }
        public string fechaNacimiento { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public string direccion { get; set; }
        public string rol { get; set; }
    }

    public class Servicio
    {
        public int servicioCodigo { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int? idTipoServicio { get; set; }
        public float costo { get; set; }
        public int? idAseguradora { get; set; }
    }
}