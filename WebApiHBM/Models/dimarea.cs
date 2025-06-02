namespace WebApiHBM.Models
{
    public class dimarea
    {
        public string ProductID { get; set; }
        public string ProductCode { get; set; }
        public string Almacen { get; set; }
        public string OrgVenta { get; set; }
        public string CodCanal { get; set; }
        public string CodSector { get; set; }
        public string CodFamilia { get; set; }
        public string CodGrupo { get; set; }
        public string CodSubGrupo { get; set; }
        public string ProductName { get; set; }
        public string CodigoBarras { get; set; }
        public decimal DescPromocional { get; set; }
        public string CodProveedor { get; set; }
        public string ConStock { get; set; }
        public string Moneda { get; set; }
        public string TipoUnidad { get; set; }
        public string FechaModificacion { get; set; }
        public string CantidadXUnidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Stock { get; set; }
        public int StockNacional { get; set; }
        public int CodActividad { get; set; }
        public int CodClasificador { get; set; }
        public int CodCatalogo { get; set; }
        public int Serie { get; set; }
        //public string Catalogo { get; set; }

    }

    public class StockProd
    {
        public string ProductCode { get; set; }
    }

    public class Stocks
    {
        public string ProductCode { get; set; }
        public string Almacen { get; set; }
        public int Stock { get; set; }
    }
}