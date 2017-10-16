using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aggregate
{
    class TGen1 : TGen
    {
        public double x, y;

        public TGen1() : base()
        {
            x = Calc.U(-5, 5);
            y = Calc.U(-5, 5);
        }

        public override TGen New()
        {
            return new TGen1();
        }

        public override double F()
        {
            return x * x + y * y;
        }

        public override TGen Op(TGen b, double d)
        {
            TGen1 Res = new TGen1();

            if (Calc.rnd.NextDouble() < 0.5)
            {
                Res.x = x;
                Res.y = ((TGen1)b).y;
            }
            else
            {
                Res.y = y;
                Res.x = ((TGen1)b).x;
            }


            return Res;
        }
    }

    class TGenRosen : TGen
    {
        public double x, y;

        public TGenRosen() : base()
        {
            x = Calc.U(-10, 10);
            y = Calc.U(-10, 10);
        }

        public override TGen New()
        {
            return new TGenRosen();
        }

        public override double F()
        {
            return Calc.S2(1 - x) + 100 * Calc.S2(y - Calc.S2(x));
        }

        public override TGen Op(TGen b, double d)
        {
            TGenRosen Res = new TGenRosen();

            if (Calc.rnd.NextDouble() < 0.5)
            {
                Res.x = x;
                Res.y = ((TGenRosen)b).y;
            }
            else
            {
                Res.y = y;
                Res.x = ((TGenRosen)b).x;
            }


            return Res;
        }
    }

    class TGenDiophant : TGen
    {
        public int N = 4;
        public int[] x;

        public TGenDiophant() : base()
        {
            x = new int[N];

            for (int n = 0; n < N; n++)
            {
                x[n] = Calc.rnd.Next(100) - 50;
            }
        }

        public override TGen New()
        {
            return new TGenDiophant();
        }

        public override double F()
        {
            return x[0] * x[0] - 2 * x[0] + x[1] * x[1] - 2 * x[1] * x[2] + 2 * x[2] * x[2] - 4 * x[2] + x[3] * x[3] - 6 * x[3] + 14;
        }

        public override TGen Op(TGen b, double d)
        {
            TGenDiophant Res = new TGenDiophant();

            for (int n = 0; n < N; n++)
            {
                if (Calc.rnd.NextDouble() < 0.5)
                {
                    Res.x[n] = this.x[n];
                }
                else
                {
                    Res.x[n] = ((TGenDiophant)b).x[n];
                }
            }

            return Res;
        }
    }


}
