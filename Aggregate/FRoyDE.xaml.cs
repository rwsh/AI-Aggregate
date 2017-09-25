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
    /// Логика взаимодействия для FRoyDE.xaml
    /// </summary>
    public partial class FRoyDE : Window
    {
        public FRoyDE()
        {
            InitializeComponent();
        }

        DispatcherTimer Timer;

        TRoyDE RoyDE;

        private void cmRun(object sender, RoutedEventArgs e)
        {
            g.Children.Clear();

            RoyDE = new TRoyDE();

            Counter = 0;

            Timer = new DispatcherTimer();
            Timer.Tick += new EventHandler(onTick);
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            Timer.Start();
        }

        int Counter;

        TFig Fig, Fig0;

        void onTick(object sender, EventArgs e)
        {
            Counter++;

            if (Counter > RoyInfo.L)
            {
                Timer.Stop();
            }

            g.Children.Clear();

            Fig = new TFig(g, 1, true);
            Fig0 = new TFig(g, 1, true);

            RoyDE.Run();

            tb1.Text = RoyDE.J.ToString();

            double x = 0;

            for (int n = 0; n < RoyInfo.N; n++)
            {
                x = (n + 1) * RoyDE.h;

                Fig0.AddXY(x, Math.Exp(-x));

                Fig.AddXY(x, RoyDE.G[n]);
            }

            Fig.SetData();
            Fig0.SetData();

            if (Fig.XY.my < Fig0.XY.my)
            {
                Fig0.XY.my = Fig.XY.my;
            }

            if (Fig.XY.My > Fig0.XY.My)
            {
                Fig0.XY.My = Fig.XY.My;
            }

            Fig0.Draw(Brushes.Red);

            Fig.Draw();

        }

    }
}
