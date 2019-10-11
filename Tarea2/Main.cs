using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
//MessageBox.Show("si entre al run");

namespace Tarea2
{
    public partial class Main : Form
    {
        List<Thread> threads = new List<Thread>();
        Queue<Thread> cola = new Queue<Thread>();
        int counter = 0;

        public Main()
        {
            InitializeComponent();
            for (int x = 1; x < 2; x++)
            {
                int tmp = x;
                Thread.Sleep(100);               
                Thread thread = new Thread(() => Process(tmp));                
                ListViewItem item = new ListViewItem("Proceso #" + x);
                item.SubItems.Add("READY");              
                listview1.Items.Add(item);
                threads.Add(thread);
                cola.Enqueue(thread);
                FIFO();
            }
            if (listview1.Items.Count != 0)
            {
                listview1.Items[0].Selected = true;
                listview1.Select();
            }
        }
        private void btnSuspend_Click(object sender, EventArgs e)
        {
            try
            {
                int x = listview1.FocusedItem.Index;
                threads[x].Suspend();
                listview1.Items[x].SubItems[1].Text = threads[x].ThreadState.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnResume_Click(object sender, EventArgs e)
        {
            try
            {
                int x = listview1.FocusedItem.Index;
                threads[x].Resume();
                listview1.Items[x].SubItems[1].Text = threads[x].ThreadState.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int x = listview1.FocusedItem.Index;
                threads[x].Abort();
                listview1.Items[x].SubItems[1].Text = threads[x].ThreadState.ToString();
                NewThread();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void tsmiCredit_Click(object sender, EventArgs e)
        {
        }
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
        private int Random()
        {
            Random random = new Random();
            int rnd = random.Next(2000, 5000);
            return rnd;
        }
        private void Process(int x)
        {
            Thread.Sleep(Random());
            listview1.BeginInvoke((MethodInvoker)delegate () { listview1.Items[x - 1].SubItems[1].Text = "TERMINATED"; });
            NewThread();
        }
        private void NewThread()
        {
            Thread.Sleep(2000);
            int tmp = listview1.Items.Count + 1;         
            Thread thread = new Thread(() => Process(tmp));
            thread.Start();
            ListViewItem item = new ListViewItem("Proceso #" + tmp);
            item.SubItems.Add("RUNNABLE");
            listview1.BeginInvoke((MethodInvoker)delegate () { listview1.Items.Add(item); });
            threads.Add(thread);
            cola.Enqueue(thread);
           // FIFO();
        }
        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "El algoritmo de planificación:  Primero en llegar primero en servirse \n (FCFS, First - come, First - served)," +
                " también llamada primero en entrar / primero en salir(FIFO, First -in, First -out).Cada vez que un proceso esté listo para ejecutar," +
                " se incorpora a la cola de Listos.Cuando el proceso actual cesa su ejecución, se selecciona el proceso más antiguo de la cola." +
                "Primero, se determina el tiempo de finalización de cada proceso.A partir de él, es posible determinar el tiempo de retorno." +
                "En términos del modelo de colas, el tiempo de retorno es el tiempo en cola o tiempo total que el elemento consume en el sistema" + 
                "(tiempo de espera más tiempo de servicio).","Algoritmo FCFS");
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            counter++;
            if (counter == 10)
            {
                int x = listview1.FocusedItem.Index;
                listview1.Items[x].SubItems[1].Text = "RUNNABLE";
                timer1.Stop();
                counter = 0;
                MessageBox.Show("si entre al run");
            }
        }
        private void FIFO()
        {
            cola.Dequeue().Start();
            //int x = listview1.FocusedItem.Index;
            //listview1.Items[x].SubItems[1].Text = "RUNNABLE";
            Console.WriteLine("DESENCOLO PROCESO ---- RUNNIG");
        }
        private void materiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
            "Seminario de Solución de Problemas de Sistemas Operativos\n","Materia:");
        }
        private void alumnoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
            "Molina Villanueva Kevin Isrrael\n","Alumno:");
        }
        private void maestroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
            "Quintanilla Moreno Francisco Javier\n","Maestro:");
        }
    }
}
