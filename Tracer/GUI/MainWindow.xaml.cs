﻿using System.Windows;
using ViewModel;

namespace GUI
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ReflectionViewModel();
        }

    }
}
