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
        public int SemillaX0 { get; set; }
        public int MultiplicadorA { get; set; }
        public int ModuloM { get; set; }
        public int ConstanteAditivaC { get; set; }

        // Resultado (La lista de números generados)
        public List<double> ListaNumeros { get; set; } = new List<double>();
    }
}

