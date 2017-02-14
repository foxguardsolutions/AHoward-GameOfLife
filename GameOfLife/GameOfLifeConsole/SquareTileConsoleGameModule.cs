using Autofac;
using GameOfLife;

namespace GameOfLifeConsole
{
    public class SquareTileConsoleGameModule : ConsoleGameModule
    {
        protected override void RegisterConfigurableGameMechanicsComponents(ContainerBuilder builder)
        {
            builder.RegisterType<SquareTileGridWriter>().As<IGridWriter>();
            builder.RegisterType<SquareTileGridSettings>().As<ISettings>();
        }
    }
}
