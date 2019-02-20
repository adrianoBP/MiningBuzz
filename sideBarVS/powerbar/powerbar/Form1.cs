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

namespace powerbar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int iMax = -300;
        int iMin = 300;
        int iNow = 0;
        int incrementor = 1;
        int minPerc = 50;
        int maxPerc = 80;
        private void trackBar1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (iNow > (minPerc * iMax) / 100 && iNow < (maxPerc * iMax) / 100)
                {
                    button1.BackColor = Color.Green;
                    tMove.Abort();
                    tMove.Join();
                }
                else
                {
                    button1.BackColor = Color.Red;
                    if(incrementor<32)
                        incrementor *= 2;
                }
            }
        }

        Thread tMove;
        private void button1_Click(object sender, EventArgs e)
        {
                tMove = new Thread(new ThreadStart(move));
                tMove.Start();
                trackBar1.Focus();
        }
        bool positive = true;
        void move()
        {
            while (true)
            {
                trackBar1.Value = iNow;

                if (iNow + incrementor >= iMax)
                    positive = false;
                else if (iNow - incrementor <= iMin)
                    positive = true;
                if (positive)
                    iNow+=incrementor;
                else
                    iNow -= incrementor;
                
                Thread.Sleep(5);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            trackBar1.Maximum = iMax;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            tMove.Abort();
            tMove.Join();
        }
    }
}
