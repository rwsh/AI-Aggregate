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
    /// Логика взаимодействия для FGA.xaml
    /// </summary>
    public partial class FGA : Window
    {
        public FGA()
        {
            InitializeComponent();

            Run();
        }

        TGA GA;

        public void Run()
        {
            //            GA = new TGA(100, 80, new TGen1());

            //            GA = new TGA(200, 50, new TGenRosen());

            GA = new TGA(1000, 800, new TGenDiophant());

            int L = 100;

            for (int l = 0; l < L; l++)
            {
                //                lb1.Items.Add(((TGen1)GA[0]).x.ToString() + "\t" + ((TGen1)GA[0]).y.ToString() + "\t" + GA.nu(0).ToString() + "\t" + GA[0].F().ToString());
                //                lb1.Items.Add(((TGenRosen)GA[0]).x.ToString() + "\t" + ((TGenRosen)GA[0]).y.ToString() + "\t" + GA.nu(0).ToString() + "\t" + GA[0].F().ToString());
                string s = "";

                TGenDiophant Gen = (TGenDiophant)GA[0];

                for (int n = 0; n < Gen.N; n++)
                {
                    s += Gen.x[n].ToString() + " ";
                }

                s += Gen.F();

                lb1.Items.Add(s);

                GA.Next();
            }

        }

    }
}
