using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Dictionary.MVVM.ViewModel;
using Dictionary.Core;
using Dictionary.Services;
using Dictionary.Models;

namespace Dictionary
{
    public partial class App : Application
    {

        private readonly ServiceProvider _serviceProvider;
        public App()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<MainWindow>(provider => new MainWindow()
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });

            services.AddSingleton<MainViewModel>(); // main

            services.AddSingleton<HomeViewModel>(); // dictionary
            services.AddSingleton<AdminViewModel>(); // admin
            services.AddSingleton<GameViewModel>(); // game
            services.AddSingleton<LoginViewModel>(); // login
            services.AddSingleton<INavigationService, NavigationService>();

            services.AddSingleton<Func<Type, ViewModel>>(serviceProvider => viewModelType => serviceProvider.GetRequiredService(viewModelType) as ViewModel);

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
