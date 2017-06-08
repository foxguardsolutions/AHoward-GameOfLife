using System;
using System.Linq;

namespace GameOfLife.Operations
{
    public static class CommandFactory
    {
        private static readonly ICommand[] _commands =
            AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => typeof(ICommand).IsAssignableFrom(type) && !type.IsAbstract)
                .Select(type => (ICommand) Activator.CreateInstance(type))
                .ToArray();

        public static ICommand GetCommand(CellOperation operation)
            => _commands.Single(command => command.Operation == operation);
    }
}
