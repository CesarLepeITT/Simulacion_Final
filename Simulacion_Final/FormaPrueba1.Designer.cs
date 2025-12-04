using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Simulacion_Final
{
    partial class FormaPrueba1
    {

        // Componentes
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private TextBox txtGradoConfianza;
        private TextBox txtAlfa;
        private TextBox txtPromedio;
        private TextBox txtZaMedios;
        private TextBox txtLimiteInferior;
        private TextBox txtLimiteSuperior;
        private TextBox txtValorDistribucion;
        
        private Button btnProbar;
        
        private Label lblValorDistribucion;
        private Label lblGradoConfianza;
        private Label lblAlfa;
        private Label lblPromedio;
        private Label lblZaMedios;
        private Label lblLimiteInferior;
        private Label lblLimiteSuperior;
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
            this.txtPromedio = new TextBox();
            this.txtZaMedios = new TextBox();
            this.txtLimiteInferior = new TextBox();
            this.txtLimiteSuperior = new TextBox();
            this.txtValorDistribucion = new TextBox();

            this.btnProbar = new Button();

            this.lblValorDistribucion = new Label();
            this.lblGradoConfianza = new Label();
            this.lblAlfa = new Label();
            this.lblPromedio = new Label();
            this.lblZaMedios = new Label();
            this.lblLimiteInferior = new Label();
            this.lblLimiteSuperior = new Label();

            this.SuspendLayout();

            // Propiedades Base del Formulario
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(700, 600);
            this.Name = "Form1";
            this.Text = "Generador Pseudo-Aleatorio";

            // lblTitulo
            this.lblTitulo.Location = new Point(0, 10);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new Size(700, 40);
            this.lblTitulo.Text = "Prueba de Promedio";
            this.lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            this.lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);

            // Etiquetas
            CrearEtiqueta(lblGradoConfianza, "Grado de Confianza", 50, 70);
            
            CrearEtiqueta(lblAlfa, "Alfa", 50, 150);
            CrearEtiqueta(lblPromedio, "Promedio", 230, 150);
            CrearEtiqueta(lblZaMedios, "Za/2", 410, 150);

            CrearEtiqueta(lblValorDistribucion, "Valor de distribución", 50, 230);
            CrearEtiqueta(lblLimiteSuperior, "Límite Superior", 230, 230);

            // Inputs (entradas)
            CrearInput(txtGradoConfianza, 50, 100);

            // Outputs (solo lectura)
            CrearOutput(txtAlfa, 50, 180);
            CrearOutput(txtPromedio, 230, 180);
            CrearOutput(txtZaMedios, 410, 180);

            CrearOutput(txtValorDistribucion, 50, 260);
            CrearOutput(txtLimiteSuperior, 230, 260);

            // Botón Generar
            this.btnProbar.Location = new Point(410, 260);
            this.btnProbar.Name = "btnGenerar";
            this.btnProbar.Size = new Size(150, 35);
            this.btnProbar.Text = "Probar";
            this.btnProbar.UseVisualStyleBackColor = true;
            this.btnProbar.Click += new EventHandler(this.btnProbar_Click);


            // Agregar controles al formulario
            this.Controls.Add(lblTitulo);

            this.Controls.Add(lblGradoConfianza); this.Controls.Add(txtGradoConfianza);
            this.Controls.Add(lblAlfa); this.Controls.Add(txtAlfa);
            this.Controls.Add(lblPromedio); this.Controls.Add(txtPromedio);
            this.Controls.Add(lblZaMedios); this.Controls.Add(txtZaMedios);

            this.Controls.Add(lblValorDistribucion); this.Controls.Add(txtValorDistribucion);

            this.Controls.Add(lblLimiteInferior); this.Controls.Add(txtLimiteInferior);
            this.Controls.Add(lblLimiteSuperior); this.Controls.Add(txtLimiteSuperior);

            this.Controls.Add(btnProbar);

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
            txtGradoConfianza.Text = "99";
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

            // Hover efect simple
            btnProbar.MouseEnter += (s, e) => btnProbar.BackColor = colorBotonHover;
            btnProbar.MouseLeave += (s, e) => btnProbar.BackColor = colorBoton;

        }

        private void btnProbar_Click(object sender, EventArgs e)
        {
            try
            {
                DatosSimulacion datos = _gestorArchivos.CargarNumerosDesdeArchivo();

                if (!_gestorArchivos.TieneValores())
                {
                    MessageBox.Show("Se deben generar números pseudo-aleatorios antes de realizar la prueba.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                List<double> numeros = datos.ListaNumeros;
                int n = numeros.Count;

                double alfa = 0, z = 0;
                double nivelConfianza = double.Parse(txtGradoConfianza.Text);

                // Selección del valor crítico
                switch(nivelConfianza)
                {
                    case 90:
                        z = 1.645;
                        alfa = 0.10;
                        break;
                    case 95:
                        z = 1.96;
                        alfa = 0.05;
                        break;
                    case 99:
                        z = 2.576;
                        alfa = 0.01;
                        break;
                    default:
                        MessageBox.Show("Ingresar uno de los siguientes valores: 90, 95 o 99",
                            "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                }

                txtValorDistribucion.Text = $"{z}";

                // --- Calcular promedio ---
                double suma = 0;
                foreach (double x in numeros)
                    suma += x;

                double promedio = suma / n;

                // --- Límites de aceptación ---
                double error = z * Math.Sqrt(1.0 / (12.0 * n));
                double limInf = 0.5 - error;
                double limSup = 0.5 + error;

                // Asegurar límites válidos
                limInf = Math.Max(0, limInf);
                limSup = Math.Min(1, limSup);

                // --- Mostrar en pantalla ---
                txtAlfa.Text = alfa.ToString();
                txtPromedio.Text = promedio.ToString("F5");
                txtZaMedios.Text = z.ToString();
                txtLimiteInferior.Text = limInf.ToString("F5");
                txtLimiteSuperior.Text = limSup.ToString("F5");

                // --- Conclusión ---
                string conclusion =
                    (promedio >= limInf && promedio <= limSup)
                    ? "Los datos están distribuidos uniformemente."
                    : "Los datos no están distribuidos uniformemente.";

                // Crear etiqueta de conclusión
                this.lblConclusion = new Label();
                CrearEtiqueta(lblConclusion, conclusion, 50, 320);
                this.Controls.Add(lblConclusion);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error Crítico",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Limpieza de recursos
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }
    }
}