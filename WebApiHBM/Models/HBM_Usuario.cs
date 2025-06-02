using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiHBM.Models
{
    public class HBM_Usuario
    {
        public string Organizacion { get; set; }
        public string CodUsuario { get; set; }
        public string Cargo { get; set; }
        public string RolID { get; set; }        
    }

    public class HBM_UsuarioRol
    {
        public string Organizacion { get; set; }
        public string CodUsuario { get; set; }
        public string Nombre { get; set; }
        public string Cargo { get; set; }
        public string RolID { get; set; }
        public string Rol { get; set; }
        public string RegionalID { get; set; }
        public string Regional { get; set; }        
    }
}