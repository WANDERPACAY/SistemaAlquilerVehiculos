using System;
using System.Drawing;
using System.IO;

namespace SistemaAlquilerVehiculos.Utilidades
{
    public static class FotosLicencias
    {
        public static Image Cargar(string ruta)
        {
            if (string.IsNullOrWhiteSpace(ruta))
                return null;

            string rutaCompleta = Path.IsPathRooted(ruta)
                ? ruta
                : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ruta);

            using (Image original = Image.FromFile(rutaCompleta))
            {
                return new Bitmap(original);
            }
        }

        public static string Copiar(string rutaOrigen)
        {
            string extension = Path.GetExtension(rutaOrigen)
                .ToLowerInvariant();

            if (extension != ".jpg" &&
                extension != ".jpeg" &&
                extension != ".png")
                throw new ArgumentException(
                    "Selecciona una imagen JPG o PNG.");

            // Verificar que el archivo contenga una imagen válida.
            using (Image imagen = Image.FromFile(rutaOrigen))
            {
                if (imagen.Width <= 0 || imagen.Height <= 0)
                    throw new ArgumentException("La imagen no es válida.");
            }

            string rutaRelativa = Path.Combine(
                "Fotos",
                "Licencias",
                Guid.NewGuid().ToString("N") + extension);

            string destino = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                rutaRelativa);

            Directory.CreateDirectory(Path.GetDirectoryName(destino));
            File.Copy(rutaOrigen, destino);

            return rutaRelativa;
        }
    }
}