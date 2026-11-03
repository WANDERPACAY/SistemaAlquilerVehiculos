using System;
using System.Text.RegularExpressions;
using SistemaAlquilerVehiculos.Entidades;
using SistemaAlquilerVehiculos.Datos;

namespace SistemaAlquilerVehiculos.Logica
{
    public static class ClienteLogica
    {
        public static int Guardar(Cliente cliente, int idUsuario)
        {
            Validar(cliente);

            return new ClienteDatos().Guardar(cliente, idUsuario);
        }
        public static void Editar(Cliente cliente, int idUsuario)
        {
            Validar(cliente);

            if (cliente.IdCliente <= 0)
                throw new ArgumentException(
                    "Selecciona un cliente para editar.");

            new ClienteDatos().Editar(cliente, idUsuario);
        }
        public static void Eliminar(int idCliente, int idUsuario)
        {
            if (idCliente <= 0)
                throw new ArgumentException(
                    "Selecciona un cliente para eliminar.");

            new ClienteDatos().Eliminar(idCliente, idUsuario);
        }
        public static void Validar(Cliente cliente)
        {
            if (cliente == null)
                throw new ArgumentException(
                    "No se recibieron los datos del cliente.");

            cliente.DPI = (cliente.DPI ?? "").Trim();
            cliente.Nombres = (cliente.Nombres ?? "").Trim();
            cliente.Apellidos = (cliente.Apellidos ?? "").Trim();
            cliente.Telefono = (cliente.Telefono ?? "").Trim();
            cliente.Direccion = (cliente.Direccion ?? "").Trim();
            cliente.NumeroLicencia =
                (cliente.NumeroLicencia ?? "").Trim();
            cliente.TipoLicencia =
                (cliente.TipoLicencia ?? "").Trim().ToUpperInvariant();
            cliente.FotoLicenciaRuta =
                (cliente.FotoLicenciaRuta ?? "").Trim();

            if (!Regex.IsMatch(cliente.DPI, @"\A[0-9]{13}\z"))
                throw new ArgumentException(
                    "El DPI debe contener exactamente 13 dígitos.");

            ValidarTexto(cliente.Nombres, "Los nombres", 50, true);
            ValidarTexto(cliente.Apellidos, "Los apellidos", 50, true);
            ValidarTexto(cliente.Telefono, "El teléfono", 20, false);
            ValidarTexto(cliente.Direccion, "La dirección", 150, false);

            if (!cliente.IdMunicipio.HasValue ||
                cliente.IdMunicipio.Value <= 0)
                throw new ArgumentException(
                    "Selecciona un departamento y un municipio.");

            ValidarTexto(
                cliente.NumeroLicencia,
                "El número de licencia",
                30,
                true);

            if (cliente.TipoLicencia != "A" &&
                cliente.TipoLicencia != "B" &&
                cliente.TipoLicencia != "C" &&
                cliente.TipoLicencia != "M" &&
                cliente.TipoLicencia != "E")
                throw new ArgumentException(
                    "Selecciona un tipo de licencia válido.");

            if (!cliente.FechaVencLicencia.HasValue)
                throw new ArgumentException(
                    "Selecciona la fecha de vencimiento de la licencia.");

            ValidarTexto(
                cliente.FotoLicenciaRuta,
                "La fotografía de la licencia",
                255,
                true);
        }

        private static void ValidarTexto(
            string valor,
            string nombreCampo,
            int longitudMaxima,
            bool obligatorio)
        {
            if (obligatorio && string.IsNullOrWhiteSpace(valor))
                throw new ArgumentException(
                    nombreCampo + ": debes completar este dato.");

            if (valor.Length > longitudMaxima)
                throw new ArgumentException(
                    nombreCampo + ": admite como máximo " +
                    longitudMaxima + " caracteres.");
        }
    }
}