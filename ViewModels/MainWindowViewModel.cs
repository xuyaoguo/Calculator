using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Calculator.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        private string _value = "";

        public string Value
        {
            get => _value;
            set
            {
                SetProperty(ref _value, value);
            }
        }

        private string _datetime;
        
        public string Datetime
        {
            get => _datetime;
            set
            {
                SetProperty(ref _datetime, value);
            }
        }

        public MainWindowViewModel()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);//设置的间隔为1秒钟
            timer.Tick += DispatcherTimer_Tick;
            timer.IsEnabled = true;
            timer.Start();
        }
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            _datetime = DateTime.Now.ToString();
            RaisePropertyChanged(nameof(Datetime));
        }
        public ICommand NumerCommand
        {
            get => new DelegateCommand<object>(ButtonClick);
        }

        private void ButtonClick(object obj)
        {
            //点击到Clear按钮就清空TextBox
            if(obj.ToString() == "Clear")
            {
                _value = "";
                RaisePropertyChanged(nameof(Value));
            }
            //点击到"="就开始进行计算
            else if(obj.ToString() == "=")
            {
                if (_value.Contains("+") || _value.Contains("-") || _value.Contains("*") || _value.Contains("÷"))
                {
                    if (_value.IndexOf("+") != -1) 
                    {
                        string[] strArray = _value.Split("+");
                        var number1 = Convert.ToInt64(strArray[0]);
                        var number2 = Convert.ToInt64(strArray[1]);
                        _value = Convert.ToString(number1 + number2);
                        RaisePropertyChanged(nameof(Value));
                    }
                    if (_value.IndexOf("*") != -1)
                    {
                        string[] strArray = _value.Split("*");
                        var number1 = Convert.ToInt64(strArray[0]);
                        var number2 = Convert.ToInt64(strArray[1]);
                        _value = Convert.ToString(number1 * number2);
                        RaisePropertyChanged(nameof(Value));
                    }
                    if (_value.IndexOf("÷") != -1)
                    {
                        string[] strArray = _value.Split("÷");
                        var number1 = Convert.ToInt64(strArray[0]);
                        var number2 = Convert.ToInt64(strArray[1]);
                        _value = Convert.ToString( number1 / number2);
                        RaisePropertyChanged(nameof(Value));
                    }
                    if (_value.IndexOf("-") != -1)
                    {
                        if (_value.StartsWith("-")||(_value.IndexOf("-")<_value.LastIndexOf("-")))
                        {

                        }
                        else
                        {
                            string[] strArray = _value.Split("-");
                            var number1 = Convert.ToInt64(strArray[0]);
                            var number2 = Convert.ToInt64(strArray[1]);
                            _value = Convert.ToString(number1 - number2);
                            RaisePropertyChanged(nameof(Value));
                        }

                    }
                }
            }
            else
            {
                _value += obj.ToString();
                RaisePropertyChanged(nameof(Value));
            }


        }
    }
}
