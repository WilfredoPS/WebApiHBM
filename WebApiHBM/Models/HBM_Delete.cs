using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiHBM.Models
{
    public class HBM_Delete
    {
        public string DeleteTrack_ID { get; set; }
        public string ID { get; set; }
        public string Tabla { get; set; } 
        public string FechaModificacion { get; set; }
        public string CodUsuario { get; set; }
        public string Campo_Llave { get; set; }
    }
}