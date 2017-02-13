namespace GameOfLife
{
    public class CommandRunner : ICommandRunner
    {
        public IGrid Grid { get; private set; }
        public IRuleset Rules { get; private set; }
        private IGridFactory _gridFactory;
        private IGameAdvancer _advancer;
        private IGridWriter _gridWriter;
        private ISettings _settings;

        public CommandRunner(IRuleset rules, IGridFactory gridFactory, IGameAdvancer advancer, IGridWriter gridWriter, ISettings settings)
        {
            Rules = rules;
            _gridFactory = gridFactory;
            _advancer = advancer;
            _gridWriter = gridWriter;
            _settings = settings;
        }

        public void Execute(Command command)
        {
            if (command == Command.Reload)
                SetDefaults();
            else if (command == Command.Step)
                _advancer.Step(Grid, Rules);
            else if (command == Command.Display)
                _gridWriter.WriteCurrentStateOf(Grid, _settings.StateRepresentations);
        }

        private void SetDefaults()
        {
            Rules.SetDefaultRules(_settings);
            Grid = _gridFactory.CreateDefaultGrid(_settings);
        }
    }
}
