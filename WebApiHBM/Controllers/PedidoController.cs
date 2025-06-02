using System;
using System.Collections.Generic;
using System.Web.Http;
using WebApiHBM.Models;
using Newtonsoft.Json;
using System.Data;
using System.Dynamic;
using SAP.Middleware.Connector;
using SAP;
using MySql.Data.MySqlClient;

namespace WebApiHBM.Controllers
{
    //Todo lo que tiene que ver con pedido.
    public class PedidoController : ApiController
    {
        ConectaSAP ConexionSAP;
        [HttpPost]
        public object RealizarPedido(HBM_Orders pedido)
        {
            ConexionSAP = new ConectaSAP();
            ConexionSAP.NombreConfiguracion = "CALIDAD";
            ConexionSAP.Servidor = "192.168.1.22";
            ConexionSAP.Usuario = "RFC";
            ConexionSAP.Contrasena = "abap2008";
            ConexionSAP.Mandante = "300";
            ConexionSAP.SistemaID = "QAS";
            ConexionSAP.Lenguaje = "ES";
            ConexionSAP.Instancia = "00";
            ConexionSAP.PoolSize = "5";

            
            string documento, mensaje_usuario, json;
            string MensajeSalida = string.Empty;
            string cabecera = pedido.cabecera.ToString();
            string detalle = pedido.detalle.ToString();
            string codcliente;
            documento = "0"; codcliente = ""; mensaje_usuario = "ERROR"; json = "";

            HBM_ped_cab deserializedCabecera = JsonConvert.DeserializeObject<HBM_ped_cab>(cabecera);
            var myobjList = JsonConvert.DeserializeObject<List<HBM_Pedido_det>>(detalle);

            using (conexion_mysql_open.obtener_conexion())
            {
                MySqlCommand mycomand = new MySqlCommand("select IdVendedor from Usuario where CodUsuario = ?usuario", conexion_mysql_open.obtener_conexion()); //@vendedor", conectar);
                mycomand.Parameters.AddWithValue("?usuario", deserializedCabecera.CodUsuario);
                MySqlDataAdapter sda = new MySqlDataAdapter(mycomand);
                conexion_mysql_open.cerrar_conexion();
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    codcliente = Convert.ToString(row["IdVendedor"]); 
                }

            }

            RfcDestination Destino = ConexionSAP.RegistrarConexion(ConexionSAP.NombreConfiguracion, out MensajeSalida);
            if (ConexionSAP.ProbarConexion(Destino, out MensajeSalida))
            {
                RfcRepository Repositorio = Destino.Repository;
                IRfcFunction Rfc = Repositorio.CreateFunction("ZSD_ALTA_PEDIDOS_VENTA5");
                IRfcTable Z_ENTRADA1 = Rfc.GetTable("Z_ENTRADA1");
                IRfcTable Z_ENTRADA2 = Rfc.GetTable("Z_ENTRADA2");
                IRfcTable Z_SALIDA = Rfc.GetTable("Z_SALIDA");
                IRfcTable Z_EDI = Rfc.GetTable("Z_EDI");
                IRfcTable Z_SALIDA2 = Rfc.GetTable("Z_SALIDA2");
                IRfcTable Z_CLIENTE = Rfc.GetTable("Z_CLIENTE");
                IRfcTable Z_CONTACTO = Rfc.GetTable("Z_CONTACTO");
                /* Con los datos Json se arman las Tablas Z_ENTRADA1 */
                Z_ENTRADA1.Append();
                Z_ENTRADA1.SetValue("zcabepos", deserializedCabecera.NroPedido);
                Z_ENTRADA1.SetValue("vkorg", deserializedCabecera.OrgVenta);
                Z_ENTRADA1.SetValue("vtWEG", deserializedCabecera.CodCanal);
                Z_ENTRADA1.SetValue("spart", deserializedCabecera.CodSector);
                Z_ENTRADA1.SetValue("auarT", deserializedCabecera.Fecha);
                Z_ENTRADA1.SetValue("kunnr", deserializedCabecera.kunnr.PadLeft(10, '0')); //deserializedCabecera.kunnr);
                Z_ENTRADA1.SetValue("kunnr_merc", deserializedCabecera.KunnrDest.PadLeft(10, '0')); //deserializedCabecera.KunnrDest);
                Z_ENTRADA1.SetValue("kunnr_fact", deserializedCabecera.KunnrFact.PadLeft(10, '0')); //deserializedCabecera.KunnrFact);
                Z_ENTRADA1.SetValue("audat", deserializedCabecera.Fecha);
                Z_ENTRADA1.SetValue("HORA_CREA", "000000"); // deserializedCabecera.HORA_CREA);
                Z_ENTRADA1.SetValue("bstkd", deserializedCabecera.NroPedido);
                Z_ENTRADA1.SetValue("xblnr", deserializedCabecera.NroPedido);
                Z_ENTRADA1.SetValue("EDATU_VBAK", "20180322"); //deserializedCabecera.EDATU_VBAK);
                Z_ENTRADA1.SetValue("kbetr", deserializedCabecera.MontoTotal);
                Z_ENTRADA1.SetValue("CONDPAGO", deserializedCabecera.CodTipoPago);
                Z_ENTRADA1.SetValue("ID_VENDEDOR", codcliente); //deserializedCabecera.CodUsuario);
                Z_ENTRADA1.SetValue("PUNTO_RECEP", deserializedCabecera.PUNTO_RECEP);
                Z_ENTRADA1.SetValue("FECHA_ENTREGA", deserializedCabecera.FECHA_ENTREGA);
                Z_ENTRADA1.SetValue("REOBS", deserializedCabecera.Obs);
                Z_ENTRADA1.SetValue("WAERS", deserializedCabecera.Moneda);
                Z_ENTRADA1.SetValue("CUSTOMERID", deserializedCabecera.PedidoID);
                /* Con los datos Json se arman las Tablas Z_ENTRADA2 */
                foreach (HBM_Pedido_det det_pedido in myobjList)
                {
                    //HBM_Pedido_det deserializedDetalle = JsonConvert.DeserializeObject<HBM_Pedido_det>(cabecera);
                    Z_ENTRADA2.Append();
                    Z_ENTRADA2.SetValue("ZCABEPOS", deserializedCabecera.NroPedido);
                    Z_ENTRADA2.SetValue("MATNR", det_pedido.ProductCode.PadLeft(18, '0')); //det_pedido.ProductCode);
                    Z_ENTRADA2.SetValue("DZMENG", det_pedido.Cantidad);
                    //Z_ENTRADA2.SetValue("KSCHL", det_pedido.KSCHL);
                    Z_ENTRADA2.SetValue("KBETR", det_pedido.Condicion1);
                    //Z_ENTRADA2.SetValue("KSCHL1", det_pedido.KSCHL1);
                    Z_ENTRADA2.SetValue("KBETR1", det_pedido.Condicion2);
                    //Z_ENTRADA2.SetValue("KSCHL2", det_pedido.KSCHL2);
                    Z_ENTRADA2.SetValue("KBETR2", det_pedido.Condicion3);
                    //Z_ENTRADA2.SetValue("KSCHL3", det_pedido.KSCHL3);
                    Z_ENTRADA2.SetValue("KBETR3", det_pedido.Condicion3);
                    Z_ENTRADA2.SetValue("VKGRP", codcliente); //det_pedido.CodUsuario);
                    Z_ENTRADA2.SetValue("CHARG", det_pedido.Lote);
                    Z_ENTRADA2.SetValue("LGORT", det_pedido.Almacen);
                    Z_ENTRADA2.SetValue("CORR_BONO", det_pedido.TipoBono);
                    Z_ENTRADA2.SetValue("PRIORIDAD", deserializedCabecera.CodPrioridadEntrega);
                    Z_ENTRADA2.SetValue("SPART", det_pedido.CodSector);
                }
                Rfc.Invoke(Destino);
                var ZSalida = Rfc.GetTable("Z_SALIDA2");
                ConexionSAP.CerrarConexionSAP(ConexionSAP.NombreConfiguracion);
                foreach (IRfcStructure item in ZSalida)
                {
                    documento = item.GetValue("DOCUMENTO").ToString();
                    mensaje_usuario = item.GetValue("MENSAJE").ToString();
                }

                List<string> zsalida = new List<string>();
                zsalida.Add(deserializedCabecera.PedidoID);
                zsalida.Add(documento);
                zsalida.Add(mensaje_usuario);
                dynamic myObject = new ExpandoObject();
                myObject.zsalida = zsalida;

                json = JsonConvert.SerializeObject(myObject);
            }
            ConexionSAP.CerrarConexionSAP(ConexionSAP.NombreConfiguracion);
            return json;
        }
        [HttpPost]
        public object ProductoStock(StockProd producto)
        {
            ConexionSAP = new ConectaSAP();
            ConexionSAP.NombreConfiguracion = "CALIDAD";
            ConexionSAP.Servidor = "192.168.1.22";
            ConexionSAP.Usuario = "RFC";
            ConexionSAP.Contrasena = "abap2008";
            ConexionSAP.Mandante = "300";
            ConexionSAP.SistemaID = "QAS";
            ConexionSAP.Lenguaje = "ES";
            ConexionSAP.Instancia = "00";
            ConexionSAP.PoolSize = "5";




            string cadena = producto.ProductCode.ToString().PadLeft(18, '0');

            string MensajeSalida = string.Empty;

            List<Stocks> Listock = new List<Stocks>();

            RfcDestination Destino = ConexionSAP.RegistrarConexion(ConexionSAP.NombreConfiguracion, out MensajeSalida);
            if (ConexionSAP.ProbarConexion(Destino, out MensajeSalida))
            {

                IRfcFunction rfcFunction = Destino.Repository.CreateFunction("ZHBM_STOCK_ALMACEN");
             
                rfcFunction.SetValue("PRODUCTOCODE", cadena);
                rfcFunction.Invoke(Destino);
                IRfcTable table = rfcFunction.GetTable("T_STOCK");
                   
                
                ConexionSAP.CerrarConexionSAP(ConexionSAP.NombreConfiguracion);
                foreach (IRfcStructure item in table)
                {
                  
                    int someInt = (int)Convert.ToDouble(item.GetValue("ITSTOCK").ToString());
                    Listock.Add(new Stocks()
                    {
                        ProductCode = item.GetValue("ITCODITEM").ToString().TrimStart('0'),
                        Almacen = item.GetValue("ITCODALM").ToString(),
                        Stock = someInt, 
                    });

                }

            }
            return Listock;

        }
    }
}
