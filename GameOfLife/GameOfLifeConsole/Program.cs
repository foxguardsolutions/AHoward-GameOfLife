using Autofac;
using GameOfLife;

namespace GameOfLifeConsole
{
    public class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleReaderWriter>().As<IConsoleReaderWriter>().As<IConsoleWriter>();
            builder.RegisterType<RuleFactory>().As<IRuleFactory>();
            builder.RegisterType<Ruleset>().As<IRuleset>();
            builder.RegisterType<GridFactory>().As<IGridFactory>();
            builder.RegisterType<GameAdvancer>().As<IGameAdvancer>();
            builder.RegisterType<SquareTileGridWriter>().As<IGridWriter>();
            builder.RegisterType<CommandRunner>().As<ICommandRunner>();
            builder.RegisterType<TextCommandParser>().As<TextCommandParser>();
            builder.RegisterType<ConsoleGame>().As<ConsoleGame>();
            Container = builder.Build();

            StartGame();
        }

        private static void StartGame()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var game = scope.Resolve<ConsoleGame>();
                game.Start();
            }
        }
    }
}
