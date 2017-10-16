using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Aggregate
{
    class TANN
    {
        public int N; // размерность входных данных
        public int M; // размерность выходных данных
        public int H; // размерность скрытого слоя
        public int L; // количество примеров

        public int T; // количество итераций

        public double alpha = 0.001; // скорость обучения

        public TANNExample[] XY; // обучающая выборка

        public double[,] W1, W2;

        public double beta = 20; // параметр сигмоида

        public TANN(TANNExample[] Ex, int aH, int aT)
        {
            XY = Ex;

            N = XY[0].X.Length;
            M = XY[0].Y.Length;

            L = XY.Length;

            H = aH;
            T = aT;

            InitW(); // инициализировать веса
        }

        public void Run()
        {
            for (int t = 0; t < T; t++)
            {
                Learn(Calc.rnd.Next(L));
            }
        }

        public void CalcANN(TANNExample XY)
        {
            XY.Y = new double[M];

            double[] U = new double[H];

            for (int h = 0; h < H; h++)
            {
                double O = 0;
        
                for (int n = 0; n < N; n++)
                {
                    O += W1[n, h] * XY.X[n];
                }
                O += W1[N, h] * (-1);

                U[h] = Sigma(O);
            }

            for (int m = 0; m < M; m++)
            {
                double O2 = 0;

                for (int h = 0; h < H; h++)
                {
                    O2 += W2[h, m] * U[h];
                }
                O2 += W2[H, m] * (-1);

                XY.Y[m] = Sigma(O2);
            }

        }

        void Learn(int l)
        {
            double[] O, O2, U, E, E2;

            O = new double[H];
            U = new double[H];

            for (int h = 0; h < H; h++)
            {
                O[h] = 0;
        
                for (int n = 0; n < N; n++)
                {
                    O[h] += W1[n, h] * XY[l].X[n];
                }

                O[h] += W1[N, h] * (-1);

                U[h] = Sigma(O[h]);
            }

            O2 = new double[M];
            E = new double[M];

            for (int m = 0; m < M; m++)
            {

                O2[m] = 0;
        

                for (int h = 0; h < H; h++)
                {
                    O2[m] += W2[h, m] * U[h];

                }

                O2[m] += W2[H, m] * (-1);

                double A = Sigma(O2[m]);

                E[m] = A - XY[l].Y[m]; // вычисление ошибки
            }

            E2 = new double[H];

            for (int h = 0; h < H; h++)
            {
                E2[h] = 0;
        
                for (int m = 0; m < M; m++)
                {
                    E2[h] += E[m] * DSigma(O2[m]) * W2[h, m];
                }
            }

            for (int h = 0; h < H; h++)
            {
                for (int m = 0; m < M; m++)
                {
                    W2[h, m] -= alpha * E[m] * DSigma(O2[m]) * U[h];
                }
            }
            for (int m = 0; m < M; m++)
            {
                W2[H, m] -= alpha * E[m] * DSigma(O2[m]) * (-1);
            }

            for (int n = 0; n < N; n++)
            {
                for (int h = 0; h < H; h++)
                {
                    W1[n, h] -= alpha * E2[h] * DSigma(O[h]) * XY[l].X[n];
                }
            }
            for (int h = 0; h < H; h++)
            {
                W1[N, h] -= alpha * E2[h] * DSigma(O[h]) * (-1);
            }
        }

        public double Sigma(double x)
        {
            return 1.0 / (1.0 + Math.Exp(-beta * x));
        }

        public double DSigma(double x)
        {
            return beta * Sigma(x) * (1.0 - Sigma(x));
        }

        void InitW()
        {
            W1 = new double[N + 1, H];
            for (int i = 0; i <= N; i++)
            {
                for (int j = 0; j < H; j++)
                {
                    W1[i, j] = Calc.U(-1.0 / (2 * N), 1.0 / (2 * N));
                }
            }

            W2 = new double[H + 1, M];
            for (int i = 0; i <= H; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    W2[i, j] = Calc.U(-1.0 / (2 * H), 1.0 / (2 * H));
                }
            }
        }
    }

    class TANNExample
    {
        public double[] X;
        public double[] Y;

        public TANNExample()
        {

        }
    }
}
