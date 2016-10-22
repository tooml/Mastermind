using mastermind.contracts;
using mastermind.contracts.data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace mastermind.view.viewmodel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IBody _body;
        private string _message;
        private ICommand _start_new_game;
        private ICommand _place_hacker_try;
        private SecretCodeViewModel _secrt_code_vm;
        private ObservableCollection<HackerTryViewModel> _try_list;

        public MainViewModel(IBody body, SecretCodeViewModel secret_code_vm)
        {
            _body = body;
            _secrt_code_vm = secret_code_vm;
            _try_list = new ObservableCollection<HackerTryViewModel>();
            _start_new_game = new DelegateCommand(() => Start_new_Game(), () => true);
            _place_hacker_try = new DelegateCommand(() => Place_hacker_Try(), () => true);
        }

        public SecretCodeViewModel Secret_Code
        {
            get { return _secrt_code_vm; }
            set
            {
                _secrt_code_vm = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Secret_Code"));
            }
        }

        public ObservableCollection<HackerTryViewModel> Try_List
        {
            get { return _try_list; }
            set
            {
                _try_list = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Try_List"));
            }
        }

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Message"));
            } 
        }

        public ICommand Start_New_Game
        {
            get { return _start_new_game; }
        }

        public ICommand Place_Hacker_Try
        {
            get { return _place_hacker_try; }
        }

        public void Start_new_Game()
        {
            _body.Game_preperation(messeage => 
            {
                Message = messeage;
                Hide_Secret_Code();
                Try_List.Clear();
                Unlock_next_Round();
            });
        }

        public void Place_hacker_Try()
        {
            var last_try = Try_List.Last();
            Try_Complete(last_try, () => Message = ("Versuch nicht komplett"), 
                () => {
                    Try _try = new Try(last_try.Pin_One, last_try.Pin_Two,
                                            last_try.Pin_Three, last_try.Pin_Four);

                    _body.Game_flow(_try, Show_Result_Pins, () => 
                                                                {
                                                                    Lock_previous_Round();
                                                                    Unlock_next_Round();
                                                                }, 
                                                                secret_code => Show_Secret_Code(secret_code));
                });   
        }

        private void Try_Complete(HackerTryViewModel last_try, Action NotComplete, Action Complete)
        {
            if(Try_List.Any())
            {
                if (last_try.Pin_One != null && last_try.Pin_Two != null &&
                    last_try.Pin_Three != null && last_try.Pin_Four != null)
                    Complete();
                else
                    NotComplete();
            }
            else
            {
                NotComplete();
            } 
        }

        private void Hide_Secret_Code()
        {
            _secrt_code_vm.Pin_One = Pin.Black;
            _secrt_code_vm.Pin_Two = Pin.Black;
            _secrt_code_vm.Pin_Three = Pin.Black;
            _secrt_code_vm.Pin_Four = Pin.Black;
        }

        private void Show_Secret_Code(SecretCode secret_code)
        {
            _secrt_code_vm.Pin_One = secret_code.Place_One;
            _secrt_code_vm.Pin_Two = secret_code.Place_Two;
            _secrt_code_vm.Pin_Three = secret_code.Place_Three;
            _secrt_code_vm.Pin_Four = secret_code.Place_Four;
        }

        public void Show_Result_Pins(Result result)
        {
            Try_List.Last().Result_Pin_One = result.Place_One;
            Try_List.Last().Result_Pin_Two = result.Place_Two;
            Try_List.Last().Result_Pin_Three = result.Place_Three;
            Try_List.Last().Result_Pin_Four = result.Place_Four;
        }

        private void Lock_previous_Round()
        {
            _try_list.ToList().ForEach(old_try => old_try.Enable = false);
        }

        private void Unlock_next_Round()
        {
            _try_list.Add(new HackerTryViewModel() { Enable = true });
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
