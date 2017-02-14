using Autofac;
using GameOfLife;

namespace GameOfLifeConsole
{
    public class HexTileB2S34ConsoleGameModule : ConsoleGameModule
    {
        protected override void RegisterConfigurableGameMechanicsComponents(ContainerBuilder builder)
        {
            builder.RegisterType<HexTileGridWriter>().As<IGridWriter>();
            builder.RegisterType<HexTileGridB2S34Settings>().As<ISettings>();
        }
    }
}
