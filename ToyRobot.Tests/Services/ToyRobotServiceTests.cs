using ToyRobot.Interfaces;
using ToyRobot.Services;
using FakeItEasy;
using NUnit.Framework;
using System;

namespace ToyRobot.Tests.Services
{
    [TestFixture]
    public class ToyRobotServiceTests
    {
        private ILogService _logService;
        private CommandFactory _commandFactory;
        private IGetInputService _getInputService;

        [SetUp]
        public void Setup()
        {
            _getInputService = A.Fake<IGetInputService>();
            _logService = A.Fake<ILogService>();
            _commandFactory = new CommandFactory(_logService);
        }

        //    a) ----------------
        //    PLACE 0,0,NORTH
        //    MOVE
        //    REPORT
        //    Output: 0,1,NORTH
        [Test]
        public void GivenScenarioA_WhenToyRobotServiceRunIsCalled_ThenOutputIsCorrect()
        {
            // Arrange
            var input = new string[] {
                "PLACE 0,0,NORTH",
                "MOVE",
                "REPORT"
            };
            A.CallTo(() => _getInputService.ReadLine())
                .ReturnsNextFromSequence(input);
            
            // Act
            var sut = new ToyRobotService(_logService, _getInputService, _commandFactory);
            sut.Run();

            // Assert
            A.CallTo(() => _logService.WriteLine("->ToyRobot")).MustHaveHappenedOnceExactly();
            A.CallTo(() => _logService.WriteLine("->Enter a command:")).MustHaveHappened(3, Times.Exactly);
            A.CallTo(() => _logService.WriteLine("Output: 0,1,NORTH")).MustHaveHappenedOnceExactly();
        }

        //  b) ----------------
        //  PLACE 0,0,NORTH
        //  LEFT
        //  REPORT
        //  Output: 0,0,WEST
        [Test]
        public void GivenScenarioB_WhenToyRobotServiceRunIsCalled_ThenOutputIsCorrect()
        {
            // Arrange
            var input = new string[] {
                "PLACE 0,0,NORTH",
                "LEFT",
                "REPORT"
            };
            A.CallTo(() => _getInputService.ReadLine())
                .ReturnsNextFromSequence(input);

            // Act
            var sut = new ToyRobotService(_logService, _getInputService, _commandFactory);
            sut.Run();

            // Assert
            A.CallTo(() => _logService.WriteLine("->ToyRobot")).MustHaveHappenedOnceExactly();
            A.CallTo(() => _logService.WriteLine("->Enter a command:")).MustHaveHappened(3, Times.Exactly);
            A.CallTo(() => _logService.WriteLine("Output: 0,0,WEST")).MustHaveHappenedOnceExactly();
        }

        //  c) ----------------
        //  PLACE 1,2,EAST
        //  MOVE
        //  MOVE
        //  LEFT
        //  MOVE
        //  REPORT
        //  Output: 3,3,NORTH
        [Test]
        public void GivenScenarioC_WhenToyRobotServiceRunIsCalled_ThenOutputIsCorrect()
        {
            // Arrange
            var input = new string[] {
                "PLACE 1,2,EAST",
                "MOVE",
                "MOVE",
                "LEFT",
                "MOVE",
                "REPORT"
            };
            A.CallTo(() => _getInputService.ReadLine())
                .ReturnsNextFromSequence(input);

            // Act
            var sut = new ToyRobotService(_logService, _getInputService, _commandFactory);
            sut.Run();

            // Assert
            A.CallTo(() => _logService.WriteLine("->ToyRobot")).MustHaveHappenedOnceExactly();
            A.CallTo(() => _logService.WriteLine("->Enter a command:")).MustHaveHappened(6, Times.Exactly);
            A.CallTo(() => _logService.WriteLine("Output: 3,3,NORTH")).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void GivenCustomScenario_WhenToyRobotServiceRunIsCalled_ThenRobotDoesntFallOffTable()
        {
            // Arrange
            var input = new string[] {
                "PLACE 5,5,NORTH",
                "MOVE",
                "RIGHT",
                "MOVE",
                "REPORT"
            };
            A.CallTo(() => _getInputService.ReadLine())
                .ReturnsNextFromSequence(input);

            // Act
            var sut = new ToyRobotService(_logService, _getInputService, _commandFactory);
            sut.Run();

            // Assert
            A.CallTo(() => _logService.WriteLine("->ToyRobot")).MustHaveHappenedOnceExactly();
            A.CallTo(() => _logService.WriteLine("->Enter a command:")).MustHaveHappened(5, Times.Exactly);
            A.CallTo(() => _logService.WriteLine("Output: 5,5,EAST")).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void GivenRobotIsPlacedOffTable_WhenToyRobotServiceRunIsCalled_ThenCommandIsIngnored()
        {
            // Arrange
            var input = new string[] {
                "PLACE 7,7,NORTH"
            };
            A.CallTo(() => _getInputService.ReadLine())
                .ReturnsNextFromSequence(input);

            // Act
            var sut = new ToyRobotService(_logService, _getInputService, _commandFactory);

            // Assert
            var e = Assert.Throws<Exception>(() => sut.Run());
            Assert.That("PLACE Command ignored. This would be off the table", Is.EqualTo(e.Message));
            A.CallTo(() => _logService.WriteLine("->ToyRobot")).MustHaveHappenedOnceExactly();
            A.CallTo(() => _logService.WriteLine("->Enter a command:")).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void GivenABunchOfNonPlaceCommands_UntilPlaceCommandIsCalled_ThenAllOtherCommandsAreIgnored()
        {
            // Arrange
            var input = new string[] {
                "LEFT",
                "MOVE",
                "RIGHT",
                "REPORT",
                "PLACE 1,1,NORTH",
                "REPORT"
            };
            A.CallTo(() => _getInputService.ReadLine())
                .ReturnsNextFromSequence(input);

            // Act
            var sut = new ToyRobotService(_logService, _getInputService, _commandFactory);
            sut.Run();

            // Assert
            A.CallTo(() => _logService.WriteLine("->ToyRobot")).MustHaveHappenedOnceExactly();
            A.CallTo(() => _logService.WriteLine("->Enter a command:")).MustHaveHappened(6, Times.Exactly);
            A.CallTo(() => _logService.WriteLine("MOVE command ignore. Cannot do this command on first go")).MustHaveHappenedOnceExactly();
            A.CallTo(() => _logService.WriteLine("LEFT command ignore. Cannot do this command on first go")).MustHaveHappenedOnceExactly();
            A.CallTo(() => _logService.WriteLine("RIGHT command ignore. Cannot do this command on first go")).MustHaveHappenedOnceExactly();
            A.CallTo(() => _logService.WriteLine("REPORT command ignore. Cannot do this command on first go")).MustHaveHappenedOnceExactly();
            A.CallTo(() => _logService.WriteLine("Output: 1,1,NORTH")).MustHaveHappenedOnceExactly();
        }
    }
}