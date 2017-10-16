using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Aggregate
{
    class TGA
    {
        public int M;
        public int K;
        public TGen[] Arr;

        public TGen Father;

        public TGA(int M, int K, TGen Father)
        {
            this.M = M;
            this.K = K;
            this.Father = Father;

            Arr = new TGen[M];

            for (int m = 0; m < M; m++)
            {
                this[m] = Father.New();
            }
        }

        public void Next()
        {
            Sort();

            for (int k = K; k < M; k++)
            {
                this[k] = Father.New();
            }

            TGen[] FNew = new TGen[M];

            for (int m = 1; m < M; m++)
            {
                //int xi = Calc.rnd.Next(M);

                int xi = Choise();

                double d = this[m].nu / (this[m].nu + this[xi].nu);

                FNew[m] = this[m].Op(this[xi], d);
            }

            for (int m = 1; m < M; m++)
            {
                this[m] = FNew[m];
            }
        }

        virtual public int Choise()
        {
            double Sigma = 0;
            double[] p = new double[M];

            for (int m = 0; m < M; m++)
            {
                Sigma += this[m].nu;
            }

            for (int m = 0; m < M; m++)
            {
                p[m] = this[m].nu / Sigma;
            }


            double s = Calc.rnd.NextDouble();

            double ps = 0;

            for (int m = 0; m < M; m++)
            {
                ps += p[m];

                if (ps > s)
                {
                    return m;
                }
            }

            return -1;
        }

        void Sort()
        {
            TGen S;

            for(int m = 0; m < M; m++)
            {
                this[m].nu = nu(m);
            }

            for (int m = 0; m < M; m++)
            {
                for (int p = m + 1; p < M; p++)
                {
                    if (this[p].nu > this[m].nu)
                    {
                        S = this[m];
                        this[m] = this[p];
                        this[p] = S;
                    }
                }
            }
        }

        public TGen this[int Ind]
        {
            get
            {
                return Arr[Ind];
            }
            set
            {
                Arr[Ind] = value;
            }
        }

        public double nu(int m)
        {
            return 1.0 / (1.0 + this[m].F());
        }
    }

    abstract class TGen
    {
        public double nu;

        public TGen()
        {
        }

        abstract public TGen New();

        abstract public double F();

        abstract public TGen Op(TGen b, double d);
    }
}
