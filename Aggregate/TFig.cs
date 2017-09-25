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
using System.IO;
using System.Collections;

namespace Aggregate
{
    class TFig
    {
        public Canvas g;

        public TData XY;

        public TFig(Canvas g, double R = 1, bool IsLine = false)
        {
            this.g = g;
            this.R = R;
            this.IsLine = IsLine;

            XY = new TData();
        }

        public double R = 1;

        bool IsLine;

        public void AddY(double y)
        {
            XY.AddY(y);
        }

        public void AddXY(double x, double y)
        {
            XY.AddXY(x, y);
        }

        public void SetData()
        {
            XY.SetData();
        }

        public void Draw(Brush br = null)
        {
            if (br == null)
            {
                br = Brushes.Blue;
            }

            if (IsLine)
            {
                for (int i = 1; i < XY.N; i++)
                {
                    Line line = new Line();

                    line.X1 = x(XY.X[i - 1]);
                    line.Y1 = y(XY.Y[i - 1]);

                    line.X2 = x(XY.X[i]);
                    line.Y2 = y(XY.Y[i]);

                    line.Stroke = br;
                    line.StrokeThickness = R;

                    g.Children.Add(line);
                }
            }
            else
            {
                for (int i = 0; i < XY.N; i++)
                {
                    Ellipse O = new Ellipse();
                    O.Width = R * 1; // !
                    O.Height = R * 1; // !
                    O.Margin = new Thickness(x(XY.X[i]) - R, y(XY.Y[i]) - R, 0, 0);
                    O.Fill = br;
                    g.Children.Add(O);
                }
            }

        }

        public double x(double x0)
        {
            double a = (g.Width - 2 * R) / (XY.Mx - XY.mx);
            double b = -a * XY.mx;

            return a * x0 + b + R;
        }

        public double y(double y0)
        {
            if (Math.Abs(XY.My - XY.my) < 1e-18)
            {
                return y0 + R;
            }

            double a = (g.Height - 2 * R) / (XY.my - XY.My);
            double b = -a * XY.My;

            return a * y0 + b + R;
        }

        public void Grid()
        {

        }

    }

    class TData
    {
        public int N;

        public double[] X, Y;

        public ArrayList aX, aY;

        public double mx, my, Mx, My;

        public TData()
        {
            aX = new ArrayList();
            aY = new ArrayList();
        }

        public void SetData()
        {
            N = aY.Count;

            X = new double[N];
            Y = new double[N];

            if (aX.Count != N)
            {
                for (int i = 0; i < N; i++)
                {
                    X[i] = i;
                }
            }
            else
            {
                for (int i = 0; i < N; i++)
                {
                    X[i] = (double)aX[i];
                }
            }

            for (int i = 0; i < N; i++)
            {
                Y[i] = (double)aY[i];
            }

            mx = double.MaxValue;
            Mx = double.MinValue;
            my = double.MaxValue;
            My = double.MinValue;

            for (int i = 0; i < N; i++)
            {
                if (X[i] > Mx)
                {
                    Mx = X[i];
                }

                if (X[i] < mx)
                {
                    mx = X[i];
                }

                if (Y[i] > My)
                {
                    My = Y[i];
                }

                if (Y[i] < my)
                {
                    my = Y[i];
                }
            }
        }

        public void AddXY(double x, double y)
        {
            aX.Add(x);
            aY.Add(y);
        }

        public void AddY(double y)
        {
            aY.Add(y);
        }
    }
}
