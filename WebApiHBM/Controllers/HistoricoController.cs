using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Threading.Tasks;
using Historial.Models;
using MySql.Data.MySqlClient;
using System.Data;
using WebApiHBM.Models;
using WebApiHBM.Controllers;
using WebApiHBM.Conexiones;

namespace Historial.Controllers
{
    public class HistoricoController : ApiController
    {
        [HttpPost]
        public object ObtenerHistoricoPedido(Pedido pedido)
        {
            List<HBM_HistoricoPedido> ListHistorico = new List<HBM_HistoricoPedido>(); 
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select * from HistorialPedidos where PedidoID =  '" + pedido.PedidoID.ToString() + "'";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListHistorico.Add(new HBM_HistoricoPedido()
                                {
                                    HistoricoID = reader.GetString("HistoricoID"),
                                    Estado = Utils.SafeGetString(reader, "Estado"), //reader.GetString("Estado"),
                                    Fecha = Utils.SafeGetString(reader, "Fecha"), //reader.GetString("Fecha"),
                                    Documento = Utils.SafeGetString(reader, "Documento"), //reader.GetString("Documento"),
                                    NroSAP = Utils.SafeGetString(reader, "NroSAP"), //reader.GetString("NroSAP"),
                                    CodUsuario = Utils.SafeGetString(reader, "CodUsuario"), //reader.GetString("CodUsuario"),
                                    CodUsuarioSAP = Utils.SafeGetString(reader, "CodUsuarioSAP"), //reader.GetString("CodUsuarioSAP"),
                                    Obs = Utils.SafeGetString(reader, "Obs"), //reader.GetString("Obs"),
                                    PedidoID = Utils.SafeGetString(reader, "PedidoID"), //reader.GetString("PedidoID"),
                                    NroPedido = Utils.SafeGetString(reader, "NroPedido"), //reader.GetString("NroPedido"),
                                    Cliente = Utils.SafeGetString(reader, "Cliente"), // reader.GetString("Cliente"),
                                    Caso = Utils.SafeGetString(reader, "Caso")
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
        [HttpPost]
        public object ObtenerHistoricoSector(credenciales usuario)
        {
            //List<HBM_HistoricoSector> ListHistorico = new List<HBM_HistoricoSector>();
            //try
            //{
            //    var conn = conexion_mysql_open.obtener_conexion();
            //    using (var command = conn.CreateCommand())
            //    {
            //        command.CommandText = "select h.*,p.RazonSocial,p.CodUsuario,e.NombreEstado from Historico h,Pedido p,Estado e where h.PedidoID = p.PedidoID and h.EstadoID = e.EstadoID and h.CodUsuario = '" + usuario.usuario.ToString() + "'";
            //        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
            //        {
            //            if (reader.HasRows)
            //            {
            //                while (reader.Read())
            //                {
            //                    ListHistorico.Add(new HBM_HistoricoSector()
            //                    {
            //                        HistoricoID = reader.GetString("HistoricoID"),
            //                        Estado = Convert.ToInt32(reader.GetString("Estado")),
            //                        Fecha = reader.GetString("Fecha"),
            //                        Documento = Convert.ToInt32(reader.GetString("Documento")),
            //                        CodUsuario = reader.GetString("CodUsuario"),
            //                        Obs = reader.GetString("Obs"),
            //                        EstadoID = reader.GetString("EstadoID"),
            //                        NombreEstado = reader.GetString("NombreEstado"),
            //                        PedidoID = reader.GetString("PedidoID"),
            //                        RazonSocial = reader.GetString("RazonSocial")
            //                    });
            //                }
            //            }
            //        }
            //    }
            //    return ListHistorico;
            //}
            //catch (MySqlException ex)
            //{
            //    conexion_mysql_open.cerrar_conexion();
            //    List<string> ListaMensajes = new List<string>();
            //    ListaMensajes.Add(ex.Message.ToString());
            //    return ListaMensajes;
            //}
            return "Servicio en Mantenimiento";
        }
    }
}
