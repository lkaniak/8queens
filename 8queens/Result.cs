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
    public partial class Result : Form
    {
        private PictureBox[,] tabuleiro = new PictureBox[8, 8];
        private bool[,] resultados = new bool[8, 8];
        private SemaphoreSlim mutexParallel = new SemaphoreSlim(0);

        public Result(bool[,] resultados)
        {
            InitializeComponent();
            this.resultados = resultados;
            this.loadTab();
            this.printTab();
        }

        private void loadTab()
        {
            //primeira linha
            this.tabuleiro[0, 0] = this.p1;
            this.tabuleiro[0, 1] = this.p2;
            this.tabuleiro[0, 2] = this.p3;
            this.tabuleiro[0, 3] = this.p4;
            this.tabuleiro[0, 4] = this.p5;
            this.tabuleiro[0, 5] = this.p6;
            this.tabuleiro[0, 6] = this.p7;
            this.tabuleiro[0, 7] = this.p8;
            //segunda linha
            this.tabuleiro[1, 0] = this.p9;
            this.tabuleiro[1, 1] = this.p10;
            this.tabuleiro[1, 2] = this.p11;
            this.tabuleiro[1, 3] = this.p12;
            this.tabuleiro[1, 4] = this.p13;
            this.tabuleiro[1, 5] = this.p14;
            this.tabuleiro[1, 6] = this.p15;
            this.tabuleiro[1, 7] = this.p16;
            //terceira linha
            this.tabuleiro[2, 0] = this.p17;
            this.tabuleiro[2, 1] = this.p18;
            this.tabuleiro[2, 2] = this.p19;
            this.tabuleiro[2, 3] = this.p20;
            this.tabuleiro[2, 4] = this.p21;
            this.tabuleiro[2, 5] = this.p22;
            this.tabuleiro[2, 6] = this.p23;
            this.tabuleiro[2, 7] = this.p24;
            //quarta linha
            this.tabuleiro[3, 0] = this.p25;
            this.tabuleiro[3, 1] = this.p26;
            this.tabuleiro[3, 2] = this.p27;
            this.tabuleiro[3, 3] = this.p28;
            this.tabuleiro[3, 4] = this.p29;
            this.tabuleiro[3, 5] = this.p30;
            this.tabuleiro[3, 6] = this.p31;
            this.tabuleiro[3, 7] = this.p32;
            //quinta linha
            this.tabuleiro[4, 0] = this.p33;
            this.tabuleiro[4, 1] = this.p34;
            this.tabuleiro[4, 2] = this.p35;
            this.tabuleiro[4, 3] = this.p36;
            this.tabuleiro[4, 4] = this.p37;
            this.tabuleiro[4, 5] = this.p38;
            this.tabuleiro[4, 6] = this.p39;
            this.tabuleiro[4, 7] = this.p40;
            //sexta linha
            this.tabuleiro[5, 0] = this.p41;
            this.tabuleiro[5, 1] = this.p42;
            this.tabuleiro[5, 2] = this.p43;
            this.tabuleiro[5, 3] = this.p44;
            this.tabuleiro[5, 4] = this.p45;
            this.tabuleiro[5, 5] = this.p46;
            this.tabuleiro[5, 6] = this.p47;
            this.tabuleiro[5, 7] = this.p48;
            //setima linha
            this.tabuleiro[6, 0] = this.p49;
            this.tabuleiro[6, 1] = this.p50;
            this.tabuleiro[6, 2] = this.p51;
            this.tabuleiro[6, 3] = this.p52;
            this.tabuleiro[6, 4] = this.p53;
            this.tabuleiro[6, 5] = this.p54;
            this.tabuleiro[6, 6] = this.p55;
            this.tabuleiro[6, 7] = this.p56;
            //oitava linha
            this.tabuleiro[7, 0] = this.p57;
            this.tabuleiro[7, 1] = this.p58;
            this.tabuleiro[7, 2] = this.p59;
            this.tabuleiro[7, 3] = this.p60;
            this.tabuleiro[7, 4] = this.p61;
            this.tabuleiro[7, 5] = this.p62;
            this.tabuleiro[7, 6] = this.p63;
            this.tabuleiro[7, 7] = this.p64;

            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    this.tabuleiro[i, j].BackColor = Color.White;
                    this.tabuleiro[i, j].BorderStyle = BorderStyle.FixedSingle;
                }
            }
        }

        private void changeCordenateColor(Color color, int x, int y)
        {
            this.tabuleiro[x, y].BackColor = color;
        }

        private void printTab()
        {
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    if(this.resultados[i, j])
                    {
                        this.changeCordenateColor(Color.Black, i, j);
                    }
                }
            }
        }
    }
}
