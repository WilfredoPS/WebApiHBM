using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Web.Http;
using WebApiHBM.Models;
using System.Data;
using System.Threading.Tasks;

namespace WebApiHBM.Controllers
{
    public class CobranzasController : ApiController
    {
        [HttpPost]
        public object ObtenerBanco()
        {
            List<HBM_Banco> ListBanco = new List<HBM_Banco>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select * from Banco ;";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListBanco.Add(new HBM_Banco()
                                {
                                    Codigo = reader.GetString("Codigo"),
                                    Moneda = reader.GetString("Moneda"),
                                    BancoID = reader.GetString("BancoID"),
                                    Nombre = reader.GetString("Nombre"),
                                    FechaModificacion = reader.GetString("FechaModificacion")
                                });
                            }
                        }
                    }
                }
                return ListBanco;
            }
            catch (MySqlException e)
            {
                conexion_mysql_open.cerrar_conexion();
                List<string> Listmensajes = new List<string>();
                Listmensajes.Add(e.Message.ToString());
                return Listmensajes;
            }
        }
        [HttpPost]
        public object ObtenerBanco02(credenciales fecha)
        {
            List<HBM_Banco> ListBanco = new List<HBM_Banco>();
            string  query = "";
            try
            {
                if (fecha.fecha.ToString() == "1900-01-01 00:00:00")
                {
                    query = "select * from Banco";
                }
                else
                {
                    query = "select * from Banco WHERE FechaModificacion >= '" + fecha.fecha.ToString() +"';";
                }
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = query;
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListBanco.Add(new HBM_Banco()
                                {
                                    Codigo = reader.GetString("Codigo"),
                                    Moneda = reader.GetString("Moneda"),
                                    BancoID = reader.GetString("BancoID"),
                                    Nombre = reader.GetString("Nombre"),
                                    FechaModificacion = reader.GetString("FechaModificacion")
                                });
                            }
                        }
                    }
                }
                return Tuple.Create(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), ListBanco);
            }
            catch (MySqlException e)
            {
                conexion_mysql_open.cerrar_conexion();
                List<string> Listmensajes = new List<string>();
                Listmensajes.Add(e.Message.ToString());
                return Listmensajes;

            }
        }
        [HttpPost]
        public object ObtenerRecibos(credenciales usuario)
        {
            string w = "";
            string RangoDias = "";
            List<HBM_Recibo> ListRecibos = new List<HBM_Recibo>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM hbm.Tablas WHERE Nombre = 'Recibo';";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                RangoDias = reader.GetString("RangoDias");
                            }
                        }
                    }
                }

                var con = conexion_mysql_open.obtener_conexion();
                using (var commands = con.CreateCommand())
                {
                    commands.CommandText = " select * " +
                            " from Recibo " +
                            " where IdVendedor = '" + usuario.usuario.ToString() + "'" +
                            " AND FechaModificacion > DATE_ADD(NOW(), INTERVAL - " + RangoDias + " DAY) " +
                            " union " +
                            " select * " +
                            " from ReciboHistorico " +
                            " where IdVendedor = '" + usuario.usuario.ToString() + "'" +
                            " AND FechaModificacion > DATE_ADD(NOW(), INTERVAL - " + RangoDias + " DAY) ";
                    using (var readers = commands.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (readers.HasRows)
                        {
                            while (readers.Read())
                            {


                                w = readers.GetString("ReciboID");

                                string notas = "";

                                if (!readers.IsDBNull(readers.GetOrdinal("Notas")))
                                {

                                    notas = readers.GetString("Notas");
                                }

                                else
                                {
                                    notas = "";
                                    //string notas = readers.GetString(readers.GetOrdinal("Notas"));

                                }

                                ListRecibos.Add(new HBM_Recibo()
                                {
                                    ReciboID = readers.GetString("ReciboID"),
                                    ActividadID = readers.GetString("ActividadID"),
                                    TareaID = readers.GetString("TareaID"),
                                    CodBanco = readers.GetString("CodBanco"),
                                    Descuento = Convert.ToDecimal(readers.GetString("Descuento")),
                                    IdVendedor = readers.GetString("IdVendedor"),
                                    FechaDoc = readers.GetString("FechaDoc"),
                                    MonedaDoc = readers.GetString("MonedaDoc"),
                                    KUNNR = readers.GetString("KUNNR"),
                                    Latitud = Convert.ToDecimal(readers.GetString("Latitud")),
                                    Longitud = Convert.ToDecimal(readers.GetString("Longitud")),
                                    FechaModificacion = readers.GetString("FechaModificacion"),
                                    NroDocumento = readers.GetString("NroDocumento"),
                                    OtroBanco = readers.GetString("OtroBanco"),
                                    ReAnulado = Convert.ToInt32(readers.GetString("ReAnulado")),
                                    CodRecibo = readers.GetString("CodRecibo"),
                                    Confirmacion = readers.GetString("Confirmacion"),
                                    DZ1 = readers.GetString("DZ1"),
                                    DZ2 = readers.GetString("DZ2"),
                                    Estado = readers.GetString("Estado"),
                                    Fecha = readers.GetString("Fecha"),
                                    FormaPago = readers.GetString("FormaPago"),
                                    FormaPagoSAP = readers.GetString("FormaPagoSAP"),
                                    SolAnulacion = readers.GetString("SolAnulacion"),
                                    ImporteBS = Convert.ToDecimal(readers.GetString("ImporteBS")),
                                    ImporteUSD = Convert.ToDecimal(readers.GetString("ImporteUSD")),
                                    ImpTotalBS = Convert.ToDecimal(readers.GetString("ImpTotalBS")),
                                    ImpTotalUSD = Convert.ToDecimal(readers.GetString("ImpTotalUSD")),
                                    Moneda = readers.GetString("Moneda"),
                                    NroReciboManual = readers.GetString("NroReciboManual"),
                                    Observacion = readers.GetString("Observacion"),
                                    TipoCambio = Convert.ToDecimal(readers.GetString("TipoCambio")),
                                    TotalBS = Convert.ToDecimal(readers.GetString("TotalBS")),
                                    TotalUSD = Convert.ToDecimal(readers.GetString("TotalUSD")),
                                    NroPosiciones = readers.GetString("NroPosiciones"),
                                    Notas = notas,
                                });
                            }
                        }
                    }
                }
                
                return ListRecibos;
            }
            catch (MySqlException e)
            {
                conexion_mysql_open.cerrar_conexion();
                List<string> Listmensajes = new List<string>();
                Listmensajes.Add(e.Message.ToString());
                return Listmensajes;
            }
        }
        [HttpPost]
        public object ObtenerRecibos02(credenciales usuario)
        {
            string w = "";
            string RangoDias = "", query = "";
            List<HBM_Recibo> ListRecibos = new List<HBM_Recibo>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM hbm.Tablas WHERE Nombre = 'Recibo';";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                RangoDias = reader.GetString("RangoDias");
                            }
                        }
                    }
                }

                var con = conexion_mysql_open.obtener_conexion();
                using (var commands = con.CreateCommand())
                {
                    if (usuario.fecha.ToString() == "1900-01-01 00:00:00")
                    {
                        query = " select * " +
                                " from Recibo " +
                                " where IdVendedor = '" + usuario.usuario.ToString() + "'" +
                                " AND FechaModificacion > DATE_ADD(NOW(), INTERVAL  - " + RangoDias + " DAY) " +
                                " union " +
                                " select * " +
                                " from ReciboHistorico " +
                                " where IdVendedor = '" + usuario.usuario.ToString() + "'" +
                                " AND FechaModificacion > DATE_ADD(NOW(), INTERVAL - " + RangoDias + " DAY) ";
                    }
                    else
                    {
                        query = " select * " +
                                " from Recibo " +
                                " where IdVendedor = '" + usuario.usuario.ToString() + "'" +
                                " AND FechaModificacion >= '" + usuario.fecha.ToString() + "'" +
                                " AND FechaModificacion > DATE_ADD(NOW(), INTERVAL - " + RangoDias + " DAY) " +
                                " union " +
                                " select * " +
                                " from ReciboHistorico " +
                                " where IdVendedor = '" + usuario.usuario.ToString() + "'" +
                                " AND FechaModificacion >= '" + usuario.fecha.ToString() + "'" +
                                " AND FechaModificacion > DATE_ADD(NOW(), INTERVAL - " + RangoDias + " DAY) ";
                    }

                    commands.CommandText = query;

                    using (var readers = commands.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (readers.HasRows)
                        {
                            while (readers.Read())
                            {

                                w = readers.GetString("ReciboID");

                                string notas = "";

                                if (!readers.IsDBNull(readers.GetOrdinal("Notas")))
                                {

                                    notas = readers.GetString("Notas");
                                }

                                else
                                {
                                    notas = "";
                                    //string notas = readers.GetString(readers.GetOrdinal("Notas"));

                                }
                                ListRecibos.Add(new HBM_Recibo()
                                {
                                    ReciboID = readers.GetString("ReciboID"),
                                    ActividadID = readers.GetString("ActividadID"),
                                    TareaID = readers.GetString("TareaID"),
                                    CodBanco = readers.GetString("CodBanco"),
                                    Descuento = Convert.ToDecimal(readers.GetString("Descuento")),
                                    IdVendedor = readers.GetString("IdVendedor"),
                                    FechaDoc = readers.GetString("FechaDoc"),
                                    MonedaDoc = readers.GetString("MonedaDoc"),
                                    KUNNR = readers.GetString("KUNNR"),
                                    Latitud = Convert.ToDecimal(readers.GetString("Latitud")),
                                    Longitud = Convert.ToDecimal(readers.GetString("Longitud")),
                                    FechaModificacion = readers.GetString("FechaModificacion"),
                                    NroDocumento = readers.GetString("NroDocumento"),
                                    OtroBanco = readers.GetString("OtroBanco"),
                                    ReAnulado = Convert.ToInt32(readers.GetString("ReAnulado")),
                                    CodRecibo = readers.GetString("CodRecibo"),
                                    Confirmacion = readers.GetString("Confirmacion"),
                                    DZ1 = readers.GetString("DZ1"),
                                    DZ2 = readers.GetString("DZ2"),
                                    Estado = readers.GetString("Estado"),
                                    Fecha = readers.GetString("Fecha"),
                                    FormaPago = readers.GetString("FormaPago"),
                                    FormaPagoSAP = readers.GetString("FormaPagoSAP"),
                                    SolAnulacion = readers.GetString("SolAnulacion"),
                                    ImporteBS = Convert.ToDecimal(readers.GetString("ImporteBS")),
                                    ImporteUSD = Convert.ToDecimal(readers.GetString("ImporteUSD")),
                                    ImpTotalBS = Convert.ToDecimal(readers.GetString("ImpTotalBS")),
                                    ImpTotalUSD = Convert.ToDecimal(readers.GetString("ImpTotalUSD")),
                                    Moneda = readers.GetString("Moneda"),
                                    NroReciboManual = readers.GetString("NroReciboManual"),
                                    Observacion = readers.GetString("Observacion"),
                                    TipoCambio = Convert.ToDecimal(readers.GetString("TipoCambio")),
                                    TotalBS = Convert.ToDecimal(readers.GetString("TotalBS")),
                                    TotalUSD = Convert.ToDecimal(readers.GetString("TotalUSD")),
                                    NroPosiciones = readers.GetString("NroPosiciones"),
                                    Notas = notas,
                                });
                            }
                        }
                    }
                }
                return Tuple.Create(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), ListRecibos);
            }
            catch (MySqlException e)
            {
                conexion_mysql_open.cerrar_conexion();
                List<string> Listmensajes = new List<string>();
                Listmensajes.Add(e.Message.ToString());
                return Listmensajes;
            }
        }
        [HttpPost]
        public object ObtenerAbonos(credenciales usuario)
        {
            List<HBM_Abono> ListAbono = new List<HBM_Abono>();
            string RangoDias = "";
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM hbm.Tablas WHERE Nombre = 'Recibo';";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                RangoDias = reader.GetString("RangoDias");
                            }
                        }
                    }
                }

                var con = conexion_mysql_open.obtener_conexion();
                using (var commands = con.CreateCommand())
                {
                    commands.CommandText = "select a.* from hbm.Recibo r inner join  hbm.Abono a on r.ReciboID=a.ReciboID and r.IdVendedor = '" + usuario.usuario.ToString() + "'" +
                              " AND r.FechaModificacion > DATE_ADD(NOW(), INTERVAL - " + RangoDias + " DAY) "
                             + " union "
                             + "select a.* from hbm.ReciboHistorico r inner join  hbm.AbonoHistorico a on r.ReciboID=a.ReciboID and r.IdVendedor = '" + usuario.usuario.ToString() + "'" +
                             " AND r.FechaModificacion > DATE_ADD(NOW(), INTERVAL - " + RangoDias + " DAY) ";

                    using (var readers = commands.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (readers.HasRows)
                        {
                            while (readers.Read())
                            {

                                string zfacturaid = "";

                                if (!readers.IsDBNull(readers.GetOrdinal("FacturaID")))
                                {

                                    zfacturaid = readers.GetString("FacturaID");
                                }

                                else
                                {
                                    zfacturaid = "";
                                    //string notas = readers.GetString(readers.GetOrdinal("Notas"));

                                }

                                ListAbono.Add(new HBM_Abono()
                                {
                                    AbonoID = readers.GetString("AbonoID"),
                                    CodFactura = readers.GetString("CodFactura"),
                                    CodRecibo = readers.GetString("CodRecibo"),
                                    Descuento = Convert.ToDecimal(readers.GetString("Descuento")),
                                    FacSaldoBS = Convert.ToDecimal(readers.GetString("FacSaldoBS")),
                                    FacSaldoUSD = Convert.ToDecimal(readers.GetString("FacSaldoUSD")),
                                    FechaCreacion = readers.GetString("FechaCreacion"),
                                    ImporteBS = Convert.ToDecimal(readers.GetString("ImporteBS")),
                                    ImporteUSD = Convert.ToDecimal(readers.GetString("ImporteUSD")),
                                    MontoFacBS = Convert.ToDecimal(readers.GetString("MontoFacBS")),
                                    MontoFacUSD = Convert.ToDecimal(readers.GetString("MontoFacUSD")),
                                    Parcial = readers.GetString("Parcial"),
                                    TipoCambio = Convert.ToDecimal(readers.GetString("TipoCambio")),
                                    Tipo = readers.GetString("Tipo"),
                                    TotalBS = Convert.ToDecimal(readers.GetString("TotalBS")),
                                    TotalUSD = Convert.ToDecimal(readers.GetString("TotalUSD")),
                                    CODSBO = readers.GetString("CODSBO"),
                                    IdVendedor = readers.GetString("IdVendedor"),
                                    KUNNR = readers.GetString("KUNNR"),
                                    FechaModificacion = readers.GetString("FechaModificacion"),
                                    ReciboID = readers.GetString("ReciboID"),
                                    Observacion = readers.GetString("Observacion"),
                                    NroDocContable = readers.GetString("NroDocContable"),
                                    FacturaID = zfacturaid,
                                });
                            }
                        }
                    }
                }
                return ListAbono;
            }
            catch (MySqlException e)
            {
                conexion_mysql_open.cerrar_conexion();
                List<string> Listmensajes = new List<string>();
                Listmensajes.Add(e.Message.ToString());
                return Listmensajes;
            }
        }
        [HttpPost]
        public object ObtenerAbonos02(credenciales usuario)
        {
            List<HBM_Abono> ListAbono = new List<HBM_Abono>();
            string RangoD = "", query ="";
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM hbm.Tablas WHERE Nombre = 'Recibo';";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                RangoD = reader.GetString("RangoDias");
                            }
                        }
                    }
                }
                var con = conexion_mysql_open.obtener_conexion();
                using (var commands = con.CreateCommand())
                {
                    if (usuario.fecha.ToString() == "1900-01-01 00:00:00")
                    {
                        query = " select " +
                                " a.* " +
                                " from hbm.Recibo r " +
                                " inner join hbm.Abono a " +
                                " on r.ReciboID = a.ReciboID " +
                                " and r.IdVendedor ='" + usuario.usuario.ToString() + "'" +
                                " and r.FechaModificacion > DATE_ADD(NOW(), INTERVAL - " + RangoD + " DAY) " +
                                " union " +
                                " select " +
                                " a.* " +
                                " from hbm.ReciboHistorico r " +
                                " inner join hbm.AbonoHistorico a " +
                                " on r.ReciboID=a.ReciboID " +
                                " and r.IdVendedor = '" + usuario.usuario.ToString() + "'" +
                                " and r.FechaModificacion > DATE_ADD(NOW(), INTERVAL - " + RangoD + " DAY) ";
                    }
                    else
                    {
                        query = " select " +
                                " a.* " +
                                " from hbm.Recibo r " +
                                " inner join hbm.Abono a " +
                                " on r.ReciboID = a.ReciboID " +
                                " and r.IdVendedor = '" + usuario.usuario.ToString() + "'" +
                                " and r.FechaModificacion >= '" + usuario.fecha.ToString() + "'" +
                                " and r.FechaModificacion > DATE_ADD(NOW(), INTERVAL -" + RangoD + " DAY) " +
                                " union " +
                                " select " +
                                " a.* " +
                                " from hbm.ReciboHistorico r " +
                                " inner join hbm.AbonoHistorico a " +
                                " on r.ReciboID=a.ReciboID " +
                                " and r.IdVendedor = '" + usuario.usuario.ToString() + "'" +
                                " and r.FechaModificacion >='" + usuario.fecha.ToString() + "'" +
                                " and r.FechaModificacion > DATE_ADD(NOW(), INTERVAL -"  + RangoD + " DAY) "; 
                    }

                    commands.CommandText = query;

                    using (var readers = commands.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (readers.HasRows)
                        {
                            while (readers.Read())
                            {

                                string zfacturaid = "";

                                if (!readers.IsDBNull(readers.GetOrdinal("FacturaID")))
                                {

                                    zfacturaid = readers.GetString("FacturaID");
                                }

                                else
                                {
                                    zfacturaid = "";
                                    //string notas = readers.GetString(readers.GetOrdinal("Notas"));

                                }

                                ListAbono.Add(new HBM_Abono()
                                {
                                    AbonoID = readers.GetString("AbonoID"),
                                    CodFactura = readers.GetString("CodFactura"),
                                    CodRecibo = readers.GetString("CodRecibo"),
                                    Descuento = Convert.ToDecimal(readers.GetString("Descuento")),
                                    FacSaldoBS = Convert.ToDecimal(readers.GetString("FacSaldoBS")),
                                    FacSaldoUSD = Convert.ToDecimal(readers.GetString("FacSaldoUSD")),
                                    FechaCreacion = readers.GetString("FechaCreacion"),
                                    ImporteBS = Convert.ToDecimal(readers.GetString("ImporteBS")),
                                    ImporteUSD = Convert.ToDecimal(readers.GetString("ImporteUSD")),
                                    MontoFacBS = Convert.ToDecimal(readers.GetString("MontoFacBS")),
                                    MontoFacUSD = Convert.ToDecimal(readers.GetString("MontoFacUSD")),
                                    Parcial = readers.GetString("Parcial"),
                                    TipoCambio = Convert.ToDecimal(readers.GetString("TipoCambio")),
                                    Tipo = readers.GetString("Tipo"),
                                    TotalBS = Convert.ToDecimal(readers.GetString("TotalBS")),
                                    TotalUSD = Convert.ToDecimal(readers.GetString("TotalUSD")),
                                    CODSBO = readers.GetString("CODSBO"),
                                    IdVendedor = readers.GetString("IdVendedor"),
                                    KUNNR = readers.GetString("KUNNR"),
                                    FechaModificacion = readers.GetString("FechaModificacion"),
                                    ReciboID = readers.GetString("ReciboID"),
                                    Observacion = readers.GetString("Observacion"),
                                    NroDocContable = readers.GetString("NroDocContable"),
                                    FacturaID = zfacturaid,
                                });
                            }
                        }
                    }
                }
                return Tuple.Create(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), ListAbono);
            }
            catch (MySqlException e)
            {
                conexion_mysql_open.cerrar_conexion();
                List<string> Listmensajes = new List<string>();
                Listmensajes.Add(e.Message.ToString());
                return Listmensajes;
            }
        }
        [HttpPost]
        public object ObtenerFacturas(credenciales usuario)
        {
            List<HBM_Facturas> ListFacturas = new List<HBM_Facturas>();
            DataTable dt = new DataTable();
            try
            {
                string DivisionID = "", RegionalCBZ = "", DivisionCBZ = "", query = "";

                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select * from Usuario where CodUsuario = '" + usuario.usuario.ToString() + "'";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                DivisionID = reader.GetString("DivisionID");
                                RegionalCBZ = reader.GetString("RegionalCBZ");
                                DivisionCBZ = reader.GetString("DivisionCBZ");
                            }
                        }
                    }
                }
                if (DivisionID == "99")
                {
                     query = "";

                    if ("00".Equals(DivisionCBZ) && !"00".Equals(RegionalCBZ))
                    {
                        query = "select * from Facturas where Regional = '" + RegionalCBZ + "'";
                    }
                    if ("00".Equals(RegionalCBZ) && !"00".Equals(DivisionCBZ))
                    {
                        query = "select * from Facturas where Division = '" + DivisionCBZ + "'";
                    }
                    if (!"00".Equals(DivisionCBZ) && !"00".Equals(RegionalCBZ))
                    {
                        query = "select * from Facturas where Division = '" + DivisionCBZ + "' and Regional = '" + RegionalCBZ + "'";
                    }
                    if ("00".Equals(DivisionCBZ) && "00".Equals(RegionalCBZ))
                    {
                        query = "select * from Facturas";
                    }
                }
                else
                {
                     query = "select * from Facturas where IdVendedor = '" + usuario.usuario.ToString() +"'";
                }

                var con = conexion_mysql_open.obtener_conexion();
                using (var command1 = con.CreateCommand())
                {
                    command1.CommandText = query;
                    using (var readers1 = command1.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (readers1.HasRows)
                        {
                            while (readers1.Read())
                            {
                                ListFacturas.Add(new HBM_Facturas()
                                {
                                    FacturaID = readers1.GetString("FacturaID"),
                                    IdVendedor = readers1.GetString("IdVendedor"),
                                    AM = readers1.GetString("AM"),
                                    BMoraBS = Convert.ToDecimal(readers1.GetString("BMoraBS")),
                                    BMoraUSD = Convert.ToDecimal(readers1.GetString("BMoraUSD")),
                                    BSaldoBS = Convert.ToDecimal(readers1.GetString("BSaldoBS")),
                                    BSaldoUSD = Convert.ToDecimal(readers1.GetString("BSaldoUSD")),
                                    CodFactura = readers1.GetString("CodFactura"),
                                    CODSBO = readers1.GetString("CODSBO"),
                                    DiaMora = Convert.ToInt32(readers1.GetString("DiaMora")),
                                    FecEmision = readers1.GetString("FecEmision"),
                                    FecVencimiento = readers1.GetString("FecVencimiento"),
                                    MoraCarteraBS = Convert.ToDecimal(readers1.GetString("MoraCarteraBS")),
                                    MoraCarteraUSD = Convert.ToDecimal(readers1.GetString("MoraCarteraUSD")),
                                    MoraFacturaUSD = Convert.ToDecimal(readers1.GetString("MoraFacturaUSD")),
                                    Moneda = readers1.GetString("Moneda"),
                                    MonedaFacturaBS = Convert.ToDecimal(readers1.GetString("MonedaFacturaBS")),
                                    NombreCliente = readers1.GetString("NombreCliente"),
                                    PagoBS = Convert.ToDecimal(readers1.GetString("PagoBS")),
                                    PagoUSD = Convert.ToDecimal(readers1.GetString("PagoUSD")),
                                    SaldoBS = Convert.ToDecimal(readers1.GetString("SaldoBS")),
                                    SaldoUSD = Convert.ToDecimal(readers1.GetString("SaldoUSD")),
                                    SePaga = readers1.GetString("SePaga"),
                                    TCFactura = Convert.ToDecimal(readers1.GetString("TCFactura")),
                                    MCODMora = readers1.GetString("MCODMora"),
                                    KUNNR = readers1.GetString("KUNNR"),
                                    FechaModificacion = readers1.GetString("FechaModificacion"),
                                    Contado = Convert.ToInt32(readers1.GetString("Contado")),
                                });
                            }
                        }
                    }
                }
                return ListFacturas;
            }
            catch (MySqlException e)
            {
                conexion_mysql_open.cerrar_conexion();
                List<string> Listmensajes = new List<string>();
                Listmensajes.Add(e.Message.ToString());
                return Listmensajes;
            }
            return ListFacturas;
        }
        [HttpPost]
        public object ObtenerFacturas02(credenciales usuario)
        {
            List<HBM_Facturas> ListFacturas = new List<HBM_Facturas>();
            DataTable dt = new DataTable();
            try
            {
                string DivisionID = "", RegionalCBZ = "", DivisionCBZ = "", query = "";

                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select * from Usuario where CodUsuario = '" + usuario.usuario.ToString() + "'";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                DivisionID = reader.GetString("DivisionID");
                                RegionalCBZ = reader.GetString("RegionalCBZ");
                                DivisionCBZ = reader.GetString("DivisionCBZ");
                            }
                        }
                    }
                }
                if (DivisionID == "99")
                {
                    query = "";

                    if ("00".Equals(DivisionCBZ) && !"00".Equals(RegionalCBZ))
                    {
                        query = "select * from Facturas where Regional = '" + RegionalCBZ + "'";
                    }
                    if ("00".Equals(RegionalCBZ) && !"00".Equals(DivisionCBZ))
                    {
                        query = "select * from Facturas where Division = '" + DivisionCBZ + "'";
                    }
                    if (!"00".Equals(DivisionCBZ) && !"00".Equals(RegionalCBZ))
                    {
                        // query = "select * from Facturas where Division = '" + DivisionID + "' and Regional = '" + RegionalCBZ + "'";
                        query = "select * from Facturas where Division = '" + DivisionCBZ + "' and Regional = '" + RegionalCBZ + "'";
                    }
                    if ("00".Equals(DivisionCBZ) && "00".Equals(RegionalCBZ))
                    {
                        query = "select * from Facturas";
                    }
                }
                else
                {
                    query = "select * from Facturas where IdVendedor = '" + usuario.usuario.ToString() + "'";
                }

                var con = conexion_mysql_open.obtener_conexion();
                using (var command1 = con.CreateCommand())
                {
                    command1.CommandText = query;
                    using (var readers1 = command1.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (readers1.HasRows)
                        {
                            while (readers1.Read())
                            {
                                ListFacturas.Add(new HBM_Facturas()
                                {
                                    FacturaID = readers1.GetString("FacturaID"),
                                    IdVendedor = readers1.GetString("IdVendedor"),
                                    AM = readers1.GetString("AM"),
                                    BMoraBS = Convert.ToDecimal(readers1.GetString("BMoraBS")),
                                    BMoraUSD = Convert.ToDecimal(readers1.GetString("BMoraUSD")),
                                    BSaldoBS = Convert.ToDecimal(readers1.GetString("BSaldoBS")),
                                    BSaldoUSD = Convert.ToDecimal(readers1.GetString("BSaldoUSD")),
                                    CodFactura = readers1.GetString("CodFactura"),
                                    CODSBO = readers1.GetString("CODSBO"),
                                    DiaMora = Convert.ToInt32(readers1.GetString("DiaMora")),
                                    FecEmision = readers1.GetString("FecEmision"),
                                    FecVencimiento = readers1.GetString("FecVencimiento"),
                                    MoraCarteraBS = Convert.ToDecimal(readers1.GetString("MoraCarteraBS")),
                                    MoraCarteraUSD = Convert.ToDecimal(readers1.GetString("MoraCarteraUSD")),
                                    MoraFacturaUSD = Convert.ToDecimal(readers1.GetString("MoraFacturaUSD")),
                                    Moneda = readers1.GetString("Moneda"),
                                    MonedaFacturaBS = Convert.ToDecimal(readers1.GetString("MonedaFacturaBS")),
                                    NombreCliente = readers1.GetString("NombreCliente"),
                                    PagoBS = Convert.ToDecimal(readers1.GetString("PagoBS")),
                                    PagoUSD = Convert.ToDecimal(readers1.GetString("PagoUSD")),
                                    SaldoBS = Convert.ToDecimal(readers1.GetString("SaldoBS")),
                                    SaldoUSD = Convert.ToDecimal(readers1.GetString("SaldoUSD")),
                                    SePaga = readers1.GetString("SePaga"),
                                    TCFactura = Convert.ToDecimal(readers1.GetString("TCFactura")),
                                    MCODMora = readers1.GetString("MCODMora"),
                                    KUNNR = readers1.GetString("KUNNR"),
                                    FechaModificacion = readers1.GetString("FechaModificacion"),
                                    Contado = Convert.ToInt32(readers1.GetString("Contado")),
                                });
                            }
                        }
                    }
                }
            return Tuple.Create(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), ListFacturas);
            }
            catch (MySqlException e)
            {
                conexion_mysql_open.cerrar_conexion();
                List<string> Listmensajes = new List<string>();
                Listmensajes.Add(e.Message.ToString());
                return Listmensajes;
            }
        }
        [HttpPost]
        public object ObtenerHistorialCobranzas(Recibo recibo)
        {
            List<HBM_HistorialCobranzas> ListHistorico = new List<HBM_HistorialCobranzas>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select * from HistorialCobranzas where ReciboID = '" + recibo.ReciboID.ToString()  + "'";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListHistorico.Add(new HBM_HistorialCobranzas()
                                {
                                    HistoricoID = reader.GetString("HistoricoID"),
                                    Estado = reader.GetString("Estado"),
                                    Fecha = reader.GetString("Fecha"),
                                    Caso = reader.GetString("Caso"),
                                    CodUsuario = reader.GetString("CodUsuario"),
                                    CodUsuarioPM = reader.GetString("CodUsuarioPM"),
                                    Obs = reader.GetString("Obs"),
                                    ReciboID = reader.GetString("ReciboID"),
                                    CodRecibo = reader.GetString("CodRecibo"),
                                    Cliente = reader.GetString("Cliente"),
                                    Comentario = reader.GetString("Comentario"),
                                    Tarea = reader.GetString("Tarea")
                                });
                            }
                        }
                    }
                }
                
                return ListHistorico;

            }
            catch (MySqlException ex)
            {
                conexion_mysql_open.cerrar_conexion();
                List<string> ListaMensajes = new List<string>();
                ListaMensajes.Add(ex.Message.ToString());
                return ListaMensajes;
            }
        }
    }
}