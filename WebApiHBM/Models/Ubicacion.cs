using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiHBM.Models
{
    public class Ubicacion
    {
        public class HBM_ClienteUbicacion
        {
            public string ClienteUbicacion { get; set; }
        }

        public class HBM_UbicacionActualizado
        {
            public string id  { get; set; }   
	        public string name  { get; set; }                  
	        public string date_entered  { get; set; }                   
	        public string date_modified  { get; set; }                   
	        public string modified_user_id  { get; set; }                  
	        public string created_by  { get; set; }                  
	        public string description  { get; set; }                 
	        public int deleted  { get; set; }                   
	        public string assigned_user_id  { get; set; }              
	        public string city  { get; set; }                   
	        public string state  { get; set; }                   
	        public string country  { get; set; }                   
	        public float jjwg_maps_lat  { get; set; }                   
	        public float jjwg_maps_lng  { get; set; }                   
	        public string marker_image  { get; set; } 
            //public string IdUbicacion { get; set; }
            //public string CodUsuario { get; set; }
            //public string Direccion { get; set; }
            //public string Fecha { get; set; }
            //public string Latitud { get; set; }
            //public string LatitudInicial { get; set; }
            //public string Longitud { get; set; }
            //public string LongitudInicial { get; set; }
            //public string kunnr { get; set; }
        }
        public class ubicacion {
            public string IdUbicacion { get; set; }
            public string CodUsuario { get; set; }
            public string Direccion { get; set; }
            public string Fecha { get; set; }
            public string Latitud { get; set; }
            public string LatitudInicial { get; set; }
            public string Longitud { get; set; }
            public string LongitudInicial { get; set; }
            public string kunnr { get; set; }
        }
        
    }
}