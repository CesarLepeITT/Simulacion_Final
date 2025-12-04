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
    public partial class FormaPrueba2 : Form
    {
        public FormaPrueba2()
        {
            InitializeComponent();
            _gestorArchivos = new GestorArchivos();
            AplicarEstilo();
            ConfigurarValoresIniciales();
        }
    }
}
