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
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Calc.Init();
        }

        private void cmClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void cmSA(object sender, RoutedEventArgs e)
        {
            FSA SA = new FSA();

            SA.Show();
        }

        private void cmSA2(object sender, RoutedEventArgs e)
        {
            FSAQueen SAQueen = new FSAQueen();

            SAQueen.Show();
        }
    }
}
