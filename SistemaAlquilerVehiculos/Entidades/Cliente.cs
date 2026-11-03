using System;

namespace SistemaAlquilerVehiculos.Entidades
{
    public class Cliente
    {
        public int IdCliente { get; set; }

        public string DPI { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public string Telefono { get; set; }

        public string Direccion { get; set; }

        public int? IdMunicipio { get; set; }

        public string NumeroLicencia { get; set; }

        public string TipoLicencia { get; set; }

        public DateTime? FechaVencLicencia { get; set; }

        public string FotoLicenciaRuta { get; set; }
    }
}