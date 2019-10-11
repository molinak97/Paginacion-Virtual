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
            int tiempo, prioridad,consumo,j;
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
                tiempo = random.Next(2, 5);
                prioridad = random.Next(1, 4);
                consumo = random.Next(10,100);
                Proceso proceso = new Proceso(process[i].Id, process[i].ProcessName, tiempo,prioridad,consumo);
                procesos.AddLast(proceso);
                agregarProceso(proceso);
                LConsumo.Add(consumo);
            }
        }

        private void agregarProceso(Proceso proceso)
        {
            string id = proceso.Id.ToString();
            string nombre = proceso.Nombre;
            string estado = proceso.Estado;
            string tiempo = proceso.Tiempo.ToString();
            string prioridad = proceso.Prioridad.ToString();
            string consumo = proceso.Consumo.ToString();
            string[] row = {id, nombre, estado, tiempo,prioridad,consumo};
            dataGridViewProcesos.Rows.Add(row);
        }

        private void IniciarPrioridad()
        {
            int quantum = 1;
            Proceso[] arrProcesos = procesos.ToArray();
            //buttonEjecutar.Hide();
            //numericUpDownQuantum.Hide();
            //labelQuantum.Text = quantum.ToString()
            runPrioridad = new MPrioridad(ref dataGridViewProcesos);
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
            MessageBox.Show("Stalling (Pagina: 433)", "Sistemas Operativos");
        }
        private void reseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
            "A cada proceso se le asigna un intervalo de tiempo, llmadado cuanto, durante el cual se le permite ejecutarse. Si el proceso todavia se esta ejecutando al expirar su cuanto, el sistema operativo se apropia del la CPU naturalmente se efectua cuando el proceso se boquea. El round robin es facil de implementar.", "Algoritmo RR");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int i=1;
                //for (int j = 0; j < 15; j++)
                //{
                //        chart1.Series["Consumidor"].Points.AddXY(i, LConsumo[j]);
                //        executionTimers(1);
                //        i++;
                //        chart1.Series["Consumidor"].Points.AddXY(i, LConsumo[j]);
                //        executionTimers(1);
                //}
                for (int j = 0; j < 15; j++)
                {
                    chart1.Series["Consumidor"].Points.AddXY(j, LConsumo[j]);
                    executionTimers(1);
                }
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
