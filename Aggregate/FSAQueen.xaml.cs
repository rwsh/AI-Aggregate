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
    /// Логика взаимодействия для FSAQueen.xaml
    /// </summary>
    public partial class FSAQueen : Window
    {
        public FSAQueen()
        {
            InitializeComponent();

            DrawPole();
        }

        int N = 8;

        private void cmRun(object sender, RoutedEventArgs e)
        {
            if (Int32.TryParse(tb1.Text, out N))
            {
                if (N < 8)
                {
                    N = 8;
                }

                if (N % 2 == 1)
                {
                    N = N - 1;
                }
            }
            else
            {
                N = 8;
            }

            tb1.Text = N.ToString();

            DrawPole();

            DrawQueens();

            SA = new TSA(SolQueen, 100, 0.98);

            SA.T = 50;

            if (Timer != null)
            {
                Timer.Stop();
            }

            Count = 0;

            Timer = new DispatcherTimer();
            Timer.Tick += new EventHandler(onTick);
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            Timer.Start();

        }

        TSA SA;

        DispatcherTimer Timer;

        int Count;

        void onTick(object sender, EventArgs e)
        {
            if (SA.T < 1e-9)
            {
                Timer.Stop();
            }

            SA.Run();

            Count++;

            for (int i = 0; i < N; i++)
            {
                Shift(Q[i], i, ((TSolQueen)SA.Sol).QQ[i]);
            }

            if (SA.Sol.H() < 0.5)
            {
                Timer.Stop();
                Win();
            }
        }

        void Win()
        {
            lb.Content = "Кол-во шагов: " + Count.ToString();

            for (int n = 0; n < N; n++)
            {
                Q[n].Fill = Brushes.Red;
            }
        }

        Ellipse[] Q;

        TSolQueen SolQueen;

        void DrawPole()
        {
            g.Children.Clear();

            lb.Content = "";

            double L = g.Width; // сохранить размер игрового поля
            double L8 = L / N;

            bool White = false;

            for (int i = 0; i < N; i++)
            {
                White = !White;

                for (int j = 0; j < N; j++)
                {
                    Rectangle Cell = new Rectangle();

                    if (White)
                    {
                        Cell.Fill = Brushes.Lavender;
                        White = false;
                    }
                    else
                    {
                        Cell.Fill = Brushes.Peru;
                        White = true;
                    }

                    Cell.Width = L8;
                    Cell.Height = L8;
                    Cell.Margin = new Thickness(i * L8, j * L8, 0, 0);
                    g.Children.Add(Cell);
                }
            }

            Rectangle R = new Rectangle();
            R.Width = L;
            R.Height = L;
            R.Stroke = Brushes.Black;
            R.Margin = new Thickness(0);
            g.Children.Add(R);
        }

        void DrawQueens()
        {
            Q = new Ellipse[N];

            SolQueen = new TSolQueen(N);

            for (int i = 0; i < N; i++)
            {
                int j = i;

                Q[i] = DrawQueen(i, j);

                SolQueen.QQ[i] = j;
            }
        }

        Ellipse DrawQueen(int i, int j)
        {
            double L = g.Width; // сохранить размер игрового поля
            double L8 = L / N;
            double kappa = 0.8;

            Ellipse O = new Ellipse();

            O.Fill = Brushes.Black;

            O.Width = kappa * L8;
            O.Height = kappa * L8;

            O.Margin = new Thickness(i * L8 + L8 * (1 - kappa) / 2, j * L8 + L8 * (1 - kappa) / 2, 0, 0);
            g.Children.Add(O);

            return O;
        }

        void Shift(Ellipse O, int i, int j)
        {
            double L = g.Width; // сохранить размер игрового поля
            double L8 = L / N;
            double kappa = 0.8;

            O.Margin = new Thickness(i * L8 + L8 * (1 - kappa) / 2, j * L8 + L8 * (1 - kappa) / 2, 0, 0);
        }
    }


    class TSolQueen : ISol
    {
        int N;

        public int[] QQ;

        public TSolQueen(int N)
        {
            this.N = N;

            QQ = new int[N];
        }

        public ISol Shift()
        {
            TSolQueen S = (TSolQueen)Copy();

            int i = 0;
            int j = 0;
            while (i == j)
            {
                i = Calc.rnd.Next(N);
                j = Calc.rnd.Next(N);
            }

            int a = S.QQ[i];
            S.QQ[i] = S.QQ[j];
            S.QQ[j] = a;

            return S;
        }

        public double H()
        {
            int Res = 0;

            for (int n = 0; n < N; n++)
            {
                int k = n - 1;

                while(k >= 0)
                {
                    if (QQ[k] == (QQ[n] + (n - k)))
                    {
                        Res++;
                    }

                    if (QQ[k] == QQ[n] - (n - k))
                    {
                        Res++;
                    }

                    k = k - 1;
                }

                k = n + 1;

                while (k < N)
                {
                    if (QQ[k] == (QQ[n] + (k - n)))
                    {
                        Res++;
                    }

                    if (QQ[k] == (QQ[n] - (k - n)))
                    {
                        Res++;
                    }

                    k = k + 1;
                }
            }

            return Res;
        }

        public ISol Copy()
        {
            TSolQueen S = new TSolQueen(N);

            for (int n = 0; n < N; n++)
            {
                S.QQ[n] = QQ[n];
            }

            return S;
        }
    }

}
