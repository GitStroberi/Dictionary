using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Core;
using Dictionary.MVVM.View;
using System.Windows.Input;
using System.Windows;
using Dictionary.Services;

namespace Dictionary.MVVM.ViewModel
{
    public class LoginViewModel : Core.ViewModel
    {
        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }

        private INavigationService _navigation;

        public INavigationService Navigation
        {
            get => _navigation;
            set
            {
                _navigation = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand LoginCommand { get; set; }

        public LoginViewModel(INavigationService navigation)
        {
            Navigation = navigation;
            LoginCommand = new RelayCommand(Login, o => true);
        }

        private void Login(object parameter)
        {
            var passwordBox = parameter as System.Windows.Controls.PasswordBox;
            var password = passwordBox.Password;
            if(password != null)
            {
                Password = password;
            }
            if (Username == "a" && Password == "a")
            {
                //navigate to Admin page
                Navigation.NavigateTo<AdminViewModel>();
            }
            else
            {
                MessageBox.Show("Incorrect username or password", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
