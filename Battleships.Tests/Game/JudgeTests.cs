﻿using System;
using System.Collections.Generic;
using Battleships.Grid;
using Battleships.Grid.Helpers;
using Battleships.Init;
using Battleships.IO;
using Battleships.Settings;
using Battleships.Tests;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Battleships.GameLogic.Tests
{
    public class JudgeTests : TestsBase
    {
        private static Random RandomInstance => new(123);

        public JudgeTests()
        {
            serviceManager.InitDefault();
            serviceManager.AddTransient(f => RandomInstance);
        }


        [Theory(Timeout = DefaultTimeoutMs)]
        [MemberData(nameof(Shoot_TestData))]
        public void Shoot_WhenValidPlayerToken_ShouldShootTheBoard(GameSettings gameSettings)
        {
            // Arrange
            (var board1, var board2) = InitBoards(gameSettings);

            var player1Token = Guid.NewGuid();
            board1.InitPlayerToken(player1Token);

            var player2Token = Guid.NewGuid();
            board2.InitPlayerToken(player2Token);

            var judge = new Judge(board1, board2);
            int y = 0;
            int x = 0;

            // Act
            judge.Shoot(player1Token, y, x);

            // Assert
            var field = board2[y, x];
            Assert.True(field.WasShot(), $"Field ({y},{x}) should have been shot.");
        }

        public static IEnumerable<object[]> Shoot_TestData =>
        new List<object[]>
        {
            GenerateTestCase(new GameSettings   // full board 
            {
                BoardHeight = 3,
                BoardWidth = 3,
                CarriersNum = 0,
                BattleshipsNum = 0,
                CruisersNum = 0,
                SubmarinesNum = 3,
                DestroyersNum = 0,
            }),
            GenerateTestCase(new GameSettings   // empty board 
            {
                BoardHeight = 3,
                BoardWidth = 3,
                CarriersNum = 0,
                BattleshipsNum = 0,
                CruisersNum = 0,
                SubmarinesNum = 0,
                DestroyersNum = 0,
            }),
        };

        [Theory(Timeout = DefaultTimeoutMs)]
        [MemberData(nameof(Shoot_TestData))]
        public void Shoot_WhenInValidPlayerToken_ShouldThrow(GameSettings gameSettings)
        {
            // Arrange
            (Board board1, Board board2) = InitBoards(gameSettings);

            var player1Token = Guid.NewGuid();
            board1.InitPlayerToken(player1Token);

            var player2Token = Guid.NewGuid();
            board2.InitPlayerToken(player2Token);

            var judge = new Judge(board1, board2);
            int y = 0;
            int x = 0;

            // Act
            Assert.Throws<InvalidOperationException>(() => judge.Shoot(Guid.NewGuid(), y, x));

            // Assert
            var field = board2[y, x];
            Assert.False(field.WasShot(), $"Field ({y},{x}) should not have been shot.");
        }

        private (Board board1, Board board2) InitBoards(GameSettings gameSettings)
        {
            var ioManager = new Mock<IIOManager>();
            ioManager.Setup(x =>
                x.GetBooleanInput(It.Is<string>(question => question.Contains("modify"))))
                .Returns(false);
            ioManager.Setup(x =>
               x.GetBooleanInput(It.Is<string>(question => question.Contains("retry"))))
               .Returns(false);
            ioManager.Setup(x =>
               x.GetBooleanInput(It.IsAny<string>()))
               .Returns(true);

            serviceManager.AddSingleton(ioManager.Object);

            var gameInitializer = serviceManager.GetServiceProvider().GetRequiredService<GameInitializer>();
            return gameInitializer.Initialize();
        }
    }
}
