using Autofac;
using Snake.Contract;
using Snake.Domain;
using Snake.Game.Helpers;
using Snake.Infrastructure.Autofac;

namespace Snake.Game.AutofacConfig
{
    class Bootstrapper
    {
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // register views and view models
            builder.RegisterType<MainWindow>().SingleInstance();
            builder.RegisterType<MainViewModel>().SingleInstance();

            // config
            builder.RegisterType<KeyboardConfigurationProvider>().As<IKeyboardConfigurationProvider>();
            builder.RegisterType<GameConfigurationProvider>().As<IGameConfigurationProvider>();

            // domain
            builder.RegisterType<CollisionDetector>().As<ICollisionDetector>();
            builder.RegisterType<FoodCreator>().As<IFoodCreator>();
            builder.RegisterType<ImageDownloader>().As<IImageDownloader>();
            builder.RegisterType<DirectionChangeDetector>().As<IDirectionChangeDetector>();

            // infrastructure
            builder.RegisterModule(new InfrastructureModule());
        }

        public void ConfigureApplication(IContainer container)
        {
            var mainView = container.Resolve<MainWindow>();
            mainView.Show();
        }
    }
}
