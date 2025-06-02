using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiHBM.Models
{
    public class HBM_Contacto
    {
        public string ContactoID { get; set; }
        public string KUNNR { get; set; }   
        public int CodContacto { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CI { get; set; }
        public string Celular { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Cargo { get; set; }
        public string CodDepartamento { get; set; }
        public string CodFuncion { get; set; }
        public string Comentario { get; set; }
        public string Fax { get; set; }
        public string FechaModificacion { get; set; }
        public string TratamientoID { get; set; }
        public string Observacion { get; set; }
    }
}