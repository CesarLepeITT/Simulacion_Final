using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulacion_Final
{
    public partial class FormaGenerarNumerosAleatorios : Form
    {
        public FormaGenerarNumerosAleatorios()
        {
            // 1. Crear los controles visuales
            InitializeComponent();

            // 2. Inicializar lógica
            _gestorArchivos = new GestorArchivos();

            // 3. Configurar valores por defecto y estilos
            ConfigurarValoresIniciales();
            AplicarEstilo();

        }
    }
}
