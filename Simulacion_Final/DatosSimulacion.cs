using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulacion_Final
{
    internal class DatosSimulacion
    {
        // Parámetros de entrada
        public long SemillaX0 { get; set; }
        public long MultiplicadorA { get; set; }
        public long ModuloM { get; set; }
        public long ConstanteAditivaC { get; set; }

        // Resultado (La lista de números generados)
        public List<double> ListaNumeros { get; set; } = new List<double>();
    }
}

