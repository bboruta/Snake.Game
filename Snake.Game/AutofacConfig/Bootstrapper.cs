using Autofac;
using Snake.Contract;
using Snake.Domain;
using Snake.Game.Helpers;

namespace Snake.Game.AutofacConfig
{
    class Bootstrapper
    {
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //register views and view models
            builder.RegisterType<MainWindow>().SingleInstance();
            builder.RegisterType<MainViewModel>().SingleInstance();

            //register all dependency types that you want to use
            //todo: move it to separate module later
            builder.RegisterType<KeyboardConfigurationProvider>().As<IKeyboardConfigurationProvider>();
            builder.RegisterType<GameConfigurationProvider>().As<IGameConfigurationProvider>();
            builder.RegisterType<CollisionDetector>().As<ICollisionDetector>();

            //register automapper
            builder.RegisterModule(new AutoMapperModule());
        }

        public void ConfigureApplication(IContainer container)
        {
            var mainView = container.Resolve<MainWindow>();
            mainView.Show();
        }
    }
}
