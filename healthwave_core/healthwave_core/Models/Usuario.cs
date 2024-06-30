using System;
using System.Collections.Generic;

namespace HospitalCore_core.Models
{
    public partial class Usuario
    {
        
        public string usuarioCodigo { get; set; }

      
        public string documentoUsuario { get; set; }

       
        public string usuarioContra { get; set; }

        public virtual PerfilUsuario PerfilUsuario { get; set; }
    }
}