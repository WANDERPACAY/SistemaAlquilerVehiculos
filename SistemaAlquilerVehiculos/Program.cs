using System;
using System.Windows.Forms;

namespace SistemaAlquilerVehiculos
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Bucle principal para controlar el ciclo Login <-> Menú
            bool mantenerSesion = true;

            while (mantenerSesion)
            {
                frmLogin login = new frmLogin();

                // Si el usuario ingresa correctamente (DialogResult.OK en el login)
                if (login.ShowDialog() == DialogResult.OK)
                {
                    frmMenu menu = new frmMenu();

                    // Cuando el menú se cierre, decidimos si fue para cerrar sesión o salir
                    DialogResult resultadoMenu = menu.ShowDialog();

                    if (resultadoMenu == DialogResult.Abort) // Indicará "Cerrar Sesión"
                    {
                        mantenerSesion = true; // Vuelve a mostrar el Login
                    }
                    else
                    {
                        mantenerSesion = false; // Cierra la aplicación por completo
                    }
                }
                else
                {
                    // Si el usuario simplemente cerró la ventanita del login con la "X"
                    mantenerSesion = false;
                }
            }
        }
    }
}