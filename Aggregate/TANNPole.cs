using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;

namespace Aggregate
{
    class TANNPole
    {
        Canvas g; // холст для поля
        public int NN; // количество клеток N*N
        double dx; // ширина в пикселях клетки

        Rectangle[,] Rs; // клетки

        public TANNBox Box;

        public TANNPole(Canvas g, int aNN)
        {
            this.g = g;

            NN = aNN;
            dx = g.Width / NN; // ширина в пикселях клетки

            Box = new TANNBox(NN);

            Rs = new Rectangle[NN, NN];

            Draw(); // перерисовать поле

            g.MouseUp += Check;
        }

        public void UnMouse()
        {
            g.MouseUp -= Check;
        }

        private void Check(object sender, MouseButtonEventArgs e)
        {
            IsCheck(e.GetPosition(g).X, e.GetPosition(g).Y);
        }

        // отметить клетку с заданными координатами
        public void IsCheck(double x, double y)
        {
            // проверить диапазон
            if ((x < 0) || (x > g.Width)) { return; }
            if ((y < 0) || (y > g.Height)) { return; }

            // расчитать номер клетки
            int i = Convert.ToInt16(Math.Floor(x / dx));
            int j = Convert.ToInt16(Math.Floor(y / dx));

            if (Rs[i, j] == null)
            {
                DrawCell(i, j);

                Box[i, j] = 1;
            }
            else
            {
                g.Children.Remove(Rs[i, j]);
                Rs[i, j] = null;

                Box[i, j] = 0;
            }
        }

        // рисовать клетку
        public void DrawCell(int i, int j)
        {
            Rs[i, j] = new Rectangle();

            Rs[i, j].Fill = Brushes.Blue;
            Rs[i, j].Margin = new Thickness(i * dx, j * dx, 0, 0);
            Rs[i, j].Height = dx;
            Rs[i, j].Width = dx;
            g.Children.Add(Rs[i, j]);
        }

        // перерисовать поле
        public void Draw()
        {
            g.Children.Clear();
            g.Background = Brushes.LightGray; // фон

            for (int k = 0; k <= NN; k++)
            {
                // рисуем горизонтальные линии
                Line l = new Line();
                l.Stroke = Brushes.Black;
                l.X1 = 0;
                l.X2 = g.Width;
                l.Y1 = k * dx;
                l.Y2 = k * dx;
                l.StrokeThickness = 1;
                g.Children.Add(l);

                // рисуем вертикальные линии
                l = new Line();
                l.Stroke = Brushes.Black;
                l.X1 = k * dx;
                l.X2 = k * dx;
                l.Y1 = 0;
                l.Y2 = g.Height;
                l.StrokeThickness = 1;
                g.Children.Add(l);
            }

            for (int i = 0; i < NN; i++)
            {
                for (int j = 0; j < NN; j++)
                {
                    if (Box[i, j] > 0)
                    {
                        DrawCell(i, j);
                    }
                }
            }
        }
    }

    [Serializable]
    class TANNBoxes
    {
        public int NN;
        ArrayList arr;

        public TANNBoxes(int NN)
        {
            this.NN = NN;
            arr = new ArrayList();
        }

        public void Add(TANNBox B)
        {
            arr.Add(B);
        }

        public bool Find(TANNBox B)
        {
            for (int n = 0; n < Count; n++)
            {
                if (B.IsEq(this[n]))
                {
                    return true;
                }
            }

            return false;
        }

        public int N
        {
            get
            {
                return NN * NN;
            }
        }


        public TANNBox this[int k]
        {
            get
            {
                return (TANNBox)arr[k];
            }
        }

        public int Count
        {
            get
            {
                return arr.Count;
            }
        }
    }

    [Serializable]
    class TANNBox
    {
        int NN; // размерность коробки
        // количество элементов N = NN * NN

        public int[] B;

        public int Y;

        public TANNBox(int NN, int d = 0)
        {
            this.NN = NN;

            B = new int[N];

            for (int n = 0; n < N; n++)
            {
                B[n] = d;
            }
        }

        public TANNBox(TANNBox TB)
        {
            NN = TB.NN;
            B = new int[N];

            for (int n = 0; n < N; n++)
            {
                B[n] = TB[n];
            }
        }

        public int N
        {
            get
            {
                return NN * NN;
            }
        }

        public bool IsEq(TANNBox B)
        {
            for (int i = 0; i < N; i++)
            {
                if (this[i] != B[i])
                {
                    return false;
                }
            }

            return true;
        }

        public int this[int n]
        {
            get
            {
                return B[n];
            }
            set
            {
                B[n] = value;
            }
        }

        public int this[int i, int j]
        {
            get
            {
                return this[i * NN + j];
            }
            set
            {
                this[i * NN + j] = value;
            }
        }
    }


}
