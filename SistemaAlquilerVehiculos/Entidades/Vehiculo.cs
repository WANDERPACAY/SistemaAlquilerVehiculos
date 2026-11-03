using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquilerVehiculos.Entidades
{
    public class Vehiculo
    {
        public int IdVehiculo { get; set; }
        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Anio { get; set; }
        public string TipoVehiculo { get; set; }
        public string Transmision { get; set; }
        public decimal TarifaDiaria { get; set; }
        public string Estado { get; set; }
        public string RutaFotografia { get; set; }
        public int KilometrajeActual { get; set; }
    }
}
