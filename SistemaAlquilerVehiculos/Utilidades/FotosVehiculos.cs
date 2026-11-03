using System;
using System.Drawing;
using System.IO;

namespace SistemaAlquilerVehiculos.Utilidades
{
    public static class FotosVehiculos
    {
        public static string Copiar(string archivoOrigen)
        {
            string extension = Path.GetExtension(
                archivoOrigen).ToLowerInvariant();

            if (extension != ".jpg" &&
                extension != ".jpeg" &&
                extension != ".png")
            {
                throw new ArgumentException(
                    "Selecciona una imagen JPG o PNG.");
            }

            // Verifica que el archivo pueda leerse como imagen.
            using (Image imagen = Image.FromFile(archivoOrigen))
            {
                if (imagen.Width <= 0 || imagen.Height <= 0)
                    throw new ArgumentException("La imagen no es válida.");
            }

            string carpetaRelativa = Path.Combine("Fotos", "Vehiculos");

            string carpetaDestino = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                carpetaRelativa);

            Directory.CreateDirectory(carpetaDestino);

            string nombre = Guid.NewGuid().ToString("N") + extension;
            string rutaRelativa = Path.Combine(carpetaRelativa, nombre);

            File.Copy(
                archivoOrigen,
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    rutaRelativa));

            return rutaRelativa;
        }

        public static Image Cargar(string ruta)
        {
            if (string.IsNullOrWhiteSpace(ruta))
                return null;

            string rutaCompleta = Path.IsPathRooted(ruta)
                ? ruta
                : Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, ruta);

            if (!File.Exists(rutaCompleta))
                return null;

            using (Image original = Image.FromFile(rutaCompleta))
            {
                // La copia permite cerrar el archivo original.
                return new Bitmap(original);
            }
        }
    }
}