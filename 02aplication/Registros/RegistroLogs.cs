using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders._02aplication.Registros
{
    static class RegistroLogs
    {
        public static string FICHERO_LOG = "..\\..\\..\\03data\\log.txt";

        public static void BorrarLog()
        {
            bool hayLog = File.Exists(FICHERO_LOG);
            if (hayLog)
            {
                File.Delete(FICHERO_LOG);
            }
        }

        public static void RegistrarEnLog(string registro)
        {
            DateTime fecha = DateTime.Now;
            StreamWriter log = new StreamWriter(FICHERO_LOG, true);
            log.WriteLine($"{fecha} => {registro}");
            log.Close();
        }
    }
}
