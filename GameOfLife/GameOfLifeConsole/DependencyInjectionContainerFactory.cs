using Autofac;

namespace GameOfLifeConsole
{
    public class DependencyInjectionContainerFactory
    {
        public static IContainer CreateSquareTileConsoleGameOfLifeContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<SquareTileConsoleGameModule>();
            return builder.Build();
        }

        public static IContainer CreateHexTileB2S34ConsoleGameOfLifeContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<HexTileB2S34ConsoleGameModule>();
            return builder.Build();
        }

        public static IContainer CreateHexTileB2S35ConsoleGameOfLifeContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<HexTileB2S35ConsoleGameModule>();
            return builder.Build();
        }
    }
}
