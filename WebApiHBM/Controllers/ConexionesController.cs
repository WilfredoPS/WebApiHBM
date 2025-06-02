using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Web.Http;
using WebApiHBM.Models;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using WebApiHBM.Conexiones;

namespace WebApiHBM.Controllers
{
    public class ConexionesController : ApiController
    {
        public static string Eliminar_Conexiones()
        {
            //string mensaje = "";
            //var conn = conexion_mysql_open.obtener_conexion();
            //try
            //{
            //    using (var command = conn.CreateCommand())
            //    {
            //        command.CommandText = "CALL Delete_connection();";
            //        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
            //        {
            //            if (reader.HasRows)
            //            {
            //                mensaje = "Procedure Executed";
            //            }
            //        }
            //    }
            //}
            //catch (MySqlException ex)
            //{
            //    mensaje = ex.ToString();
            //}
            return "";
        }
    }
}
