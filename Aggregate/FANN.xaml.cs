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

namespace Aggregate
{
    /// <summary>
    /// Логика взаимодействия для FANN.xaml
    /// </summary>
    public partial class FANN : Window
    {
        public FANN()
        {
            InitializeComponent();

            Init();
        }

        void Init()
        {
            Boxes = new TANNBoxes(NN);

            g.Children.Clear();

            Pole = new TANNPole(g, NN);
        }

        int NN = 3;
        int H = 32;

        TANNBoxes Boxes;

        TANNPole Pole;

        private void cmClearBoxes(object sender, RoutedEventArgs e)
        {
            Boxes = new TANNBoxes(NN);
        }

        private void cmAdd(object sender, RoutedEventArgs e)
        {
            TANNBox Box = new TANNBox(Pole.Box);

            if (!int.TryParse(tb3.Text, out Box.Y))
            {
                Box.Y = 0;
            }

            Boxes.Add(Box);
        }

        int CurentBox = 0;

        private void cmShow(object sender, RoutedEventArgs e)
        {
            if (Boxes.Count == 0)
            {
                return;
            }

            if (CurentBox < Boxes.Count)
            {
            }
            else
            {
                CurentBox = 0;
            }

            Pole.Box = new TANNBox(Boxes[CurentBox]);
            CurentBox++;

            Pole.Draw();

        }

        private void cmClearPole(object sender, RoutedEventArgs e)
        {
            g.Children.Clear();

            Pole = new TANNPole(g, NN);
        }

        private void cmInit(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(tb1.Text, out NN))
            {
                NN = 3;
            }

            if (NN < 2)
            {
                NN = 2;
            }

            H = 0;

            if (!int.TryParse(tb2.Text, out H))
            {
                H = 32;
            }

            if (H < 2)
            {
                H = 2;
            }

            tb1.Text = NN.ToString();
            tb2.Text = H.ToString();

            Pole.UnMouse();

            Init();
        }

        TANN ANN;

        private void cmLearn(object sender, RoutedEventArgs e)
        {
            TANNExample[] Ex = new TANNExample[Boxes.Count];

            for (int i = 0; i < Boxes.Count; i++)
            {
                Ex[i] = Convert(Boxes[i]);
            }

            ANN = new TANN(Ex, H, 1000);

            ANN.Run();
        }

        TANNExample Convert(TANNBox Box)
        {
            TANNExample Res = new TANNExample();

            Res.X = new double[Box.N];

            for (int i = 0; i < Box.N; i++)
            {
                Res.X[i] = Box[i];
            }

            Res.Y = new double[1];
            Res.Y[0] = Box.Y;

            return Res;
        }

        private void cmFind(object sender, RoutedEventArgs e)
        {
            TANNExample Image = Convert(Pole.Box);

            ANN.CalcANN(Image);

            MessageBox.Show(Image.Y[0].ToString());
        }
    }
}
