using System;
using SistemaAlquilerVehiculos.Entidades;
using SistemaAlquilerVehiculos.Datos;

namespace SistemaAlquilerVehiculos.Logica
{
    public static class VehiculoLogica
    {
        public static void Validar(Vehiculo vehiculo)
        {
            if (vehiculo == null)
                throw new ArgumentException(
                    "No se recibieron los datos del vehículo.");

            if (string.IsNullOrWhiteSpace(vehiculo.Placa))
                throw new ArgumentException("Ingresa la placa.");

            if (string.IsNullOrWhiteSpace(vehiculo.Marca))
                throw new ArgumentException("Ingresa la marca.");

            if (string.IsNullOrWhiteSpace(vehiculo.Modelo))
                throw new ArgumentException("Ingresa el modelo.");

            vehiculo.Placa = vehiculo.Placa.Trim().ToUpperInvariant();
            vehiculo.Marca = vehiculo.Marca.Trim();
            vehiculo.Modelo = vehiculo.Modelo.Trim();

            if (vehiculo.Placa.Length > 20)
                throw new ArgumentException(
                    "La placa admite un máximo de 20 caracteres.");

            if (vehiculo.Marca.Length > 50 ||
                vehiculo.Modelo.Length > 50)
                throw new ArgumentException(
                    "La marca y el modelo admiten hasta 50 caracteres.");

            if (vehiculo.Anio < 1000 || vehiculo.Anio > 9999)
                throw new ArgumentException(
                    "El año debe ser un número de cuatro dígitos.");

            if (vehiculo.TipoVehiculo != "Sedán" &&
                vehiculo.TipoVehiculo != "SUV" &&
                vehiculo.TipoVehiculo != "Pickup")
                throw new ArgumentException(
                    "Selecciona un tipo de vehículo válido.");

            if (vehiculo.Transmision != "Mecánica" &&
                vehiculo.Transmision != "Automática")
                throw new ArgumentException(
                    "Selecciona una transmisión válida.");

            if (vehiculo.TarifaDiaria <= 0 ||
                vehiculo.TarifaDiaria > 9999999999999999.99m)
                throw new ArgumentException(
                    "La tarifa debe ser mayor que cero y estar dentro del rango permitido.");

            if (decimal.Round(vehiculo.TarifaDiaria, 2) !=
                vehiculo.TarifaDiaria)
                throw new ArgumentException(
                    "La tarifa admite como máximo dos decimales.");

            if (vehiculo.KilometrajeActual < 0)
                throw new ArgumentException(
                    "El kilometraje no puede ser negativo.");

            if (vehiculo.Estado != "Disponible" &&
                vehiculo.Estado != "Rentado" &&
                vehiculo.Estado != "En Taller")
                throw new ArgumentException(
                    "Selecciona un estado válido.");

            if (vehiculo.RutaFotografia != null &&
                vehiculo.RutaFotografia.Length > 255)
                throw new ArgumentException(
                    "La ruta de la fotografía es demasiado larga.");
        }
        public static void Eliminar(int idVehiculo, int idUsuario)
        {
            if (idVehiculo <= 0)
                throw new ArgumentException(
                    "Selecciona el vehículo que deseas eliminar.");

            VehiculoDatos datos = new VehiculoDatos();
            datos.Eliminar(idVehiculo, idUsuario);
        }
        public static int Guardar(Vehiculo vehiculo, int idUsuario)
        {
            Validar(vehiculo);

            VehiculoDatos datos = new VehiculoDatos();
            return datos.Guardar(vehiculo, idUsuario);
        }
        public static void Editar(Vehiculo vehiculo, int idUsuario)
        {
            Validar(vehiculo);

            VehiculoDatos datos = new VehiculoDatos();
            datos.Editar(vehiculo, idUsuario);
        }
    }
}