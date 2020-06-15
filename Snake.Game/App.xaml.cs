using Autofac;
using Snake.Game.Autofac;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Snake.Game
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ContainerBuilder();
            var bootstrapper = new Bootstrapper();
            bootstrapper.ConfigureContainer(builder);
            var container = builder.Build();
            bootstrapper.ConfigureApplication(container);
        }
    }
}
