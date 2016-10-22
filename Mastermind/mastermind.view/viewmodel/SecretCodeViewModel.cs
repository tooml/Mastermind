using mastermind.contracts.data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace mastermind.view.viewmodel
{
    public class SecretCodeViewModel : INotifyPropertyChanged
    {
        private Pin _pin_one;
        private Pin _pin_two;
        private Pin _pin_three;
        private Pin _pin_four;

        public Pin Pin_One
        {
            get { return _pin_one; }
            set
            {
                _pin_one = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Pin_One"));
            }
        }

        public Pin Pin_Two
        {
            get { return _pin_two; }
            set
            {
                _pin_two = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Pin_Two"));
            }
        }

        public Pin Pin_Three
        {
            get { return _pin_three; }
            set
            {
                _pin_three = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Pin_Three"));
            }
        }

        public Pin Pin_Four
        {
            get { return _pin_four; }
            set
            {
                _pin_four = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Pin_Four"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
