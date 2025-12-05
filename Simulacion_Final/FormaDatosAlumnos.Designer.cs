using System;
using System.Drawing;
using System.Windows.Forms;

namespace Simulacion_Final
{
    public partial class FormaDatosAlumnos : Form
    {
        // Componentes
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private Label lblAlumno1;
        private Label lblAlumno2;
        private Label lblAlumno3;
        private Label lblAlumno4;
        private Label lblAlumno5;
        private Label lblMateria;

        // Colores (Mismo tema visual que las otras formas)
        private readonly Color colorFondo = Color.FromArgb(239, 246, 255);
        private readonly Color colorTextoTitulo = Color.FromArgb(49, 46, 129);
        //private readonly Color colorTextoNormal = Color.FromArgb(30, 41, 59); 



        private void InitializeComponent()
        {
            Color colorTextoNormal = Color.FromArgb(30, 41, 59);
            this.lblTitulo = new Label();
            this.lblAlumno1 = new Label();
            this.lblAlumno2 = new Label();
            this.lblAlumno3 = new Label();
            this.lblAlumno4 = new Label();
            this.lblAlumno5 = new Label();
            this.lblMateria = new Label();

            this.SuspendLayout();

            // 
            // Propiedades Base del Formulario
            // 
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(700, 500); // Tamaño estándar
            this.Name = "FormaDatosAlumnos";
            this.Text = "Información del Equipo";

            // 
            // lblTitulo
            // 
            this.lblTitulo.Location = new Point(0, 30);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new Size(700, 70);
            this.lblTitulo.Text = "Integrantes del Equipo";
            this.lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            this.lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);

            // 
            // Etiquetas de Alumnos
            // Utilizamos una función auxiliar (CrearLabelAlumno) o configuración directa para centrarlos
            //

            // Alumno 1
            this.lblAlumno1.Text = "Ángel Fabián Pérez Mayen - 24210515";
            this.lblAlumno1.Location = new Point(0, 80); 
            this.lblAlumno1.Size = new Size(700, 80);
            this.lblAlumno1.TextAlign = ContentAlignment.MiddleCenter;
            this.lblAlumno1.Font = new Font("Segoe UI", 14F, FontStyle.Regular);
            this.lblAlumno1.ForeColor = colorTextoNormal;
                     
            

            // Alumno 2
            this.lblAlumno2.Text = "Sanchez Resendiz Braulio - 24210531";
            this.lblAlumno2.Location = new Point(0, 160); // X=0 para usar el ancho completo y centrar
            this.lblAlumno2.Size = new Size(700, 80);
            this.lblAlumno2.TextAlign = ContentAlignment.MiddleCenter;
            this.lblAlumno2.Font = new Font("Segoe UI", 14F, FontStyle.Regular);
            this.lblAlumno2.ForeColor = colorTextoNormal;

            // Alumno 3
            
            this.lblAlumno3.Text = "Barraza Sánchez Luz del Carmen - 24210471";
            this.lblAlumno3.Location = new Point(0, 240); // X=0 para usar el ancho completo y centrar
            this.lblAlumno3.Size = new Size(700, 80);
            this.lblAlumno3.TextAlign = ContentAlignment.MiddleCenter;
            this.lblAlumno3.Font = new Font("Segoe UI", 14F, FontStyle.Regular);
            this.lblAlumno3.ForeColor = colorTextoNormal;


            // Alumno 4

            this.lblAlumno4.Text = "Lepe Garcia Cesar - C22212360";
            this.lblAlumno4.Location = new Point(0, 320); // X=0 para usar el ancho completo y centrar
            this.lblAlumno4.Size = new Size(700, 80);
            this.lblAlumno4.TextAlign = ContentAlignment.MiddleCenter;
            this.lblAlumno4.Font = new Font("Segoe UI", 14F, FontStyle.Regular);
            this.lblAlumno4.ForeColor = colorTextoNormal;

            // Alumno 5
            this.lblAlumno5.Text = "Murua Ramirez Angel Gerardo - 24210509";
            this.lblAlumno5.Location = new Point(0, 400); // X=0 para usar el ancho completo y centrar
            this.lblAlumno5.Size = new Size(700, 80);
            this.lblAlumno5.TextAlign = ContentAlignment.MiddleCenter;
            this.lblAlumno5.Font = new Font("Segoe UI", 14F, FontStyle.Regular);
            this.lblAlumno5.ForeColor = colorTextoNormal;


            // Materia / Pie de página (Opcional decorativo)
            this.lblMateria.Location = new Point(0, 400);
            this.lblMateria.Size = new Size(700, 30);
            this.lblMateria.Text = "Simulación - Proyecto Final";
            this.lblMateria.TextAlign = ContentAlignment.MiddleCenter;
            this.lblMateria.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            this.lblMateria.ForeColor = Color.Gray;


            // 
            // Agregar controles al formulario
            // 
            this.Controls.Add(lblTitulo);
            this.Controls.Add(lblAlumno1);
            this.Controls.Add(lblAlumno2);
            this.Controls.Add(lblAlumno3);
            this.Controls.Add(lblAlumno4);
            this.Controls.Add(lblAlumno5);
            this.Controls.Add(lblMateria);

            this.ResumeLayout(false);
        }



        private void AplicarEstilo()
        {
            this.BackColor = colorFondo;

            if (lblTitulo != null) lblTitulo.ForeColor = colorTextoTitulo;
        }

        // Limpieza de recursos
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}