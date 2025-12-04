using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Simulacion_Final
{
    public class SimuladorElectoral
    {
        private List<double> _numerosAleatorios;
        private int _indiceActual = 0;

        // Configuración del problema (Acciones y Probabilidades Acumuladas)
        // 1. Empleo 0.17
        // 2. Seguridad 0.23 -> 0.40
        // 3. Comercio 0.10 -> 0.50
        // 4. Servicios 0.17 -> 0.67
        // 5. Honestidad 0.19 -> 0.86
        // 6. Educación 0.14 -> 1.00
        private readonly List<(string Nombre, double Limite)> _tablaProbabilidad = new List<(string, double)>
        {
            ("Creación y protección del empleo", 0.17),
            ("Atención a la seguridad", 0.40),
            ("Impulso al comercio y turismo", 0.50),
            ("Prestación de servicios", 0.67),
            ("Honestidad de los servidores", 0.86),
            ("Atención a la educación", 1.00)
        };

        public SimuladorElectoral(List<double> numeros)
        {
            _numerosAleatorios = numeros;
        }

        // Método auxiliar para obtener el siguiente número. 
        // Si se acaban, recicla desde el inicio (o podrías generar más aquí).
        private double SiguienteRandom()
        {
            if (_indiceActual >= _numerosAleatorios.Count)
                _indiceActual = 0;

            return _numerosAleatorios[_indiceActual++];
        }

        private string ObtenerAccionPorProbabilidad(double r)
        {
            foreach (var item in _tablaProbabilidad)
            {
                if (r < item.Limite)
                    return item.Nombre;
            }
            return _tablaProbabilidad.Last().Nombre; // Fallback por redondeo
        }

        public string EjecutarSimulacion()
        {
            StringBuilder reporte = new StringBuilder();
            int poblacionTotal = 200000;

            // Definición de sectores (Porcentaje, ID)
            var sectores = new[]
            {
                new { Id = 1, Pct = 0.16 },
                new { Id = 2, Pct = 0.24 },
                new { Id = 3, Pct = 0.20 },
                new { Id = 4, Pct = 0.15 },
                new { Id = 5, Pct = 0.25 }
            };

            foreach (var sector in sectores)
            {
                // 1. Reiniciar contadores para este sector
                var accionesDelSector = _tablaProbabilidad
                    .Select(x => new AccionPolitica(x.Nombre, x.Limite))
                    .ToList();

                // 2. Calcular Muestra
                int poblacionSector = (int)(poblacionTotal * sector.Pct);
                int muestra = (int)(poblacionSector * 0.05);

                // 3. Simular Votantes
                for (int i = 0; i < muestra; i++)
                {
                    List<string> elegidas = new List<string>();

                    // Elegir 3 acciones distintas
                    while (elegidas.Count < 3)
                    {
                        double r = SiguienteRandom();
                        string accionCandidata = ObtenerAccionPorProbabilidad(r);

                        if (!elegidas.Contains(accionCandidata))
                        {
                            elegidas.Add(accionCandidata);
                        }
                    }

                    // Asignar Puntos (3, 2, 1)
                    accionesDelSector.First(a => a.Nombre == elegidas[0]).PuntosAcumulados += 3;
                    accionesDelSector.First(a => a.Nombre == elegidas[1]).PuntosAcumulados += 2;
                    accionesDelSector.First(a => a.Nombre == elegidas[2]).PuntosAcumulados += 1;
                }

                // 4. Ordenar resultados de Mayor a Menor puntaje
                var ranking = accionesDelSector.OrderByDescending(a => a.PuntosAcumulados).ToList();

                // -------------------------------------------------------------
                // 5. NUEVA GENERACIÓN DE REPORTE (Sin descartes)
                // -------------------------------------------------------------

                reporte.AppendLine($"========================================");
                reporte.AppendLine($"SECTOR {sector.Id} (Muestra: {muestra} personas)");
                reporte.AppendLine($"========================================");

                // A. Mostrar la ganadora absoluta
                reporte.AppendLine($" LA MÁS VOTADA: {ranking[0].Nombre}");
                reporte.AppendLine($"   (Puntaje Total: {ranking[0].PuntosAcumulados} pts)");
                reporte.AppendLine(""); // Espacio

                // B. Estrategia Corto Plazo (Top 3)
                reporte.AppendLine("ESTRATEGIA A CORTO PLAZO (Prioridad Alta):");
                for (int k = 0; k < 3; k++)
                {
                    reporte.AppendLine($"   #{k + 1}. {ranking[k].Nombre} ({ranking[k].PuntosAcumulados} pts)");
                }
                reporte.AppendLine("");

                // C. Estrategia Largo Plazo (Las 3 restantes, sin descartar ninguna)
                reporte.AppendLine("ESTRATEGIA A LARGO PLAZO / COMPLEMENTARIA:");
                // Recorremos desde el índice 3 hasta el final de la lista (Count)
                for (int k = 3; k < ranking.Count; k++)
                {
                    reporte.AppendLine($"   #{k + 1}. {ranking[k].Nombre} ({ranking[k].PuntosAcumulados} pts)");
                }

                reporte.AppendLine("");
                reporte.AppendLine("");
            }

            return reporte.ToString();
        }
    }


    public class AccionPolitica
    {
        public string Nombre { get; set; }
        public double ProbabilidadLimiteSuperior { get; set; } // Acumulada
        public int PuntosAcumulados { get; set; } = 0;

        public AccionPolitica(string nombre, double probAcumulada)
        {
            Nombre = nombre;
            ProbabilidadLimiteSuperior = probAcumulada;
        }
    }

    public class ResultadoSector
    {
        public int IdSector { get; set; }
        public string NombreSector { get; set; }
        public List<AccionPolitica> Ranking { get; set; }
    }


    public partial class FormaAplicacionDeSimulacion : Form
    {
        // --- COMPONENTES VISUALES ---
        private System.ComponentModel.IContainer components = null;
        private Label lblTitulo;
        private Button btnSimular;
        private Label lblEstado;
        private RichTextBox txtResultados;

        // --- GESTOR DE LÓGICA ---
        private GestorArchivos _gestorArchivos;

        // --- COLORES ---
        private readonly Color colorFondo = Color.FromArgb(239, 246, 255);
        private readonly Color colorTextoTitulo = Color.FromArgb(49, 46, 129);
        private readonly Color colorBoton = Color.FromArgb(59, 130, 246);
        private readonly Color colorBotonHover = Color.FromArgb(55, 48, 163);



        // --- MÉTODO DE VALIDACIÓN ESTADÍSTICA (Prueba de Promedios) ---
        private bool ValidarUniformidad(List<double> numeros)
        {
            // Usaremos un Nivel de Confianza del 95% (Estándar)
            // Z para 95% = 1.96
            double z = 1.96;
            int n = numeros.Count;

            // 1. Calcular Promedio
            double promedio = numeros.Sum() / n;

            // 2. Calcular Límites de Aceptación
            // Fórmula: Media (0.5) +/- Z * (1 / Raíz(12*n))
            double error = z * (1.0 / Math.Sqrt(12.0 * n));
            double limiteInferior = 0.5 - error;
            double limiteSuperior = 0.5 + error;

            // 3. Verificar
            bool pasoLaPrueba = (promedio >= limiteInferior && promedio <= limiteSuperior);

            if (!pasoLaPrueba)
            {
                MessageBox.Show($"¡ALERTA ESTADÍSTICA!\n\n" +
                                $"Los números generados NO pasan la prueba de uniformidad.\n" +
                                $"Promedio: {promedio:F5}\n" +
                                $"Rango aceptado: {limiteInferior:F5} - {limiteSuperior:F5}\n\n" +
                                $"Se recomienda borrar el archivo JSON y generar nuevos números.",
                                "Fallo de Uniformidad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return pasoLaPrueba;
        }

        // --- EVENTO PRINCIPAL ---
        private void BtnSimular_Click(object sender, EventArgs e)
        {
            DatosSimulacion datos = null;

            try
            {
                // A. CARGA DE DATOS
                if (_gestorArchivos.TieneValores())
                {
                    lblEstado.Text = "Cargando archivo...";
                    datos = _gestorArchivos.CargarNumerosDesdeArchivo();
                }

                // B. GENERACIÓN (Si no existen)
                if (datos == null || datos.ListaNumeros == null || datos.ListaNumeros.Count == 0)
                {
                    lblEstado.Text = "Generando números...";
                    // Generar 50,000 números
                    long x0 = 12345;
                    long a = 1664525;
                    long c = 1013904223;
                    long m = (long)Math.Pow(2, 32);

                    var generador = new GeneradorNumerosPseudoaleatorios();
                    var numeros = generador.GenerarNumeros(x0, a, c, m, 50000);
                    datos = numeros;
                    _gestorArchivos.GuardarNumerosEnArchivo(datos);
                }

                // C. VALIDACIÓN DE UNIFORMIDAD (¡NUEVO PASO!)
                lblEstado.Text = "Validando estadística...";
                Application.DoEvents(); // Refrescar pantalla

                if (!ValidarUniformidad(datos.ListaNumeros))
                {
                    lblEstado.Text = "Error: Distribución no uniforme.";
                    lblEstado.ForeColor = Color.Red;
                    // Opcional: ¿Quieres detener la simulación si falla?
                    // return; // Descomenta esto si quieres ser estricto y no dejar simular

                    // Si decides continuar bajo riesgo:
                    txtResultados.Text = "ADVERTENCIA: Los resultados pueden no ser confiables porque los números no pasaron la prueba de promedios.\n\n";
                }
                else
                {
                    txtResultados.Clear();
                }

                // D. SIMULACIÓN 
                lblEstado.Text = "Simulando escenarios...";
                lblEstado.ForeColor = Color.Black;
                Application.DoEvents();

                SimuladorElectoral motor = new SimuladorElectoral(datos.ListaNumeros);

                // Usamos AppendText para no borrar la advertencia si hubo una
                string reporte = motor.EjecutarSimulacion();
                txtResultados.AppendText(reporte);

                lblEstado.Text = "Simulación completada.";
                lblEstado.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }



        // --- INTERFAZ GRÁFICA ---
        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnSimular = new System.Windows.Forms.Button();
            this.lblEstado = new System.Windows.Forms.Label();
            this.txtResultados = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();

            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 650);
            this.Name = "FormaAplicacionDeSimulacion";
            this.Text = "Simulación Electoral";
            this.StartPosition = FormStartPosition.CenterScreen;

            this.lblTitulo.Location = new System.Drawing.Point(0, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(800, 40);
            this.lblTitulo.Text = "Estrategia Electoral";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);

            this.btnSimular.Location = new System.Drawing.Point(40, 80);
            this.btnSimular.Name = "btnSimular";
            this.btnSimular.Size = new System.Drawing.Size(200, 45);
            this.btnSimular.Text = "Ejecutar Simulación";
            this.btnSimular.UseVisualStyleBackColor = true;
            this.btnSimular.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSimular.Click += new System.EventHandler(this.BtnSimular_Click);

            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(260, 93);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(160, 20);
            this.lblEstado.Text = "Esperando ejecución...";

            this.txtResultados.Location = new System.Drawing.Point(40, 150);
            this.txtResultados.Name = "txtResultados";
            this.txtResultados.ReadOnly = true;
            this.txtResultados.Size = new System.Drawing.Size(720, 460);
            this.txtResultados.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtResultados.BackColor = System.Drawing.Color.White;
            this.txtResultados.BorderStyle = System.Windows.Forms.BorderStyle.None;

            this.Controls.Add(this.txtResultados);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.btnSimular);
            this.Controls.Add(this.lblTitulo);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void AplicarEstiloPersonalizado()
        {
            this.BackColor = colorFondo;
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            if (lblTitulo != null) lblTitulo.ForeColor = colorTextoTitulo;
            if (btnSimular != null)
            {
                btnSimular.FlatStyle = FlatStyle.Flat;
                btnSimular.BackColor = colorBoton;
                btnSimular.ForeColor = Color.White;
                btnSimular.FlatAppearance.BorderSize = 0;
                btnSimular.MouseEnter += (s, e) => btnSimular.BackColor = colorBotonHover;
                btnSimular.MouseLeave += (s, e) => btnSimular.BackColor = colorBoton;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }
    }
}
