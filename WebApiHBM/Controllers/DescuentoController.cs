using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Http;
using WebApiHBM.Models;

namespace WebApiHBM.Controllers
{
    public class DescuentoController : ApiController
    {
        [HttpPost]
        public object ObtenerDescuento(credenciales usuario)
        {
            List<Descuento> ListDescuento = new List<Descuento>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT D.* FROM Descuento D inner join Usuario U ON D.OrgVenta = U.Organizacion WHERE U.CodUsuario = '" + usuario.usuario.ToString() + "' and D.ClaseDocumento IN ('ZPVM','ZPVD') order by D.ESQUEMA ASC";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListDescuento.Add(new Descuento()
                                {
                                    CodDescuento = reader.GetString("CodDescuento"),
                                    ClaseDocumento = reader.GetString("ClaseDocumento"),
                                    OrgVenta = reader.GetString("OrgVenta"),
                                    CodCanal = reader.GetString("CodCanal"),
                                    CodSector = reader.GetString("CodSector"),
                                    Esquema = reader.GetString("Esquema")
                                });
                            }
                        }
                    }
                }
                return ListDescuento;
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
        public object ObtenerDetDescuento(credenciales usuario)
        {
            List<DescuentoDetalle> ListDetDescuento = new List<DescuentoDetalle>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT distinct DT.* FROM Descuento D inner join DetalleDescuento DT ON DT.Esquema = D.Esquema inner join Usuario U ON D.OrgVenta = U.Organizacion WHERE U.CodUsuario = '" + usuario.usuario.ToString() + "' AND D.ClaseDocumento IN ('ZPVM','ZPVD') order by DT.ESQUEMA ASC";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListDetDescuento.Add(new DescuentoDetalle()
                                {
                                    CodDetalleDescuento = reader.GetString("CodDetalleDescuento"),
                                    Esquema = reader.GetString("Esquema"),
                                    ClaseCondicion = reader.GetString("ClaseCondicion"),
                                    Posicion = Convert.ToInt32(reader.GetString("Posicion")),
                                    Denominacion = reader.GetString("Denominacion"),
                                    Tipo = reader.GetString("Tipo"),
                                    TipoPM = reader.GetString("TipoPM"),
                                    Activado = Convert.ToInt32(reader.GetString("Activado")),
                                    Modificable = Convert.ToInt32(reader.GetString("Modificable"))
                                });
                            }
                        }
                    }
                }
                return ListDetDescuento;
            }
            catch (MySqlException e)
            {
                conexion_mysql_open.cerrar_conexion();
                List<string> Listmensajes = new List<string>();
                Listmensajes.Add(e.Message.ToString());
                return Listmensajes;
            }
        }

        //[HttpPost]
        //public object ObtenerProductoDesc(credenciales usuario)
        //{
        //    List<ProductoDesc> ListProductoDesc = new List<ProductoDesc>();
        //    try
        //    {
        //        var conn = conexion_mysql_open.obtener_conexion();
        //        using (var command = conn.CreateCommand())
        //        {
        //            command.CommandText = "SELECT * FROM ProductosDescuento WHERE now() BETWEEN Fecha_Inicio AND Fecha_Fin";
        //            using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
        //            {
        //                if (reader.HasRows)
        //                {
        //                    while (reader.Read())
        //                    {
        //                        ListProductoDesc.Add(new ProductoDesc()
        //                        {
        //                            CodDetalleDescuento = reader.GetString("CodDetalleDescuento"),
        //                            Esquema = reader.GetString("Esquema"),
        //                            ClaseCondicion = reader.GetString("ClaseCondicion"),
        //                            Posicion = Convert.ToInt32(reader.GetString("Posicion")),
        //                            Denominacion = reader.GetString("Denominacion"),
        //                            Tipo = reader.GetString("Tipo"),
        //                            TipoPM = reader.GetString("TipoPM"),
        //                            Activado = Convert.ToInt32(reader.GetString("Activado")),
        //                            Modificable = Convert.ToInt32(reader.GetString("Modificable"))
        //                        });
        //                    }
        //                }
        //            }
        //        }
        //        return ListDetDescuento;
        //    }
        //    catch (MySqlException e)
        //    {
        //        conexion_mysql_open.cerrar_conexion();
        //        List<string> Listmensajes = new List<string>();
        //        Listmensajes.Add(e.Message.ToString());
        //        return Listmensajes;
        //    }
        //}
    }
}
