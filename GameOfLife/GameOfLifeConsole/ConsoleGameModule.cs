﻿using Autofac;
using GameOfLife;

namespace GameOfLifeConsole
{
    public abstract class ConsoleGameModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterStandardGameMechanicsComponents(builder);
            RegisterConfigurableGameMechanicsComponents(builder);
            RegisterConsoleComponents(builder);
        }

        private static void RegisterStandardGameMechanicsComponents(ContainerBuilder builder)
        {
            builder.RegisterType<RuleFactory>().As<IRuleFactory>();
            builder.RegisterType<Ruleset>().As<IRuleset>();
            builder.RegisterType<GridFactory>().As<IGridFactory>();
            builder.RegisterType<GameAdvancer>().As<IGameAdvancer>();
            builder.RegisterType<CommandRunner>().As<ICommandRunner>();
        }

        protected abstract void RegisterConfigurableGameMechanicsComponents(ContainerBuilder builder);

        private static void RegisterConsoleComponents(ContainerBuilder builder)
        {
            builder.RegisterType<ConsoleReaderWriter>().As<IConsoleReaderWriter>().As<IConsoleWriter>();
            builder.RegisterType<TextCommandParser>().AsSelf();
            builder.RegisterType<ConsoleGame>().AsSelf();
        }
    }
}
