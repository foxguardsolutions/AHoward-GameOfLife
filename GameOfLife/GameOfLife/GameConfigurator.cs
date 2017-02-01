using System;

namespace GameOfLife
{
    public class GameConfigurator
    {
        private IGridFactory _gridFactory;
        private IRuleFactory _ruleFactory;
        private IGame _game;

        public IGrid Grid { get; private set; }
        public IRuleset Rules { get; private set; }

        public GameConfigurator(IGridFactory gridFactory, IRuleFactory ruleFactory, IGame game)
        {
            _gridFactory = gridFactory;
            _ruleFactory = ruleFactory;
            _game = game;
        }

        public void LoadGrid(LifeState[,] seed)
        {
            LoadGrid(seed, false, false);
        }

        public void LoadGrid(LifeState[,] seed, bool wrapsOnRows, bool wrapsOnColumns)
        {
            _game.Grid = _gridFactory.CreateSquareTileGrid(seed, wrapsOnRows, wrapsOnColumns);
        }

        public void SetRuleFor(LifeState state, params uint[] numbersYieldingLive)
        {
            var newRule = _ruleFactory.Create(numbersYieldingLive);
            _game.Rules[state] = newRule;
        }

        public void LoadDefaultConfiguration()
        {
            LoadGrid(DefaultSettings.Seed, true, true);
            SetRuleFor(LifeState.Alive, DefaultSettings.SurvivalNumbers);
            SetRuleFor(LifeState.Dead, DefaultSettings.ReproductionNumbers);
        }
    }
}
