using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Web.Http;
using WebApiHBM.Models;
using FormsAuth;
using System.Data;
using WebApiHBM.Conexiones;
using System.Web.Script.Serialization;

namespace WebApiHBM.Controllers
{
    public class AuthenticationController : ApiController
    {
        [HttpPost]
        public object Auth(credenciales clientes)
        {
            DateTime dateTime = new DateTime();
            String adPath = "LDAP://192.168.1.30";
            string dominio = "Hansa.com.bo";
            Informacion inf = new Informacion();
            LdapAuthentication adAuth = new LdapAuthentication(adPath);
            dateTime = DateTime.Now; 
            inf.Estado = 0;
            HBM_Log objLog = new HBM_Log();
            objLog.CodUsuario = clientes.usuario;
            objLog.FechaInicio = Convert.ToDateTime(dateTime).ToString("yyyy-MM-dd H:mm:ss");
            try
            {
                if (true == adAuth.IsAuthenticated(dominio, clientes.usuario, clientes.password))
                {
                    String nombre = adAuth.GetGroups();
                    String[] ad = nombre.Split(',');
                    inf.Estado = 1;
                    inf.Nombre = ad[0].Substring(10, ad[0].Length - 10);
                    inf.Division = ad[2].Substring(3, ad[2].Length - 3);
                    //inf.usuario = clientes.usuario.ToUpper();
                    inf.Regional = ad[3].Substring(3, ad[3].Length - 3);
                    
                    var conn = conexion_mysql_open.obtener_conexion();

                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = "SELECT u.*, r.MultiUser FROM Usuario u inner join Rol r on u.RolID=r.RolID where u.CodUsuario = '" + clientes.usuario + "';";

                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    inf.Estado = 1;
                                    inf.RolID = reader.GetString("RolID"); 
                                    inf.Division = reader.GetString("Division"); 
                                    inf.DivisionID = reader.GetString("DivisionID"); 
                                    inf.CodigoSap = reader.GetString("CodigoSap");
                                    inf.Cargo = reader.GetString("Cargo");
                                    inf.Organizacion = reader.GetString("Organizacion"); // y.IdOrgVentas;
                                    inf.Regional = reader.GetString("Regional");
                                    inf.CodUsuario = reader.GetString("CodUsuario");
                                    inf.MultiUser = reader.GetInt32("MultiUser");
                                    inf.RegionalCBZ = reader.GetString("RegionalCBZ");
                                    inf.RegionalID = reader.GetString("RegionalID");
                                    inf.CodUsuario_SATELICAR = reader.GetString("CodUsuario_SATELICAR");//kunnr Sin Nombre
                                    //inf.Idamercado = "";
                                    //inf.Amercado = "";
                                }
                            }
                        }
                    }
                    
                    if (inf.DivisionID == "99")
                    {
                        inf.Organizacion = "9900";
                    }
                }
                objLog.MensajeUsuario = "INGRESO EXITOSO";
                objLog.MensajeTecnico = "INGRESO EXITOSO";
                dateTime = DateTime.Now;
                objLog.FechaFin = Convert.ToDateTime(dateTime).ToString("yyyy-MM-dd H:mm:ss");
                var json = new JavaScriptSerializer().Serialize(objLog);
                LogHBM.GenerarLog(json.ToString());
            }
            catch (Exception e)
            {
                objLog.MensajeUsuario = "INGRESO ERRONEO";
                objLog.MensajeTecnico = e.Message.ToString();
                conexion_mysql_open.cerrar_conexion();
                dateTime = DateTime.Now;
                objLog.FechaFin = Convert.ToDateTime(dateTime).ToString("yyyy-MM-dd H:mm:ss");
                inf.Estado = 0;
                var json = new JavaScriptSerializer().Serialize(objLog);
                LogHBM.GenerarLog(json.ToString());
            }
            return inf;
        }
        [HttpPost]
        public object ObtenerUsuarios(credenciales usuario)
        {
            List<HBM_Usuario> ListUsuarios = new List<HBM_Usuario>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select * from Usuario where Estado = '1' AND CodUsuario = '" + usuario.usuario + "';";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.GetString("RolID") == "FFVV" | reader.GetString("RolID") == "EECC" | reader.GetString("RolID") == "FFVVC" | reader.GetString("RolID") == "FFVVI")
                                //if (reader.GetString("RolID").Trim().Contains("FFVV") | reader.GetString("RolID") == "EECC")
                                    {
                                    ListUsuarios.Add(new HBM_Usuario()
                                    {
                                        Organizacion = reader.GetString("Organizacion"),
                                        CodUsuario = reader.GetString("CodUsuario"),
                                        Cargo = reader.GetString("Cargo"),
                                        RolID = reader.GetString("RolID")
                                    });
                                }
                                else
                                {
                                    conexion_mysql_open.cerrar_conexion();
                                    var con = conexion_mysql_open.obtener_conexion();
                                    using (var comman = con.CreateCommand())
                                    {
                                        var consulta = "select U.* from Usuario U ";
                                        if (reader.GetString("RolID") == "SSFFVV")
                                        {
                                            consulta = consulta + " where Estado = '1' AND Organizacion = '" + reader.GetString("Organizacion") +"'";
                                        }
                                        else
                                        { consulta = consulta + " where Estado = '1'"; }
                                        consulta = consulta + " ORDER BY CodUsuario ASC";
                                        comman.CommandText = consulta;

                                        using (var readers = comman.ExecuteReader(CommandBehavior.CloseConnection))
                                        {
                                            if (readers.HasRows)
                                            {
                                                while (readers.Read())
                                                {
                                                    ListUsuarios.Add(new HBM_Usuario()
                                                    {
                                                        Organizacion = readers.GetString("Organizacion"),
                                                        CodUsuario = readers.GetString("CodUsuario"),
                                                        Cargo = readers.GetString("Cargo"),
                                                        RolID = readers.GetString("RolID")
                                                    });
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return ListUsuarios;
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
        public object ObtenerUsuarios02(credenciales usuario)
        {
            List<HBM_UsuarioRol> ListUsuarios = new List<HBM_UsuarioRol>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {                    
                    command.CommandText = "select u.Organizacion,u.CodUsuario,u.Cargo,u.RolID,r.Rol,u.RegionalID,u.Regional,u.Nombre from Usuario u inner join Rol r on u.RolID = r.RolID where u.Estado = 1 and Organizacion = '" + usuario.OrgVenta + "' and RegionalID='"+ usuario.RegionalID +"' ; ";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListUsuarios.Add(new HBM_UsuarioRol()
                                {
                                    Organizacion = reader.GetString("Organizacion"),
                                    CodUsuario = reader.GetString("CodUsuario"),
                                    Nombre = reader.GetString("Nombre"),
                                    Cargo = reader.GetString("Cargo"),
                                    RolID = reader.GetString("RolID"),
                                    Rol = reader.GetString("Rol"),
                                    RegionalID = Utils.SafeGetString(reader, "RegionalID"),                                    
                                    Regional = Utils.SafeGetString(reader, "Regional")
                                });
                            }                                                         
                        }
                    }
                }
                return ListUsuarios;
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
        public object ObtenerModulo(credenciales usuario)
        {
            List<HBM_Modulo> ListModulo = new List<HBM_Modulo>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select * from Modulo;";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListModulo.Add(new HBM_Modulo()
                                {
                                    ModuloID = reader.GetString("ModuloID"),
                                    Nombre = reader.GetString("Nombre"),
                                    FotoImage = reader.GetString("FotoImage")
                                });
                            }
                        }
                    }
                }
                return ListModulo;
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
        public object ObtenerRol(credenciales usuario)
        {
            List<HBM_Rol> ListRol = new List<HBM_Rol>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select r.* from Rol r inner join Usuario u on u.RolID = r.RolID where u.CodUsuario = '" + usuario.usuario + "'";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListRol.Add(new HBM_Rol()
                                {
                                    RolID = reader.GetString("RolID"),
                                    Rol = reader.GetString("Rol"),
                                    Descripcion = reader.GetString("Descripcion"),
                                    Estado = Convert.ToInt32(reader.GetString("Estado"))
                                });
                            }
                        }
                    }
                }
                return ListRol;
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
        public object ObtenerRolModulo(credenciales usuario)
        {
            List<HBM_RolModulo> ListUsuarioRMenu = new List<HBM_RolModulo>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select * from RolModulo r inner join Usuario u on u.RolID = r.RolID where u.CodUsuario = '" + usuario.usuario + "'";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListUsuarioRMenu.Add(new HBM_RolModulo()
                                {
                                    RolID = reader.GetString("RolID"),
                                    ModuloID = reader.GetString("ModuloID"),
                                    FechaModificacion = reader.GetString("FechaModificacion")
                                });
                            }
                        }
                    }
                }
                return ListUsuarioRMenu;
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
        public object ObtenerTablas(credenciales usuario)
        {
            List<HBM_Tablas> ListTablas = new List<HBM_Tablas>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select t2.* from Tablas t2";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListTablas.Add(new HBM_Tablas()
                                {
                                    Nombre = reader.GetString("Nombre"),
                                    RangoDias = reader.GetString("RangoDias"),
                                    CampoFecha = reader.GetString("CampoFecha"),
                                    FechaUltSync = reader.GetString("FechaUltSync"),
                                    Rol = reader.GetString("Rol"),
                                    Estado = reader.GetString("Estado"),
                                    Observacion = reader.GetString("Observacion"),
                                    Sync = reader.GetString("Sync")
                                });
                            }
                        }
                    }
                }
                return ListTablas;
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
        public object ObtenerTablas02(credenciales usuario)
        {
            List<HBM_Tablas> ListTablas = new List<HBM_Tablas>();
            string query = "";
            try 
            {
                if (usuario.fecha.ToString() == "")
                {
                    query = "select t2.* from Tablas t2";
                }
                else
                {
                    query = "select t2.* from Tablas t2 where t2.FechaUltSync >= '" + usuario.fecha.ToString() + "';";
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
                                ListTablas.Add(new HBM_Tablas()
                                {
                                    Nombre = reader.GetString("Nombre"),
                                    RangoDias = reader.GetString("RangoDias"),
                                    CampoFecha = reader.GetString("CampoFecha"),
                                    FechaUltSync = reader.GetString("FechaUltSync"),
                                    Rol = reader.GetString("Rol"),
                                    Estado = reader.GetString("Estado"),
                                    Observacion = reader.GetString("Observacion"),
                                    Sync = reader.GetString("Sync")
                                });
                            }
                        }
                    }
                }
                return Tuple.Create(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), ListTablas);
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
        public object ObtenerPermisoFormulario(credenciales usuario)
        {
            List<HBM_FormularioCampo> ListFormularioCampo = new List<HBM_FormularioCampo>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM hbm.PermisoFormularioCampo where RolID = '"+ usuario .RolID + "';";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListFormularioCampo.Add(new HBM_FormularioCampo()
                                {
                                    Id = Utils.SafeGetString(reader, "Id"),
                                    RolID = Utils.SafeGetString(reader, "RolID"),
                                    Formulario = Utils.SafeGetString(reader, "Formulario"),
                                    Campo = Utils.SafeGetString(reader, "Campo"),
                                    Ver =  Utils.SafeGetInt(reader, "Ver"),
                                    Editar = Utils.SafeGetInt(reader, "Editar"),
                                    FechaModificacion = Utils.SafeGetString(reader, "FechaModificacion")
                                });
                            }
                        }
                    }
                }
                return ListFormularioCampo;
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
        public object ObtenerNroDocumentos(credenciales usuario)
        {
            List<HBM_NroDocumentos> ListFormularioCampo = new List<HBM_NroDocumentos>();
            int pedido = 0, recibo = 0, recepcion = 0;
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT MAX(CAST(NroPedido AS UNSIGNED)) as NroPedido, CodUsuario " +
                                          "FROM (SELECT NroPedido, CodUsuario FROM Pedido " +
                                               " UNION ALL "+
                                               " SELECT NroPedido, CodUsuario FROM PedidoHistorico ) dummy " +
                                               " WHERE CodUsuario = '" + usuario.usuario + "';";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                pedido = Utils.SafeGetInt(reader, "NroPedido");
                            }
                        }
                    }
                }
                var conn1 = conexion_mysql_open.obtener_conexion();
                using (var command1 = conn1.CreateCommand())
                {
                    command1.CommandText = "SELECT MAX(CAST(CodRecibo AS UNSIGNED)) as NroRecibo, IdVendedor " +
                                             "FROM (SELECT CodRecibo, IdVendedor FROM Recibo " +
                                                  " UNION ALL " +
                                                  " SELECT CodRecibo, IdVendedor FROM ReciboHistorico ) dummy " +
                                                  " where IdVendedor = '" + usuario.usuario + "';";
                    using (var reader = command1.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                recibo = Utils.SafeGetInt(reader, "NroRecibo");
                            }
                        }
                    }
                }
                var conn2 = conexion_mysql_open.obtener_conexion();
                using (var command2 = conn2.CreateCommand())
                {
                    command2.CommandText = "SELECT MAX(CAST(CodRecepcion AS UNSIGNED)) as CodRecepcion, IdVendedor  " +
                                             "FROM(SELECT CodRecepcion, IdVendedor FROM RecepcionEquipos " +
                                                  " UNION ALL " +
                                                  " SELECT CodRecepcion, IdVendedor FROM HistorialRecepcionEquipos) dummy " +
                                                  " where IdVendedor = '" + usuario.usuario + "';";
                    using (var reader = command2.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                recepcion = Utils.SafeGetInt(reader, "CodRecepcion");
                            }
                        }
                    }
                }

                ListFormularioCampo.Add(new HBM_NroDocumentos()
                {
                    NroRecibo = recibo,
                    NroPedido = pedido,
                    NroRecepcionEquipo = recepcion,
                    MaximosCorrectos = 1,
                });
                return ListFormularioCampo;
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
        public object ObtenerParamActividad(PMActividad PMActividad)
        {
            List<HBM_ParamActividad> ListPMActividad = new List<HBM_ParamActividad>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select * from ParamActividad where RolID = '" + PMActividad.RolID.ToString() + "';";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListPMActividad.Add(new HBM_ParamActividad()
                                {
                                    RolID = reader.GetString("RolID"),
                                    RutaObligatoria = reader.GetString("RutaObligatoria"),
                                    InicioQR = reader.GetString("InicioQR"),
                                    FechaModificacion = reader.GetString("FechaModificacion")
                                });
                            }
                        }
                    }
                }
                return ListPMActividad;
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
        public object ObtenerRecurso()
        {
            List<HBM_Recurso> Lista = new List<HBM_Recurso>();            
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    //command.CommandText = "SELECT r.*FROM Recurso r inner join RolRecurso rr on r.RecursoID = rr.RecursoID where rr.RolID = '" + usuario.RolID + "'";
                    command.CommandText = "SELECT * FROM Recurso ";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Lista.Add(new HBM_Recurso()
                                {
                                    RecursoID = reader.GetString("RecursoID"),
                                    Recurso = reader.GetString("Recurso"),
                                    Descripcion = reader.GetString("Descripcion"),
                                    Accion = reader.GetString("Accion"),
                                    Tipo = reader.GetString("Tipo"),
                                    Modulo = reader.GetString("Modulo"),
                                    Orden = Convert.ToInt32(reader.GetString("Orden"))                                    
                                });
                            }
                        }
                    }
                }
                return Lista;
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
        public object ObtenerRolRecurso()
        {
            List<HBM_RolRecurso> Lista = new List<HBM_RolRecurso>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM RolRecurso ";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Lista.Add(new HBM_RolRecurso()
                                {
                                    RolRecursoID = reader.GetString("RolRecursoID"),
                                    RolID = reader.GetString("RolID"),
                                    RecursoID = reader.GetString("RecursoID"),
                                    FechaModificacion = reader.GetString("FechaModificacion"),
                                    
                                });
                            }
                        }
                    }
                }
                return Lista;
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
        public object ObtenerFechaHora()
        {

            try
            {
                return new Info() { Fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };   
            }
            catch (MySqlException e)
            {
               
                return String.Empty;
            }
        }

    }


}

