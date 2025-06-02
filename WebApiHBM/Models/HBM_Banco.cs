using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiHBM.Models
{
    public class HBM_Banco
    {
        public string Codigo { get; set; }
        public string Moneda { get; set; }
        public string BancoID { get; set; }
        public string Nombre { get; set; }
        public string FechaModificacion { get; set; }        
    }
}