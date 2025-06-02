using Historial.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebApiHBM.Conexiones;
using WebApiHBM.Models;

namespace WebApiHBM.Controllers
{
    public class SifeController : ApiController
    {
        [System.Web.Http.HttpPost]
        public object ObtenerActividadSife(credenciales usuario)
        {
            List<HBM_ActividadSife> ListActividadSife = new List<HBM_ActividadSife>();
            CultureInfo culture = new CultureInfo("en-US");
            DateTime fecha = Convert.ToDateTime("9999-12-31", culture);

            try
            {
                MySqlCommand cmd = new MySqlCommand("Get_ActividadSife", conexion_mysql_open.obtener_conexion());
                cmd.CommandType = CommandType.StoredProcedure;
                using (MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dr.Read())
                    {
                        ListActividadSife.Add(new HBM_ActividadSife()
                        {
                            CodActividad = Utils.SafeGetString(dr, "CodActividad"),
                            Descripcion = Utils.SafeGetString(dr, "Descripcion"),
                            HomologaSap = Utils.SafeGetString(dr, "HomologaSap"),
                            TipoActividad = Utils.SafeGetString(dr, "TipoActividad"),
                            DescLeyenda = Utils.SafeGetString(dr, "DescLeyenda"),
                        });
                    }
                }

                return ListActividadSife;
            }
            catch (MySqlException e)
            {
                conexion_mysql_open.cerrar_conexion();
                List<string> Listmensajes = new List<string>();
                Listmensajes.Add(e.Message.ToString());
                return Listmensajes;
            }
        }

        [System.Web.Http.HttpPost]
        public object ObtenerHistorialFacturas(Factura Factura)
        {
            List<HBM_HistorialFactura> ListHistorialFactura = new List<HBM_HistorialFactura>();
            CultureInfo culture = new CultureInfo("en-US");
            DateTime fecha = Convert.ToDateTime("9999-12-31", culture);

            try
            {
                MySqlCommand cmd = new MySqlCommand("Get_HistorialFactura", conexion_mysql_open.obtener_conexion());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("FacturaID", Factura.FacturaID));
                using (MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dr.Read())
                    {
                        ListHistorialFactura.Add(new HBM_HistorialFactura()
                        {
                            historicoID = Utils.SafeGetString(dr, "historicoID"),
                            facturaID = Utils.SafeGetString(dr, "facturaID"),
                            orgVenta = Utils.SafeGetString(dr, "orgVenta"),
                            cuf = Utils.SafeGetString(dr, "cuf"),
                            fecha = Utils.SafeGetString(dr, "fecha"),
                            estado = Utils.SafeGetString(dr, "estadoID"),
                            //descripcion = Utils.SafeGetString(dr, "descripcion"),
                            nroDocumento = Utils.SafeGetString(dr, "nroDocumento"),
                            codUsuario = Utils.SafeGetString(dr, "usuarioTrans"),
                            transaccion = Utils.SafeGetString(dr, "transaccion"),
                            mensaje = Utils.SafeGetString(dr, "mensaje"),
                            mensajeTec = Utils.SafeGetString(dr, "mensajeTec"),
                            origen = Utils.SafeGetString(dr, "origen"),
                        });
                    }
                }

                return ListHistorialFactura;
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