using Newtonsoft.Json;
using System;
using System.IO;

namespace Simulacion_Final
{
    internal class GestorArchivos
    {
        // Usamos readonly para asegurar que no se cambien por accidente después del constructor
        private readonly string rutaCarpeta = AppDomain.CurrentDomain.BaseDirectory;
        private readonly string nombreArchivo = "numeros_pseudoaleatorios.json";
        private readonly string rutaCompleta;

        public GestorArchivos()
        {
            rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);
            VerificarYCrearArchivos();
        }

        public void VerificarYCrearArchivos()
        {
            AsegurarCarpeta();
            AsegurarArchivo();
        }

        private void AsegurarCarpeta()
        {

            if (!Directory.Exists(rutaCarpeta))
            {
                Directory.CreateDirectory(rutaCarpeta);
            }
        }

        private void AsegurarArchivo()
        {
            if (!File.Exists(rutaCompleta))
            {
                CrearJsonVacio();
            }
        }

        private void CrearJsonVacio()
        {
            try
            {
                DatosSimulacion datosVacios = new DatosSimulacion();
                string jsonVacio = JsonConvert.SerializeObject(datosVacios, Formatting.Indented);
                File.WriteAllText(rutaCompleta, jsonVacio);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear archivo vacío: {ex.Message}");
            }
        }

        public void GuardarNumerosEnArchivo(DatosSimulacion datos)
        {
            try
            {
                VerificarYCrearArchivos();

                string json = JsonConvert.SerializeObject(datos, Formatting.Indented);
                File.WriteAllText(rutaCompleta, json);
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error de escritura: El archivo podría estar en uso. {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general al guardar: {ex.Message}");
            }
        }

        public DatosSimulacion CargarNumerosDesdeArchivo()
        {
            try
            {
                VerificarYCrearArchivos();

                string json = File.ReadAllText(rutaCompleta);

                if (string.IsNullOrWhiteSpace(json))
                    return new DatosSimulacion();

                DatosSimulacion datos = JsonConvert.DeserializeObject<DatosSimulacion>(json);

                return datos ?? new DatosSimulacion();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar: {ex.Message}");
                return new DatosSimulacion(); 
            }
        }
    }
}