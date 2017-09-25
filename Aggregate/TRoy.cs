using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Aggregate
{
    abstract class TRoy
    {
        public TAgent[] Agents;

        public TX G;

        public double J;

        public TRoy()
        {
            Init();

            Agents = new TAgent[RoyInfo.M];

            for (int m = 0; m < RoyInfo.M; m++)
            {
                Agents[m] = new TAgent(this);
            }

            Calc();
        }

        public virtual void Init()
        {
            RoyInfo.Init(1, 1, 1);
        }

        void Calc()
        {
            G = new TX(Agents[0].P);
            J = F(G);

            for (int m = 1; m < RoyInfo.M; m++)
            {
                double j = F(Agents[m].P);

                if (j < J)
                {
                    G = new TX(Agents[m].P);
                    J = j;
                }
            }
        }

        abstract public double F(TX X);

        virtual public void Check()
        {

        }


        public void Run()
        {
            for (int m = 0; m < RoyInfo.M; m++)
            {
                Agents[m].Move(G);
            }

            Calc();

            Check();
        }

    }

    static class RoyInfo
    {
        public static int N; // размерность

        public static int M; // количество частиц

        public static int L; // количество итераций

        public static double A = -10; // ширина куба

        public static double B = 10; // ширина куба

        public static double alpha = 0.95;

        public static double beta = 0.2;

        public static double gamma = 0.2;

        public static void Init(int aN, int aM, int aL)
        {
            N = aN;
            M = aM;
            L = aL;
        }

    }

    class TX
    {
        public int N;
        double[] A;

        public TX(TX X = null)
        {
            N = RoyInfo.N;

            A = new double[N];

            if (X == null)
            {
                for (int n = 0; n < N; n++)
                {
                    this[n] = Calc.U(RoyInfo.A, RoyInfo.B);
                }
            }
            else
            {
                for (int n = 0; n < N; n++)
                {
                    this[n] = X[n];
                }
            }
        }

        public double this[int Ind]
        {
            get
            {
                return A[Ind];
            }

            set
            {
                A[Ind] = value;
            }
        }

        public void Print(string Name)
        {
            StreamWriter f = new StreamWriter(Name);

            for (int n = 0; n < RoyInfo.N; n++)
            {
                f.WriteLine(this[n]);
            }

            f.Close();
        }
    }

    class TAgent
    {
        TRoy Roy;

        public TX X;

        public TX V;

        public TX P;

        public double J;

        public TAgent(TRoy Roy)
        {
            this.Roy = Roy;

            X = new TX();
            P = new TX(X);
            J = Roy.F(P);

            V = new TX();

            for (int n = 0; n < RoyInfo.N; n++)
            {
                V[n] = Calc.U(-(RoyInfo.B - RoyInfo.A), RoyInfo.B - RoyInfo.A);
            }
        }

        public void Move(TX G)
        {
            for (int n = 0; n < RoyInfo.N; n++)
            {
                V[n] = RoyInfo.alpha * V[n] + RoyInfo.beta * (P[n] - X[n]) * Calc.U(0, 1) +
                    RoyInfo.gamma * (G[n] - X[n]) * Calc.U(0, 1);

                X[n] = X[n] + V[n];
            }

            double j = Roy.F(X);

            if (j < J)
            {
                P = new TX(X);
                J = j;
            }
        }
    }


}
