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
        List<int> LConsumo = new List<int>();
        private Random random,randomP;
        private MPrioridad runPrioridad;
        int quantum = 2;
        String[] Colors = { "#E3A21A", "#7e3878", "#1e7145", "#000000", "#603cba", "#00aba9", "#2d89ef", "#2b5797", "#ffc40d", "#da532c"};

        public MainForm()
        {
            InitializeComponent();
            procesos = new LinkedList<Proceso>();
            random = new Random();
            randomP = new Random();
            process = Process.GetProcesses();
            cargarProcesos();
        }

        private void cargarProcesos()
        {
            int tiempo,memoria;
            for (int i = 0; i < 8; i++)
            {
                tiempo = random.Next(3, 5);
                memoria = random.Next(20, 100);

                Proceso proceso = new Proceso(process[i].Id, process[i].ProcessName, tiempo, memoria, Colors[i]);
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
            string memoria = proceso.Memoria.ToString();
            string[] row = { id, nombre, estado, tiempo, memoria };
            dataGridViewProcesos.Rows.Add(row);
        }

        private void IniciarPrioridad()
        {
            Proceso[] arrProcesos = procesos.ToArray();
            //runPrioridad = new MPrioridad(ref dataGridViewProcesos);
            //runPrioridad.runPrioridad(ref arrProcesos, quantum);
            runPrioridad = new MPrioridad(ref dataGridViewProcesos, ref pictureBox1, ref pictureBox2, ref pictureBox3, ref pictureBox4, ref pictureBox5, ref pictureBox6, ref pictureBox7, ref pictureBox8, ref pictureBox9, ref pictureBox10, ref pictureBox11, ref pictureBox12, ref pictureBox13, ref pictureBox14, ref pictureBox15, ref pictureBox16);
            runPrioridad.runPrioridad(ref arrProcesos, quantum);
        }
        private void buttonCorrer_Click(object sender, EventArgs e)
        {
            IniciarPrioridad();
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
        }
        private void reseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {
        }
        public void executionTimers(int tempTime)
        {
            int executionTime = tempTime * 500;
            System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
            if (executionTime == 0 || executionTime < 0)
            {
                return;
            }
            timer1.Interval = executionTime;
            timer1.Enabled = true;
            timer1.Start();
            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
            };
            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }
    }
}
