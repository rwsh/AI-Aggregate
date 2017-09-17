using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aggregate
{
    class TSA
    {
        public double T = 100; // начальная температура
        public double alpha = 0.95; // коэффициент снижения температуры

        public ISol Sol;

        public TSA(ISol Sol, double T = 100, double alpha = 0.95)
        {
            this.Sol = Sol;
            this.T = T;
            this.alpha = alpha;
        }

        public void Run()
        {
            ISol S = Sol.Shift(); // модифицировать решение

            double delta = S.H() - Sol.H(); // вычислить разницу между новым решением и старым

            // сравнить разницу
            if (delta < 0)
            {
                // если новое решение лучше, то выбирать его
                Sol = S;
            }
            else
            {
                // если решение ухудшилось, то принять его с вероятностью
                if (Calc.rnd.NextDouble() < Math.Exp(-delta / T))
                {
                    Sol = S;
                }
            }

            T = alpha * T; // снизить температуру

        }
    }

    // интерфейс класса описания решения
    interface ISol
    {
        ISol Shift(); // метод модификации решения (случайное, малое)
        double H(); // значение решения

        ISol Copy(); // создать новую копию решения
    }

}
