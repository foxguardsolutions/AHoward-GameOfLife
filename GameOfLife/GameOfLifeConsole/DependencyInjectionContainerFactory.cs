using Autofac;

namespace GameOfLifeConsole
{
    public class DependencyInjectionContainerFactory
    {
        public static IContainer CreateConsoleGameOfLifeContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<ConsoleGameModule>();
            return builder.Build();
        }
    }
}
