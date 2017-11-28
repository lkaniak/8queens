using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace _8queens
{
    public partial class Form1 : Form
    {
        private List<Pair<string, bool[,]>> resultList1 = new List<Pair<string, bool[,]>>();
        private List<Pair<string, bool[,]>> resultList2 = new List<Pair<string, bool[,]>>();
        private List<Pair<string, bool[,]>> resultList3 = new List<Pair<string, bool[,]>>();
        private List<Pair<string, bool[,]>> resultList4 = new List<Pair<string, bool[,]>>();

        private double[] times = new double[3];
        //private bool[,] resultados = new bool[8, 8];
        private int size = 8;
        private int iteracoesParalelismo = 0;

        private SemaphoreSlim mutex;
        private SemaphoreSlim mutexBarreira;
        private SemaphoreSlim barreira;
        private SemaphoreSlim paralelo;
        private SemaphoreSlim it = new SemaphoreSlim(1, 1);

        private string textLabel1 = "";
        private string textLabel2 = "";
        private string textLabel3 = "";
        private string textLabel4 = "";

        public Form1()
        {
            InitializeComponent();
            this.textLabel1 = this.label1.Text.ToString();
            this.textLabel2 = this.label2.Text.ToString();
            this.textLabel3 = this.label3.Text.ToString();
            this.textLabel4 = this.label4.Text.ToString();
        }

        private void setSemaphores()
        {
            this.barreira = new SemaphoreSlim(0);
            this.mutex = new SemaphoreSlim(1, 1);
            this.mutexBarreira = new SemaphoreSlim(1, 1);
            this.paralelo = new SemaphoreSlim(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            this.listBox2.Items.Clear();
            this.listBox3.Items.Clear();
            this.listBox4.Items.Clear();

            this.resultList1.Clear();
            this.resultList2.Clear();
            this.resultList3.Clear();
            this.resultList4.Clear();


            this.setSemaphores();
            double t0 = Environment.TickCount;
            double t1 = 0.0;

            //this.it.Wait();
            this.callThreads(1);
            //this.it.Release();
            t1 = (double)(Environment.TickCount - t0);
            this.times[0] = t1 / 1000;

            this.setSemaphores();
            t0 = Environment.TickCount;
            //this.it.Wait();
            this.callThreads(2);
            //this.it.Release();
            t1 = (double)(Environment.TickCount - t0);
            this.times[1] = t1 / 1000;

            this.setSemaphores();
            t0 = Environment.TickCount;
            //this.it.Wait();
            this.callThreads(4);
            //this.it.Release();
            t1 = (double)(Environment.TickCount - t0);
            this.times[2] = t1 / 1000;


            this.label1.Text = this.textLabel1 + ": " + this.resultList1.Count.ToString();
            this.label2.Text = this.textLabel2 + ": " + this.resultList2.Count.ToString();
            this.label3.Text = this.textLabel3 + ": " + this.resultList3.Count.ToString();
            this.label4.Text = this.textLabel4 + ": " + this.resultList4.Count.ToString();

            for (int i = 0; i < this.resultList1.Count; i++)
            {
                this.listBox1.Items.Add(this.resultList1[i].first);
            }

            for (int i = 0; i < this.resultList2.Count; i++)
            {
                this.listBox2.Items.Add(this.resultList2[i].first);
            }

            for (int i = 0; i < this.resultList3.Count; i++)
            {
                this.listBox3.Items.Add(this.resultList3[i].first);
            }

            for (int i = 0; i < this.resultList4.Count; i++)
            {
                this.listBox4.Items.Add(this.resultList4[i].first);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string currentItem = this.listBox1.SelectedItem.ToString();
            bool[,] r = this.getResultMatrix(currentItem, 1);

            Result result = new Result(r);
            result.Text = result.Text + " " + currentItem;
            result.Show();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string currentItem = this.listBox2.SelectedItem.ToString();
            bool[,] r = this.getResultMatrix(currentItem, 2);

            Result result = new Result(r);
            result.Text = result.Text + " " + currentItem;
            result.Show();
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string currentItem = this.listBox3.SelectedItem.ToString();
            bool[,] r = this.getResultMatrix(currentItem, 3);

            Result result = new Result(r);
            result.Text = result.Text + " " + currentItem;
            result.Show();
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            string currentItem = this.listBox4.SelectedItem.ToString();
            bool[,] r = this.getResultMatrix(currentItem, 4);

            Result result = new Result(r);
            result.Text = result.Text + " " + currentItem;
            result.Show();
        }

        private bool[,] getResultMatrix(string item, int list)
        {
            if (list == 1)
            {
                for (int i = 0; i < this.resultList1.Count; i++)
                {
                    if (this.resultList1[i].first == item)
                    {
                        return this.resultList1[i].second;
                    }
                }
            }
            else if (list == 2)
            {
                for (int i = 0; i < this.resultList2.Count; i++)
                {
                    if (this.resultList2[i].first == item)
                    {
                        return this.resultList2[i].second;
                    }
                }
            }
            else if (list == 3)
            {
                for (int i = 0; i < this.resultList3.Count; i++)
                {
                    if (this.resultList3[i].first == item)
                    {
                        return this.resultList3[i].second;
                    }
                }
            }
            else if (list == 4)
            {
                for (int i = 0; i < this.resultList4.Count; i++)
                {
                    if (this.resultList4[i].first == item)
                    {
                        return this.resultList4[i].second;
                    }
                }
            }

            return new bool[8, 8];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.listBox1.Items.Count > 0 && this.listBox2.Items.Count > 0 && this.listBox4.Items.Count > 0)
            {
                SpeedUp speedUp = new SpeedUp(this.times.ToArray());
                speedUp.Show();
            }
            else
            {
                MessageBox.Show(this, "Gere os resultados antes do cálculo do SpeedUp", "ERRO", MessageBoxButtons.OK);
            }
        }

        private bool cheque(int x, int y, bool[,] resultados)
        {
            return chequeDiagonal(x, y, resultados) || chequeHorizontal_Vertical(x, y, resultados);
        }

        private bool chequeDiagonal(int x, int y, bool[,] resultados)
        {
            /* checar / diagonal */
            //|        | b[x-i,j+i] |
            //| b[x,y] |            |
            for (int i = 1; x - i >= 0 && y + i < size; i++)
            {
                if (resultados[x - i, y + i])
                {
                    return true;
                }
            }
            /* checar / diagonal */
            //|            | b[x,y] |
            //| b[x+i,j-i] |        |
            for (int i = 1; x + i < size && y - i >= 0; i++)
            {
                if (resultados[x + i, y - i])
                {
                    return true;
                }
            }
            /* checar \ diagonal */
            //| b[x-i,y-i] |        |
            //|            | b[x,y] |
            for (int i = 1; x - i >= 0 && y - i >= 0; i++)
            {
                if (resultados[x - i, y - i])
                {
                    return true;
                }

            }
            /* checar \ diagonal */
            //| b[x,y] |            |
            //|        | b[x+i,y+i] |
            for (int i = 1; x + i < size && y + i < size; i++)
            {

                if (resultados[x + i, y + i])
                {
                    return true;
                }

            }
            return false;
        }

        private bool chequeHorizontal_Vertical(int x, int y, bool[,] resultados)
        {
            /* checar horizontal */
            for (int i = 0; i < size; i++)
            {
                if (i == x) { continue; }
                else
                {
                    if (resultados[i, y]) { return true; }
                }
            }

            /* checar vertical */
            for (int i = 0; i < size; i++)
            {
                if (i == y) { continue; }
                else
                {
                    if (resultados[x, i]) { return true; }
                }
            }

            return false;
        }

        private bool solve(int pos, bool[,] resultados, int numberOfThreads)
        {
            for (int j = 0; j < size; j++)
            {
                if (!cheque(pos, j, resultados))
                {
                    resultados[pos, j] = true;
                    if (pos == size - 1)
                    {
                        //achou um resultado !!
                        if (numberOfThreads == 2)
                        {
                            this.mutex.Wait();
                            this.resultList1.Add(new Pair<string, bool[,]>("Resultado " + (this.resultList1.Count + 1).ToString() +
                                "  dois cores", (bool[,])resultados.Clone()));
                            this.mutex.Release();
                        }
                        else if (numberOfThreads == 4)
                        {
                            this.mutex.Wait();
                            this.resultList2.Add(new Pair<string, bool[,]>("Resultado " + (this.resultList2.Count + 1).ToString() +
                                "  quatro cores", (bool[,])resultados.Clone()));
                            this.mutex.Release();
                        }
                        else if (numberOfThreads == 8)
                        {
                            this.mutex.Wait();
                            this.resultList3.Add(new Pair<string, bool[,]>("Resultado " + (this.resultList3.Count + 1).ToString() +
                                "  oito cores", (bool[,])resultados.Clone()));
                            this.mutex.Release();
                        }
                        else if (numberOfThreads == 1)
                        {
                            this.mutex.Wait();
                            this.resultList4.Add(new Pair<string, bool[,]>("Resultado " + (this.resultList4.Count + 1).ToString() +
                                "  serial", (bool[,])resultados.Clone()));
                            this.mutex.Release();
                        }

                        resultados[pos, j] = false;
                        return true;
                    }
                    else
                    {
                        solve(pos + 1, resultados, numberOfThreads);
                        resultados[pos, j] = false; //backtracking
                    }
                }
            }
            return false;
        }

        private void acharSolucoes(int comeco, int fim, bool[,] resultados, int number)
        {
            for (int col = comeco; col < fim; col++)
            {
                resultados[0, col] = true; //primeira rainha inserida
                solve(1, resultados, number);
                resultados[0, col] = false;
            }
            this.mutexBarreira.Wait();
            this.iteracoesParalelismo++;
            if(iteracoesParalelismo == size/(fim - comeco))
            {
                this.barreira.Release();
            }
            this.mutexBarreira.Release();
            this.paralelo.Release();
        }

        private void callThreads(int numberOfThreads)
        {
            int comeco = 0;
            int fim = 0;

            int intervalo = size / numberOfThreads;
            fim = intervalo;

            for (int i = 0; i < numberOfThreads; i++)
            {
                new Thread(() => this.acharSolucoes(comeco, fim, new bool[8, 8], numberOfThreads)).Start();
                this.paralelo.Wait();
                if (fim < size)
                {
                    comeco = fim;
                    fim += intervalo;
                }
            }
            this.barreira.Wait();


            this.iteracoesParalelismo = 0;
            this.paralelo.Dispose();
            this.barreira.Dispose();
            this.mutex.Dispose();
            this.mutexBarreira.Dispose();              
        }
    }
}
