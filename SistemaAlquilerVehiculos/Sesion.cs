using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquilerVehiculos
{
    internal class Sesion
    {
        public static int IdUsuario { get; set; }
        public static string NombreCompleto { get; set; }
        public static string Username { get; set; }
        public static string Rol { get; set; }

        public static void CerrarSesion()
        {
            IdUsuario = 0;
            NombreCompleto = string.Empty;
            Username = string.Empty;
            Rol = string.Empty;
        }
    }
}
