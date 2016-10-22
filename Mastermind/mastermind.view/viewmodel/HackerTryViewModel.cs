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
    public class HackerTryViewModel : INotifyPropertyChanged
    {
        private bool _enable;
        private IEnumerable<string> _pins;
        private Pin _pin_one;
        private Pin _pin_two;
        private Pin _pin_three;
        private Pin _pin_four;
        private Pin _result_pin_one;
        private Pin _result_pin_two;
        private Pin _result_pin_three;
        private Pin _result_pin_four;

        public HackerTryViewModel()
        {
            _pins = new string[] { "Yellow", "Brown", "Red", "Blue",
                                    "Green", "Orange", "Black","White" };
        }

        public IEnumerable<string> Pins
        {
            get { return _pins; }
            set
            {
                _pins = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Pins"));
            }
        }

        public bool Enable
        {
            get { return _enable; }
            set
            {
                _enable = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Enable"));
            }
        }

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

        public Pin Result_Pin_One
        {
            get { return _result_pin_one; }
            set
            {
                _result_pin_one = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Result_Pin_One"));
            }
        }

        public Pin Result_Pin_Two
        {
            get { return _result_pin_two; }
            set
            {
                _result_pin_two = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Result_Pin_Two"));
            }
        }

        public Pin Result_Pin_Three
        {
            get { return _result_pin_three; }
            set
            {
                _result_pin_three = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Result_Pin_Three"));
            }
        }

        public Pin Result_Pin_Four
        {
            get { return _result_pin_four; }
            set
            {
                _result_pin_four = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Result_Pin_Four"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
