using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;	
using System.Diagnostics;


namespace SimuladorProcesos
{
    public partial class MainForm : Form
    {
        private Process[] process;
        private LinkedList<Proceso> procesos;
        private Random random;
        private RoundRobin roundRobin;

        public MainForm()
        {
            InitializeComponent();
            procesos = new LinkedList<Proceso>();
            random = new Random();
            process = Process.GetProcesses();
            cargarProcesos();
            buttonBloquear.Hide();
            buttonTerminar.Hide();
        }

        private void cargarProcesos()
        {
            int tiempo;
            /* Carga todo lo procesos*/
            //foreach (Process p in process)
            //{
            //    tiempo = random.Next(2, 8);
            //    Proceso proceso = new Proceso(p.Id, p.ProcessName, tiempo);
            //    procesos.AddLast(proceso);
            //    agregarProceso(proceso);
            //}
            /*Carga solo 15*/
            for (int i = 0; i < 15; i++)
            {
                tiempo = random.Next(2, 30);
                Proceso proceso = new Proceso(process[i].Id, process[i].ProcessName, tiempo);
                procesos.AddLast(proceso);
                agregarProceso(proceso);
            }
        }

        private void agregarProceso(Proceso proceso)
        {
            string id = proceso.Id.ToString();
            string nombre = proceso.Nombre;
            string estado = proceso.Estado;
            string tiempo = proceso.Tiempo.ToString();
            string[] row = {id, nombre, estado, tiempo};
            dataGridViewProcesos.Rows.Add(row);
        }

        private void IniciarRR()
        {
            int quantum = 2;
            Proceso[] arrProcesos = procesos.ToArray();
            //buttonEjecutar.Hide();
            //numericUpDownQuantum.Hide();
            //labelQuantum.Text = quantum.ToString()
            roundRobin = new RoundRobin(ref dataGridViewProcesos);
            roundRobin.runRoundRobin(ref arrProcesos, quantum);
        }
        private void buttonCorrer_Click(object sender, EventArgs e)
        {
            IniciarRR();
        }

        private void buttonSuspender_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonFinalizar_Click(object sender, EventArgs e)
        {
            
        }

        private void materiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
            "Seminario de Solución de Problemas de Sistemas Operativos\n", "Materia:");
        }

        private void alumnoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
            "Molina Villanueva Kevin Isrrael\n", "Alumno:");
        }

        private void maestroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
            "Quintanilla Moreno Francisco Javier\n", "Maestro:");
        }

        private void refernciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("TANENBAUM Andrew (Pagina: 87)", "Sistemas Operativos");
        }

        private void reseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
            "Por ejemplo, consideramos un proceso que necesita calcular continuamente durante 100 cuantos; inicialmente, se le daría un cuanto, y luego se intercambiaría por otro proceso. La siguiente vez, recibiría dos cuantos antes de ser intercambiado. En ocasiones subsecuentes obtendría 4,8,46,32 y 64 cuantos, aunque sólo usaría 37 de los últimos 64 cuantos para completar su trabajo. Sólo se necesitarían 7 intercambios (incluida la carga de inicial) en lugar de 100 si se usara un algoritmo round cada vez con menor frecuencia, guardando la CPU para procesos interactivos cortos.", "Algoritmo Colas Multiples");
        }
    }
}
