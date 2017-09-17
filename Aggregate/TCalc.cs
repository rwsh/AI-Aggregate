using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aggregate
{
    static class Calc
    {
        public static Random rnd;

        public static void Init()
        {
            rnd = new Random();
        }

        public static double S2(double x)
        {
            return x * x;
        }

        public static double U(double a, double b)
        {
            return a + (b - a) * rnd.NextDouble();
        }

        public static double Exp(double la)
        {
            return (-1.0 / la) * Math.Log(U(1e-17, 1));
        }

    }
}
