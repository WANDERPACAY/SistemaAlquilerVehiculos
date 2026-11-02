using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaAlquilerVehiculos
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 1. Instanciar el Login
            frmLogin login = new frmLogin();

            // 2. Mostrar el login. Si el usuario ingresa correctamente (DialogResult.OK)...
            if (login.ShowDialog() == DialogResult.OK)
            {
                // 3. Abrir el menú principal de forma limpia
                Application.Run(new frmMenu());
            }
            else
            {
                // Si le dio a salir o cerró la 'X', se termina la aplicación por completo
                Application.Exit();
            }
        }
    }
}
