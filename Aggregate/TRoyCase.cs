using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aggregate
{
    class TRoyOpt : TRoy
    {
        public override void Init()
        {
            base.Init();

            RoyInfo.N = 2;
            RoyInfo.M = 500;
            RoyInfo.L = 1000;
        }
        public override double F(TX X)
        {
            return Calc.S2(1 - X[0]) + 100 * Calc.S2(X[1] - Calc.S2(X[0]));
        }
    }

    class TRoyDE : TRoy
    {
        public double h = 0.1;

        public override void Init()
        {
            base.Init();

            RoyInfo.N = 10;
            RoyInfo.M = 1000;
            RoyInfo.L = 100000;
        }

        public double f(double x)
        {
            return x;
        }

        public override double F(TX X)
        {
            double J = 0;

            for (int n = 0; n < RoyInfo.N; n++)
            {
                double xn1;

                if (n == 0)
                {
                    xn1 = 1;
                }
                else
                {
                    xn1 = X[n - 1];
                }

                double j = Math.Abs((X[n] - xn1) / h - f(xn1));

                if (j > J)
                {
                    J = j;
                }
            }

            return J;
        }
    }

}
