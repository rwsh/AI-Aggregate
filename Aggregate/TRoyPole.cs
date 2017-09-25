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

namespace Aggregate
{
    class TRoyPole
    {
        Canvas g;

        Ellipse[] OO;

        float R = 4;

        Brush br;

        public TRoyPole(Canvas g)
        {
            this.g = g;

            OO = new Ellipse[RoyInfo.M];

            br = Brushes.Red;

            for (int m = 0; m < RoyInfo.M; m++)
            {
                OO[m] = new Ellipse();
                OO[m].Width = R;
                OO[m].Height = R;
                OO[m].Fill = br;

                g.Children.Add(OO[m]);
            }
        }

        public void Draw(TRoy Roy)
        {
            for (int m = 0; m < RoyInfo.M; m++)
            {
                OO[m].Margin = new Thickness(x2(Roy.Agents[m].P[0]) - R / 2.0, y2(Roy.Agents[m].P[1]) - R / 2.0, 0, 0);
            }
        }

        public void SetU(TX X)
        {
            Ellipse O = new Ellipse();

            float R = 5;

            O.Width = R;
            O.Height = R;

            Brush br = Brushes.Blue;
            O.Fill = br;

            O.Margin = new Thickness(x2(X[0]) - R / 2.0, y2(X[1]) - R / 2.0, 0, 0);

            g.Children.Add(O);
        }

        public float x2(double x)
        {
            float a = (float)(g.Width / (RoyInfo.B - RoyInfo.A));

            float b = (float)(-a * RoyInfo.A);

            return a * (float)x + b;
        }

        public float y2(double y)
        {
            float a = (float)(g.Height / (RoyInfo.A - RoyInfo.B));

            float b = (float)(-a * RoyInfo.B);

            return a * (float)y + b;
        }
    }
}
