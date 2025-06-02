using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Web.Http;
using WebApiHBM.Models;
using System.Data;
using WebApiHBM.Conexiones;

namespace WebApiHBM.Controllers
{
    public class ActivityController : ApiController
    {
        [HttpPost]
        public object ObtenerActividad(credenciales usuario)
        {
            string RangoDias = "";
            List<HBM_Actividad> ListActividad = new List<HBM_Actividad>();
            try
            {
                var con = conexion_mysql_open.obtener_conexion();
                using (var commanmy_sql = con.CreateCommand())
                {
                    commanmy_sql.CommandText = "SELECT * FROM hbm.Tablas WHERE Nombre = 'Actividad';";
                    using (var read = commanmy_sql.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (read.HasRows)
                        {
                            while (read.Read())
                            {
                                RangoDias = read.GetString("RangoDias");
                            }
                        }
                    }
                }

                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select * from Actividad where CodUsuario = '" + usuario.usuario + "'AND FechaCrea > DATE_ADD(NOW(), INTERVAL - " + RangoDias + " DAY) ";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListActividad.Add(new HBM_Actividad()
                                {
                                    ActividadID = Utils.SafeGetString(reader, "ActividadID"), //reader.GetString("ActividadID"),
                                    CustomerID = Utils.SafeGetString(reader, "CustomerID"), //reader.GetString("CustomerID"),
                                    Kunnr = Utils.SafeGetString(reader, "Kunnr"), //reader.GetString("Kunnr"),
                                    Descripcion = Utils.SafeGetString(reader, "Descripcion"), //reader.GetString("Descripcion"),
                                    CodUsuario = Utils.SafeGetString(reader, "CodUsuario"), //reader.GetString("CodUsuario"),
                                    FechaIniPlan = Utils.SafeGetString(reader, "FechaIniPlan"), //reader.GetString("FechaIniPlan"),
                                    FechaFinPlan = Utils.SafeGetString(reader, "FechaFinPlan"), //reader.GetString("FechaFinPlan"),
                                    FechaIniReal = Utils.SafeGetString(reader, "FechaIniReal"), //reader.GetString("FechaIniReal"),
                                    FechaFinReal = Utils.SafeGetString(reader, "FechaFinReal"), //reader.GetString("FechaFinReal"),
                                    LatitudPlan = Utils.SafeGetInt(reader, "LatitudPlan"),//Convert.ToDecimal(reader.GetString("LatitudPlan")),
                                    LongitudPlan = Utils.SafeGetInt(reader, "LongitudPlan"),//Convert.ToDecimal(reader.GetString("LongitudPlan")),
                                    LatitudReal = Utils.SafeGetInt(reader, "LatitudReal"),//Convert.ToDecimal(reader.GetString("LatitudReal")),
                                    LongitudReal = Utils.SafeGetInt(reader, "LongitudReal"),//Convert.ToDecimal(reader.GetString("LongitudReal")),
                                    FechaCrea = Utils.SafeGetString(reader, "FechaCrea"), //reader.GetString("FechaCrea"),
                                    Observacion = Utils.SafeGetString(reader, "Observacion"), //reader.GetString("Observacion"),
                                    Estado = Utils.SafeGetString(reader, "Estado"), //reader.GetString("Estado"),
                                    MotivoNoActividad = Utils.SafeGetString(reader, "MotivoNoActividad"), //reader.GetString("MotivoNoActividad")
                                });
                            }
                        }
                    }
                }
                return ListActividad;
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
        public object ObtenerTarea(credenciales usuario)
        {
            List<HBM_Tarea> ListTarea = new List<HBM_Tarea>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT T.* FROM Tarea T INNER JOIN Actividad A ON T.ActividadID = A.ActividadID WHERE A.CodUsuario = '" + usuario.usuario + "';";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListTarea.Add(new HBM_Tarea()
                                {
                                    TareaID = Utils.SafeGetString(reader, "TareaID"), //reader.GetString("TareaID"),
                                    ActividadID = Utils.SafeGetString(reader, "ActividadID"), //reader.GetString("ActividadID"),
                                    Tarea = Utils.SafeGetInt(reader, "Tarea"),//Convert.ToInt32(reader.GetString("Tarea")),
                                    FechaInicio = Utils.SafeGetString(reader, "FechaInicio"), //reader.GetString("FechaInicio"),
                                    FechaFin = Utils.SafeGetString(reader, "FechaFin"), //reader.GetString("FechaFin"),
                                    Estado = Utils.SafeGetInt(reader, "Estado"),//Convert.ToInt32(reader.GetString("Estado")),
                                    Observacion = Utils.SafeGetString(reader, "Observacion"), //reader.GetString("Observacion"),
                                    CodUsuario = Utils.SafeGetString(reader, "CodUsuario"), //reader.GetString("CodUsuario")
                                });
                            }
                        }
                    }
                }
                return ListTarea;
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
        public object ObtenerTipoTarea(credenciales usuario)
        {
            List<HBM_TipoTarea> ListTipoTarea = new List<HBM_TipoTarea>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT T.* FROM TipoTarea T INNER JOIN  Usuario U ON T.Division = U.Organizacion and T.RolID = U.RolID WHERE CodUsuario = '" + usuario.usuario + "';";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListTipoTarea.Add(new HBM_TipoTarea()
                                {
                                    TipoTareaID = Utils.SafeGetString(reader, "TipoTareaID"),
                                    CodTipoTarea = Utils.SafeGetString(reader, "CodTipoTarea"),//Convert.ToInt32(reader.GetString("CodTipoTarea")),
                                    Descripcion = Utils.SafeGetString(reader, "Descripcion"), //reader.GetString("Descripcion"),
                                    Observacion = Utils.SafeGetString(reader, "Observacion"), //reader.GetString("Observacion"),
                                    Division = Utils.SafeGetInt(reader, "Division"),//Convert.ToInt32(reader.GetString("Division")),
                                    RolID = Utils.SafeGetString(reader, "RolID"), //reader.GetString("RolID"),
                                    Estado = Utils.SafeGetInt(reader, "Estado"), //Convert.ToInt32(reader.GetString("Estado")),
                                    TiempoEstimado = Utils.SafeGetInt(reader, "TiempoEstimado"), //Convert.ToInt32(reader.GetString("TiempoEstimado"))
                                });
                            }
                        }
                    }
                }
                return ListTipoTarea;
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
        public object ObtenerMotivoNoActividad(credenciales usuario)
        {
            List<HBM_MotivoNoActividad> ListMotivoNoActividad = new List<HBM_MotivoNoActividad>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT T.* FROM MotivoNoActividad T INNER JOIN  Usuario U ON T.DivisionID = U.DivisionID WHERE CodUsuario = '" + usuario.usuario + "';";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListMotivoNoActividad.Add(new HBM_MotivoNoActividad()
                                {
                                    CodNoActividad = Utils.SafeGetInt(reader, "CodNoActividad"), //Convert.ToInt32(reader.GetString("CodNoActividad")),
                                    Evento = Utils.SafeGetString(reader, "Evento"),
                                    Descripcion = Utils.SafeGetString(reader, "Descripcion"),
                                    Observacion = Utils.SafeGetString(reader, "Observacion"),
                                    DivisionID = Utils.SafeGetString(reader, "DivisionID"),
                                    Estado = Utils.SafeGetInt(reader, "Estado") //Convert.ToInt32(reader.GetString("Estado"))
                                });
                            }
                        }
                    }
                }
                return ListMotivoNoActividad;
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
