using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _8queens
{
    public partial class SpeedUp : Form
    {
        private double[] times;
        private int[] cores = new int[3];

        public SpeedUp(double[] times)
        {
            InitializeComponent();
            this.times = times;

            this.cores[0] = 1;
            this.cores[1] = 2;
            this.cores[2] = 4;

            this.chart1.ChartAreas[0].AxisX.Title = "NÚMERO DE CORES";
            this.chart1.ChartAreas[0].AxisY.Title = "SPEED UP";

            this.chart1.Series[0].LegendText = "SPEED UP";

            this.printChart();
        }

        private void printChart()
        {
            this.chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            this.chart1.Series[0].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            this.chart1.Series[0].MarkerSize = 8;

            this.chart1.ChartAreas[0].AxisX.Minimum = 1;
            this.chart1.ChartAreas[0].AxisY.Minimum = 1;

            this.chart1.Series[0].Points.SuspendUpdates();
            for (int i = 0; i < 3; i++)
            {
                if (i == 0)
                {
                    this.chart1.Series[0].Points.AddXY(1, 1);
                    this.chart1.Series[0].Points[i].ToolTip = "SpeedUp: " + (this.times[0] / this.times[i]).ToString() +
                        "\nTempo de execução: " + this.times[i].ToString() + " seg.";
                }
                else
                {
                    if(this.times[i] == 0)
                    {
                        this.times[i] = 0.001;
                    }

                    this.chart1.Series[0].Points.AddXY(this.cores[i], (this.times[0]/this.times[i]));
                    this.chart1.Series[0].Points[i].ToolTip = "SpeedUp: " + (this.times[0] / this.times[i]).ToString() +
                        "\nTempo de execução: " + this.times[i].ToString() + " seg.";
                }

                this.listBox1.Items.Add(this.cores[i].ToString() + " processador(es):");
                this.listBox1.Items.Add("Tempo: " + this.times[i].ToString());
            }
            this.chart1.Series[0].Points.ResumeUpdates();
        }
    }
}
