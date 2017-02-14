using System;
using System.Collections.Generic;
using Autofac;
using GameOfLife;
using GameOfLifeConsole;
using NUnit.Framework;

namespace GameOfLifeTests
{
    public class DependencyInjectionContainerFactoryTests : BaseTests
    {
        [TestCaseSource(nameof(StandardComponentRegistrationTestCases))]
        [TestCaseSource(nameof(SquareTileComponentRegistrationTestsCases))]
        public void Resolve_GivenSquareTileContainerCreatedByContainerFactory_ReturnsInstanceOfCorrectComponentType(
            Type serviceToResolve, Type expectedComponentType)
        {
            using (var container = DependencyInjectionContainerFactory.CreateSquareTileConsoleGameOfLifeContainer())
            {
                var componentInstance = container.Resolve(serviceToResolve);
                Assert.That(componentInstance, Is.TypeOf(expectedComponentType));
            }
        }

        [TestCaseSource(nameof(StandardComponentRegistrationTestCases))]
        [TestCaseSource(nameof(HexTileB2S34ComponentRegistrationTestsCases))]
        public void Resolve_GivenHexTileB2S34ContainerCreatedByContainerFactory_ReturnsInstanceOfCorrectComponentType(
            Type serviceToResolve, Type expectedComponentType)
        {
            using (var container = DependencyInjectionContainerFactory.CreateHexTileB2S34ConsoleGameOfLifeContainer())
            {
                var componentInstance = container.Resolve(serviceToResolve);
                Assert.That(componentInstance, Is.TypeOf(expectedComponentType));
            }
        }

        [TestCaseSource(nameof(StandardComponentRegistrationTestCases))]
        [TestCaseSource(nameof(HexTileB2S35ComponentRegistrationTestsCases))]
        public void Resolve_GivenHexTileB2S35ContainerCreatedByContainerFactory_ReturnsInstanceOfCorrectComponentType(
            Type serviceToResolve, Type expectedComponentType)
        {
            using (var container = DependencyInjectionContainerFactory.CreateHexTileB2S35ConsoleGameOfLifeContainer())
            {
                var componentInstance = container.Resolve(serviceToResolve);
                Assert.That(componentInstance, Is.TypeOf(expectedComponentType));
            }
        }

        private static IEnumerable<TestCaseData> StandardComponentRegistrationTestCases()
        {
            yield return new TestCaseData(typeof(ConsoleGame), typeof(ConsoleGame));
            yield return new TestCaseData(typeof(TextCommandParser), typeof(TextCommandParser));
            yield return new TestCaseData(typeof(IConsoleReaderWriter), typeof(ConsoleReaderWriter));
            yield return new TestCaseData(typeof(IConsoleWriter), typeof(ConsoleReaderWriter));
            yield return new TestCaseData(typeof(IRuleFactory), typeof(RuleFactory));
            yield return new TestCaseData(typeof(IRuleset), typeof(Ruleset));
            yield return new TestCaseData(typeof(IGridFactory), typeof(GridFactory));
            yield return new TestCaseData(typeof(IGameAdvancer), typeof(GameAdvancer));
            yield return new TestCaseData(typeof(ICommandRunner), typeof(CommandRunner));
        }

        private static IEnumerable<TestCaseData> SquareTileComponentRegistrationTestsCases()
        {
            yield return new TestCaseData(typeof(IGridWriter), typeof(SquareTileGridWriter));
            yield return new TestCaseData(typeof(ISettings), typeof(SquareTileGridSettings));
        }

        private static IEnumerable<TestCaseData> HexTileB2S34ComponentRegistrationTestsCases()
        {
            yield return new TestCaseData(typeof(IGridWriter), typeof(HexTileGridWriter));
            yield return new TestCaseData(typeof(ISettings), typeof(HexTileGridB2S34Settings));
        }

        private static IEnumerable<TestCaseData> HexTileB2S35ComponentRegistrationTestsCases()
        {
            yield return new TestCaseData(typeof(IGridWriter), typeof(HexTileGridWriter));
            yield return new TestCaseData(typeof(ISettings), typeof(HexTileGridB2S35Settings));
        }
    }
}
