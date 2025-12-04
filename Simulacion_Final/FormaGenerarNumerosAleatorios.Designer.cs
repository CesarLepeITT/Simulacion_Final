using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Simulacion_Final
{
    partial class FormaGenerarNumerosAleatorios
    {
        // Componentes
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private TextBox txtA;
        private TextBox txtC;
        private TextBox txtM;
        private TextBox txtX;
        private TextBox txtTotal;
        private Button btnGenerar;
        private DataGridView dgvResultados;
        private Label lblA;
        private Label lblC;
        private Label lblM;
        private Label lblX;
        private Label lblTotal;

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
            this.txtA = new TextBox();
            this.txtC = new TextBox();
            this.txtM = new TextBox();
            this.txtX = new TextBox();
            this.txtTotal = new TextBox();
            this.btnGenerar = new Button();
            this.dgvResultados = new DataGridView();
            this.lblA = new Label();
            this.lblC = new Label();
            this.lblM = new Label();
            this.lblX = new Label();
            this.lblTotal = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).BeginInit();
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
            this.lblTitulo.Text = "Generador de Números Pseudo-Aleatorios";
            this.lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            this.lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);

            // Etiquetas            
            this.lblA.AutoSize = true;
            this.lblA.Location = new Point(50, 70);
            this.lblA.Text = "Multiplicador (a)";
            this.lblA.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.lblA.ForeColor = Color.DimGray;

            this.lblC.AutoSize = true;
            this.lblC.Location = new Point(230, 70);
            this.lblC.Text = "Incremento (c)";
            this.lblC.Font  = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.lblC.ForeColor = Color.DimGray;

            this.lblM.AutoSize = true;
            this.lblM.Location = new Point(410, 70);
            this.lblM.Text = "Módulo (m)";
            this.lblM.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.lblM.ForeColor = Color.DimGray;

            
            this.lblX.AutoSize = true;
            this.lblX.Location = new Point(50, 140);
            this.lblX.Text = "Semilla (X0)";
            this.lblX.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.lblX.ForeColor = Color.DimGray;

            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new Point(230, 140);
            this.lblTotal.Text = "Total a generar";
            this.lblTotal.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.lblTotal.ForeColor = Color.DimGray;

            // Inputs
            
            this.txtA.Location = new Point(50, 95);
            this.txtA.Size = new Size(150, 27);
            this.txtA.Font = new Font("Segoe UI", 10F);
            
            this.txtC.Location = new Point(230, 95);
            this.txtC.Size = new Size(150, 27);
            this.txtC.Font = new Font("Segoe UI", 10F);

            this.txtM.Location = new Point(410, 95);    
            this.txtM.Size = new Size(150, 27);
            this.txtM.Font = new Font("Segoe UI", 10F);

            this.txtX.Location = new Point(50, 165);
            this.txtX.Size = new Size(150, 27);
            this.txtX.Font = new Font("Segoe UI", 10F);

            this.txtTotal.Location = new Point(230, 165);
            this.txtTotal.Size = new Size(150, 27);
            this.txtTotal.Font = new Font("Segoe UI", 10F);

            // Botón Generar
            this.btnGenerar.Location = new Point(410, 160);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new Size(150, 35);
            this.btnGenerar.Text = "Generar";
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Click += new EventHandler(this.btnGenerar_Click);

            // DataGridView
            this.dgvResultados.Location = new Point(50, 220);
            this.dgvResultados.Size = new Size(600, 320);
            this.dgvResultados.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvResultados.AllowUserToAddRows = false;
            this.dgvResultados.ReadOnly = true;
            this.dgvResultados.BackgroundColor = Color.White;
            this.dgvResultados.BorderStyle = BorderStyle.None;

            // Agregar controles al formulario
            this.Controls.Add(lblTitulo);
            this.Controls.Add(lblA); this.Controls.Add(txtA);
            this.Controls.Add(lblC); this.Controls.Add(txtC);
            this.Controls.Add(lblM); this.Controls.Add(txtM);
            this.Controls.Add(lblX); this.Controls.Add(txtX);
            this.Controls.Add(lblTotal); this.Controls.Add(txtTotal);
            this.Controls.Add(btnGenerar);
            this.Controls.Add(dgvResultados);

            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).EndInit();
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


        private void ConfigurarValoresIniciales()
        {
           
            if(!_gestorArchivos.TieneValores())
            {
                txtA.Text = "1103515245";
                txtC.Text = "12345";
                txtM.Text = "2147483648";
                txtX.Text = "1";
                txtTotal.Text = "20";              
            }
            else
            {
                DatosSimulacion datos = _gestorArchivos.CargarNumerosDesdeArchivo();
                txtA.Text = datos.MultiplicadorA.ToString();
                txtC.Text = datos.ConstanteAditivaC.ToString();
                txtM.Text = datos.ModuloM.ToString();
                txtX.Text = datos.SemillaX0.ToString();
                txtTotal.Text = datos.ListaNumeros.Count.ToString();
                DesplegarDatos(datos);

            }


        }

        private void AplicarEstilo()
        {
            this.BackColor = colorFondo;
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            if (lblTitulo != null) lblTitulo.ForeColor = colorTextoTitulo;

            // Estilo Botón
            btnGenerar.FlatStyle = FlatStyle.Flat;
            btnGenerar.BackColor = colorBoton;
            btnGenerar.ForeColor = Color.White;
            btnGenerar.FlatAppearance.BorderSize = 0;
            btnGenerar.Cursor = Cursors.Hand;

            // Hover efect simple
            btnGenerar.MouseEnter += (s, e) => btnGenerar.BackColor = colorBotonHover;
            btnGenerar.MouseLeave += (s, e) => btnGenerar.BackColor = colorBoton;

            // Estilo Tabla
            dgvResultados.EnableHeadersVisualStyles = false;
            dgvResultados.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvResultados.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;
            dgvResultados.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvResultados.DefaultCellStyle.SelectionBackColor = colorFondo;
            dgvResultados.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvResultados.GridColor = colorBorde;
            dgvResultados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvResultados.RowHeadersVisible = false;
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!long.TryParse(txtA.Text, out long a) ||
                    !long.TryParse(txtC.Text, out long c) ||
                    !long.TryParse(txtM.Text, out long m) ||
                    !long.TryParse(txtX.Text, out long x) ||
                    !int.TryParse(txtTotal.Text, out int total))
                {
                    MessageBox.Show("Por favor, ingresa valores numéricos válidos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // TODO: Agregar validaciones adicionales
                if (x <= 0)
                {
                    MessageBox.Show("Valor inválido de semilla. Debe ser mayor a 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (a <= 0)
                {
                    MessageBox.Show("Valor inválido del multiplicador. Debe ser mayor a 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (c <= 0)
                {
                    MessageBox.Show("Valor inválido del incremento. Debe ser mayor a 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (m <= 0)
                {
                    MessageBox.Show("Valor inválido del módulo. Debe ser mayor a 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (total <= 0)
                {
                    MessageBox.Show("Cantidad inválida a generar. Debe ser mayor a 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Llamada a generador
                var generador = new GeneradorNumerosPseudoaleatorios();
                var resultados = generador.GenerarNumeros(x, a, c, m, total);

                // Guardar datos en archivo Json
                _gestorArchivos.GuardarNumerosEnArchivo(resultados);


                // Crear tabla
                DesplegarDatos(resultados);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DesplegarDatos(DatosSimulacion resultados)
        {
            try
            {
                var tabla = new System.Data.DataTable();
                tabla.Columns.Add("Iteración", typeof(int));
                tabla.Columns.Add("Valor (Xi)", typeof(double));

                // Llenar tabla con los números generados
                int iter = 1;
                foreach (var numero in resultados.ListaNumeros)
                {
                    tabla.Rows.Add(iter, numero);
                    iter++;
                }

                dgvResultados.DataSource = tabla;
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error: {ex.Message}", "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
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