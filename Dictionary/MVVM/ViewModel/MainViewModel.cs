using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Core;
using Dictionary.Services;

namespace Dictionary.MVVM.ViewModel
{
    public class MainViewModel : Core.ViewModel
    {
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

        public RelayCommand NavigateHomeCommand { get; set; }
        public RelayCommand NavigateLoginCommand { get; set; }
        public RelayCommand NavigateAdminCommand { get; set; }
        public RelayCommand NavigateGameCommand { get; set; }


        public MainViewModel(INavigationService navService)
        {
            Navigation = navService;
            NavigateHomeCommand = new RelayCommand(o => { Navigation.NavigateTo<HomeViewModel>(); }, o => true);
            NavigateLoginCommand = new RelayCommand(o => { Navigation.NavigateTo<LoginViewModel>(); }, o => true);
            NavigateAdminCommand = new RelayCommand(o => { Navigation.NavigateTo<AdminViewModel>(); }, o => true);
            NavigateGameCommand = new RelayCommand(o => { Navigation.NavigateTo<GameViewModel>(); }, o => true);

            Navigation.NavigateTo<HomeViewModel>();
        }
    }
}
