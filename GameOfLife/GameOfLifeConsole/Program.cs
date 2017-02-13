using Autofac;

namespace GameOfLifeConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            // using (var container = DependencyInjectionContainerFactory.CreateSquareTileConsoleGameOfLifeContainer())
            // using (var container = DependencyInjectionContainerFactory.CreateHexTileB2S34ConsoleGameOfLifeContainer())
            using (var container = DependencyInjectionContainerFactory.CreateHexTileB2S35ConsoleGameOfLifeContainer())
            {
                var game = container.Resolve<ConsoleGame>();
                game.Start();
            }
        }
    }
}
