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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Aggregate
{
    /// <summary>
    /// Логика взаимодействия для SA.xaml
    /// </summary>
    public partial class FSA : Window
    {
        public FSA()
        {
            InitializeComponent();

            Init();

            DrawFig();

        }

        TSA SA;

        TFig Fig;

        DispatcherTimer Timer;

        void Init()
        {
            SA = new TSA(new TSol1());
        }

        private void cmRun(object sender, RoutedEventArgs e)
        {
            if (Timer != null)
            {
                Timer.Stop();
            }

            Init();

            Timer = new DispatcherTimer();
            Timer.Tick += new EventHandler(onTick);
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            Timer.Start();
        }

        void DrawFig()
        {
            g.Children.Clear();

            Fig = new TFig(g);

            double x = -10;
            double dx = 0.001;

            while (x <= 10)
            {
                Fig.XY.AddXY(x, ((TSol1)SA.Sol).F(x));

                x = x + dx;
            }

            Fig.SetData();
            Fig.Draw();

            RedLine = new Line();

            RedLine.Y1 = 0;
            RedLine.Y2 = g.Height;

            RedLine.StrokeThickness = 1;
            RedLine.Stroke = Brushes.Red;

            g.Children.Add(RedLine);
        }

        Line RedLine;

        void onTick(object sender, EventArgs e)
        {
            if (SA.T < 1e-9)
            {
                Timer.Stop();
            }

            SA.Run();

            RedLine.X1 = Fig.x(((TSol1)SA.Sol).x);
            RedLine.X2 = Fig.x(((TSol1)SA.Sol).x);


            tb1.Text = SA.T.ToString();

            tb2.Text = SA.Sol.H().ToString();

            tb3.Text = ((TSol1)SA.Sol).x.ToString();
        }

    }

    class TSol1 : ISol
    {
        public double x;

        public TSol1()
        {
            x = Calc.U(-10, 10);
        }

        public ISol Shift()
        {
            TSol1 S = new TSol1();

            //S.x = x + 0.1 * 2 * (Calc.rnd.Next(2) - 0.5);
            S.x = x + Calc.U(-1, 1);

            return S;
        }

        public double H()
        {
            return F(x);
        }

        public double F(double x)
        {
            return x * x * (2 + Math.Abs(Math.Sin(8 * x)));
        }

        public ISol Copy()
        {
            TSol1 S = new TSol1();

            S.x = x;

            return S;
        }
    }


}
