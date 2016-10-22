using mastermind.contracts;
using System.Windows;
using System;
using mastermind.view.viewmodel;

namespace mastermind.view
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDependsOn<MainViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Inject(MainViewModel viewmodel)
        {
            this.DataContext = viewmodel;
        }

        public void Run()
        {
            this.Show();
        }
    }
}
