using Autofac;

namespace GameOfLifeConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var container = DependencyInjectionContainerFactory.CreateConsoleGameOfLifeContainer())
            {
                var game = container.Resolve<ConsoleGame>();
                game.Start();
            }
        }
    }
}
