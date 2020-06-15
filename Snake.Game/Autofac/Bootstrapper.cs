using Autofac;
using Snake.Game.Helpers;

namespace Snake.Game.Autofac
{
    class Bootstrapper
    {
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<MainWindow>().SingleInstance();
            builder.RegisterType<MainViewModel>().SingleInstance();
            builder.RegisterType<KeyboardConfigurationProvider>().As<IKeyboardConfigurationProvider>();
        }

        public void ConfigureApplication(IContainer container)
        {
            var mainView = container.Resolve<MainWindow>();
            mainView.Show();
        }
    }
}
