using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SudokuClass
{
    class SudokuEngine
    {
        public double[][] vetEntrada = new double[9][];
        public double[][] vetSaida = new double[9][];
        public double[][][] possible = new double[9][][];
        public string line;

        public double [][] solver( double [][] vetEntrada )
        {

            int p = 0;
            double numerosPossiveis = 0;
            double unicoNumero = 0;
            double primeiroNumero = 0;
            bool continua = true;
            bool deseperoMode = false;
            bool houveMudanca = true;
            int iteractions = 0;
            int desesperoCount = 0;

            while (continua)
            {

                if (vetEntrada[0] == null)
                {
                    continua = false;
                    break;

                }

                iteractions++;

                if (!houveMudanca)
                {
                    if (desesperoCount <= 10)
                    {
                        deseperoMode = true;
                        desesperoCount++;
                        
                    }
                    else
                    {
                        break;
                    }
                }

                #region MainLoop
                continua = false;
                houveMudanca = false;

                for (int x = 0; x < 9; x++)
                {

                    vetSaida[x] = new double[9];
                    possible[x] = new double[9][];

                    for (int y = 0; y < 9; y++)
                    {

                        possible[x][y] = new double[9];

                        if (vetEntrada[x][y] > 0)
                        {
                            p = 0;
                            vetSaida[x][y] = vetEntrada[x][y];
                            possible[x][y][p] = vetEntrada[x][y];
                        }
                        else
                        {
                            continua = true;

                            // Ver quais numeros sao possiveis para cada posicao
                            p = 0;
                            char achei = 'N';

                            // para cada numero
                            #region ParaCadaNumero
                            for (int num = 1; num < 10; num++)
                            {
                                achei = 'N';

                                // procura na coluna
                                for (int coluna = 0; coluna < 9; coluna++)
                                {
                                    if (vetEntrada[x][coluna] == num)
                                    {
                                        // Encontrei o numero, nao posso usar
                                        achei = 'S';
                                        break;
                                    }
                                }

                                if (achei == 'N')
                                {

                                    // procura na linha
                                    for (int linha = 0; linha < 9; linha++)
                                    {
                                        if (vetEntrada[linha][y] == num)
                                        {
                                            // Encontrei o numero, nao posso usar
                                            achei = 'S';
                                        }
                                    }
                                }
                                if (achei == 'N')
                                {
                                    // procura no quadrante
                                    int qStX, qStY, qEnX, qEnY;
                                    qStX = 0; qStY = 0; qEnX = 2; qEnY = 2;

                                    // Definicao da coluna x
                                    if (x < 3)
                                    {
                                        qStX = 0; qEnX = 2;
                                    }
                                    else if (x < 6)
                                    {
                                        qStX = 3; qEnX = 5;
                                    }
                                    else if (x < 9)
                                    {
                                        qStX = 6; qEnX = 8;
                                    }

                                    // Definicao da linha
                                    if (y < 3)
                                    {
                                        qStY = 0; qEnY = 2;
                                    }
                                    else if (y < 6)
                                    {
                                        qStY = 3; qEnY = 5;
                                    }
                                    else if (x < 9)
                                    {
                                        qStY = 6; qEnY = 8;
                                    }

                                    for (int chkqSt = qStX; chkqSt <= qEnX; chkqSt++)
                                    {
                                        for (int chkqEn = qStY; chkqEn <= qEnY; chkqEn++)
                                        {
                                            if (vetEntrada[chkqSt][chkqEn] == num)
                                            {
                                                // Encontrei o numero, nao posso usar
                                                achei = 'S';
                                            }
                                        }
                                    }

                                }

                                if (achei == 'N')
                                    possible[x][y][p++] = num;

                            }

                            #endregion ParaCadaNumero

                            // Ver quantos numeros sao possiveis para esta celula
                            // Caso apenas um seja possivel, mover para a group de entrada
                            // Refazendo todo o processo, a cada passada, tera menos celulas
                            // com mais de uma opcao

                            numerosPossiveis = 0;
                            unicoNumero = 0;
                            primeiroNumero = 0;

                            for (int verPos = 0; verPos < 9; verPos++)
                            {
                                if (possible[x][y][verPos] > 0)
                                {
                                    unicoNumero = possible[x][y][verPos];
                                    numerosPossiveis++;
                                    if (numerosPossiveis == 1)
                                        primeiroNumero = unicoNumero;
                                }
                                else
                                {
                                    break;
                                }

                            }
                            // Caso apenas um numero seja encontrado
                            // Mover para group de entrada que por enquanto e' a usada

                            if (numerosPossiveis == 1)
                            {
                                vetEntrada[x][y] = unicoNumero;
                                houveMudanca = true;
                            }

                            if (numerosPossiveis > 1 && deseperoMode)
                            {
                                vetEntrada[x][y] = primeiroNumero;
                                houveMudanca = true;
                                deseperoMode = false;
                                desesperoCount++;
                            }
                        }
                    }

                }
                #endregion MainLoop

            }
            return vetEntrada;

        }

        public double [][] loadEasy()
        {
            for (int x = 0; x < 9; x++)
            {
                vetEntrada[x] = new double[9];
            }

            vetEntrada[0][1] = 8;
            vetEntrada[0][5] = 2;
            vetEntrada[0][6] = 4;
            vetEntrada[0][7] = 9;

            vetEntrada[1][2] = 5;
            vetEntrada[1][4] = 7;
            vetEntrada[1][5] = 1;
            vetEntrada[1][8] = 8;

            vetEntrada[2][0] = 6;
            vetEntrada[2][1] = 2;
            vetEntrada[2][2] = 9;
            vetEntrada[2][3] = 4;

            vetEntrada[3][1] = 1;
            vetEntrada[3][4] = 3;
            vetEntrada[3][6] = 9;
            vetEntrada[3][8] = 5;

            vetEntrada[4][1] = 6;
            vetEntrada[4][3] = 7;
            vetEntrada[4][5] = 5;
            vetEntrada[4][7] = 1;

            vetEntrada[5][0] = 2;
            vetEntrada[5][2] = 8;
            vetEntrada[5][4] = 9;
            vetEntrada[5][7] = 3;

            vetEntrada[6][5] = 9;
            vetEntrada[6][6] = 3;
            vetEntrada[6][7] = 8;
            vetEntrada[6][8] = 1;

            vetEntrada[7][0] = 3;
            vetEntrada[7][3] = 8;
            vetEntrada[7][4] = 5;
            vetEntrada[7][6] = 6;

            vetEntrada[8][1] = 4;
            vetEntrada[8][2] = 2;
            vetEntrada[8][3] = 3;
            vetEntrada[8][7] = 7;

            return vetEntrada;
        }

        public double [][] loadHard(Form form)
        {
            for (int x = 0; x < 9; x++)
            {
                vetEntrada[x] = new double[9];
            }

            vetEntrada[0][1] = 1;
            form.Controls["input_00_01"].Text = "1";
            vetEntrada[0][2] = 9;
            form.Controls["input_00_02"].Text = "9";
            vetEntrada[0][5] = 6;
            form.Controls["input_00_05"].Text = "6";
            vetEntrada[0][8] = 2;
            form.Controls["input_00_08"].Text = "2";

            vetEntrada[1][0] = 2;
            form.Controls["input_01_00"].Text = "2";
            vetEntrada[1][2] = 8;
            form.Controls["input_01_02"].Text = "8";
            vetEntrada[1][3] = 3;
            form.Controls["input_01_03"].Text = "3";
            vetEntrada[1][4] = 1;
            form.Controls["input_01_04"].Text = "1";
            vetEntrada[1][6] = 5;
            form.Controls["input_01_06"].Text = "5";
            vetEntrada[1][8] = 6;
            form.Controls["input_01_08"].Text = "6";

            vetEntrada[2][1] = 6;
            form.Controls["input_02_01"].Text = "6";
            vetEntrada[2][4] = 7;
            form.Controls["input_02_04"].Text = "7";
            vetEntrada[2][5] = 2;
            form.Controls["input_02_05"].Text = "2";
            vetEntrada[2][7] = 1;
            form.Controls["input_02_07"].Text = "1";

            vetEntrada[3][1] = 3;
            form.Controls["input_03_01"].Text = "3";
            vetEntrada[3][2] = 2;
            form.Controls["input_03_02"].Text = "2";
            vetEntrada[3][3] = 6;
            form.Controls["input_03_03"].Text = "6";
            vetEntrada[3][5] = 1;
            form.Controls["input_03_05"].Text = "1";
            vetEntrada[3][6] = 7;
            form.Controls["input_03_06"].Text = "7";
            vetEntrada[3][8] = 4;
            form.Controls["input_03_08"].Text = "4";

            vetEntrada[4][0] = 1;
            form.Controls["input_04_00"].Text = "1";
            vetEntrada[4][1] = 8;
            form.Controls["input_04_01"].Text = "8";
            vetEntrada[4][2] = 7;
            form.Controls["input_04_02"].Text = "7";
            vetEntrada[4][3] = 2;
            form.Controls["input_04_03"].Text = "2";
            vetEntrada[4][5] = 4;
            form.Controls["input_04_05"].Text = "4";
            vetEntrada[4][7] = 5;
            form.Controls["input_04_07"].Text = "5";
            vetEntrada[4][8] = 3;
            form.Controls["input_04_08"].Text = "3";

            vetEntrada[5][2] = 4;
            form.Controls["input_05_02"].Text = "4";
            vetEntrada[5][3] = 8;
            form.Controls["input_05_03"].Text = "8";
            vetEntrada[5][6] = 1;
            form.Controls["input_05_06"].Text = "1";
            vetEntrada[5][7] = 2;
            form.Controls["input_05_07"].Text = "2";
            vetEntrada[5][8] = 9;
            form.Controls["input_05_08"].Text = "9";

            vetEntrada[6][0] = 4;
            form.Controls["input_06_00"].Text = "4";
            vetEntrada[6][1] = 2;
            form.Controls["input_06_01"].Text = "2";
            vetEntrada[6][2] = 1;
            form.Controls["input_06_02"].Text = "1";
            vetEntrada[6][4] = 6;
            form.Controls["input_06_04"].Text = "6";
            vetEntrada[6][7] = 3;
            form.Controls["input_06_07"].Text = "3";

            vetEntrada[7][3] = 1;
            form.Controls["input_07_03"].Text = "1";
            vetEntrada[7][6] = 2;
            form.Controls["input_07_06"].Text = "2";
            vetEntrada[7][8] = 5;
            form.Controls["input_07_08"].Text = "5";

            vetEntrada[8][1] = 9;
            form.Controls["input_08_01"].Text = "9";
            vetEntrada[8][4] = 2;
            form.Controls["input_08_04"].Text = "2";
            vetEntrada[8][5] = 8;
            form.Controls["input_08_05"].Text = "8";
            vetEntrada[8][6] = 4;
            form.Controls["input_08_06"].Text = "4";
            vetEntrada[8][7] = 6;
            form.Controls["input_08_07"].Text = "6";
            vetEntrada[8][8] = 1;
            form.Controls["input_08_08"].Text = "1";

            return vetEntrada;
        }
        
    }
}
