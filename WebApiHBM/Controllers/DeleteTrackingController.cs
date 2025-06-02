//using System.Web.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Http;
using WebApiHBM.Models;

namespace WebApiHBM.Controllers
{
    public class DeleteTrackingController : ApiController // : Controller
    {
        [HttpPost]
        public object ObtenerDeleteTracking(credenciales usuario)
        {
            List<HBM_Delete> ListDelete = new List<HBM_Delete>();

            var conn = conexion_mysql_open.obtener_conexion();

            try
            {
                using (var command = conn.CreateCommand())
                {
                    if (usuario.fecha.ToString() == "1900-01-01 00:00:00")
                    {
                        command.CommandText = "SELECT * FROM delete_tracking where CodUsuario = '" + usuario.usuario + "';";
                    }
                    else
                    {
                        command.CommandText = "SELECT * FROM delete_tracking where CodUsuario = '" + usuario.usuario + "' AND FechaModificacion >= '" + usuario.fecha.ToString() + "';";
                    }

                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListDelete.Add(new HBM_Delete()
                                {
                                    DeleteTrack_ID = reader.GetString("DeleteTrack_ID"),
                                    ID = reader.GetString("ID"),
                                    Tabla = reader.GetString("Tabla"),
                                    FechaModificacion = reader.GetString("FechaModificacion"),
                                    CodUsuario = reader.GetString("CodUsuario"),
                                    Campo_Llave = reader.GetString("Campo_Llave")
                                });
                            }
                        }
                    }
                    return Tuple.Create(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), ListDelete);
                }
            }
            catch (MySqlException ex)
            {
                return ex;
            }
        }

        [HttpPost]
        public string Eliminar_Conexiones()
        {
            string mensaje = "";
            var conn = conexion_mysql_open.obtener_conexion();
            try
            {
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "CALL Delete_connection();";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            mensaje = "Procedure Executed";
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                mensaje = ex.ToString();
            }
            return mensaje;
        }
    }
}