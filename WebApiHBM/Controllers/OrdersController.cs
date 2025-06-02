using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Web.Http;
using WebApiHBM.Models;
using WebApiHBM.Conexiones;
using System.Data;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Globalization;

namespace WebApiHBM.Controllers
{
    public class OrdersController : ApiController
    {
        [HttpPost]
        public object ObtenerProductos(credenciales usuario)
        {
            List<dimarea> Listproductos = new List<dimarea>();
            CultureInfo culture = new CultureInfo("en-US");
            DateTime fecha = Convert.ToDateTime("9999-12-31", culture);
            int cod_actividad;

            try
            {
                MySqlCommand cmd = new MySqlCommand("Get_Productos", conexion_mysql_open.obtener_conexion());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("CodUsuario", usuario.usuario));

                // MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                using (MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dr.Read())
                    {

                        if (dr.GetString("FechaBloq") != "0000-00-00")
                        {

                            fecha = Convert.ToDateTime(dr.GetString("FechaBloq"), culture);
                        }

                        if (dr.GetString("FechaBloq") == "0000-00-00" || fecha > DateTime.Now)
                        {
                            DateTime fechamod = DateTime.Parse(dr.GetString("FechaModificacion"));
                            string m = fechamod.ToString("yyyy-MM-dd HH:mm:ss");

                            Listproductos.Add(new dimarea()
                            {
                                ProductID = Utils.SafeGetString(dr, "ProductID"),
                                ProductCode = Utils.SafeGetString(dr, "ProductCode"),
                                Almacen = Utils.SafeGetString(dr, "Almacen"),
                                OrgVenta = Utils.SafeGetString(dr, "OrgVenta"),
                                CodCanal = Utils.SafeGetString(dr, "CodCanal"),
                                CodSector = Utils.SafeGetString(dr, "CodSector"),
                                CodFamilia = Utils.SafeGetString(dr, "CodFamilia"),
                                CodGrupo = Utils.SafeGetString(dr, "CodGrupo"),
                                CodSubGrupo = Utils.SafeGetString(dr, "CodSubGrupo"),
                                ProductName = Utils.SafeGetString(dr, "ProductName"),
                                CodigoBarras = Utils.SafeGetString(dr, "CodigoBarras"),
                                DescPromocional = Convert.ToDecimal(dr.GetString("DescPromocional")),
                                CodProveedor = Utils.SafeGetString(dr, "CodProveedor"),
                                ConStock = Utils.SafeGetString(dr, "ConStock"),
                                Moneda = Utils.SafeGetString(dr, "Moneda"),
                                TipoUnidad = Utils.SafeGetString(dr, "TipoUnidad"),
                                FechaModificacion = m, //Utils.SafeGetString(dr, "FechaModificacion").ToString("MM-dd-yyyy HH:mm") ,
                                CantidadXUnidad = Utils.SafeGetString(dr, "CantidadXUnidad"),
                                PrecioUnitario = Convert.ToDecimal(dr.GetString("PrecioUnitario")),
                                Stock = Convert.ToInt32(dr.GetString("Stock")),
                                StockNacional = Convert.ToInt32(dr.GetString("StockNacional")),
                                CodActividad = Convert.ToInt32(dr.GetString("CodActividad")), //Convert.ToString(dr["CodActividad"]).TrimStart(),
                                CodClasificador = Convert.ToInt32(dr.GetString("CodClasificador")), // Convert.ToString(dr["CodClasificador"]).TrimStart(),
                                CodCatalogo = Convert.ToInt32(dr.GetString("CodCatalogo")),
                                Serie = Convert.ToInt32(dr.GetString("Serie"))
                            });

                        }
                    }
                }
                return Listproductos;
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
        public object ObtenerProductos02(credenciales usuario)
        {
            string query="";
            List<dimarea> Listproductos = new List<dimarea>();
            CultureInfo culture = new CultureInfo("en-US");
            DateTime fecha = Convert.ToDateTime("9999-12-31", culture);
            try
            {
                MySqlCommand cmd = new MySqlCommand("Get_Productos", conexion_mysql_open.obtener_conexion());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("CodUsuario", usuario.usuario));

                // MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                using (MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dr.Read())
                    {
                        DateTime fechamod = DateTime.Parse(dr.GetString("FechaModificacion"));
                        string m = fechamod.ToString("yyyy-MM-dd HH:mm:ss"); ;

                        if (dr.GetString("FechaBloq") != "0000-00-00")
                        {

                            fecha = Convert.ToDateTime(dr.GetString("FechaBloq"), culture);
                        }

                        if (dr.GetString("FechaBloq") == "0000-00-00" || fecha > DateTime.Now)
                        {
                            Listproductos.Add(new dimarea()
                            {
                                ProductID = Utils.SafeGetString(dr, "ProductID"),
                                ProductCode = Utils.SafeGetString(dr, "ProductCode"),
                                Almacen = Utils.SafeGetString(dr, "Almacen"),
                                OrgVenta = Utils.SafeGetString(dr, "OrgVenta"),
                                CodCanal = Utils.SafeGetString(dr, "CodCanal"),
                                CodSector = Utils.SafeGetString(dr, "CodSector"),
                                CodFamilia = Utils.SafeGetString(dr, "CodFamilia"),
                                CodGrupo = Utils.SafeGetString(dr, "CodGrupo"),
                                CodSubGrupo = Utils.SafeGetString(dr, "CodSubGrupo"),
                                ProductName = Utils.SafeGetString(dr, "ProductName"),
                                CodigoBarras = Utils.SafeGetString(dr, "CodigoBarras"),
                                DescPromocional = Convert.ToDecimal(dr.GetString("DescPromocional")),
                                CodProveedor = Utils.SafeGetString(dr, "CodProveedor"),
                                ConStock = Utils.SafeGetString(dr, "ConStock"),
                                Moneda = Utils.SafeGetString(dr, "Moneda"),
                                TipoUnidad = Utils.SafeGetString(dr, "TipoUnidad"),
                                FechaModificacion = m, //Utils.SafeGetString(dr, "FechaModificacion"),
                                CantidadXUnidad = Utils.SafeGetString(dr, "CantidadXUnidad"),
                                PrecioUnitario = Convert.ToDecimal(dr.GetString("PrecioUnitario")),
                                Stock = Convert.ToInt32(dr.GetString("Stock")),
                                StockNacional = Convert.ToInt32(dr.GetString("StockNacional")),
                                CodActividad = Convert.ToInt32(dr.GetString("CodActividad")), //Convert.ToString(dr["CodActividad"]).TrimStart(),
                                CodClasificador = Convert.ToInt32(dr.GetString("CodClasificador")), // Convert.ToString(dr["CodClasificador"]).TrimStart(),
                                CodCatalogo = Convert.ToInt32(dr.GetString("CodCatalogo")), //Convert.ToString(dr["CodCatalogo"]).TrimStart()
                                Serie = Convert.ToInt32(dr.GetString("Serie"))  //Convert.ToString(dr["CodCatalogo"]).TrimStart()
                                // Catalogo = Utils.SafeGetString(dr, "Catalogo"),
                            });

                        }
                    }
                }
                //return Listproductos;
                return Tuple.Create(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), Listproductos);
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
        public object ObtenerProductoLote(credenciales usuario)
        {
            List<HBM_ProductoLote> ListproductosLote = new List<HBM_ProductoLote>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select PL.* from ProductoLote PL INNER JOIN UsuarioAlmacen U ON PL.Almacen = U.Almacen WHERE U.CodUsuario = '"+ usuario.usuario.ToString() +"'";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {

                                DateTime fechamod = DateTime.Parse(reader.GetString("FechaModificacion"));
                                DateTime fechavenc = DateTime.Parse(reader.GetString("FechaVencimiento"));
                                string m = fechamod.ToString("dd/MM/yyyy HH:mm:ss");
                                string v = fechavenc.ToString("dd/MM/yyyy HH:mm:ss");

                                ListproductosLote.Add(new HBM_ProductoLote()
                                {
                                    ProductoLoteID = Utils.SafeGetString(reader, "ProductoLoteID"),
                                    ProductCode = Utils.SafeGetString(reader, "ProductCode"),
                                    Almacen = Utils.SafeGetString(reader, "Almacen"),
                                    Lote = Utils.SafeGetString(reader, "Lote"),
                                    //FechaVencimiento = Utils.SafeGetString(reader, "FechaVencimiento"),
                                    FechaVencimiento = v,
                                    Stock = Convert.ToInt32(reader.GetString("Stock")),
                                    //FechaModificacion = Utils.SafeGetString(reader, "FechaModificacion")
                                    FechaModificacion = m
                                });
                            }
                        }
                    }
                }
                return ListproductosLote;

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
        public object ObtenerProductoLote02(credenciales usuario)
        {
            List<HBM_ProductoLote> ListproductosLote = new List<HBM_ProductoLote>();
            string query = "";
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    if (usuario.fecha.ToString() == "1900-01-01 00:00:00")
                    {
                        query = "select PL.* from ProductoLote PL  " + " INNER JOIN UsuarioAlmacen U ON PL.Almacen = U.Almacen WHERE U.CodUsuario = '" + usuario.usuario.ToString() + "'";
                    }
                    else
                    {
                        query = "select PL.* from ProductoLote PL INNER JOIN UsuarioAlmacen U ON PL.Almacen = U.Almacen WHERE U.CodUsuario = '" + usuario.usuario.ToString() + "' AND PL.FechaModificacion >= '" + usuario.fecha.ToString() + "'";
                    }
                    command.CommandText = query;
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                DateTime fechamod = DateTime.Parse(reader.GetString("FechaModificacion"));
                                DateTime fechavenc = DateTime.Parse(reader.GetString("FechaVencimiento"));
                                string m = fechamod.ToString("dd/MM/yyyy HH:mm:ss");
                                string v = fechavenc.ToString("dd/MM/yyyy HH:mm:ss");

                                ListproductosLote.Add(new HBM_ProductoLote()
                                {
                                    ProductoLoteID = Utils.SafeGetString(reader, "ProductoLoteID"),
                                    ProductCode = Utils.SafeGetString(reader, "ProductCode"),
                                    Almacen = Utils.SafeGetString(reader, "Almacen"),
                                    Lote = Utils.SafeGetString(reader, "Lote"),
                                    //FechaVencimiento = Utils.SafeGetString(reader, "FechaVencimiento"),
                                    FechaVencimiento = v,
                                    Stock = Convert.ToInt32(reader.GetString("Stock")),
                                    FechaModificacion = m
                                    //FechaModificacion = Utils.SafeGetString(reader, "FechaModificacion")
                                });
                            }
                        }
                    }
                }
                return Tuple.Create(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), ListproductosLote);
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
        public object ObtenerRegion()
        {
            List<HBM_Region> ListRegion = new List<HBM_Region>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select * from Region R";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListRegion.Add(new HBM_Region()
                                {
                                    RegionID = reader.GetString("RegionID"),
                                    CodRegion = reader.GetString("CodRegion"),
                                    Denominacion = reader.GetString("Denominacion"),
                                    Abv = reader.GetString("Abv"),
                                    FechaModificacion = reader.GetString("FechaModificacion")
                                });
                            }
                        }
                    }
                }
                return ListRegion;
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
        public object ObtenerCanal()
        {
            List<HBM_Canal> ListCanal = new List<HBM_Canal>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select * from Canal C";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListCanal.Add(new HBM_Canal()
                                {
                                    CodCanal = reader.GetString("CodCanal"),
                                    Canal = reader.GetString("Canal"),
                                    Estado = Convert.ToInt32(reader.GetString("Estado")),
                                    FechaModificacion = reader.GetString("FechaModificacion")
                                });
                            }
                        }
                    }
                }
                return ListCanal;
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
        public object ObtenerDivision()
        {
            List<HBM_Division> ListDivision = new List<HBM_Division>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select * from Division D";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListDivision.Add(new HBM_Division()
                                {
                                    DivisionID = reader.GetString("DivisionID"),
                                    Division = reader.GetString("Division"),
                                    OrgVenta = reader.GetString("OrgVenta")
                                });
                            }
                        }
                    }
                }
                return ListDivision;
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
        public object ObtenerFamilia(credenciales usuario)
        {
            List<HBM_Familia> ListFamilia = new List<HBM_Familia>();
            string vendedor = usuario.usuario.ToString();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT DISTINCT F.* FROM Familia F INNER JOIN Sector S ON F.CodSector = S.CodSector INNER JOIN Usuario U ON U.Organizacion = S.OrgVenta  WHERE CodUsuario = '" + usuario.usuario.ToString() + "'";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListFamilia.Add(new HBM_Familia()
                                {
                                    CodFamilia = reader.GetString("CodFamilia"),
                                    Familia = reader.GetString("Familia"),
                                    CodSector = reader.GetString("CodSector"),
                                    FechaModificacion = reader.GetString("FechaModificacion")
                                });
                            }
                        }
                    }
                }
                return ListFamilia;

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
        public object ObtenerSector(credenciales usuario)
        {
            List<HBM_Sector> ListSector = new List<HBM_Sector>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select S.* from Sector S INNER JOIN UsuarioSector US ON S.CodSector = US.CodSector AND S.CodCanal = US.CodCanal WHERE CodUsuario = '" + usuario.usuario.ToString() + "'";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListSector.Add(new HBM_Sector()
                                {
                                    SectorID = reader.GetString("SectorID"),
                                    CodSector = reader.GetString("CodSector"),
                                    OrgVenta = reader.GetString("OrgVenta"),
                                    CodCanal = reader.GetString("CodCanal"),
                                    Sector = reader.GetString("Sector"),
                                    FechaModificacion = reader.GetString("FechaModificacion")
                                });
                            }
                        }
                    }
                }
                return ListSector;
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
        public object ObtenerSector02(credenciales usuario)
        {
            List<HBM_Sector> ListSector = new List<HBM_Sector>();
            string query = "";
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    if (usuario.fecha.ToString() == "1900-01-01 00:00:00")
                    {
                        query = "select S.* from Sector S INNER JOIN UsuarioSector US ON S.CodSector = US.CodSector AND S.CodCanal = US.CodCanal WHERE CodUsuario = '" + usuario.usuario.ToString() + "'";
                    }
                    else
                    {
                        query = "select S.* from Sector S INNER JOIN UsuarioSector US ON S.CodSector = US.CodSector AND S.CodCanal = US.CodCanal WHERE CodUsuario = '" + usuario.usuario.ToString() + "' AND S.FechaModificacion >= '" + usuario.fecha.ToString() + "'";
                    }
                    command.CommandText = query;
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListSector.Add(new HBM_Sector()
                                {
                                    SectorID = reader.GetString("SectorID"),
                                    CodSector = reader.GetString("CodSector"),
                                    OrgVenta = reader.GetString("OrgVenta"),
                                    CodCanal = reader.GetString("CodCanal"),
                                    Sector = reader.GetString("Sector"),
                                    FechaModificacion = reader.GetString("FechaModificacion")
                                });
                            }
                        }
                    }
                }
                return Tuple.Create(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), ListSector);
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
        public object ObtenerGrupo(credenciales usuario)
        {
            List<HBM_Grupo> ListGrupo = new List<HBM_Grupo>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT distinct G.* FROM Grupo G INNER JOIN UsuarioSector US ON G.CodSector = US.CodSector where US.CodUsuario = '" + usuario.usuario.ToString() + "'";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListGrupo.Add(new HBM_Grupo()
                                {
                                    CodGrupo = reader.GetString("CodGrupo"),
                                    CodFamilia = reader.GetString("CodFamilia"),
                                    CodSector = reader.GetString("CodSector"),
                                    Grupo = reader.GetString("Grupo"),
                                    FechaModificacion = reader.GetString("FechaModificacion")
                                });
                            }
                        }
                    }
                }
                return ListGrupo;
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
        public object ObtenerGrupo02(credenciales usuario)
        {
            List<HBM_Grupo> ListGrupo = new List<HBM_Grupo>();
            string query = "";
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    if (usuario.fecha.ToString() == "1900-01-01 00:00:00")
                    {
                        query = "SELECT distinct G.* FROM Grupo G INNER JOIN UsuarioSector US ON G.CodSector = US.CodSector where US.CodUsuario = '" + usuario.usuario.ToString() + "'";
                    }
                    else
                    {
                        query = "SELECT distinct G.* FROM Grupo G INNER JOIN UsuarioSector US ON G.CodSector = US.CodSector where US.CodUsuario = '" + usuario.usuario.ToString() + "' AND G.FechaModificacion >= '" + usuario.fecha.ToString() + "'";
                    }
                    command.CommandText = query;
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListGrupo.Add(new HBM_Grupo()
                                {
                                    CodGrupo = reader.GetString("CodGrupo"),
                                    CodFamilia = reader.GetString("CodFamilia"),
                                    CodSector = reader.GetString("CodSector"),
                                    Grupo = reader.GetString("Grupo"),
                                    FechaModificacion = reader.GetString("FechaModificacion")
                                });
                            }
                        }
                    }
                }
                return Tuple.Create(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), ListGrupo);
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
        public object ObtenerSubGrupo(credenciales usuario)
        {
            List<HBM_SubGrupo> ListSubGrupo = new List<HBM_SubGrupo>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT distinct SG.* FROM SubGrupo SG INNER JOIN UsuarioSector US ON SG.CodSector = US.CodSector where US.CodUsuario = '" + usuario.usuario.ToString() + "'";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListSubGrupo.Add(new HBM_SubGrupo()
                                {
                                    CodSubGrupo = reader.GetString("CodSubGrupo"),
                                    CodSector = reader.GetString("CodSector"),
                                    CodFamilia = reader.GetString("CodFamilia"),
                                    CodGrupo = reader.GetString("CodGrupo"),
                                    SubGrupo = reader.GetString("SubGrupo"),
                                    FechaModificacion = reader.GetString("FechaModificacion")

                                });
                            }
                        }
                    }
                }
                return ListSubGrupo;
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
        public object ObtenerSubGrupo02(credenciales usuario)
        {
            List<HBM_SubGrupo> ListSubGrupo = new List<HBM_SubGrupo>();
            string query = "";
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    if (usuario.fecha.ToString() == "1900-01-01 00:00:00")
                    {
                        query = "SELECT distinct SG.* FROM SubGrupo SG INNER JOIN UsuarioSector US ON SG.CodSector = US.CodSector where US.CodUsuario = '" + usuario.usuario.ToString() + "'";
                    }
                    else
                    {
                        query = "SELECT distinct SG.* FROM SubGrupo SG INNER JOIN UsuarioSector US ON SG.CodSector = US.CodSector where US.CodUsuario = '" + usuario.usuario.ToString() + "' AND SG.FechaModificacion >= '" + usuario.fecha.ToString() + "'";
                    }
                    command.CommandText = query;
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListSubGrupo.Add(new HBM_SubGrupo()
                                {
                                    CodSubGrupo = reader.GetString("CodSubGrupo"),
                                    CodSector = reader.GetString("CodSector"),
                                    CodFamilia = reader.GetString("CodFamilia"),
                                    CodGrupo = reader.GetString("CodGrupo"),
                                    SubGrupo = reader.GetString("SubGrupo"),
                                    FechaModificacion = reader.GetString("FechaModificacion")

                                });
                            }
                        }
                    }
                }
                return Tuple.Create(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), ListSubGrupo);
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
        public object ObtenerOficinaVenta()
        {
            List<HBM_OficinaVenta> ListOficinaVentas = new List<HBM_OficinaVenta>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select * from OficinaVenta O";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListOficinaVentas.Add(new HBM_OficinaVenta()
                                {
                                    CodigoOficinaVenta = reader.GetString("CodigoOficinaVenta"),
                                    Estado = Convert.ToInt32(reader.GetString("Estado")),
                                    IdOficinaVenta = reader.GetString("IdOficinaVenta"),
                                    ModifiedOn = reader.GetString("ModifiedOn"),
                                    OficinaVentas = reader.GetString("OficinaVentas"),
                                    Client = reader.GetInt32("Client")
                                });
                            }
                        }
                    }
                }
                return ListOficinaVentas;

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
        public object ObtenerBonificacionCab()
        {
            List<HBM_BonificacionCab> ListBonificacionCab = new List<HBM_BonificacionCab>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select B.* from BonificacionCab B";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListBonificacionCab.Add(new HBM_BonificacionCab()
                                {
                                    BonoGrupoId = reader.GetString("BonoGrupoId"),
                                    CodRegion = reader.GetString("CodRegion"),
                                    CodCanal = reader.GetString("CodCanal"),
                                    CantBonificacion = Convert.ToInt32(reader.GetString("CantBonificacion")),
                                    CantidadMin = Convert.ToInt32(reader.GetString("CantidadMin")),
                                    CantObligatoriaMax = Convert.ToInt32(reader.GetString("CantObligatoriaMax")),
                                    CantObligatoriaMin = Convert.ToInt32(reader.GetString("CantObligatoriaMin")),
                                    Correlativo = Convert.ToInt32(reader.GetString("Correlativo")),
                                    FechaInicio = reader.GetString("FechaInicio"),
                                    FechaFin = reader.GetString("FechaFin"),
                                    FechaModificacion = reader.GetString("FechaModificacion")
                                });
                            }
                        }
                    }
                }
                return ListBonificacionCab;

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
        public object ObtenerBonificacionCab02(credenciales fecha)
        {

            string query = "";
            List<HBM_BonificacionCab> ListBonificacionCab = new List<HBM_BonificacionCab>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    if (fecha.fecha.ToString() == "1900-01-01 00:00:00")
                    {
                        query = "select B.* from BonificacionCab B";
                    }
                    else
                    {
                        query = "select B.* from BonificacionCab B WHERE FechaModificacion >= '" + fecha.fecha.ToString() + "'";
                    }
                    command.CommandText = query;
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListBonificacionCab.Add(new HBM_BonificacionCab()
                                {
                                    BonoGrupoId = reader.GetString("BonoGrupoId"),
                                    CodRegion = reader.GetString("CodRegion"),
                                    CodCanal = reader.GetString("CodCanal"),
                                    CantBonificacion = Convert.ToInt32(reader.GetString("CantBonificacion")),
                                    CantidadMin = Convert.ToInt32(reader.GetString("CantidadMin")),
                                    CantObligatoriaMax = Convert.ToInt32(reader.GetString("CantObligatoriaMax")),
                                    CantObligatoriaMin = Convert.ToInt32(reader.GetString("CantObligatoriaMin")),
                                    Correlativo = Convert.ToInt32(reader.GetString("Correlativo")),
                                    FechaInicio = reader.GetString("FechaInicio"),
                                    FechaFin = reader.GetString("FechaFin"),
                                    FechaModificacion = reader.GetString("FechaModificacion")
                                });
                            }
                        }
                    }
                }
                return Tuple.Create(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), ListBonificacionCab);
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
        public object ObtenerProdConBono(credenciales usuario)
        {
            string division = "";
            List<HBM_ProdConBono> ListProdConBono = new List<HBM_ProdConBono>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select DivisionID from Usuario where CodUsuario = '"+ usuario.usuario.ToString() + "'";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                division = reader.GetString("DivisionID").Substring(1, 1) + "%";
                            }
                        }
                    }
                }
                var con = conexion_mysql_open.obtener_conexion();
                using (var commands = con.CreateCommand())
                {
                    commands.CommandText = "select * from ProdConBono PC where PC.ProductCode like '" + division + "'";
                    using (var readers = commands.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (readers.HasRows)
                        {
                            while (readers.Read())
                            {
                                ListProdConBono.Add(new HBM_ProdConBono()
                                {
                                    ProdConBonoID = readers.GetString("ProdConBonoID"),
                                    Correlativo = Convert.ToInt32(readers.GetString("Correlativo")),
                                    FechaModificacion = readers.GetString("FechaModificacion"),
                                    ProductCode = readers.GetString("ProductCode")
                                });
                            }
                        }
                    }
                }
                return ListProdConBono;

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
        public object ObtenerProdConBono02(credenciales fecha)
        {
            List<HBM_ProdConBono> ListProdConBono = new List<HBM_ProdConBono>();
            string query = "", division = "";
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select DivisionID from Usuario where CodUsuario = '" + fecha.usuario.ToString() + "'";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                division = reader.GetString("DivisionID").Substring(1, 1) + "%";
                            }
                        }
                    }
                }
                var con = conexion_mysql_open.obtener_conexion();
                using (var commands = con.CreateCommand())
                {
                    if (fecha.fecha.ToString() == "1900-01-01 00:00:00")
                    {
                        query = "select * from ProdConBono PC where PC.ProductCode like '" + division + "'";
                    }
                    else
                    {
                        query = "select * from ProdConBono PC where PC.ProductCode like '" + division + "' AND PC.FechaModificacion >= '" + fecha.fecha.ToString() + "'";
                    }

                    commands.CommandText = query;
                    using (var readers = commands.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (readers.HasRows)
                        {
                            while (readers.Read())
                            {
                                ListProdConBono.Add(new HBM_ProdConBono()
                                {
                                    ProdConBonoID = readers.GetString("ProdConBonoID"),
                                    Correlativo = Convert.ToInt32(readers.GetString("Correlativo")),
                                    FechaModificacion = readers.GetString("FechaModificacion"),
                                    ProductCode = readers.GetString("ProductCode")
                                });
                            }
                        }
                    }
                }
                return Tuple.Create(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), ListProdConBono);

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
        public object ObtenerProdParaBono(credenciales usuario)
        {
            string division = "";
            List<HBM_ProdParaBono> ListProdParaBono = new List<HBM_ProdParaBono>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select DivisionID from Usuario where CodUsuario = '" + usuario.usuario.ToString() + "'";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                division = reader.GetString("DivisionID").Substring(1, 1) + "%";
                            }
                        }
                    }
                }
                var con = conexion_mysql_open.obtener_conexion();
                using (var commands = con.CreateCommand())
                {
                    commands.CommandText = "select * from ProdParaBono PP where PP.ProductCode like '" + division + "'";
                    using (var readers = commands.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (readers.HasRows)
                        {
                            while (readers.Read())
                            {
                                ListProdParaBono.Add(new HBM_ProdParaBono()
                                {
                                    ProdParaBonoID = readers.GetString("ProdParaBonoID"),
                                    Correlativo = Convert.ToInt32(readers.GetString("Correlativo")),
                                    FechaModificacion = readers.GetString("FechaModificacion"),
                                    ProductCode = readers.GetString("ProductCode")
                                });
                            }
                        }
                    }
                }
                return ListProdParaBono;
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
        public object ObtenerProdParaBono02(credenciales fecha)
        {
            List<HBM_ProdParaBono> ListProdParaBono = new List<HBM_ProdParaBono>();
            string query = "", division = "";
             try
                {
                    var conn = conexion_mysql_open.obtener_conexion();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = "select DivisionID from Usuario where CodUsuario = '" + fecha.usuario.ToString() + "'";
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    division = reader.GetString("DivisionID").Substring(1, 1) + "%";
                                }
                            }
                        }
                    }
                    var con = conexion_mysql_open.obtener_conexion();
                    using (var commands = con.CreateCommand())
                    {
                        if (fecha.fecha.ToString() == "1900-01-01 00:00:00")
                        {
                            query = "select * from ProdParaBono PP where PP.ProductCode like '" + division + "'";
                        }
                        else
                        {
                            query = "select * from ProdParaBono PP where PP.ProductCode like'" + division + "' AND PP.FechaModificacion >= '" + fecha.fecha.ToString() + "'";
                        }
                        commands.CommandText = query;
                        using (var readers = commands.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (readers.HasRows)
                            {
                                while (readers.Read())
                                {
                                ListProdParaBono.Add(new HBM_ProdParaBono()
                                {
                                    ProdParaBonoID = readers.GetString("ProdParaBonoID"),
                                    Correlativo = Convert.ToInt32(readers.GetString("Correlativo")),
                                    FechaModificacion = readers.GetString("FechaModificacion"),
                                    ProductCode = readers.GetString("ProductCode")
                                });
                            }
                            }
                        }
                    }
                return Tuple.Create(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), ListProdParaBono);
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
        public object ObtenerProductoAlternativa(credenciales usuario)
        {
            List<HBM_ProductoAlternativa> ListProductoAlternativa = new List<HBM_ProductoAlternativa>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select distinct t3.CodUsuario, t1.* from ProductoAlternativa as t1 INNER JOIN UsuarioAlmacen as t2 ON t2.Centro = t1.Centro INNER JOIN Usuario as t3 ON t3.CodigoSap = t2.IdVendedor where t3.CodUsuario ='" + usuario.usuario.ToString()+ "'";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListProductoAlternativa.Add(new HBM_ProductoAlternativa()
                                {
                                    ProductoAlternativaID = reader.GetString("ProductoAlternativaID"),
                                    CodigoAlternativa = reader.GetString("CodigoAlternativa"),
                                    CodigoSinAlternativa = reader.GetString("CodigoSinAlternativa"),
                                    CodigoProducto = reader.GetString("CodigoProducto"),
                                    OficinaVentas = reader.GetString("OficinaVentas"),
                                    CodCanal = reader.GetString("CodCanal"),
                                    Descripcion = reader.GetString("Descripcion"),
                                    CodVendedor = reader.GetString("CodVendedor"),
                                    Estado = Convert.ToInt32(reader.GetString("Estado")),
                                    EstadoStock = reader.GetString("EstadoStock"),
                                    FechaValidez = reader.GetString("FechaValidez"),
                                    GrupoCliente = reader.GetString("GrupoCliente"),
                                    FechaModificacion = reader.GetString("FechaModificacion"),
                                    Moneda = reader.GetString("Moneda"),
                                    Posiciones = reader.GetString("Posiciones"),
                                    Precio = Convert.ToDecimal(reader.GetString("Precio")),
                                    Stock = Convert.ToInt32(reader.GetString("Stock")),
                                    Centro = reader.GetString("Centro")
                                });
                            }
                        }
                    }
                }
                return ListProductoAlternativa;
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
        public object ObtenerReglaPedido()
        {
            List<HBM_ReglaPedido> ListReglaPedido = new List<HBM_ReglaPedido>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select * FROM ReglaPedido ;";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListReglaPedido.Add(new HBM_ReglaPedido()
                                {
                                    ReglaPedidoID = reader.GetString("ReglaPedidoID"),
                                    Bonificacion = reader.GetString("Bonificacion"),
                                    Grupo = Convert.ToInt32(reader.GetString("Grupo")),
                                    CodCanal = reader.GetString("CodCanal"),
                                    CodSector = reader.GetString("CodSector"),
                                    LimitePosicionFactura = Convert.ToInt32(reader.GetString("LimitePosicionFactura")),
                                    MontoMaximoPedido = Convert.ToDouble(reader.GetString("MontoMaximoPedido")),
                                    MontoFactNombre = Convert.ToDouble(reader.GetString("MontoFactNombre")),
                                    FechaModificacion = reader.GetString("FechaModificacion"),
                                    EsProveedor = Convert.ToInt32(reader.GetString("EsProveedor")),
                                    OrgVenta = reader.GetString("OrgVenta"),
                                    MonedaTrabajo = reader.GetString("MonedaTrabajo")
                                });
                            }
                        }
                    }
                }
                return ListReglaPedido;
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
        public object ObtenerReglaPedido02(credenciales fecha)
        {
            List<HBM_ReglaPedido> ListReglaPedido = new List<HBM_ReglaPedido>();
            string query = "";
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    if (fecha.fecha.ToString() == "1900-01-01 00:00:00")
                    {
                        query = "SELECT * FROM ReglaPedido";
                    }
                    else
                    {
                        query = "SELECT * FROM ReglaPedido WHERE FechaModificacion >= '" + fecha.fecha.ToString() + "'";
                    }
                    command.CommandText = query;
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListReglaPedido.Add(new HBM_ReglaPedido()
                                {
                                                                      
                                    ReglaPedidoID = reader.GetString("ReglaPedidoID"),
                                    Bonificacion = reader.GetString("Bonificacion"),
                                    Grupo = Convert.ToInt32(reader.GetString("Grupo")),
                                    CodCanal = reader.GetString("CodCanal"),
                                    CodSector = reader.GetString("CodSector"),
                                    LimitePosicionFactura = Convert.ToInt32(reader.GetString("LimitePosicionFactura")),
                                    MontoMaximoPedido = Convert.ToDouble(reader.GetString("MontoMaximoPedido")),
                                    MontoFactNombre = Convert.ToDouble(reader.GetString("MontoFactNombre")),
                                    FechaModificacion = reader.GetString("FechaModificacion"),
                                    EsProveedor = Convert.ToInt32(reader.GetString("EsProveedor")),
                                    OrgVenta = reader.GetString("OrgVenta")
                                });
                            }
                        }
                    }
                }
                return Tuple.Create(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), ListReglaPedido);
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
        public object ObtenerTipoPago()
        {
            List<HBM_TipoPago> ListTipoPago = new List<HBM_TipoPago>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select * from TipoPago T";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListTipoPago.Add(new HBM_TipoPago()
                                {
                                    TipoPagoID = reader.GetString("TipoPagoID"),
                                    CodTipoPago = reader.GetString("CodTipoPago"),
                                    OrgVenta = reader.GetString("OrgVenta"),
                                    CodCanal = reader.GetString("CodCanal"),
                                    TipoPago = reader.GetString("TipoPago"),
                                    FechaModificacion = reader.GetString("FechaModificacion"),
                                    Estado = Convert.ToInt32(reader.GetString("Estado")),
                                    Orden = Convert.ToInt32(reader.GetString("Orden"))
                                });
                            }
                        }
                    }
                }
                return ListTipoPago;
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
        public object ObtenerPrioridadEntrega()
        {
            List<HBM_PrioridadEntrega> ListPrioridadEntrega = new List<HBM_PrioridadEntrega>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select * from PrioridadEntrega PE";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListPrioridadEntrega.Add(new HBM_PrioridadEntrega()
                                {
                                    PrioridadEntregaID = reader.GetString("PrioridadEntregaID"),
                                    CodPrioridad = Convert.ToInt32(reader.GetString("CodPrioridad")),
                                    OrgVenta = reader.GetString("OrgVenta"),
                                    CodCanal = reader.GetString("CodCanal"),
                                    Prioridad = reader.GetString("Prioridad"),
                                    FechaModificacion = reader.GetString("FechaModificacion"),
                                    Estado = Convert.ToInt32(reader.GetString("Estado"))
                                });
                            }
                        }
                    }
                }
                return ListPrioridadEntrega;
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
        public object ObtenerPlanZona(credenciales usuario)
        {
            List<HBM_PlanZona> ListPlanZona = new List<HBM_PlanZona>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select PL.* from PlanZona PL INNER JOIN Usuario ON PL.OrgVenta = Usuario.Organizacion where Usuario.CodUsuario = '" + usuario.usuario.ToString() + "'";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListPlanZona.Add(new HBM_PlanZona()
                                {
                                    PlanZonaID = reader.GetString("PlanZonaID"),
                                    OrgVenta = reader.GetString("OrgVenta"),
                                    CodZona = reader.GetString("CodZona"),
                                    Zona = reader.GetString("Zona"),
                                    Dia = reader.GetString("Dia"),
                                    HoraInicio = reader.GetString("HoraInicio"),
                                    HoraFin = reader.GetString("HoraFin"),
                                    FechaModificacion = reader.GetString("FechaModificacion")
                                });
                            }
                        }
                    }
                }
                return ListPlanZona;
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
        public object ObtenerPlanZona02(credenciales fecha)
        {
            List<HBM_PlanZona> ListPlanZona = new List<HBM_PlanZona>();
            string query = "";
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    if (fecha.fecha.ToString() == "1900-01-01 00:00:00")
                    {
                        query = "select PL.* from PlanZona PL INNER JOIN Usuario ON PL.OrgVenta = Usuario.Organizacion where Usuario.CodUsuario = '" + fecha.usuario.ToString() + "'";
                    }
                    else
                    {
                        query = "select PL.* from PlanZona PL INNER JOIN Usuario ON PL.OrgVenta = Usuario.Organizacion where Usuario.CodUsuario = '" + fecha.usuario.ToString() + "' and FechaModificacion >= '" + fecha.fecha.ToString() + "'";
                    }
                    command.CommandText = query;
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListPlanZona.Add(new HBM_PlanZona()
                                {
                                    PlanZonaID = reader.GetString("PlanZonaID"),
                                    OrgVenta = reader.GetString("OrgVenta"),
                                    CodZona = reader.GetString("CodZona"),
                                    Zona = reader.GetString("Zona"),
                                    Dia = reader.GetString("Dia"),
                                    HoraInicio = reader.GetString("HoraInicio"),
                                    HoraFin = reader.GetString("HoraFin"),
                                    FechaModificacion = reader.GetString("FechaModificacion")
                                });
                            }
                        }
                    }
                }
                return Tuple.Create(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), ListPlanZona);
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
        public object ObtenerDestinatarios(credenciales usuario)
        {
            List<HBM_Destinatarios> ListDestinatarios = new List<HBM_Destinatarios>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select * from Destinatarios where CodUsuario = '" + usuario.usuario.ToString() + "'";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListDestinatarios.Add(new HBM_Destinatarios()
                                {
                                    DestinatarioID = reader.GetString("DestinatarioID"),
                                    CodDestinatario = reader.GetString("CodDestinatario"),
                                    CodCanal = reader.GetString("CodCanal"),
                                    CodSector = reader.GetString("CodSector"),
                                    CodZona = reader.GetString("CodZona"),
                                    KUNNR = reader.GetString("KUNNR"),
                                    KUNNRDEST = reader.GetString("KUNNRDEST"),
                                    Destinatario = reader.GetString("Destinatario"),
                                    Direccion = reader.GetString("Direccion"),
                                    TipoDestinatario = reader.GetString("TipoDestinatario"),
                                    CodUsuario = reader.GetString("CodUsuario"),
                                    FechaModificacion = reader.GetString("FechaModificacion")
                                });
                            }
                        }
                    }
                }
                return ListDestinatarios;
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
        public object ObtenerDestinatarios02(credenciales usuario)
        {
            List<HBM_Destinatarios> ListDestinatarios = new List<HBM_Destinatarios>();
            string query = "";
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    if (usuario.fecha.ToString() == "1900-01-01 00:00:00")
                    {
                        query = "select * from Destinatarios where CodUsuario = '" + usuario.usuario.ToString() + "'";
                    }
                    else
                    {
                        query = "select * from Destinatarios where CodUsuario = '" + usuario.usuario.ToString() + "' AND FechaModificacion >= '" + usuario.fecha.ToString() + "'";
                    }
                    command.CommandText = query;
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListDestinatarios.Add(new HBM_Destinatarios()
                                {
                                    DestinatarioID = reader.GetString("DestinatarioID"),
                                    CodDestinatario = reader.GetString("CodDestinatario"),
                                    CodCanal = reader.GetString("CodCanal"),
                                    CodSector = reader.GetString("CodSector"),
                                    CodZona = reader.GetString("CodZona"),
                                    KUNNR = reader.GetString("KUNNR"),
                                    KUNNRDEST = reader.GetString("KUNNRDEST"),
                                    Destinatario = reader.GetString("Destinatario"),
                                    Direccion = reader.GetString("Direccion"),
                                    TipoDestinatario = reader.GetString("TipoDestinatario"),
                                    CodUsuario = reader.GetString("CodUsuario"),
                                    FechaModificacion = reader.GetString("FechaModificacion")
                                });
                            }
                        }
                    }
                }
                return Tuple.Create(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), ListDestinatarios);
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
        public object ObtenerTipoPedido(credenciales usuario)
        {
            List<HBM_TipoPedido> ListTipoPedido = new List<HBM_TipoPedido>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select * from TipoPedido where CodUsuario = '" + usuario.usuario.ToString() + "'";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListTipoPedido.Add(new HBM_TipoPedido()
                                {
                                    TipoPedidoID = reader.GetString("TipoPedidoID"),
                                    OrgVenta = reader.GetString("OrgVenta"),
                                    CodCanal = reader.GetString("CodCanal"),
                                    CodigoTipoPedido = Convert.ToInt32(reader.GetString("CodigoTipoPedido")),
                                    Descripcion = reader.GetString("Descripcion"),
                                    CodUsuario = reader.GetString("CodUsuario"),
                                    Estado = Convert.ToInt32(reader.GetString("Estado")),
                                    FechaModificacion = reader.GetString("FechaModificacion")
                                });
                            }
                        }
                    }
                }
                return ListTipoPedido;
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
        public object ObtenerUsuarioSector(credenciales usuario)
        {
            List<HBM_UsuarioSector> ListUsuarioSector = new List<HBM_UsuarioSector>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select * from UsuarioSector where CodUsuario = '" + usuario.usuario.ToString() + "'";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListUsuarioSector.Add(new HBM_UsuarioSector()
                                {
                                    UsuarioSectorID = reader.GetString("UsuarioSectorID"),
                                    OrgVenta = reader.GetString("OrgVenta"),
                                    CodCanal = reader.GetString("CodCanal"),
                                    CodSector = reader.GetString("CodSector"),
                                    Sector = reader.GetString("Sector"),
                                    CodUsuario = reader.GetString("CodUsuario"),
                                    FechaModificacion = reader.GetString("FechaModificacion"),
                                    Flujo = reader.GetString("Flujo")
                                    
                                });
                            }
                        }
                    }
                }
                return ListUsuarioSector;
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
        public object ObtenerUsuarioAlmacen(credenciales usuario)
        {
            List<HBM_UsuarioAlmacen> ListUsuarioSector = new List<HBM_UsuarioAlmacen>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select * from UsuarioAlmacen where CodUsuario = '" + usuario.usuario.ToString() + "'";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListUsuarioSector.Add(new HBM_UsuarioAlmacen()
                                {
                                    UsuarioAlmacenID = reader.GetString("UsuarioAlmacenID"),
                                    CodUsuario = reader.GetString("CodUsuario"),
                                    Almacen = reader.GetString("Almacen"),
                                    CodCanal = reader.GetString("CodCanal"),
                                    Centro = reader.GetString("Centro")
                                });
                            }
                        }
                    }
                }
                
                return ListUsuarioSector;
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
        public object ObtenerPedido(credenciales usuario)
        {
            List<HBM_Pedido> ListPedido = new List<HBM_Pedido>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * from Pedido where CodUsuario = '" + usuario.usuario.ToString() + "' AND FechaModificacion > DATE_ADD(NOW(), INTERVAL - 27 DAY) UNION SELECT * from PedidoHistorico where CodUsuario ='" + usuario.usuario.ToString() + "' AND FechaModificacion > DATE_ADD(NOW(), INTERVAL - 27 DAY)";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListPedido.Add(new HBM_Pedido()
                                {
                                    PedidoID = reader.GetString("PedidoID"),
                                    ActividadID = reader.GetString("ActividadID"),
                                    TareaID = reader.GetString("TareaID"),
                                    NroPedido = reader.GetString("NroPedido"),
                                    Fecha = reader.GetString("Fecha"),
                                    NroSAP = reader.GetString("NroSAP"),
                                    CodTipoPedido = Convert.ToInt32(reader.GetString("CodTipoPedido")),
                                    OrgVenta = reader.GetString("OrgVenta"),
                                    CodCanal = reader.GetString("CodCanal"),
                                    CodSector = reader.GetString("CodSector"),
                                    KUNNR = reader.GetString("KUNNR"),
                                    Moneda = reader.GetString("Moneda"),
                                    MontoTotal = Convert.ToDecimal(reader.GetString("MontoTotal")),
                                    CodTipoPago = reader.GetString("CodTipoPago"),
                                    KunnrFact = reader.GetString("KunnrFact"),
                                    RazonSocial = reader.GetString("RazonSocial"),
                                    Nit = reader.GetString("Nit"),
                                    DirFactura = reader.GetString("DirFactura"),
                                    CodPrioridadEntrega = Convert.ToInt32(reader.GetString("CodPrioridadEntrega")),
                                    KunnrDest = reader.GetString("KunnrDest"),
                                    DirEntrega = reader.GetString("DirEntrega"),
                                    FechaEntrega = reader.GetString("FechaEntrega"),
                                    LugarEntrega = reader.GetString("LugarEntrega"),
                                    FechaCompromisoPago = reader.GetString("FechaCompromisoPago"),
                                    MonedaCompromisoPago = reader.GetString("MonedaCompromisoPago"),
                                    MontoCompromisoPago = Convert.ToDecimal(reader.GetString("MontoCompromisoPago")),
                                    ComentarioCompPago = reader.GetString("ComentarioCompPago"),
                                    Condicion1 = Convert.ToDecimal(reader.GetString("Condicion1")),
                                    Condicion2 = Convert.ToDecimal(reader.GetString("Condicion2")),
                                    Condicion3 = Convert.ToDecimal(reader.GetString("Condicion3")),
                                    Condicion4 = Convert.ToDecimal(reader.GetString("Condicion4")),
                                    ContactoSolID = reader.GetString("ContactoSolID"),
                                    ContactoJmID = reader.GetString("ContactoJmID"),
                                    ContactoRpID = reader.GetString("ContactoRpID"),
                                    Nota1 = reader.GetString("Nota1"),
                                    Nota2 = reader.GetString("Nota2"),
                                    Nota3 = reader.GetString("Nota3"),
                                    TipoCambio = Convert.ToDecimal(reader.GetString("TipoCambio")),
                                    TipoDescCab = Convert.ToInt32(reader.GetString("TipoDescCab")),
                                    Pendiente = Convert.ToInt32(reader.GetString("Pendiente")),
                                    TipoSinc = reader.GetString("TipoDescCab"),
                                    EstadoSync = Convert.ToInt32(reader.GetString("EstadoSync")),
                                    Latitud = Convert.ToDecimal(reader.GetString("Latitud")),
                                    Longitud = Convert.ToDecimal(reader.GetString("Longitud")),
                                    CodUsuario = reader.GetString("CodUsuario"),
                                    Obs = reader.GetString("Obs"),
                                    FechaModificacion = reader.GetString("FechaModificacion"),
                                });
                            }
                        }
                    }
                }
                return ListPedido;
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
        public object ObtenerPedido02(credenciales usuario)
        {
            List<HBM_Pedido> ListPedido = new List<HBM_Pedido>();
            string query = "";
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    if (usuario.fecha.ToString() == "1900-01-01 00:00:00")
                    {
                        query = " SELECT * " +
                                " from Pedido " +
                                " where CodUsuario = '"+ usuario.usuario.ToString() +"' " +
                                " AND FechaModificacion > DATE_ADD(NOW(), INTERVAL - 27 DAY) " +
                                " UNION " +
                                " SELECT * " +
                                " from PedidoHistorico " +
                                " where CodUsuario = '" + usuario.usuario.ToString() + "' " +
                                " AND FechaModificacion > DATE_ADD(NOW(), INTERVAL - 27 DAY)";
                    }
                    else
                    {
                        query = " SELECT * " +
                                " from Pedido " +
                                " where CodUsuario = '" + usuario.usuario.ToString() + "' " +
                                " AND FechaModificacion >= '" + usuario.fecha.ToString() + "' " +
                                " AND FechaModificacion > DATE_ADD(NOW(), INTERVAL - 27 DAY) " +
                                " UNION SELECT * " +
                                " from PedidoHistorico " +
                                " where CodUsuario = '" + usuario.usuario.ToString() + "' " +
                                " AND FechaModificacion >= '" + usuario.fecha.ToString() + "' " +
                                " AND FechaModificacion > DATE_ADD(NOW(), INTERVAL - 27 DAY) ";
                    }
                    command.CommandText = query;
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListPedido.Add(new HBM_Pedido()
                                {
                                    PedidoID = reader.GetString("PedidoID"),
                                    ActividadID = reader.GetString("ActividadID"),
                                    TareaID = reader.GetString("TareaID"),
                                    NroPedido = reader.GetString("NroPedido"),
                                    Fecha = reader.GetString("Fecha"),
                                    NroSAP = reader.GetString("NroSAP"),
                                    CodTipoPedido = Convert.ToInt32(reader.GetString("CodTipoPedido")),
                                    OrgVenta = reader.GetString("OrgVenta"),
                                    CodCanal = reader.GetString("CodCanal"),
                                    CodSector = reader.GetString("CodSector"),
                                    KUNNR = reader.GetString("KUNNR"),
                                    Moneda = reader.GetString("Moneda"),
                                    MontoTotal = Convert.ToDecimal(reader.GetString("MontoTotal")),
                                    CodTipoPago = reader.GetString("CodTipoPago"),
                                    KunnrFact = reader.GetString("KunnrFact"),
                                    RazonSocial = reader.GetString("RazonSocial"),
                                    Nit = reader.GetString("Nit"),
                                    DirFactura = reader.GetString("DirFactura"),
                                    CodPrioridadEntrega = Convert.ToInt32(reader.GetString("CodPrioridadEntrega")),
                                    KunnrDest = reader.GetString("KunnrDest"),
                                    DirEntrega = reader.GetString("DirEntrega"),
                                    FechaEntrega = reader.GetString("FechaEntrega"),
                                    LugarEntrega = reader.GetString("LugarEntrega"),
                                    FechaCompromisoPago = reader.GetString("FechaCompromisoPago"),
                                    MonedaCompromisoPago = reader.GetString("MonedaCompromisoPago"),
                                    MontoCompromisoPago = Convert.ToDecimal(reader.GetString("MontoCompromisoPago")),
                                    ComentarioCompPago = reader.GetString("ComentarioCompPago"),
                                    Condicion1 = Convert.ToDecimal(reader.GetString("Condicion1")),
                                    Condicion2 = Convert.ToDecimal(reader.GetString("Condicion2")),
                                    Condicion3 = Convert.ToDecimal(reader.GetString("Condicion3")),
                                    Condicion4 = Convert.ToDecimal(reader.GetString("Condicion4")),
                                    ContactoSolID = reader.GetString("ContactoSolID"),
                                    ContactoJmID = reader.GetString("ContactoJmID"),
                                    ContactoRpID = reader.GetString("ContactoRpID"),
                                    Nota1 = reader.GetString("Nota1"),
                                    Nota2 = reader.GetString("Nota2"),
                                    Nota3 = reader.GetString("Nota3"),
                                    TipoCambio = Convert.ToDecimal(reader.GetString("TipoCambio")),
                                    TipoDescCab = Convert.ToInt32(reader.GetString("TipoDescCab")),
                                    Pendiente = Convert.ToInt32(reader.GetString("Pendiente")),
                                    TipoSinc = reader.GetString("TipoDescCab"),
                                    EstadoSync = Convert.ToInt32(reader.GetString("EstadoSync")),
                                    Latitud = Convert.ToDecimal(reader.GetString("Latitud")),
                                    Longitud = Convert.ToDecimal(reader.GetString("Longitud")),
                                    CodUsuario = reader.GetString("CodUsuario"),
                                    Obs = reader.GetString("Obs"),
                                    FechaModificacion = reader.GetString("FechaModificacion"),
                                });
                            }
                        }
                    }
                }
                return Tuple.Create(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), ListPedido);
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
        public object ObtenerPedidoDetalle(credenciales usuario)
        {
            List<HBM_PedidoDetalle> ListPedidoDetalle = new List<HBM_PedidoDetalle>();
            DateTime date1 = new DateTime();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                //DateTime date1;
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * from PedidoDetalle where CodUsuario = '" + usuario.usuario.ToString() + "' AND FechaModificacion > DATE_ADD(NOW(), INTERVAL - 27 DAY) UNION SELECT * from PedidoDetalleHistorico where CodUsuario = '" + usuario.usuario.ToString() + "' AND FechaModificacion > DATE_ADD(NOW(), INTERVAL - 27 DAY)";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (Utils.SafeGetString(reader, "FechaValidezAlt") == "" || reader.GetString("FechaValidezAlt").Equals(null))
                                {
                                    //fechavalidez = "1970-01-01 00:00:00";
                                    date1 = new DateTime(1970, 1, 1, 7, 0, 0);
                                }
                                else
                                {
                                    date1 = Convert.ToDateTime(reader.GetString("FechaValidezAlt"));
                                }

                                ListPedidoDetalle.Add(new HBM_PedidoDetalle()
                                {  
                                    PedidoDetalleID = reader.GetString("PedidoDetalleID"),
                                    PedidoID = Utils.SafeGetString(reader, "PedidoID"),
                                    NroPedido = Utils.SafeGetString(reader, "NroPedido"),
                                    Fecha = Utils.SafeGetString(reader, "Fecha"),
                                    BonoId = Utils.SafeGetString(reader, "BonoId"),
                                    TipoBono = Utils.SafeGetString(reader, "TipoBono"),
                                    CorrelativoBono = Utils.SafeGetInt(reader, "CorrelativoBono"),
                                    CorrelativoVirtual = Utils.SafeGetString(reader, "CorrelativoVirtual"),
                                    Almacen = Utils.SafeGetString(reader, "Almacen"),
                                    ProductCode = Utils.SafeGetString(reader, "ProductCode"),
                                    Lote = Utils.SafeGetString(reader, "Lote"),
                                    Producto = Utils.SafeGetString(reader, "Producto"),
                                    CodSector = Utils.SafeGetString(reader, "CodSector"),
                                    Condicion1 = Convert.ToDecimal(reader.GetString("Condicion1")),
                                    Condicion2 = Convert.ToDecimal(reader.GetString("Condicion2")),
                                    Condicion3 = Convert.ToDecimal(reader.GetString("Condicion3")),
                                    Condicion4 = Convert.ToDecimal(reader.GetString("Condicion4")),
                                    TipoPM1 = Utils.SafeGetString(reader, "TipoPM1"),
                                    TipoPM2 = Utils.SafeGetString(reader, "TipoPM2"),
                                    TipoPM3 = Utils.SafeGetString(reader, "TipoPM3"),
                                    TipoPM4 = Utils.SafeGetString(reader, "TipoPM4"),
                                    Cantidad = Convert.ToInt32(reader.GetString("Cantidad")),
                                    PrecioUnitario = Convert.ToDecimal(reader.GetString("PrecioUnitario")),
                                    Moneda = Utils.SafeGetString(reader, "Moneda"),
                                    SubTotalNeto = Convert.ToDecimal(reader.GetString("SubTotalNeto")),
                                    SubTotalDesc = Convert.ToDecimal(reader.GetString("SubTotalDesc")),
                                    SubTotal = Convert.ToDecimal(reader.GetString("SubTotal")),
                                    CodigoAlternativa = Utils.SafeGetString(reader, "CodigoAlternativa"),
                                    CodigoSinAlternativa = Utils.SafeGetString(reader, "CodigoSinAlternativa"),
                                    CodUsuario = Utils.SafeGetString(reader, "CodUsuario"),
                                    FechaModificacion = Utils.SafeGetString(reader, "FechaModificacion"),
                                    ProductoAlt = Utils.SafeGetString(reader, "ProductoAlt"),
                                    FechaValidezAlt = date1.ToString(), //Utils.SafeGetString(reader, "FechaValidezAlt"), 
                                    EstadoRegistro = Utils.SafeGetString(reader, "EstadoRegistro") 
                                });
                            }
                        }
                    }
                }
                return ListPedidoDetalle;
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
        public object ObtenerPedidoDetalle02(credenciales usuario)
        {
            
            List<HBM_PedidoDetalle> ListPedidoDetalle = new List<HBM_PedidoDetalle>();
            string query = "";
            DateTime date1 = new DateTime();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    if (usuario.fecha.ToString() == "1900-01-01 00:00:00")
                    {
                        query = " SELECT * " +
                                " from PedidoDetalle " +
                                " where CodUsuario = '" + usuario.usuario.ToString()+ "' " +
                                " AND FechaModificacion > DATE_ADD(NOW(), INTERVAL - 27 DAY) " +
                                " UNION " +
                                " SELECT* " +
                                " from PedidoDetalleHistorico " +
                                " where CodUsuario = '" + usuario.usuario.ToString() + "' " +
                                " AND FechaModificacion > DATE_ADD(NOW(), INTERVAL - 27 DAY) ";
                    }
                    else
                    {
                        query = " SELECT * " +
                                " from PedidoDetalle " +
                                " where CodUsuario = '" + usuario.usuario.ToString() + "' " +
                                " AND FechaModificacion >= '" + usuario.fecha.ToString() + "' " +
                                " AND FechaModificacion > DATE_ADD(NOW(), INTERVAL - 27 DAY) " +
                                " UNION " +
                                " SELECT* " +
                                " from PedidoDetalleHistorico " +
                                " where CodUsuario = '" + usuario.usuario.ToString() + "' " +
                                " AND FechaModificacion >= '" + usuario.fecha.ToString() + "' " +
                                " AND FechaModificacion > DATE_ADD(NOW(), INTERVAL - 27 DAY) ";
                    }
                    command.CommandText = query;
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (Utils.SafeGetString(reader, "FechaValidezAlt") == "" || reader.GetString("FechaValidezAlt").Equals(null))
                                {
                                    //fechavalidez = "1970-01-01 00:00:00";
                                    date1 = new DateTime(1970, 1, 1, 7, 0, 0);
                                }
                                else
                                {
                                    date1 =Convert.ToDateTime(reader.GetString("FechaValidezAlt"));
                                }
                                
                                ListPedidoDetalle.Add(new HBM_PedidoDetalle()
                                {
                                    PedidoDetalleID = reader.GetString("PedidoDetalleID"),
                                    PedidoID = Utils.SafeGetString(reader, "PedidoID"),
                                    NroPedido = Utils.SafeGetString(reader, "NroPedido"),
                                    Fecha = Utils.SafeGetString(reader, "Fecha"),
                                    BonoId = Utils.SafeGetString(reader, "BonoId"),
                                    TipoBono = Utils.SafeGetString(reader, "TipoBono"),
                                    CorrelativoBono = Utils.SafeGetInt(reader, "CorrelativoBono"),
                                    CorrelativoVirtual = Utils.SafeGetString(reader, "CorrelativoVirtual"),
                                    Almacen = Utils.SafeGetString(reader, "Almacen"),
                                    ProductCode = Utils.SafeGetString(reader, "ProductCode"),
                                    Lote = Utils.SafeGetString(reader, "Lote"),
                                    Producto = Utils.SafeGetString(reader, "Producto"),
                                    CodSector = Utils.SafeGetString(reader, "CodSector"),
                                    Condicion1 = Convert.ToDecimal(reader.GetString("Condicion1")),
                                    Condicion2 = Convert.ToDecimal(reader.GetString("Condicion2")),
                                    Condicion3 = Convert.ToDecimal(reader.GetString("Condicion3")),
                                    Condicion4 = Convert.ToDecimal(reader.GetString("Condicion4")),
                                    TipoPM1 = Utils.SafeGetString(reader, "TipoPM1"),
                                    TipoPM2 = Utils.SafeGetString(reader, "TipoPM2"),
                                    TipoPM3 = Utils.SafeGetString(reader, "TipoPM3"),
                                    TipoPM4 = Utils.SafeGetString(reader, "TipoPM4"),
                                    Cantidad = Convert.ToInt32(reader.GetString("Cantidad")),
                                    PrecioUnitario = Convert.ToDecimal(reader.GetString("PrecioUnitario")),
                                    Moneda = Utils.SafeGetString(reader, "Moneda"),
                                    SubTotalNeto = Convert.ToDecimal(reader.GetString("SubTotalNeto")),
                                    SubTotalDesc = Convert.ToDecimal(reader.GetString("SubTotalDesc")),
                                    SubTotal = Convert.ToDecimal(reader.GetString("SubTotal")),
                                    CodigoAlternativa = Utils.SafeGetString(reader, "CodigoAlternativa"),
                                    CodigoSinAlternativa = Utils.SafeGetString(reader, "CodigoSinAlternativa"),
                                    CodUsuario = Utils.SafeGetString(reader, "CodUsuario"),
                                    FechaModificacion = Utils.SafeGetString(reader, "FechaModificacion"),
                                    ProductoAlt = Utils.SafeGetString(reader, "ProductoAlt"),
                                    FechaValidezAlt = date1.ToString(), //Utils.SafeGetString(reader, "FechaValidezAlt"), 
                                    EstadoRegistro = Utils.SafeGetString(reader, "EstadoRegistro")
                                });
                            }
                        }
                    }
                }
                return Tuple.Create(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), ListPedidoDetalle);
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
        public object ObtenerRamo()
        {
            List<HBM_Ramo> ListRamo = new List<HBM_Ramo>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select * from Ramo R";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListRamo.Add(new HBM_Ramo()
                                {
                                    CodRamo = reader.GetString("CodRamo"),
                                    Denominacion = reader.GetString("Denominacion"),
                                    Division = reader.GetString("Division"),
                                    FechaModificacion = reader.GetString("FechaModificacion")
                                });
                            }
                        }
                    }
                }
                return ListRamo;
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
        public object ObtenerRamo02(credenciales fecha)
        {
            List<HBM_Ramo> ListRamo = new List<HBM_Ramo>();
            string query = "";
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    if (fecha.fecha.ToString() == "1900-01-01 00:00:00")
                    {
                        query = "select * from Ramo R";
                    }
                    else
                    {
                        query = "select * from Ramo R WHERE FechaModificacion >= '"+ fecha.fecha.ToString()+"'";
                    }
                    command.CommandText = query;
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListRamo.Add(new HBM_Ramo()
                                {
                                    CodRamo = reader.GetString("CodRamo"),
                                    Denominacion = reader.GetString("Denominacion"),
                                    Division = reader.GetString("Division"),
                                    FechaModificacion = reader.GetString("FechaModificacion")
                                });
                            }
                        }
                    }
                }
                return Tuple.Create(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), ListRamo);
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
        public object ObtenerSubRamo()
        {
            List<HBM_SubRamo> ListSubRamo = new List<HBM_SubRamo>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select * from SubRamo R";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListSubRamo.Add(new HBM_SubRamo()
                                {
                                    CodSubRamo = reader.GetString("CodSubRamo"),
                                    Denominacion = reader.GetString("Denominacion"),
                                    Division = reader.GetString("Division"),
                                    FechaModificacion = reader.GetString("FechaModificacion")
                                });
                            }
                        }
                    }
                }
                return ListSubRamo;
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
        public object ObtenerSubRamo02(credenciales fecha)
        {
            List<HBM_SubRamo> ListSubRamo = new List<HBM_SubRamo>();
            string query = "";
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    if (fecha.fecha.ToString() == "1900-01-01 00:00:00")
                    {
                        query = "select * from SubRamo R";
                    }
                    else
                    {
                        query = "select * from SubRamo R WHERE FechaModificacion >= '"+ fecha.fecha.ToString()+"'";
                    }
                    command.CommandText = "select * from SubRamo R";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListSubRamo.Add(new HBM_SubRamo()
                                {
                                    CodSubRamo = reader.GetString("CodSubRamo"),
                                    Denominacion = reader.GetString("Denominacion"),
                                    Division = reader.GetString("Division"),
                                    FechaModificacion = reader.GetString("FechaModificacion")
                                });
                            }
                        }
                    }
                }
                return Tuple.Create(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), ListSubRamo);
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
        public object ObtenerTratamiento(credenciales usuario)
        {
            List<HBM_Tratamiento> ListTramo = new List<HBM_Tratamiento>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select TR.* from Tratamiento TR  inner join Usuario U on U.Organizacion = TR.Division WHERE U.CodUsuario = '" + usuario.usuario.ToString() + "'";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListTramo.Add(new HBM_Tratamiento()
                                {
                                    Codigo = reader.GetString("Codigo"),
                                    Descripcion = reader.GetString("Descripcion"),
                                    Observacion = reader.GetString("Observacion"),
                                    Division = reader.GetString("Division")
                                });
                            }
                        }
                    }
                }
                return ListTramo;

            }
            catch (MySqlException e)
            {
                conexion_mysql_open.cerrar_conexion();
                List<string> Listmensajes = new List<string>();
                Listmensajes.Add(e.Message.ToString());
                return Listmensajes;
            }
        }
    }
}

