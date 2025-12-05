using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion_Final
{
    internal class GeneradorNumerosPseudoaleatorios
    {

        public DatosSimulacion GenerarNumeros(long xo, long a, long c, long m, int cantidad)
        {
            DatosSimulacion resultado = new DatosSimulacion();

            resultado.SemillaX0 = xo;
            resultado.MultiplicadorA = a;
            resultado.ModuloM = m;
            resultado.ConstanteAditivaC = c;

            if (m <= 0) throw new ArgumentException("El módulo (m) debe ser mayor a 0.");

            long actual = xo;

            for (int i = 0; i < cantidad; i++)
            {

                long siguiente = (a * actual + c) % m;

                if (siguiente < 0)
                    siguiente += m;

                actual = siguiente;

                double ri = (double)actual / (double)m;


                resultado.ListaNumeros.Add(ri);
            }

            return resultado;
        }
    }
}