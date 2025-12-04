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
        private Label lblMateria;

        // Colores (Mismo tema visual que las otras formas)
        private readonly Color colorFondo = Color.FromArgb(239, 246, 255);
        private readonly Color colorTextoTitulo = Color.FromArgb(49, 46, 129);
        private readonly Color colorTextoNormal = Color.FromArgb(30, 41, 59); 



        private void InitializeComponent()
        {
            this.lblTitulo = new Label();
            this.lblAlumno1 = new Label();
            this.lblAlumno2 = new Label();
            this.lblAlumno3 = new Label();
            this.lblAlumno4 = new Label();
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
            ConfigurarLabelAlumno(out lblAlumno1, "Ángel Fabián Pérez Mayen - 24210515", 150);

            // Alumno 2
            ConfigurarLabelAlumno(out lblAlumno2, "Sanchez Resendiz Braulio - 24210531", 230);

            // Alumno 3
            ConfigurarLabelAlumno(out lblAlumno3, "Barraza Sánchez Luz del Carmen - 24210471", 310);

            // Alumno 4
            ConfigurarLabelAlumno(out lblAlumno4, "Lepe Garcia Cesar - C22212360", 390);

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
            this.Controls.Add(lblMateria);

            this.ResumeLayout(false);
        }

        // Método auxiliar para configurar las etiquetas de nombres repetitivas
        private void ConfigurarLabelAlumno(out Label lbl, string texto, int topPosition)
        {
            lbl = new Label();
            lbl.Text = texto;
            lbl.Location = new Point(0, topPosition); // X=0 para usar el ancho completo y centrar
            lbl.Size = new Size(700, 80);
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.Font = new Font("Segoe UI", 14F, FontStyle.Regular);
            lbl.ForeColor = colorTextoNormal;
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