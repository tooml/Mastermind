using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mastermind.view.viewmodel
{
    public class StatsViewModel : INotifyPropertyChanged
    {
        private string _winner;
        private TimeSpan _duration;

        public string Winner
        {
            get { return _winner; }
            set
            {
                _winner = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Winner"));
            }
        }

        public TimeSpan Duration
        {
            get { return _duration; }
            set
            {
                _duration = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Duration"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
