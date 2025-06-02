using MySql.Data.MySqlClient;

namespace WebApiHBM.Models
{
    public class conexion_mysql_open 
    {
        public static MySqlConnection obtener_conexion()
        {
            //MySqlConnection conectar = new MySqlConnection("server=docker-qas.hansa.com.bo; port=10001; database=hbm; Uid=desarrollo; pwd=desarrollo2017;");// QAS
            MySqlConnection conectar = new MySqlConnection("server=192.168.1.71; port=3306; database=hbm; Uid=hbm; pwd=hbm2018;");// PRD  
            conectar.Open();
            return conectar;
        }

        public static MySqlConnection cerrar_conexion()
        {
            //MySqlConnection conectar = new MySqlConnection("server=docker-qas.hansa.com.bo; port=10001; database=hbm; Uid=desarrollo; pwd=desarrollo2017;"); //("server=192.168.1.84; database=hbm; Uid=hbm_qas; pwd=Desarrollo.2020;");// QAS
           MySqlConnection conectar = new MySqlConnection("server=192.168.1.71; port=3306; database=hbm; Uid=hbm; pwd=hbm2018;");// PRD 
            conectar.Close();
            conectar.Dispose();
            return conectar;

        }
    }
}