using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Simulacion_Final
{
    partial class FormaMenu
    {

        // Controles Principales
        private FlowLayoutPanel panelMenu;
        private Panel panelContenido; 
        private Label lblTitulo;

        // Estado
        private List<Button> listaBotones = new List<Button>();
        private int indiceActivo = -1; 
        private Form formularioActivo = null;

        // Botones del Menú
        private string[] menuItems = {
            "Generar Números",
            "Prueba 1",
            "Prueba 2",
            "Simular",
            "Datos Alumnos"
        };

        // Colores
        private Color colorFondoMenu = Color.White;
        private Color colorFondoContenido = Color.FromArgb(241, 245, 249); // Slate-100
        private Color colorBotonActivo = Color.FromArgb(59, 130, 246);    // Blue-500
        private Color colorBotonTextoActivo = Color.White;
        private Color colorBotonInactivo = Color.White;
        private Color colorBotonTextoInactivo = Color.FromArgb(51, 65, 85); // Slate-700


        private void ConfigurarUI()
        {
            // Configuración del Formulario Principal
            this.Text = "Simulación";
            this.Size = new Size(900, 600); 
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = colorFondoContenido;

            // Panel Izquierdo (Menú)
            panelMenu = new FlowLayoutPanel();
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Width = 250;
            panelMenu.BackColor = colorFondoMenu;
            panelMenu.FlowDirection = FlowDirection.TopDown;
            panelMenu.Padding = new Padding(10, 10, 10, 10); 

            // Título del Menú
            lblTitulo = new Label();
            lblTitulo.Text = "Menú";
            lblTitulo.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitulo.ForeColor = colorBotonTextoInactivo;
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(20, 20);
            
            lblTitulo.Parent = panelMenu; 
            this.Controls.Add(lblTitulo);
            lblTitulo.BringToFront();

           
            // Contenedor de Formas
            panelContenido = new Panel();
            panelContenido.Dock = DockStyle.Fill;
            panelContenido.BackColor = colorFondoContenido;
           

            // panelMenu sale encima de panelContenido
            this.Controls.Add(panelContenido);
            this.Controls.Add(panelMenu); 

        }

        private void GenerarMenu()
        {
            panelMenu.Controls.Clear();
            listaBotones.Clear();

            // Espaciador inicial para bajar los botones si usamos FlowLayout
            Label espaciador = new Label();
            espaciador.AutoSize = false;
            espaciador.Height = 40;
            panelMenu.Controls.Add(espaciador);

            for (int i = 0; i < menuItems.Length; i++)
            {
                Button btn = new Button();
                btn.Text = menuItems[i];
                btn.Tag = i;

                // Estilos visuales
                btn.Size = new Size(230, 50);
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Font = new Font("Segoe UI", 11, FontStyle.Regular);
                btn.Margin = new Padding(0, 0, 0, 10);
                btn.Cursor = Cursors.Hand;
                btn.TextAlign = ContentAlignment.MiddleLeft;
                btn.Padding = new Padding(20, 0, 0, 0); 

                btn.Click += Boton_Click;

                panelMenu.Controls.Add(btn);
                listaBotones.Add(btn);
            }
        }

        private void Boton_Click(object sender, EventArgs e)
        {
            Button btnClickeado = (Button)sender;
            int indice = (int)btnClickeado.Tag;

            if (indiceActivo != indice)
            {
                ActualizarEstiloBotones(indice);

                Form forma = ObtenerFormaPorIndice(indice);
                
                AbrirFormularioEnPanel(forma);
            }
        }

        private void ActualizarEstiloBotones(int nuevoIndice)
        {
            indiceActivo = nuevoIndice;

            for (int i = 0; i < listaBotones.Count; i++)
            {
                Button btn = listaBotones[i];
                if (i == indiceActivo)
                {
                    btn.BackColor = colorBotonActivo;
                    btn.ForeColor = colorBotonTextoActivo;
                    btn.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                }
                else
                {
                    btn.BackColor = colorBotonInactivo;
                    btn.ForeColor = colorBotonTextoInactivo;
                    btn.Font = new Font("Segoe UI", 11, FontStyle.Regular);
                }
            }
        }

        // Abrir un formulario dentro del panel de contenido
        private void AbrirFormularioEnPanel(Form formularioHijo)
        {
            // 1. Si ya hay uno abierto, cerrarlo
            if (formularioActivo != null)
            {
                formularioActivo.Close();
            }

            formularioActivo = formularioHijo;

            // 2. Configurar el formulario para comportarse como un control
            formularioHijo.TopLevel = false;
            formularioHijo.FormBorderStyle = FormBorderStyle.None;
            formularioHijo.Dock = DockStyle.Fill;

            // 3. Agregarlo al panel y mostrarlo
            panelContenido.Controls.Add(formularioHijo);
            panelContenido.Tag = formularioHijo;
            formularioHijo.BringToFront();
            formularioHijo.Show();
        }

        // Helper para retornar formas
        private Form ObtenerFormaPorIndice(int index)
        {
            try
            {
                Form[] formas = {
                    new FormaGenerarNumerosAleatorios(),
                    new FormaPrueba1(),
                    new FormaPrueba2(),
                    new FormaAplicacionDeSimulacion(),
                    new FormaDatosAlumnos()

                };
                return formas[index];
            }
            catch (Exception)
            {
                return new Form(); 
            }

        }

        // Boilerplate de Windows Forms
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormaMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "FormaMenu";
            this.Text = " ";
            this.ResumeLayout(false);

        }
    }
}

