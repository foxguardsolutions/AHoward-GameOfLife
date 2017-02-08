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
        [TestCaseSource(nameof(ComponentRegistrationTestCases))]
        public void Resolve_GivenContainerCreatedBycontainerFactory_ReturnsInstanceOfCorrectComponentType(
            Type serviceToResolve, Type expectedComponentType)
        {
            using (var container = DependencyInjectionContainerFactory.CreateConsoleGameOfLifeContainer())
            {
                var componentInstance = container.Resolve(serviceToResolve);
                Assert.That(componentInstance, Is.TypeOf(expectedComponentType));
            }
        }

        private static IEnumerable<TestCaseData> ComponentRegistrationTestCases()
        {
            yield return new TestCaseData(typeof(ConsoleGame), typeof(ConsoleGame));
            yield return new TestCaseData(typeof(TextCommandParser), typeof(TextCommandParser));
            yield return new TestCaseData(typeof(IConsoleReaderWriter), typeof(ConsoleReaderWriter));
            yield return new TestCaseData(typeof(IConsoleWriter), typeof(ConsoleReaderWriter));
            yield return new TestCaseData(typeof(IRuleFactory), typeof(RuleFactory));
            yield return new TestCaseData(typeof(IRuleset), typeof(Ruleset));
            yield return new TestCaseData(typeof(IGridFactory), typeof(GridFactory));
            yield return new TestCaseData(typeof(IGameAdvancer), typeof(GameAdvancer));
            yield return new TestCaseData(typeof(IGridWriter), typeof(SquareTileGridWriter));
            yield return new TestCaseData(typeof(ICommandRunner), typeof(CommandRunner));
        }
    }
}
