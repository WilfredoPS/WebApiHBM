using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiHBM.Models
{
    public class HBM_UsuarioSector
    {
        public string UsuarioSectorID { get; set; }
        public string OrgVenta { get; set; }
        public string CodCanal { get; set; }
        public string CodSector { get; set; }
        public string Sector { get; set; }
        public string CodUsuario { get; set; }
        public string FechaModificacion { get; set; }
        public string Flujo { get; set; }
    }

    public class HBM_UsuarioAlmacen
    {
        public string UsuarioAlmacenID { get; set; }
        public string CodUsuario { get; set; }
        public string Almacen { get; set; }
        public string CodCanal { get; set; }
        public string Centro { get; set; }
    }
}