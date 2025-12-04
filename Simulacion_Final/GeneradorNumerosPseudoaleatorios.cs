using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion_Final
{
    internal class GeneradorNumerosPseudoaleatorios
    {
        // Se encarga de generar los números pseudoaleatorios según los parámetros dados y escribirlos en un archivo csv ademas de dar la capacidad de
        // leer números pseudoaleatorios desde un archivo json

        public DatosSimulacion GenerarNumeros(int xo, int a, int c, int m, int cantidad)
        {
            DatosSimulacion resultado = new DatosSimulacion();

            // Guardamos los parámetros en el modelo
            resultado.SemillaX0 = xo;
            resultado.MultiplicadorA = a;
            resultado.ModuloM = m;
            resultado.ConstanteAditivaC = c;

            // Congruencial Mixto
            double actual = xo;
            for (int i = 0; i < cantidad; i++) 
            {
                actual = (a * actual + c) % m;
                double ri = actual / m; 
                resultado.ListaNumeros.Add(ri); 
            }

            return resultado; 
        }

    }
}
