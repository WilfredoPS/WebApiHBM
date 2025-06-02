using System;
using System.IO;

namespace WebApiHBM.Conexiones
{
    public class LogHBM
    {

        // para crear el archivo
        public static void GenerarLog(string sms)
        {
            try
            {
                DateTime dateTime = new DateTime();
                dateTime = DateTime.Now;
                string rutaCompleta = @"d:\Log-HBM\LogAuthentications_" + Convert.ToDateTime(dateTime).ToString("yyyy-MM") + ".txt"; // C:\inetpub\wwwroot\Log-HBM";
                if (System.IO.File.Exists(rutaCompleta))
                {
                    using (StreamWriter file = new StreamWriter(rutaCompleta, true))
                    {
                        dateTime = DateTime.Now;
                        string strDate = Convert.ToDateTime(dateTime).ToString("yyyy-MM-dd H:mm:ss");
                        file.WriteLine("[" + strDate + "] " + sms);
                        file.Close();
                    }
                }
                else
                {
                    string texto = "INICIANDO...!!!!!";
                    using (StreamWriter mylogs = File.AppendText(rutaCompleta))
                    {
                        dateTime = DateTime.Now;
                        string strDate = Convert.ToDateTime(dateTime).ToString("yyyy-MM-dd H:mm:ss");
                        mylogs.WriteLine("[" + strDate + "] " + texto);
                        mylogs.WriteLine("[" + strDate + "] " + sms);
                        mylogs.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                string ms = ex.ToString();
            }
        }
    }
}