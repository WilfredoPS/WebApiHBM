using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiHBM.Models
{
    public class HBM_PlanInventarios
    {
        public string KUNNR { get; set; }
        public string ProductCode { get; set; }
        public int CantidadMin { get; set; }
        public int CantidadMax { get; set; }
        public string CodUsuario { get; set; }
        public string Fecha { get; set; }
        public string Codigo_Vendedor { get; set; }
        public string OrgVenta { get; set; }
        public string CodCanal { get; set; }
        public string CodSector { get; set; } 
    }

    public class HBM_Inventarios
    {
        public string InventarioID { get; set; }
        public string TareaID { get; set; }
        public string ActividadID { get; set; }
        public string ProductCode { get; set; }
        public string CodUsuario { get; set; }
        public int CantidadMin { get; set; }
        public int CantidadMax { get; set; }
        public int Cantidad { get; set; }
        public string Fecha { get; set; }
        public string Programado { get; set; }
        public string KUNNR { get; set; }
        public string ProductName { get; set; }
        public string CodSector { get; set; }
        public string CodFamilia { get; set; }
        public string CodGrupo { get; set; }
        public string CodSubGrupo { get; set; }
        public string Sector { get; set; }
        public string Familia { get; set; }
        public string Grupo { get; set; }
        public string SubGrupo { get; set; }
    }
    
    public class HBM_ParamInventarios
    {
        public string ParamInventarioID { get; set; }
        public string CodUsuario { get; set; }
        public string InvTodos { get; set; }
        public string InvCliente { get; set; }
        public string Modo { get; set; }
        public string Semana1 { get; set; }
        public string Semana2 { get; set; }
        public string Semana3 { get; set; }
        public string Semana4 { get; set; }
        public string Semana5 { get; set; }
        public string FechaModificacion { get; set; }
    }


}