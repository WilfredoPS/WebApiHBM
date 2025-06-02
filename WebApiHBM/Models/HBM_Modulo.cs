namespace WebApiHBM.Models
{
    public class HBM_Modulo
    {
        
            public string ModuloID { get; set; }
            public string Nombre { get; set; }
            public string FotoImage { get; set; }
     }
    public class HBM_Rol
    {

        public string RolID { get; set; }
        public string Rol { get; set; }
        public string Descripcion { get; set; }
        public int Estado { get; set; }
        //public int MultiUser { get; set; }
    }
    public class HBM_RolModulo
    {
       public string RolID { get; set; }
        public string ModuloID { get; set; }
        public string FechaModificacion { get; set; }
    }
    public class HBM_Tablas
    {
        public string Nombre { get; set; }
        public string RangoDias { get; set; }
        public string CampoFecha { get; set; }
        public string FechaUltSync { get; set; }
        public string Rol { get; set; }
        public string Estado { get; set; }
        public string Observacion { get; set; }
        public string Sync { get; set; }
    }
    public class HBM_FormularioCampo
    {
        public string Id { get; set; }
        public string RolID { get; set; }
        public string Formulario { get; set; }
        public string Campo { get; set; }
        public int Ver { get; set; }
        public int Editar { get; set; }
        public string FechaModificacion { get; set; }
    }
    public class HBM_NroDocumentos
    {
        public int NroPedido { get; set; }
        public int NroRecibo { get; set; }
        public int NroRecepcionEquipo { get; set; }
        public int MaximosCorrectos { get; set; }
    }

    public class HBM_ParamActividad
    {
        public string RolID { get; set; }
        public string RutaObligatoria { get; set; }
        public string InicioQR { get; set; }
        public string FechaModificacion { get; set; }
    }
}