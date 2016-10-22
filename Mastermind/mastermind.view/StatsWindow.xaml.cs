using mastermind.contracts;
using mastermind.view.viewmodel;
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
using System.ComponentModel;

namespace mastermind.view
{
    /// <summary>
    /// Interaktionslogik für StatsWindow.xaml
    /// </summary>
    public partial class StatsWindow : Window, IDependsOn<StatsViewModel>
    {
        public StatsWindow()
        {
            InitializeComponent();
        }

        public void Inject(StatsViewModel viewmodel)
        {
            this.DataContext = viewmodel;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        public void Run()
        {
            this.Show();
        }
    }
}
