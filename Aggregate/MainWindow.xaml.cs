﻿using System;
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

        private void cmRoy(object sender, RoutedEventArgs e)
        {
            FRoy Roy = new FRoy();

            Roy.Show();
        }

        private void cmRoyDE(object sender, RoutedEventArgs e)
        {
            FRoyDE RoyDE = new FRoyDE();

            RoyDE.Show();
        }

        private void cmGA(object sender, RoutedEventArgs e)
        {
            FGA GA = new FGA();

            GA.Show();
        }

        private void cmANN(object sender, RoutedEventArgs e)
        {
            FANN ANN = new FANN();

            ANN.Show();
        }
    }
}
