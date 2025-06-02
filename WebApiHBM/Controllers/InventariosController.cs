using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Web.Http;
using WebApiHBM.Models;
using System.Data;
using System.Threading.Tasks;
using WebApiHBM.Conexiones;

namespace WebApiHBM.Controllers
{
    public class InventariosController : ApiController
    {
        [HttpPost]
        public object ObtenerPlanInventario(credenciales usuario)
        {
            List<HBM_PlanInventarios> PlanInventario = new List<HBM_PlanInventarios>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select * from PlanInventario where CodUsuario ='" + usuario.usuario.ToString() + "'";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection)) 
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                PlanInventario.Add(new HBM_PlanInventarios()
                                {
                                    KUNNR = reader.GetString("KUNNR"),
                                    ProductCode = reader.GetString("ProductCode"),
                                    CantidadMin = Convert.ToInt32(reader.GetString("CantidadMin")),
                                    CantidadMax = Convert.ToInt32(reader.GetString("CantidadMax")), 
                                    CodUsuario = reader.GetString("CodUsuario"), 
                                    Fecha = reader.GetString("Fecha"),
                                    Codigo_Vendedor = reader.GetString("CodVendedor"),
                                    OrgVenta = reader.GetString("OrgVenta"),
                                    CodCanal = reader.GetString("CodCanal"),
                                    CodSector = reader.GetString("AreaMercado"), //.Substring(0 , 3),
                                });
                            }
                        }
                    }
                }
                return PlanInventario;
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
        public object ObtenerInventario(credenciales usuario)
        {
            List<HBM_Inventarios> Inventario = new List<HBM_Inventarios>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select * from Inventario where CodUsuario = '" + usuario.usuario.ToString() + "' AND Fecha BETWEEN CURDATE() - INTERVAL 30 DAY AND CURDATE() + interval 1 day";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Inventario.Add(new HBM_Inventarios()
                                {
                                    InventarioID = reader.GetString("InventarioID"),
                                    TareaID = reader.GetString("TareaID"),
                                    ActividadID = reader.GetString("ActividadID"),
                                    ProductCode = reader.GetString("ProductCode"),
                                    CodUsuario = reader.GetString("CodUsuario"),
                                    CantidadMin = Utils.SafeGetInt(reader,"CantidadMin"),
                                    CantidadMax = Utils.SafeGetInt(reader, "CantidadMax"), //Convert.ToInt32(reader.GetString("CantidadMax")),
                                    Cantidad = Utils.SafeGetInt(reader, "Cantidad"), //C Convert.ToInt32(reader.GetString("Cantidad")),
                                    Fecha = Utils.SafeGetString(reader, "Fecha"),
                                    Programado = Utils.SafeGetString(reader, "Programado"),
                                    KUNNR = Utils.SafeGetString(reader, "KUNNR"),
                                    ProductName = Utils.SafeGetString(reader, "ProductName"),
                                    CodSector = Utils.SafeGetString(reader, "CodSector"),
                                    CodFamilia = Utils.SafeGetString(reader, "CodFamilia"),
                                    CodGrupo = Utils.SafeGetString(reader, "CodGrupo"),
                                    CodSubGrupo = Utils.SafeGetString(reader, "CodSubGrupo"),
                                    Sector = Utils.SafeGetString(reader, "Sector"),
                                    Familia = Utils.SafeGetString(reader, "Familia"),
                                    Grupo = Utils.SafeGetString(reader, "Grupo"),
                                    SubGrupo = Utils.SafeGetString(reader, "SubGrupo"),
                                });
                            }
                        }
                    }
                }
                return Inventario;
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
        public object ObtenerparamInventario(credenciales usuario)
        {
            List<HBM_ParamInventarios> Inventario = new List<HBM_ParamInventarios>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select * from ParamInventario where CodUsuario = '" + usuario.usuario.ToString() + "'";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Inventario.Add(new HBM_ParamInventarios()
                                {
                                    ParamInventarioID = reader.GetString("ParamInventarioID"),
                                    CodUsuario = reader.GetString("CodUsuario"),
                                    InvTodos = reader.GetString("InvTodos"),
                                    InvCliente = reader.GetString("InvCliente"),
                                    Modo = reader.GetString("Modo"),
                                    Semana1 = reader.GetString("Semana1"),
                                    Semana2 = reader.GetString("Semana2"),
                                    Semana3 = reader.GetString("Semana3"),
                                    Semana4 = reader.GetString("Semana4"),
                                    Semana5 = reader.GetString("Semana5"),
                                    FechaModificacion = reader.GetString("FechaModificacion")
                                });
                            }
                        }
                    }
                }
                return Inventario;
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
        public object ObtenerRecepcionEquipos(credenciales usuario)
        {
            List<HBM_RecepcionEquipos> ListRecepcionEquipos = new List<HBM_RecepcionEquipos>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select * from RecepcionEquipos WHERE IdVendedor = '" + usuario.usuario.ToString() + "'";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListRecepcionEquipos.Add(new HBM_RecepcionEquipos()
                                {
                                    RecepcionID = Utils.SafeGetString(reader, "RecepcionID"), //reader.GetString("RecepcionID"),
                                    Estado = Utils.SafeGetString(reader, "Estado"), //reader.GetString("Estado"),
                                    CodRecepcion = Utils.SafeGetString(reader, "CodRecepcion"), //reader.GetString("CodRecepcion"),
                                    IdVendedor = Utils.SafeGetString(reader, "IdVendedor"), //reader.GetString("IdVendedor"),
                                    Latitud = Convert.ToDecimal(reader.GetString("Latitud")),
                                    Longitud = Convert.ToDecimal(reader.GetString("Longitud")),
                                    OrgVenta = Utils.SafeGetString(reader, "OrgVenta"), //reader.GetString("OrgVenta"),
                                    Regional = Utils.SafeGetString(reader, "Regional"), //reader.GetString("Regional"),
                                    KUNNR = Utils.SafeGetString(reader, "KUNNR"), //reader.GetString("KUNNR"),
                                    NombreCliente = Utils.SafeGetString(reader, "NombreCliente"), //reader.GetString("NombreCliente"),
                                    Distribuidor = Utils.SafeGetString(reader, "Distribuidor"), //reader.GetString("Distribuidor"),
                                    Email = Utils.SafeGetString(reader, "Email"), //reader.GetString("Email"),
                                    TelefonoCelular = Utils.SafeGetString(reader, "TelefonoCelular"), //reader.GetString("TelefonoCelular"),
                                    NombreEquipo = Utils.SafeGetString(reader, "NombreEquipo"), //reader.GetString("NombreEquipo"),
                                    Modelo = Utils.SafeGetString(reader, "Modelo"), //reader.GetString("Modelo"),
                                    Serie = Utils.SafeGetString(reader, "Serie"), //reader.GetString("Serie"),
                                    Obs = Utils.SafeGetString(reader, "Obs"), //reader.GetString("Obs"),
                                    Foto = Utils.SafeGetString(reader, "Foto"), //reader.GetString("Foto"),
                                    Fecha = Utils.SafeGetString(reader, "Fecha"), //reader.GetString("Fecha"),
                                    TipoEnvio = Utils.SafeGetString(reader, "TipoEnvio"), //reader.GetString("TipoEnvio"),
                                    FechaCreate = Utils.SafeGetString(reader, "FechaCreate"), //reader.GetString("FechaCreate"),
                                    FechaUpdate = Utils.SafeGetString(reader, "FechaUpdate"), //reader.GetString("FechaUpdate"),
                                    NombreEV = Utils.SafeGetString(reader, "NombreEV"),
                                    TipoRecepcion = Utils.SafeGetString(reader, "TipoRecepcion"),
                                    OT = Utils.SafeGetString(reader, "OT"),

                                });
                            }
                        }
                    }
                }
                return ListRecepcionEquipos;
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
        public object ObtenerHistorialRecepcionEquipos(credenciales usuario)
        {
            List<HistorialRecepcionEquipos> ListRecepcionEquipos = new List<HistorialRecepcionEquipos>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select * from HistorialRecepcionEquipos WHERE IdVendedor = '" + usuario.usuario.ToString() + "'";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListRecepcionEquipos.Add(new HistorialRecepcionEquipos()
                                {
                                    HistoricoID = Utils.SafeGetString(reader, "HistoricoID"),
                                    RecepcionID = Utils.SafeGetString(reader, "RecepcionID"),
                                    CodRecepcion = Utils.SafeGetString(reader, "CodRecepcion"),
                                    Estado = Utils.SafeGetString(reader, "Estado"),
                                    IdVendedor = Utils.SafeGetString(reader, "IdVendedor"),
                                    Usuario = Utils.SafeGetString(reader, "Usuario"),
                                    Fecha = Utils.SafeGetString(reader, "Fecha"),
                                    Cliente = Utils.SafeGetString(reader, "Cliente"),
                                    TipoEnvio = Utils.SafeGetString(reader, "TipoEnvio"),
                                    NombreRecepcionista = Utils.SafeGetString(reader, "NombreRecepcionista"),
                                    Obs = Utils.SafeGetString(reader, "Obs"),
                                    Transportadora = Utils.SafeGetString(reader, "Transportadora"),
                                    NroGuia = Utils.SafeGetString(reader, "NroGuia"),
                                    FechaEnvio = Utils.SafeGetString(reader, "FechaEnvio"),
                                    ContEstado = Utils.SafeGetString(reader, "ContEstado"),
                                    Foto = Utils.SafeGetString(reader, "Foto"),
                                });
                            }
                        }
                    }
                }
                return ListRecepcionEquipos;

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
        public object ObtenerRecepcionRegionalTaller(credenciales usuario)
        {
            List<RecepcionEquiposRegionalTaller> ListRegionalTaller = new List<RecepcionEquiposRegionalTaller>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM hbm.RecepcionEquiposRegionalTaller";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListRegionalTaller.Add(new RecepcionEquiposRegionalTaller()
                                {
                                    RegionalTallerID = Utils.SafeGetString(reader, "RegionalTallerID"), //reader.GetString("RecepcionID"),
                                    CodigoRegional = Utils.SafeGetString(reader, "CodigoRegional"), //reader.GetString("Estado"),
                                    Regional = Utils.SafeGetString(reader, "Regional")

                                });
                            }
                        }
                    }
                }
                return ListRegionalTaller;
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
