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
    /// Логика взаимодействия для FRoy.xaml
    /// </summary>
    public partial class FRoy : Window
    {
        public FRoy()
        {
            InitializeComponent();
        }

        DispatcherTimer Timer;

        private void cmRun(object sender, RoutedEventArgs e)
        {
            g.Children.Clear();

            Roy = new TRoyOpt();

            Pole = new TRoyPole(g);

            TX X0 = new TX();
            X0[0] = 1;
            X0[1] = 1;

            Pole.SetU(X0);

            Counter = 0;

            Timer = new DispatcherTimer();
            Timer.Tick += new EventHandler(onTick);
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 10);

            Timer.Start();
        }

        int Counter = 0;

        TRoyOpt Roy;

        TRoyPole Pole;

        void onTick(object sender, EventArgs e)
        {
            Counter++;

            if (Counter > RoyInfo.L)
            {
                Timer.Stop();
            }

            Roy.Run();

            Pole.Draw(Roy);

            tb1.Text = Roy.J.ToString();
        }

    }
}
