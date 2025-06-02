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
    public class CustomersController : ApiController
    {
        [HttpPost]
        public object ObtenerCustomers(credenciales usuario)
        {
            List<HBM_Customers> ListCustomers = new List<HBM_Customers>();
            string query = "", DivisionID = "", RegionalCBZ = "", DivisionCBZ = "", iddivision = "", Latitud = "", Longitud = "", UsuarioAmercado = "";
            UsuarioAmercado = usuarioAreaMercado(usuario.usuario.ToString());
            DateTime dateTime = new DateTime();
            dateTime = DateTime.Now;
            try
            {
                if (UsuarioAmercado == null || UsuarioAmercado == String.Empty)
                {
                    var conn = conexion_mysql_open.obtener_conexion();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = "select * from Usuario where CodUsuario = '" + usuario.usuario.ToString() + "'";
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    DivisionID = reader.GetString("DivisionID");
                                    RegionalCBZ = reader.GetString("RegionalCBZ");
                                    DivisionCBZ = reader.GetString("DivisionCBZ");
                                    iddivision = "1" + DivisionCBZ.Substring(1, 1) + "00";
                                }
                            }
                        }
                    }

                    if (DivisionID == "99") 
                    {
                        if ("00".Equals(DivisionCBZ) && !"00".Equals(RegionalCBZ))
                        {
                            query = ("select distinct KUNNR, NombreCliente, Direccion, Nit, Ciudad, Regional from Facturas where Regional = '" + RegionalCBZ + "' ");
                           // query = ("select distinct KUNNR, NombreCliente, Direccion, Nit, Ciudad, Regional from Facturas where Regional = '" + RegionalCBZ + "' and direccion  != ''");
                        }
                        if ("00".Equals(RegionalCBZ) && !"00".Equals(DivisionCBZ))
                        {
                            query = ("select distinct KUNNR, NombreCliente, Direccion, Nit, Ciudad, Regional from Facturas where Division = '" + DivisionCBZ + "' ");
                        }
                        if (!"00".Equals(DivisionCBZ) && !"00".Equals(RegionalCBZ))
                        {
                            query = ("select distinct KUNNR, NombreCliente, Direccion, Nit, Ciudad, Regional from Facturas where Division = '" + DivisionCBZ + "' and Regional = '" + RegionalCBZ + "'");
                        }
                        if ("00".Equals(DivisionCBZ) && "00".Equals(RegionalCBZ))
                        {
                            query = "select distinct KUNNR, NombreCliente, Direccion, Nit, Ciudad, Regional from Facturas ";
                        }

                        var con99 = conexion_mysql_open.obtener_conexion();
                        using (var command99 = con99.CreateCommand())
                        {
                            command99.CommandText = query;
                            using (var reader99 = command99.ExecuteReader(CommandBehavior.CloseConnection))
                            {
                                if (reader99.HasRows)
                                {
                                    while (reader99.Read())
                                    {
                                        //var conll = conexion_mysql_open.obtener_conexion();
                                        //using (var commandll = conll.CreateCommand())
                                        //{
                                        //    var clientes = reader99.GetString("KUNNR");
                                        //    Latitud = "0.0";
                                        //    Longitud = "0.0";
                                        //    commandll.CommandText = "select distinct C.KUNNR, C.Latitud, C.Longitud FROM Customers_Cobranzas C where C.KUNNR = '" + clientes + "'";
                                        //    using (var readerll = commandll.ExecuteReader(CommandBehavior.CloseConnection))
                                        //    {
                                        //        if (readerll.HasRows)
                                        //        {
                                        //            while (readerll.Read())
                                        //            {
                                        //                Latitud = readerll.GetString("Latitud");
                                        //                Longitud = readerll.GetString("Longitud");
                                        //            }
                                        //        }
                                        //    }
                                        //}

                                        Latitud = Utils.IsNullOrEmpty(Latitud);
                                        Longitud = Utils.IsNullOrEmpty(Longitud);
                                        ListCustomers.Add(new HBM_Customers()
                                        {
                                            KUNNR = Utils.SafeGetString(reader99, "KUNNR"),
                                            Zona = "",
                                            Avenida = "",
                                            Calle = "",
                                            Numero = "",
                                            LimiteCredito = 0,
                                            Nit = Utils.SafeGetString(reader99, "Nit"),
                                            CodRegimenTrib = "",
                                            CodTipoDocIden = "",
                                            Direccion = Utils.SafeGetString(reader99, "Direccion"),
                                            Tratamiento = "Señor",
                                            NombreCliente = Utils.SafeGetString(reader99, "NombreCliente"),
                                            Ciudad = Utils.SafeGetString(reader99, "Ciudad"),
                                            Email = "",
                                            Fax = "0",
                                            Latitud = Convert.ToDecimal(Latitud),
                                            Longitud = Convert.ToDecimal(Longitud),
                                            FechaModificacion = Convert.ToDateTime(dateTime).ToString("dd/MM/yyyy hh:mm:ss tt"),
                                            MontoFacturaBs = Convert.ToDecimal(0.00),
                                            Notes = "",
                                            Telefono = "0",
                                            Movil = "0",
                                            Observacion = "",
                                            Region = Utils.SafeGetString(reader99, "Regional"),
                                            Regional = "",
                                            SYNC = "1",
                                            TipoCliente = 1,
                                            Visible = 1,
                                            Web = "",
                                            ZonaTransporte = "",
                                        });
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        /*MySqlCommand cmd = new MySqlCommand("Get_Cliente", conexion_mysql_open.obtener_conexion());
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new MySqlParameter("CodUsuario", usuario.usuario));

                        // MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                         using (MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (dr.Read())
                            {
                                ListCustomers.Add(new HBM_Customers()
                                {
                                    KUNNR = Utils.SafeGetString(dr, "KUNNR"),
                                    Zona = Utils.SafeGetString(dr, "Zona"),
                                    Avenida = Utils.SafeGetString(dr, "Avenida"),
                                    Calle = Utils.SafeGetString(dr, "Calle"),
                                    Numero = Utils.SafeGetString(dr, "Numero"),
                                    LimiteCredito = Convert.ToDecimal(dr.GetString("LimiteCredito")),
                                    Nit = Utils.SafeGetString(dr, "Nit"),
                                    CodRegimenTrib = Utils.SafeGetString(dr, "CodRegimenTrib"),
                                    CodTipoDocIden = Utils.SafeGetString(dr, "CodTipoDocIden"),
                                    Direccion = Utils.SafeGetString(dr, "Direccion"),
                                    Tratamiento = Utils.SafeGetString(dr, "Tratamiento"),
                                    NombreCliente = Utils.SafeGetString(dr, "NombreCliente"),
                                    Ciudad = Utils.SafeGetString(dr, "Ciudad"),
                                    Email = Utils.SafeGetString(dr, "Email"),
                                    Fax = Utils.SafeGetString(dr, "Fax"),
                                    Latitud = Convert.ToDecimal(dr.GetString("Latitud")),
                                    Longitud = Convert.ToDecimal(dr.GetString("Longitud")),
                                    FechaModificacion = Utils.SafeGetString(dr, "FechaModificacion"),
                                    MontoFacturaBs = Convert.ToDecimal(dr.GetString("MontoFacturaBs")),
                                    Notes = Utils.SafeGetString(dr, "Notes"),
                                    Telefono = Utils.SafeGetString(dr, "Telefono"),
                                    Movil = Utils.SafeGetString(dr, "Movil"),
                                    Observacion = Utils.SafeGetString(dr, "Observacion"),
                                    Region = Utils.SafeGetString(dr, "Region"),
                                    Regional = Utils.SafeGetString(dr, "Regional"),
                                    SYNC = Utils.SafeGetString(dr, "SYNC"),
                                    TipoCliente = Convert.ToInt32(dr.GetString("TipoCliente")),
                                    Visible = Convert.ToInt32(dr.GetString("Visible")),
                                    Web = Utils.SafeGetString(dr, "Web"),
                                    ZonaTransporte = Utils.SafeGetString(dr, "ZonaTransporte"),
                                    CodClasificador = Convert.ToInt32(dr.GetString("CodClasificador"))
                                });
                            }
                        }*/

                        var con = conexion_mysql_open.obtener_conexion();
                        using (var commands = con.CreateCommand())
                        {
                            commands.CommandText = "select DISTINCT C.* from Customers C inner join CustomersSector CS ON CS.KUNNR = C.KUNNR WHERE CS.CodUsuario = '" + usuario.usuario.ToString() + "' and CS.Visible = '1'";
                            using (var readers = commands.ExecuteReader(CommandBehavior.CloseConnection))
                            {
                                if (readers.HasRows)
                                {
                                    while (readers.Read())
                                    {
                                        ListCustomers.Add(new HBM_Customers()
                                        {
                                            KUNNR = Utils.SafeGetString(readers, "KUNNR"),
                                            Zona = Utils.SafeGetString(readers, "Zona"),
                                            Avenida = Utils.SafeGetString(readers, "Avenida"),
                                            Calle = Utils.SafeGetString(readers, "Calle"),
                                            Numero = Utils.SafeGetString(readers, "Numero"),
                                            LimiteCredito = Convert.ToDecimal(readers.GetString("LimiteCredito")),
                                            Nit = Utils.SafeGetString(readers, "Nit"),
                                            CodRegimenTrib = Utils.SafeGetString(readers, "CodRegimenTrib"),
                                            CodTipoDocIden = Utils.SafeGetString(readers, "CodTipoDocIden"),
                                            Direccion = Utils.SafeGetString(readers, "Direccion"),
                                            Tratamiento = Utils.SafeGetString(readers, "Tratamiento"),
                                            NombreCliente = Utils.SafeGetString(readers, "NombreCliente"),
                                            Ciudad = Utils.SafeGetString(readers, "Ciudad"),
                                            Email = Utils.SafeGetString(readers, "Email"),
                                            Fax = Utils.SafeGetString(readers, "Fax"),
                                            Latitud = Convert.ToDecimal(readers.GetString("Latitud")),
                                            Longitud = Convert.ToDecimal(readers.GetString("Longitud")),
                                            FechaModificacion = Utils.SafeGetString(readers, "FechaModificacion"),
                                            MontoFacturaBs = Convert.ToDecimal(readers.GetString("MontoFacturaBs")),
                                            Notes = Utils.SafeGetString(readers, "Notes"),
                                            Telefono = Utils.SafeGetString(readers, "Telefono"),
                                            Movil = Utils.SafeGetString(readers, "Movil"),
                                            Observacion = Utils.SafeGetString(readers, "Observacion"),
                                            Region = Utils.SafeGetString(readers, "Region"),
                                            Regional = Utils.SafeGetString(readers, "Regional"),
                                            SYNC = Utils.SafeGetString(readers, "SYNC"),
                                            TipoCliente = Convert.ToInt32(readers.GetString("TipoCliente")),
                                            Visible = Convert.ToInt32(readers.GetString("Visible")),
                                            Web = Utils.SafeGetString(readers, "Web"),
                                            ZonaTransporte = Utils.SafeGetString(readers, "ZonaTransporte")
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    var con = conexion_mysql_open.obtener_conexion();
                    using (var commands = con.CreateCommand())
                    {
                        commands.CommandText = "select DISTINCT C.* from Customers C inner join CustomersSector CS ON CS.KUNNR = C.KUNNR " +
                            " inner join UsuarioSector US ON CS.CodSector = US.CodSector and CS.CodCanal =  US.CodCanal " +
                            " WHERE C.ZonaMercado IN (" + UsuarioAmercado + ") and US.CodUsuario = '" + usuario.usuario.ToString() + "' and CS.Visible = '1';";

                        using (var readers = commands.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (readers.HasRows)
                            {
                                while (readers.Read())
                                {
                                    ListCustomers.Add(new HBM_Customers()
                                    {
                                        KUNNR = Utils.SafeGetString(readers, "KUNNR"),
                                        Zona = Utils.SafeGetString(readers, "Zona"),
                                        Avenida = Utils.SafeGetString(readers, "Avenida"),
                                        Calle = Utils.SafeGetString(readers, "Calle"),
                                        Numero = Utils.SafeGetString(readers, "Numero"),
                                        LimiteCredito = Convert.ToDecimal(readers.GetString("LimiteCredito")),
                                        Nit = Utils.SafeGetString(readers, "Nit"),
                                        CodRegimenTrib = Utils.SafeGetString(readers, "CodRegimenTrib"),
                                        CodTipoDocIden = Utils.SafeGetString(readers, "CodTipoDocIden"),
                                        Direccion = Utils.SafeGetString(readers, "Direccion"),
                                        Tratamiento = Utils.SafeGetString(readers, "Tratamiento"),
                                        NombreCliente = Utils.SafeGetString(readers, "NombreCliente"),
                                        Ciudad = Utils.SafeGetString(readers, "Ciudad"),
                                        Email = Utils.SafeGetString(readers, "Email"),
                                        Fax = Utils.SafeGetString(readers, "Fax"),
                                        Latitud = Convert.ToDecimal(readers.GetString("Latitud")),
                                        Longitud = Convert.ToDecimal(readers.GetString("Longitud")),
                                        FechaModificacion = Utils.SafeGetString(readers, "FechaModificacion"),
                                        MontoFacturaBs = Convert.ToDecimal(readers.GetString("MontoFacturaBs")),
                                        Notes = Utils.SafeGetString(readers, "Notes"),
                                        Telefono = Utils.SafeGetString(readers, "Telefono"),
                                        Movil = Utils.SafeGetString(readers, "Movil"),
                                        Observacion = Utils.SafeGetString(readers, "Observacion"),
                                        Region = Utils.SafeGetString(readers, "Region"),
                                        Regional = Utils.SafeGetString(readers, "Regional"),
                                        SYNC = Utils.SafeGetString(readers, "SYNC"),
                                        TipoCliente = Convert.ToInt32(readers.GetString("TipoCliente")),
                                        Visible = Convert.ToInt32(readers.GetString("Visible")),
                                        Web = Utils.SafeGetString(readers, "Web"),
                                        ZonaTransporte = Utils.SafeGetString(readers, "ZonaTransporte"),
                                        CodClasificador = Convert.ToInt32(readers.GetString("CodClasificador"))
                                    });
                                }
                            }

                        }
                    }

                }
            }
            catch (MySqlException ex)
            {
                conexion_mysql_open.cerrar_conexion();
                List<string> Listmensajes = new List<string>();
                Listmensajes.Add(ex.Message.ToString());
                return Listmensajes;
            }
            return ListCustomers;
        }
        [HttpPost]
        public object ObtenerCustomers02(credenciales usuario)
        {
            string fecha = usuario.fecha.ToString(); string UsuarioAmercado = "";

            DataTable dt = new DataTable();
            DataTable dtt = new DataTable();
            ConexionesController.Eliminar_Conexiones();
            List<HBM_Customers> ListCustomers = new List<HBM_Customers>();
            string vendedor = usuario.usuario.ToString();
            string query = "", DivisionID = "", RegionalCBZ = "", DivisionCBZ = "", iddivision = "", Latitud = "", Longitud = "";

            DateTime dateTime = new DateTime();
            dateTime = DateTime.Now;

             UsuarioAmercado = usuarioAreaMercado(usuario.usuario.ToString());
            try
            {
                if (UsuarioAmercado == null || UsuarioAmercado == String.Empty)
                {
                    var conn = conexion_mysql_open.obtener_conexion();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = "select * from Usuario where CodUsuario = '" + usuario.usuario.ToString() + "'";
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    DivisionID = reader.GetString("DivisionID");
                                    RegionalCBZ = reader.GetString("RegionalCBZ");
                                    DivisionCBZ = reader.GetString("DivisionCBZ");
                                    iddivision = "1" + DivisionCBZ.Substring(1, 1) + "00";
                                }
                            }
                        }
                    }


                    if (DivisionID == "99")
                    {
                        if ("00".Equals(DivisionCBZ) && !"00".Equals(RegionalCBZ))
                        {
                            query = ("select distinct KUNNR, NombreCliente, Direccion, Nit, Ciudad, Regional from Facturas where Regional = '" + RegionalCBZ + "'");
                        }
                        if ("00".Equals(RegionalCBZ) && !"00".Equals(DivisionCBZ))
                        {
                            query = ("select distinct KUNNR, NombreCliente, Direccion, Nit, Ciudad, Regional from Facturas where Division = '" + DivisionCBZ + "' ");
                        }
                        if (!"00".Equals(DivisionCBZ) && !"00".Equals(RegionalCBZ))
                        {
                            query = ("select distinct KUNNR, NombreCliente, Direccion, Nit, Ciudad, Regional from Facturas where Division = '" + DivisionCBZ + "' and Regional = '" + RegionalCBZ + "'");
                        }
                        if ("00".Equals(DivisionCBZ) && "00".Equals(RegionalCBZ))
                        {
                            query = "select distinct KUNNR, NombreCliente, Direccion, Nit, Ciudad, Regional from Facturas";
                        }
                        if ("00".Equals(DivisionCBZ) && "00".Equals(RegionalCBZ) && usuario.fecha.ToString() != "1900-01-01 00:00:00")
                        {
                            query = query + " WHERE FechaModificacion >= '" + usuario.fecha.ToString() + "' ";
                        }
                        if (usuario.fecha.ToString() != "1900-01-01 00:00:00")
                        {
                            query = query + " AND FechaModificacion >= '" + usuario.fecha.ToString() + "'";
                        }
                        var con99 = conexion_mysql_open.obtener_conexion();
                        using (var command99 = con99.CreateCommand())
                        {
                            command99.CommandText = query;
                            using (var reader99 = command99.ExecuteReader(CommandBehavior.CloseConnection))
                            {
                                if (reader99.HasRows)
                                {
                                    while (reader99.Read())
                                    {
                                        //var conll = conexion_mysql_open.obtener_conexion();
                                        //using (var commandll = conll.CreateCommand())
                                        //{
                                        //    var clientes = reader99.GetString("KUNNR");
                                        //    Latitud = "0.0";
                                        //    Longitud = "0.0";

                                        //    commandll.CommandText = "select distinct C.KUNNR, C.Latitud, C.Longitud FROM Customers_Cobranzas C where C.KUNNR = '" + clientes + "'";
                                        //    using (var readerll = commandll.ExecuteReader(CommandBehavior.CloseConnection))
                                        //    {
                                        //        if (readerll.HasRows)
                                        //        {
                                        //            while (readerll.Read())
                                        //            {
                                        //                Latitud = readerll.GetString("Latitud");
                                        //                Longitud = readerll.GetString("Longitud");

                                        //            }
                                        //        }
                                        //    }
                                        //}

                                        Latitud = Utils.IsNullOrEmpty(Latitud);
                                        Longitud = Utils.IsNullOrEmpty(Longitud);
                                        ListCustomers.Add(new HBM_Customers()
                                        {
                                            KUNNR = Utils.SafeGetString(reader99, "KUNNR"),
                                            Zona = "",
                                            Avenida = "",
                                            Calle = "",
                                            Numero = "",
                                            LimiteCredito = 0,
                                            Nit = Utils.SafeGetString(reader99, "Nit"),
                                            CodRegimenTrib = "",
                                            CodTipoDocIden = "",
                                            Direccion = Utils.SafeGetString(reader99, "Direccion"),
                                            Tratamiento = "Señor",
                                            NombreCliente = Utils.SafeGetString(reader99, "NombreCliente"),
                                            Ciudad = Utils.SafeGetString(reader99, "Ciudad"),
                                            Email = "",
                                            Fax = "0",
                                            Latitud = Convert.ToDecimal(Latitud),
                                            Longitud = Convert.ToDecimal(Longitud),
                                            FechaModificacion = Convert.ToDateTime(dateTime).ToString("dd/MM/yyyy hh:mm:ss tt"),
                                            MontoFacturaBs = Convert.ToDecimal(0.00),
                                            Notes = "",
                                            Telefono = "0",
                                            Movil = "0",
                                            Observacion = "",
                                            Region = Utils.SafeGetString(reader99, "Regional"),
                                            Regional = "",
                                            SYNC = "1",
                                            TipoCliente = 1,
                                            Visible = 1,
                                            Web = "",
                                            ZonaTransporte = "",
                                            CodClasificador = 0
                                        });
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                         var con = conexion_mysql_open.obtener_conexion();
                        using (var commands = con.CreateCommand())
                        {
                            commands.CommandText = "select DISTINCT C.* from Customers C inner join CustomersSector CS ON CS.KUNNR = C.KUNNR WHERE CS.CodUsuario = '" + usuario.usuario.ToString() + "' and CS.Visible = '1'";
                            if (usuario.fecha.ToString() != "1900-01-01 00:00:00")
                            {
                                commands.CommandText = commands.CommandText + " AND C.FechaModificacion >= '" + usuario.fecha.ToString() + "'";
                            }

                            using (var readers = commands.ExecuteReader(CommandBehavior.CloseConnection))
                            {
                                if (readers.HasRows)
                                {
                                    while (readers.Read())
                                    {
                                        ListCustomers.Add(new HBM_Customers()
                                        {
                                            KUNNR = Utils.SafeGetString(readers, "KUNNR"),
                                            Zona = Utils.SafeGetString(readers, "Zona"),
                                            Avenida = Utils.SafeGetString(readers, "Avenida"),
                                            Calle = Utils.SafeGetString(readers, "Calle"),
                                            Numero = Utils.SafeGetString(readers, "Numero"),
                                            LimiteCredito = Convert.ToDecimal(readers.GetString("LimiteCredito")),
                                            Nit = Utils.SafeGetString(readers, "Nit"),
                                            CodRegimenTrib = Utils.SafeGetString(readers, "CodRegimenTrib"),
                                            CodTipoDocIden = Utils.SafeGetString(readers, "CodTipoDocIden"),
                                            Direccion = Utils.SafeGetString(readers, "Direccion"),
                                            Tratamiento = Utils.SafeGetString(readers, "Tratamiento"),
                                            NombreCliente = Utils.SafeGetString(readers, "NombreCliente"),
                                            Ciudad = Utils.SafeGetString(readers, "Ciudad"),
                                            Email = Utils.SafeGetString(readers, "Email"),
                                            Fax = Utils.SafeGetString(readers, "Fax"),
                                            Latitud = Convert.ToDecimal(readers.GetString("Latitud")),
                                            Longitud = Convert.ToDecimal(readers.GetString("Longitud")),
                                            FechaModificacion = Utils.SafeGetString(readers, "FechaModificacion"),
                                            MontoFacturaBs = Convert.ToDecimal(readers.GetString("MontoFacturaBs")),
                                            Notes = Utils.SafeGetString(readers, "Notes"),
                                            Telefono = Utils.SafeGetString(readers, "Telefono"),
                                            Movil = Utils.SafeGetString(readers, "Movil"),
                                            Observacion = Utils.SafeGetString(readers, "Observacion"),
                                            Region = Utils.SafeGetString(readers, "Region"),
                                            Regional = Utils.SafeGetString(readers, "Regional"),
                                            SYNC = Utils.SafeGetString(readers, "SYNC"),
                                            TipoCliente = Convert.ToInt32(readers.GetString("TipoCliente")),
                                            Visible = Convert.ToInt32(readers.GetString("Visible")),
                                            Web = Utils.SafeGetString(readers, "Web"),
                                            ZonaTransporte = Utils.SafeGetString(readers, "ZonaTransporte"),
                                            CodClasificador = Convert.ToInt32(readers.GetString("CodClasificador")),
                                            nitValido = Convert.ToInt32(readers.GetString("nitValido"))
                                        });
                                    }
                                }

                            }
                        }
                    }
                }
                else
                {
                    var con = conexion_mysql_open.obtener_conexion();
                    using (var commands = con.CreateCommand())
                    {
                        commands.CommandText = "select DISTINCT C.* from Customers C inner join CustomersSector CS ON CS.KUNNR = C.KUNNR " +
                            " inner join UsuarioSector US ON CS.CodSector = US.CodSector and CS.CodCanal =  US.CodCanal " +
                            " WHERE C.ZonaMercado IN (" + UsuarioAmercado + ") and US.CodUsuario = '" + usuario.usuario.ToString() + "' and CS.Visible = '1';";

                        using (var readers = commands.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (readers.HasRows)
                            {
                                while (readers.Read())
                                {
                                    ListCustomers.Add(new HBM_Customers()
                                    {
                                        KUNNR = Utils.SafeGetString(readers, "KUNNR"),
                                        Zona = Utils.SafeGetString(readers, "Zona"),
                                        Avenida = Utils.SafeGetString(readers, "Avenida"),
                                        Calle = Utils.SafeGetString(readers, "Calle"),
                                        Numero = Utils.SafeGetString(readers, "Numero"),
                                        LimiteCredito = Convert.ToDecimal(readers.GetString("LimiteCredito")),
                                        Nit = Utils.SafeGetString(readers, "Nit"),
                                        CodRegimenTrib = Utils.SafeGetString(readers, "CodRegimenTrib"),
                                        CodTipoDocIden = Utils.SafeGetString(readers, "CodTipoDocIden"),
                                        Direccion = Utils.SafeGetString(readers, "Direccion"),
                                        Tratamiento = Utils.SafeGetString(readers, "Tratamiento"),
                                        NombreCliente = Utils.SafeGetString(readers, "NombreCliente"),
                                        Ciudad = Utils.SafeGetString(readers, "Ciudad"),
                                        Email = Utils.SafeGetString(readers, "Email"),
                                        Fax = Utils.SafeGetString(readers, "Fax"),
                                        Latitud = Convert.ToDecimal(readers.GetString("Latitud")),
                                        Longitud = Convert.ToDecimal(readers.GetString("Longitud")),
                                        FechaModificacion = Utils.SafeGetString(readers, "FechaModificacion"),
                                        MontoFacturaBs = Convert.ToDecimal(readers.GetString("MontoFacturaBs")),
                                        Notes = Utils.SafeGetString(readers, "Notes"),
                                        Telefono = Utils.SafeGetString(readers, "Telefono"),
                                        Movil = Utils.SafeGetString(readers, "Movil"),
                                        Observacion = Utils.SafeGetString(readers, "Observacion"),
                                        Region = Utils.SafeGetString(readers, "Region"),
                                        Regional = Utils.SafeGetString(readers, "Regional"),
                                        SYNC = Utils.SafeGetString(readers, "SYNC"),
                                        TipoCliente = Convert.ToInt32(readers.GetString("TipoCliente")),
                                        Visible = Convert.ToInt32(readers.GetString("Visible")),
                                        Web = Utils.SafeGetString(readers, "Web"),
                                        ZonaTransporte = Utils.SafeGetString(readers, "ZonaTransporte"),
                                        CodClasificador = Convert.ToInt32(readers.GetString("CodClasificador")),
                                        nitValido = Convert.ToInt32(readers.GetString("nitValido"))
                                    });
                                }
                            }

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                conexion_mysql_open.cerrar_conexion();
            }
            return Tuple.Create(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), ListCustomers);
        }
        [HttpPost]
        public object ObtenerCustomersSector(credenciales usuario)
        {
            string cuenta_asc = "", UsuarioAmercado = "";
            List<HBM_CustomersSector> ListCustomersSector = new List<HBM_CustomersSector>();
            UsuarioAmercado = usuarioAreaMercado(usuario.usuario.ToString());

            try
            {
                if (UsuarioAmercado == null || UsuarioAmercado == String.Empty)
                {
                    var conn = conexion_mysql_open.obtener_conexion();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = "select DISTINCT CS.*, IF(p.CodVendedor is null, '0', '1') as Inventario from Customers C inner join CustomersSector CS ON CS.KUNNR = C.KUNNR left join PlanInventario p on CS.KUNNR = p.KUNNR  and CS.CodCanal = p.CodCanal and CS.CodUsuario = p.CodUsuario WHERE CS.CodUsuario = '" + usuario.usuario.ToString() + "' and CS.Visible = '1' ";
                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cuenta_asc = "";
                                    if (reader.GetString("OrgVenta") == "1200" && reader.GetString("Cuenta_Asc") != "")
                                    {
                                        if (reader.GetString("Cuenta_Asc").Substring(0, 1) == "2")
                                        {
                                            cuenta_asc = reader.GetString("Cuenta_Asc");
                                        }
                                    }
                                    if (reader.GetString("OrgVenta") != "1200")
                                    {
                                        cuenta_asc = reader.GetString("Cuenta_Asc");
                                    }
                                    ListCustomersSector.Add(new HBM_CustomersSector()
                                    {
                                        CustomersSectorID = reader.GetString("CustomersSectorID"),
                                        KUNNR = reader.GetString("KUNNR"),
                                        OrgVenta = reader.GetString("OrgVenta"),
                                        CodCanal = reader.GetString("CodCanal"),
                                        CodSector = reader.GetString("CodSector"),
                                        CodRamo = reader.GetString("CodRamo"),
                                        CodSubRamo = reader.GetString("CodSubRamo"),
                                        OficinaVenta = reader.GetString("OficinaVenta"),
                                        Clasificacion = reader.GetString("Clasificacion"),
                                        DescBase = Convert.ToDecimal(reader.GetString("DescBase")),
                                        GrupoCliente = reader.GetString("GrupoCliente"),
                                        GrupoPrecio = reader.GetString("GrupoPrecio"),
                                        CondicionPago = reader.GetString("CondicionPago"),
                                        TomaInventario = reader.GetString("Inventario"),
                                        CodUsuario = reader.GetString("CodUsuario"),
                                        FechaModificacion = reader.GetString("FechaModificacion"),
                                        Visible = reader.GetString("Visible"),
                                        Cuenta_Asc = cuenta_asc  //reader.GetString("Cuenta_Asc")
                                    });
                                }
                            }
                        }
                    }
                    return ListCustomersSector;
                }
                else
                {
                    var conn = conexion_mysql_open.obtener_conexion();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = "select CS.*, IF(p.CodVendedor is null, '0', '1') as Inventario from Customers C " +
                           " inner join CustomersSector CS ON CS.KUNNR = C.KUNNR " +
                           " left join PlanInventario p on CS.KUNNR = p.KUNNR  and CS.CodCanal = p.CodCanal and CS.CodUsuario = p.CodUsuario " +
                           " inner join UsuarioSector US ON CS.CodSector = US.CodSector and CS.CodCanal = US.CodCanal " +
                           " WHERE C.ZonaMercado IN (" + UsuarioAmercado + ") AND US.CodUsuario = '" + usuario.usuario.ToString() + "' AND CS.Visible = '1'; ";

                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cuenta_asc = "";
                                    if (reader.GetString("OrgVenta") == "1200" && reader.GetString("Cuenta_Asc") != "")
                                    {
                                        if (reader.GetString("Cuenta_Asc").Substring(0, 1) == "2")
                                        {
                                            cuenta_asc = reader.GetString("Cuenta_Asc");
                                        }
                                    }
                                    if (reader.GetString("OrgVenta") != "1200")
                                    {
                                        cuenta_asc = reader.GetString("Cuenta_Asc");
                                    }
                                    ListCustomersSector.Add(new HBM_CustomersSector()
                                    {
                                        CustomersSectorID = reader.GetString("CustomersSectorID"),
                                        KUNNR = reader.GetString("KUNNR"),
                                        OrgVenta = reader.GetString("OrgVenta"),
                                        CodCanal = reader.GetString("CodCanal"),
                                        CodSector = reader.GetString("CodSector"),
                                        CodRamo = reader.GetString("CodRamo"),
                                        CodSubRamo = reader.GetString("CodSubRamo"),
                                        OficinaVenta = reader.GetString("OficinaVenta"),
                                        Clasificacion = reader.GetString("Clasificacion"),
                                        DescBase = Convert.ToDecimal(reader.GetString("DescBase")),
                                        GrupoCliente = reader.GetString("GrupoCliente"),
                                        GrupoPrecio = reader.GetString("GrupoPrecio"),
                                        CondicionPago = reader.GetString("CondicionPago"),
                                        TomaInventario = reader.GetString("Inventario"),
                                        CodUsuario = usuario.usuario.ToString(), //reader.GetString("CodUsuario"),
                                        FechaModificacion = reader.GetString("FechaModificacion"),
                                        Visible = reader.GetString("Visible"),
                                        Cuenta_Asc = cuenta_asc  //reader.GetString("Cuenta_Asc")
                                    });
                                }
                            }
                        }
                    }
                    return ListCustomersSector;
                }
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
        public object ObtenerCustomersSector02(credenciales usuario)
        {
            List<HBM_CustomersSector> ListCustomersSector = new List<HBM_CustomersSector>();
            string query = "";
            string cuenta_asc = "", UsuarioAmercado = "";
            UsuarioAmercado = usuarioAreaMercado(usuario.usuario.ToString());
            try
            {
                if (UsuarioAmercado == null || UsuarioAmercado == String.Empty)
                {
                    var conn = conexion_mysql_open.obtener_conexion();
                    using (var command = conn.CreateCommand())
                    {
                        if (usuario.fecha.ToString() == "1900-01-01 00:00:00")
                        {
                            //query = "select DISTINCT CS.*, IF(p.CodVendedor is null, '0', '1') as Inventario from Customers C inner join CustomersSector CS ON CS.KUNNR = C.KUNNR left join PlanInventario p on CS.KUNNR = p.KUNNR  and CS.CodCanal = p.CodCanal and CS.CodUsuario = p.CodUsuario WHERE CS.CodUsuario = '" + usuario.usuario.ToString() + "' and CS.Visible = '1'";
                            query = "select DISTINCT  CS.*, IF(p.CodVendedor is null, '0', '1') as Inventario from Customers C  inner join CustomersSector CS ON CS.KUNNR = C.KUNNR left join (select DISTINCT KUNNR, CodUsuario,CodCanal,CodVendedor from PlanInventario ) as p on CS.KUNNR = p.KUNNR  and CS.CodCanal = p.CodCanal and CS.CodUsuario = p.CodUsuario WHERE CS.CodUsuario = '" + usuario.usuario.ToString() + "' and CS.Visible = '1'";
                        }
                        else
                        {
                            query = "select DISTINCT  CS.*, IF(p.CodVendedor is null, '0', '1') as Inventario from Customers C  inner join CustomersSector CS ON CS.KUNNR = C.KUNNR left join (select DISTINCT KUNNR, CodUsuario,CodCanal,CodVendedor from PlanInventario ) as p on CS.KUNNR = p.KUNNR  and CS.CodCanal = p.CodCanal and CS.CodUsuario = p.CodUsuario WHERE CS.CodUsuario = '" + usuario.usuario.ToString() + "' and CS.Visible = '1' AND CS.FechaModificacion >= '" + usuario.fecha.ToString() + "'";
                            //query = "select DISTINCT CS.*, IF(p.CodVendedor is null, '0', '1') as Inventario from Customers C inner join CustomersSector CS ON CS.KUNNR = C.KUNNR left join PlanInventario p on CS.KUNNR = p.KUNNR  and CS.CodCanal = p.CodCanal and CS.CodUsuario = p.CodUsuario WHERE CS.CodUsuario = '" + usuario.usuario.ToString() + "' and CS.Visible = '1' AND CS.FechaModificacion >= '" + usuario.fecha.ToString() + "'";
                        }
                        command.CommandText = query;


                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cuenta_asc = "";
                                    if (reader.GetString("OrgVenta") == "1200" && reader.GetString("Cuenta_Asc") != "")
                                    {
                                        if (reader.GetString("Cuenta_Asc").Substring(0, 1) == "2")
                                        {
                                            cuenta_asc = reader.GetString("Cuenta_Asc");
                                        }
                                    }
                                    if (reader.GetString("OrgVenta") != "1200")
                                    {
                                        cuenta_asc = reader.GetString("Cuenta_Asc");
                                    }
                                    string clien = reader.GetString("KUNNR").ToString();
                                    string vs = reader.GetInt32("Inventario").ToString();

                                    ListCustomersSector.Add(new HBM_CustomersSector()
                                    {
                                        CustomersSectorID = reader.GetString("CustomersSectorID"),
                                        KUNNR = reader.GetString("KUNNR"),
                                        OrgVenta = reader.GetString("OrgVenta"),
                                        CodCanal = reader.GetString("CodCanal"),
                                        CodSector = reader.GetString("CodSector"),
                                        CodRamo = reader.GetString("CodRamo"),
                                        CodSubRamo = reader.GetString("CodSubRamo"),
                                        OficinaVenta = reader.GetString("OficinaVenta"),
                                        Clasificacion = reader.GetString("Clasificacion"),
                                        DescBase = Convert.ToDecimal(reader.GetString("DescBase")),
                                        GrupoCliente = reader.GetString("GrupoCliente"),
                                        GrupoPrecio = reader.GetString("GrupoPrecio"),
                                        CondicionPago = reader.GetString("CondicionPago"),
                                        TomaInventario = reader.GetString("Inventario"),
                                        CodUsuario = reader.GetString("CodUsuario"),
                                        FechaModificacion = reader.GetString("FechaModificacion"),
                                        Visible = reader.GetString("Visible"),
                                        Cuenta_Asc = cuenta_asc  //reader.GetString("Cuenta_Asc")
                                    });
                                }
                            }
                        }
                    }
                    return Tuple.Create(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), ListCustomersSector);
                }
                else
                {
                    var conn = conexion_mysql_open.obtener_conexion();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = "select CS.*, IF(p.CodVendedor is null, '0', '1') as Inventario from Customers C " +
                           " inner join CustomersSector CS ON CS.KUNNR = C.KUNNR " +
                           " left join PlanInventario p on CS.KUNNR = p.KUNNR  and CS.CodCanal = p.CodCanal and CS.CodUsuario = p.CodUsuario " +
                           " inner join UsuarioSector US ON CS.CodSector = US.CodSector and CS.CodCanal = US.CodCanal " +
                           " WHERE C.ZonaMercado IN (" + UsuarioAmercado + ") AND US.CodUsuario = '" + usuario.usuario.ToString() + "' AND CS.Visible = '1'; ";

                        using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    cuenta_asc = "";
                                    if (reader.GetString("OrgVenta") == "1200" && reader.GetString("Cuenta_Asc") != "")
                                    {
                                        if (reader.GetString("Cuenta_Asc").Substring(0, 1) == "2")
                                        {
                                            cuenta_asc = reader.GetString("Cuenta_Asc");
                                        }
                                    }
                                    if (reader.GetString("OrgVenta") != "1200")
                                    {
                                        cuenta_asc = reader.GetString("Cuenta_Asc");
                                    }
                                    ListCustomersSector.Add(new HBM_CustomersSector()
                                    {
                                        CustomersSectorID = reader.GetString("CustomersSectorID"),
                                        KUNNR = reader.GetString("KUNNR"),
                                        OrgVenta = reader.GetString("OrgVenta"),
                                        CodCanal = reader.GetString("CodCanal"),
                                        CodSector = reader.GetString("CodSector"),
                                        CodRamo = reader.GetString("CodRamo"),
                                        CodSubRamo = reader.GetString("CodSubRamo"),
                                        OficinaVenta = reader.GetString("OficinaVenta"),
                                        Clasificacion = reader.GetString("Clasificacion"),
                                        DescBase = Convert.ToDecimal(reader.GetString("DescBase")),
                                        GrupoCliente = reader.GetString("GrupoCliente"),
                                        GrupoPrecio = reader.GetString("GrupoPrecio"),
                                        CondicionPago = reader.GetString("CondicionPago"),
                                        TomaInventario = reader.GetString("Inventario"),
                                        CodUsuario = usuario.usuario.ToString(), //reader.GetString("CodUsuario"),
                                        FechaModificacion = reader.GetString("FechaModificacion"),
                                        Visible = reader.GetString("Visible"),
                                        Cuenta_Asc = cuenta_asc  //reader.GetString("Cuenta_Asc")
                                    });
                                }
                            }
                        }
                    }
                    return Tuple.Create(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), ListCustomersSector);
                }
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
        public object ObtenerContactos(credenciales usuario)
        {
            List<HBM_Contacto> ListContactos = new List<HBM_Contacto>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select distinct C.* from Contactos C INNER JOIN CustomersSector CS ON CS.KUNNR = C.KUNNR WHERE CS.CodUsuario = '" + usuario.usuario.ToString() + "' and CS.Visible = '1';";

                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListContactos.Add(new HBM_Contacto()
                                {
                                    ContactoID = reader.GetString("ContactoID"),
                                    KUNNR = Utils.SafeGetString(reader, "KUNNR"),
                                    CodContacto = Convert.ToInt32(reader.GetString("CodContacto")),
                                    Nombre = Utils.SafeGetString(reader, "Nombre"),
                                    Apellido = Utils.SafeGetString(reader, "Apellido"),
                                    CI = Utils.SafeGetString(reader, "CI"),
                                    Celular = Utils.SafeGetString(reader, "Celular"),
                                    Telefono = Utils.SafeGetString(reader, "Telefono"),
                                    Email = Utils.SafeGetString(reader, "Email"),
                                    Cargo = Utils.SafeGetString(reader, "Cargo"),
                                    CodDepartamento = Utils.SafeGetString(reader, "CodDepartamento"),
                                    CodFuncion = Utils.SafeGetString(reader, "CodFuncion"),
                                    Comentario = Utils.SafeGetString(reader, "Comentario"),
                                    Fax = Utils.SafeGetString(reader, "Fax"),
                                    FechaModificacion = reader.GetString("FechaModificacion"),
                                    TratamientoID = reader.GetString("TratamientoID"),
                                    Observacion = Utils.SafeGetString(reader, "Observacion")
                                });
                            }
                        }
                    }
                }
                return ListContactos;
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
        public object ObtenerContactos02(credenciales usuario)
        {
            List<HBM_Contacto> ListContactos = new List<HBM_Contacto>();
            string query = "";

            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    if (usuario.fecha.ToString() == "1900-01-01 00:00:00")
                    {
                        query = "select distinct C.* from Contactos C INNER JOIN CustomersSector CS ON CS.KUNNR = C.KUNNR WHERE CS.CodUsuario = '" + usuario.usuario.ToString() + "' and CS.Visible = '1'";
                    }
                    else
                    {
                        query = "select distinct C.* from Contactos C INNER JOIN CustomersSector CS ON CS.KUNNR = C.KUNNR WHERE CS.CodUsuario = '" + usuario.usuario.ToString() + "' and CS.Visible = '1' AND C.FechaModificacion >= '" + usuario.fecha.ToString() + "'";
                    }
                    command.CommandText = query;

                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListContactos.Add(new HBM_Contacto()
                                {
                                    ContactoID = reader.GetString("ContactoID"),
                                    KUNNR = Utils.SafeGetString(reader, "KUNNR"),
                                    CodContacto = Convert.ToInt32(reader.GetString("CodContacto")),
                                    Nombre = Utils.SafeGetString(reader, "Nombre"),
                                    Apellido = Utils.SafeGetString(reader, "Apellido"),
                                    CI = Utils.SafeGetString(reader, "CI"),
                                    Celular = Utils.SafeGetString(reader, "Celular"),
                                    Telefono = Utils.SafeGetString(reader, "Telefono"),
                                    Email = Utils.SafeGetString(reader, "Email"),
                                    Cargo = Utils.SafeGetString(reader, "Cargo"),
                                    CodDepartamento = Utils.SafeGetString(reader, "CodDepartamento"),
                                    CodFuncion = Utils.SafeGetString(reader, "CodFuncion"),
                                    Comentario = Utils.SafeGetString(reader, "Comentario"),
                                    Fax = Utils.SafeGetString(reader, "Fax"),
                                    FechaModificacion = reader.GetString("FechaModificacion"),
                                    TratamientoID = reader.GetString("TratamientoID"),
                                    Observacion = Utils.SafeGetString(reader, "Observacion")
                                });
                            }
                        }
                    }
                }
                return Tuple.Create(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), ListContactos);
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
        public object ObtenerRegimenTributario()
        {
            List<HBM_RegimenTributario> ListRegimenTributario = new List<HBM_RegimenTributario>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT RT.* FROM RegimenTributario RT";

                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListRegimenTributario.Add(new HBM_RegimenTributario()
                                {
                                    RegimenTribID = reader.GetString("RegimenTribID"),
                                    CodRegimenTrib = reader.GetString("CodRegimenTrib"),
                                    Denominacion = reader.GetString("Denominacion"),
                                    Estado = Convert.ToInt32(reader.GetString("Estado")),
                                    FechaModificacion = reader.GetString("FechaModificacion"),
                                    Monto = Convert.ToDecimal(reader.GetString("Monto"))
                                });
                            }
                        }
                    }
                }
                return ListRegimenTributario;
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
        public object ObtenerTipoDocIdentificacion()
        {
            List<HBM_TipoDocIdentificacion> ListTipoDoc = new List<HBM_TipoDocIdentificacion>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT T.* FROM TipoDocIdentificacion T";

                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListTipoDoc.Add(new HBM_TipoDocIdentificacion()
                                {
                                    CodTipoDocIden = reader.GetString("CodTipoDocIden"),
                                    Denominacion = reader.GetString("Denominacion"),
                                    Estado = Convert.ToInt32(reader.GetString("Estado")),
                                    FechaModificacion = reader.GetString("FechaModificacion"),
                                    TipoDocIdenID = reader.GetString("TipoDocIdenID")
                                });
                            }
                        }
                    }
                }
                return ListTipoDoc;
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
        public object BusquedaCustomers(credenciales usuario)
        {
            List<HBM_BCustomers> ListCustomers = new List<HBM_BCustomers>();
            string query;
            long number1 = 0;
            bool canConvert = long.TryParse(usuario.usuario.ToString(), out number1);
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    if (canConvert == true)
                    {
                        query = ("select DISTINCT C.KUNNR, C.Zona, C.Avenida,	C.Calle, C.Numero, C.LimiteCredito, C.Nit, C.CodRegimenTrib, C.CodTipoDocIden, C.Direccion, C.Tratamiento, C.NombreCliente, C.Ciudad, C.Email, C.Fax, C.Latitud, C.Longitud, C.FechaModificacion, C.MontoFacturaBs, C.Notes, C.Telefono, C.Movil, C.Observacion, C.Region, C.Regional, C.SYNC, C.TipoCliente, C.Visible, C.Web, C.ZonaTransporte " +
                         " from Customers C inner join CustomersSector CS ON CS.KUNNR = C.KUNNR WHERE C.Nit like '%" + usuario.usuario.ToString() + "%'");
                    }
                    else
                    {
                        query = ("select DISTINCT C.KUNNR, C.Zona, C.Avenida,	C.Calle, C.Numero, C.LimiteCredito, C.Nit, C.CodRegimenTrib, C.CodTipoDocIden, C.Direccion, C.Tratamiento, C.NombreCliente, C.Ciudad, C.Email, C.Fax, C.Latitud, C.Longitud, C.FechaModificacion, C.MontoFacturaBs, C.Notes, C.Telefono, C.Movil, C.Observacion, C.Region, C.Regional, C.SYNC, C.TipoCliente, C.Visible, C.Web, C.ZonaTransporte " +
                         " from Customers C inner join CustomersSector CS ON CS.KUNNR = C.KUNNR WHERE C.NombreCliente like '%" + usuario.usuario.ToString() + "%'");

                    }
                    command.CommandText = query;

                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListCustomers.Add(new HBM_BCustomers()
                                {
                                    KUNNR = reader.GetString("KUNNR"),
                                    Zona = reader.GetString("Zona"),
                                    Avenida = reader.GetString("Avenida"),
                                    Calle = reader.GetString("Calle"),
                                    Numero = reader.GetString("Numero"),
                                    LimiteCredito = Convert.ToDecimal(reader.GetString("LimiteCredito")),
                                    Nit = reader.GetString("Nit"),
                                    CodRegimenTrib = reader.GetString("CodRegimenTrib"),
                                    CodTipoDocIden = reader.GetString("CodTipoDocIden"),
                                    Direccion = reader.GetString("Direccion"),
                                    Tratamiento = reader.GetString("Tratamiento"),
                                    NombreCliente = reader.GetString("NombreCliente"),
                                    Ciudad = reader.GetString("Ciudad"),
                                    Email = reader.GetString("Email"),
                                    Fax = reader.GetString("Fax"),
                                    Latitud = Convert.ToDecimal(reader.GetString("Latitud")),
                                    Longitud = Convert.ToDecimal(reader.GetString("Longitud")),
                                    FechaModificacion = reader.GetString("FechaModificacion"),
                                    MontoFacturaBs = Convert.ToDecimal(reader.GetString("MontoFacturaBs")),
                                    Telefono = reader.GetString("Telefono"),
                                    Movil = reader.GetString("Movil"),
                                    Region = reader.GetString("Region"),
                                    Regional = reader.GetString("Regional"),
                                    TipoCliente = Convert.ToInt32(reader.GetString("TipoCliente")),
                                    Web = reader.GetString("Web"),
                                    ZonaTransporte = reader.GetString("ZonaTransporte")
                                });
                            }
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                conexion_mysql_open.cerrar_conexion();
                List<string> Listmensajes = new List<string>();
                Listmensajes.Add(e.Message.ToString());
                return Listmensajes;
            }
            return ListCustomers;
        }
        [HttpPost]
        public object ObtenerCentro()
        {
            List<HBM_Centro> ListTipoDoc = new List<HBM_Centro>();
            try
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "select * from Centro";

                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ListTipoDoc.Add(new HBM_Centro()
                                {
                                    CodCentro = reader.GetString("CodCentro"),
                                    Centro = reader.GetString("Centro"),
                                    CodRegion = reader.GetString("CodRegion")                                   
                                });
                            }
                        }
                    }
                }
                return ListTipoDoc;
            }
            catch (MySqlException e)
            {
                conexion_mysql_open.cerrar_conexion();
                List<string> Listmensajes = new List<string>();
                Listmensajes.Add(e.Message.ToString());
                return Listmensajes;
            }
        }

        public string usuarioAreaMercado(string usuario)
        {
            var conn = conexion_mysql_open.obtener_conexion();

            string consulta = "";
            int connt = 1;

            using (var command = conn.CreateCommand())
            {
                command.CommandText = "select * from UsuarioMercado where CodUsuario = '" + usuario + "';";
                using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (connt.Equals(1))
                            {
                                consulta = "'" + reader.GetString("ZonaMercado").ToString() + "'";
                            }
                            else
                            {
                                consulta = consulta + ",'" + reader.GetString("ZonaMercado") + "'";
                            }
                            connt++;
                        }
                    }
                }
            }
            return consulta;
        }

        public string ModoInventario(string usuario)
        {
            string modoInv = "";
            var conn = conexion_mysql_open.obtener_conexion();
            using (var command = conn.CreateCommand())
            {
                command.CommandText = "SELECT ModoInventario FROM hbm.ModoInventario where CodUsuario = '" + usuario + "';";
                using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            modoInv = reader.GetString("ModoInventario").ToString();
                        }
                    }
                }
            }
            return modoInv;
        }

        public string ValInventario(string kunnr, string codusuario, string modINV)
        {
            string inv = "1", Semana ="0";
            if (modINV.Equals("M"))
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "Select  max(Fecha) as Fecha, CodUsuario, KUNNR FROM Inventario WHERE MONTH(Fecha) = MONTH('2019-08-01') AND CodUsuario = '" + codusuario + "'and KUNNR = '" + kunnr + "' GROUP BY CodUsuario, KUNNR;";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                inv = "0";
                            }
                        }
                    }
                }
            }
            else
            {
                var conn = conexion_mysql_open.obtener_conexion();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT max(Fecha) as Fecha, CodUsuario,  WEEK(Fecha, 5) - WEEK(DATE_SUB(Fecha, INTERVAL DAYOFMONTH(Fecha) - 1 DAY), 5) + 1  as Semana FROM Inventario WHERE MONTH(Fecha) = MONTH('2019-08-01') and CodUsuario = '" + codusuario + "'and KUNNR = '" + kunnr + "' group by CodUsuario, KUNNR";
                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Semana = reader.GetString("Semana");
                            }
                        }
                    }
                }
                if (Semana.Equals("2") || Semana.Equals("4"))
                {
                    inv = "0";
                }
                else {
                    inv = "1";
                }
            }
            return inv;
        }
    }
}


