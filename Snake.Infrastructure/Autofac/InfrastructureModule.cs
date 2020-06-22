using Autofac;

namespace Snake.Infrastructure.Autofac
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(InfrastructureModule).Assembly)
                .AsImplementedInterfaces();
        }
    }
}
