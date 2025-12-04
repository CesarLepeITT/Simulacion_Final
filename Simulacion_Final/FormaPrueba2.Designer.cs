using System;
using System.Collections.Generic;
using System.Data; // Necesario para DataTable
using System.Drawing;
using System.Windows.Forms;

namespace Simulacion_Final
{
    public partial class FormaPrueba2 : Form
    {
        // Componentes
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;

        // Inputs
        private TextBox txtGradoConfianza;

        // Outputs (Resultados)
        private TextBox txtAlfa;
        private TextBox txtValorEsperado;
        private TextBox txtVarianza;
        private TextBox txtEstadistico;
        private TextBox txtNCorridas;
        private TextBox txtValorDistribucion;

        private Button btnProbar;
        private DataGridView dgvCorridas; // <--- Nuevo componente

        // Etiquetas
        private Label lblValorDistribucion;
        private Label lblGradoConfianza;
        private Label lblAlfa;
        private Label lblValorEsperado;
        private Label lblVarianza;
        private Label lblEstadistico;
        private Label lblNCorridas;
        private Label lblConclusion;

        // Gestor de Archivos 
        private GestorArchivos _gestorArchivos;

        // Colores 
        private readonly Color colorFondo = Color.FromArgb(239, 246, 255);
        private readonly Color colorTextoTitulo = Color.FromArgb(49, 46, 129);
        private readonly Color colorBoton = Color.FromArgb(59, 130, 246);
        private readonly Color colorBotonHover = Color.FromArgb(55, 48, 163);
        private readonly Color colorBorde = Color.FromArgb(226, 232, 240);


        private void InitializeComponent()
        {
            this.lblTitulo = new Label();
            this.txtGradoConfianza = new TextBox();
            this.txtAlfa = new TextBox();
            this.txtValorEsperado = new TextBox();
            this.txtVarianza = new TextBox();
            this.txtEstadistico = new TextBox();
            this.txtNCorridas = new TextBox();
            this.txtValorDistribucion = new TextBox();

            this.btnProbar = new Button();
            this.dgvCorridas = new DataGridView(); // <--- Inicialización

            this.lblValorDistribucion = new Label();
            this.lblGradoConfianza = new Label();
            this.lblAlfa = new Label();
            this.lblValorEsperado = new Label();
            this.lblVarianza = new Label();
            this.lblEstadistico = new Label();
            this.lblNCorridas = new Label();
            this.lblConclusion = new Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvCorridas)).BeginInit();
            this.SuspendLayout();

            // Propiedades Base del Formulario
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            // Aumentamos la altura para que quepa la tabla
            this.ClientSize = new Size(700, 750);
            this.Name = "FormaPrueba2";
            this.Text = "Prueba de Independencia";

            // lblTitulo
            this.lblTitulo.Location = new Point(0, 10);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new Size(700, 40);
            this.lblTitulo.Text = "Prueba de Corridas (Independencia)";
            this.lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            this.lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);

            // Etiquetas
            CrearEtiqueta(lblGradoConfianza, "Grado de Confianza", 50, 70);

            CrearEtiqueta(lblAlfa, "Alfa", 50, 150);
            CrearEtiqueta(lblValorEsperado, "Valor Esperado", 230, 150);
            CrearEtiqueta(lblVarianza, "Varianza", 410, 150);

            CrearEtiqueta(lblValorDistribucion, "Valor Crítico (Z)", 50, 230);
            CrearEtiqueta(lblEstadistico, "Estadístico Z0", 230, 230);
            CrearEtiqueta(lblNCorridas, "Nº Corridas", 410, 230);

            // Etiqueta Conclusión
            CrearEtiqueta(lblConclusion, "", 50, 400);
            lblConclusion.AutoSize = true;
            lblConclusion.Font = new Font("Segoe UI", 12F, FontStyle.Bold);

            // Inputs
            CrearInput(txtGradoConfianza, 50, 100);

            // Outputs
            CrearOutput(txtAlfa, 50, 180);
            CrearOutput(txtValorEsperado, 230, 180);
            CrearOutput(txtVarianza, 410, 180);

            CrearOutput(txtValorDistribucion, 50, 260);
            CrearOutput(txtEstadistico, 230, 260);
            CrearOutput(txtNCorridas, 410, 260);

            // Botón Probar
            this.btnProbar.Location = new Point(50, 320);
            this.btnProbar.Name = "btnProbar";
            this.btnProbar.Size = new Size(150, 35);
            this.btnProbar.Text = "Probar";
            this.btnProbar.UseVisualStyleBackColor = true;
            this.btnProbar.Click += new EventHandler(this.btnProbar_Click);

            // DataGridView de Corridas
            this.dgvCorridas.Location = new Point(50, 450); // Debajo de la conclusión
            this.dgvCorridas.Size = new Size(600, 250);
            this.dgvCorridas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvCorridas.AllowUserToAddRows = false;
            this.dgvCorridas.ReadOnly = true;
            this.dgvCorridas.BackgroundColor = Color.White;
            this.dgvCorridas.BorderStyle = BorderStyle.FixedSingle;

            // Agregar controles
            this.Controls.Add(lblTitulo);
            this.Controls.Add(lblGradoConfianza); this.Controls.Add(txtGradoConfianza);
            this.Controls.Add(lblAlfa); this.Controls.Add(txtAlfa);
            this.Controls.Add(lblValorEsperado); this.Controls.Add(txtValorEsperado);
            this.Controls.Add(lblVarianza); this.Controls.Add(txtVarianza);
            this.Controls.Add(lblValorDistribucion); this.Controls.Add(txtValorDistribucion);
            this.Controls.Add(lblEstadistico); this.Controls.Add(txtEstadistico);
            this.Controls.Add(lblNCorridas); this.Controls.Add(txtNCorridas);
            this.Controls.Add(btnProbar);
            this.Controls.Add(lblConclusion);
            this.Controls.Add(dgvCorridas); // <--- Agregado a controles

            ((System.ComponentModel.ISupportInitialize)(this.dgvCorridas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void CrearEtiqueta(Label lbl, string texto, int x, int y)
        {
            lbl.AutoSize = true;
            lbl.Location = new Point(x, y);
            lbl.Text = texto;
            lbl.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            lbl.ForeColor = Color.DimGray;
        }

        private void CrearInput(TextBox txt, int x, int y)
        {
            txt.Location = new Point(x, y);
            txt.Size = new Size(150, 27);
            txt.Font = new Font("Segoe UI", 10F);
        }

        private void CrearOutput(TextBox txt, int x, int y)
        {
            txt.Location = new Point(x, y);
            txt.Size = new Size(150, 27);
            txt.Font = new Font("Segoe UI", 10F);
            txt.ReadOnly = true;
        }

        private void ConfigurarValoresIniciales()
        {
            txtGradoConfianza.Text = "95";
        }

        private void AplicarEstilo()
        {
            this.BackColor = colorFondo;
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            if (lblTitulo != null) lblTitulo.ForeColor = colorTextoTitulo;

            // Estilo Botón
            btnProbar.FlatStyle = FlatStyle.Flat;
            btnProbar.BackColor = colorBoton;
            btnProbar.ForeColor = Color.White;
            btnProbar.FlatAppearance.BorderSize = 0;
            btnProbar.Cursor = Cursors.Hand;

            btnProbar.MouseEnter += (s, e) => btnProbar.BackColor = colorBotonHover;
            btnProbar.MouseLeave += (s, e) => btnProbar.BackColor = colorBoton;

            // Estilo Tabla
            dgvCorridas.EnableHeadersVisualStyles = false;
            dgvCorridas.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvCorridas.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;
            dgvCorridas.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvCorridas.DefaultCellStyle.SelectionBackColor = colorFondo;
            dgvCorridas.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvCorridas.GridColor = colorBorde;
            dgvCorridas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCorridas.RowHeadersVisible = false;
        }

        private void btnProbar_Click(object sender, EventArgs e)
        {
            try
            {
                lblConclusion.Text = "";

                DatosSimulacion datos = _gestorArchivos.CargarNumerosDesdeArchivo();

                if (!_gestorArchivos.TieneValores())
                {
                    MessageBox.Show("Se deben generar números pseudo-aleatorios antes de realizar la prueba.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                List<double> numeros = datos.ListaNumeros;
                int n = numeros.Count;

                // --- Preparar Tabla ---
                DataTable dt = new DataTable();
                dt.Columns.Add("i", typeof(int));
                dt.Columns.Add("Xi (Actual)", typeof(double));
                dt.Columns.Add("Xi-1 (Anterior)", typeof(double));
                dt.Columns.Add("Signo", typeof(string));
                dt.Columns.Add("Corrida #", typeof(int));

                // --- Lógica de Corridas ---
                int nCorridas = -1;
                double? tendenciaAnt = null;

                // Iterar
                for (int i = 1; i < n; i++)
                {
                    double diferencia = 0; // 0 = Decremento, 1 = Incremento
                    string signoStr = "";

                    // Comparar actual con anterior
                    if (numeros[i] - numeros[i - 1] < 0)
                    {
                        diferencia = 0;
                        signoStr = "-";
                    }
                    else
                    {
                        diferencia = 1;
                        signoStr = "+";
                    }

                    if (tendenciaAnt == null || diferencia != tendenciaAnt)
                    {
                        nCorridas++;
                    }

                    tendenciaAnt = diferencia;

                    // Agregar fila al DataGrid
                    // (nCorridas + 1 para que visualmente empiece en 1 y no en 0)
                    dt.Rows.Add(i, numeros[i], numeros[i - 1], signoStr, nCorridas + 1);
                }

                // Asignar datos al Grid
                dgvCorridas.DataSource = dt;

                // --- Cálculos Estadísticos ---
                double valorEsperado = (2.0 * n - 1.0) / 3.0;
                double desviacionEstandar = Math.Sqrt((16.0 * n - 29.0) / 90.0);
                double datEstadisticos = (nCorridas - valorEsperado) / desviacionEstandar;

                if (!int.TryParse(txtGradoConfianza.Text, out int nivelConfianza))
                {
                    MessageBox.Show("Grado de confianza inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                double zcritico = 0;
                double alfa = 0;

                switch (nivelConfianza)
                {
                    case 90: zcritico = 1.645; alfa = 0.10; break;
                    case 95: zcritico = 1.96; alfa = 0.05; break;
                    case 99: zcritico = 2.576; alfa = 0.01; break;
                    default:
                        MessageBox.Show("Ingresar 90, 95 o 99", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                }

                double zcriticoInf = -zcritico;

                // Mostrar Resultados
                txtAlfa.Text = alfa.ToString();
                txtValorDistribucion.Text = zcritico.ToString();
                txtValorEsperado.Text = valorEsperado.ToString("F4");
                txtVarianza.Text = desviacionEstandar.ToString("F4");
                txtEstadistico.Text = datEstadisticos.ToString("F4");
                txtNCorridas.Text = (nCorridas + 1).ToString(); // Ajuste visual (+1) para coincidir con la tabla

                // Conclusión
                bool pasaPrueba = (datEstadisticos > zcriticoInf && datEstadisticos < zcritico);

                lblConclusion.Text = pasaPrueba
                    ? "Los datos están distribuidos uniformemente (Independientes)."
                    : "Los datos NO están distribuidos uniformemente.";

                lblConclusion.ForeColor = pasaPrueba ? Color.Green : Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error Crítico",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }
    }
}