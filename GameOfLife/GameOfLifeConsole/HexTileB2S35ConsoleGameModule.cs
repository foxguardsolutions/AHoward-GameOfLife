using Autofac;
using GameOfLife;

namespace GameOfLifeConsole
{
    public class HexTileB2S35ConsoleGameModule : ConsoleGameModule
    {
        protected override void RegisterConfigurableGameMechanicsComponents(ContainerBuilder builder)
        {
            builder.RegisterType<HexTileGridWriter>().As<IGridWriter>();
            builder.RegisterType<HexTileGridB2S35Settings>().As<ISettings>();
        }
    }
}
