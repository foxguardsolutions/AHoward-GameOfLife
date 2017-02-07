namespace GameOfLife
{
    public class CommandRunner : ICommandRunner
    {
        public IGrid Grid { get; private set; }
        public IRuleset Rules { get; private set; }
        private IGridFactory _gridFactory;
        private IGameAdvancer _advancer;
        private IGridWriter _gridWriter;

        public CommandRunner(IRuleset rules, IGridFactory gridFactory, IGameAdvancer advancer, IGridWriter gridWriter)
        {
            Rules = rules;
            _gridFactory = gridFactory;
            _advancer = advancer;
            _gridWriter = gridWriter;
        }

        public void Execute(Command command)
        {
            if (command == Command.Reload)
                SetDefaults();
            else if (command == Command.Step)
                _advancer.Step(Grid, Rules);
            else if (command == Command.Display)
                _gridWriter.WriteCurrentStateOf(Grid);
        }

        private void SetDefaults()
        {
            Rules.SetDefaultRules();
            Grid = _gridFactory.CreateDefaultGrid();
        }
    }
}
