namespace WebApiHBM.Models
{
    public class HBM_BonificacionCab
    {
        public string BonoGrupoId { get; set; }
        public string CodRegion { get; set; }
        public string CodCanal { get; set; }
        public int CantBonificacion { get; set; }
        public int CantidadMin { get; set; }
        public int CantObligatoriaMax { get; set; }
        public int CantObligatoriaMin { get; set; }
        public int Correlativo { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string FechaModificacion { get; set; }
    }
}