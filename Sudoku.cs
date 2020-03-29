using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SudokuClass
{
    public partial class Sudoku : Form
    {
        public double[][] vetEntrada = new double[9][];
        public double[][] vetSaida = new double[9][];
        public double[][][] possible = new double[9][][];
        public string line;
        public string [] multipleLine = new string[9];

        SudokuEngine se = new SudokuEngine();

        public Sudoku()
        {
            InitializeComponent();

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // se.loadEasy(); 

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            loadInput();
            loadListBox(listBox1, vetEntrada);

        }

        private void loadInput()
        {
            for (int x = 0; x < 9; x++)
            {
                vetEntrada[x] = new double[9];
            }

            string s;
            
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    s = "input" + "_0" + x.ToString() + "_0" + y.ToString();
                    if (this.Controls[s].Text == "")
                    {
                        this.Controls[s].Text = "0";
                        vetEntrada[x][y] = 0;
                    }
                    else
                    {
                        vetEntrada[x][y] = Convert.ToDouble(this.Controls[s].Text);
                    }
                }
            }

        }



        private void btnTestLoad_Click(object sender, EventArgs e)
        {

            vetEntrada = se.loadHard(this);
            loadListBox(listBox1,vetEntrada);
        }

        private void btnCalcula_Click(object sender, EventArgs e)
        {
            loadListBox(listBox2,vetEntrada);
        }

        private void loadListBox( ListBox lb, double [][] vetor)
        {
            lb.Items.Clear();

            for (int x = 0; x < 9; x++)
            {
                line = "";

                for (int y = 0; y < 9; y++)
                {
                    if (vetor[x][y] < 10)
                    {
                        line = line + "   0" + vetor[x][y].ToString();
                    }
                    else
                    {
                        line = line + "   " + vetor[x][y].ToString();
                    }

                    // load into data grid view
                    //
                    //dgvInput[x, y].Value = vetor[x][y].ToString(); 

                }
                lb.Items.Add(line);


            }

        }

        private void btnEngine_Click(object sender, EventArgs e)
        {
            vetSaida = se.solver(vetEntrada);
            if (vetSaida[0] != null)
                loadListBox(listBox2,vetSaida);
        }

        private void loadDataGrid()
        {


        }

    }
}