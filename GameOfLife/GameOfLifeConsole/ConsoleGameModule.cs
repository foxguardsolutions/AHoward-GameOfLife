using Autofac;
using GameOfLife;

namespace GameOfLifeConsole
{
    public class ConsoleGameModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterGameMechanicsComponents(builder);
            RegisterConsoleComponents(builder);
        }

        private static void RegisterGameMechanicsComponents(ContainerBuilder builder)
        {
            builder.RegisterType<RuleFactory>().As<IRuleFactory>();
            builder.RegisterType<Ruleset>().As<IRuleset>();
            builder.RegisterType<GridFactory>().As<IGridFactory>();
            builder.RegisterType<GameAdvancer>().As<IGameAdvancer>();
            builder.RegisterType<SquareTileGridWriter>().As<IGridWriter>();
            builder.RegisterType<CommandRunner>().As<ICommandRunner>();
        }

        private static void RegisterConsoleComponents(ContainerBuilder builder)
        {
            builder.RegisterType<ConsoleReaderWriter>().As<IConsoleReaderWriter>().As<IConsoleWriter>();
            builder.RegisterType<TextCommandParser>().AsSelf();
            builder.RegisterType<ConsoleGame>().AsSelf();
        }
    }
}
