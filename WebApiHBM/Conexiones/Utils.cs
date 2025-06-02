using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiHBM.Conexiones
{    
    public static class Utils
    {
        public static string IsNullOrEmpty(object valor)
        {
            string val = valor.ToString();
            if (valor == null || valor.ToString().Length == 0)
            { return "0,0000000000"; }
            else
            { return val; }

            //try
            //{
            //    return valor == null || valor.ToString().Length == 0;
            //}
            //catch(Exception ex) {
            //    return "0";
            //}
        }
        public static string SafeGetString(this MySqlDataReader reader, string nombrecol)
        {
           int i = reader.GetOrdinal(nombrecol);
            if (!reader.IsDBNull(i))
                return reader.GetString(i);
            return string.Empty;
        }
        public static int SafeGetInt(this MySqlDataReader reader, string nombrecol)
        {
            int i = reader.GetOrdinal(nombrecol);
            if (!reader.IsDBNull(i))
                return reader.GetInt32(i);
            return 0;
        }
    }
}