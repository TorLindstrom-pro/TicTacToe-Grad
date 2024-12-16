﻿using NSubstitute;
using NSubstitute.Core;
using TicTacToe_Grad;

namespace Test;

public class TicTacToeTests
{
    [Fact(DisplayName = "Starting game should print empty board")]
    public void StartingGame_ShouldPrintEmptyBoard()
    {
        // Arrange
        var outputMock = Substitute.For<Output>();
        
        var subject = new TicTacToe(outputMock);
        
        // Act
        subject.Play(new Bot("X"), new Bot("O"));
        
        // Assert
        outputMock.Received().Print(
        "   |   |   " + "\n" +
            "---+---+---" + "\n" +
            "   |   |   " + "\n" +
            "---+---+---" + "\n" +
            "   |   |   ");
    }

    [Theory(DisplayName = "Starting game should print starting bot")]
    [InlineData("X")]
    [InlineData("H")]
    public void StartingGame_ShouldPrintStartingBot(string startingBotMarker)
    {
        // Arrange
        var outputMock = Substitute.For<Output>();
        
        var subject = new TicTacToe(outputMock);
        
        // Act
        subject.Play(new Bot(startingBotMarker), new Bot("O"));
        
        // Assert
        outputMock.Received().Print($"Bot {startingBotMarker} starts the game");
    }

    [Fact(DisplayName = "Starting bot should make a move")]
    public void StartingBotPlaysAMove_ShouldMarkAndPrintMove()
    {
        // Arrange
        var outputMock = Substitute.For<Output>();
        
        var subject = new TicTacToe(outputMock);
    
        var startingBot = Substitute.For<Bot>("X");
        startingBot
            .When(bot => bot.PlayMove(Arg.Any<Board>()))
            .Do(callInfo => MarkTile(callInfo, startingBot, 1, 0));
        
        // Act
        subject.Play(startingBot, new Bot("O"));
    
        // Assert
        outputMock.Print("   | X |   ");
        outputMock.Print("---+---+---");
        outputMock.Print("   |   |   ");
        outputMock.Print("---+---+---");
        outputMock.Print("   |   |   ");
    }

    [Fact(DisplayName = "Both bots should make moves")]
    public void BothBotsPlaysAMove_ShouldMarkAndPrintBothMoves()
    {
        // Arrange
        var outputMock = Substitute.For<Output>();
        
        var subject = new TicTacToe(outputMock);
    
        var startingBot = Substitute.For<Bot>("X");
        startingBot
            .When(bot => bot.PlayMove(Arg.Any<Board>()))
            .Do(callInfo => MarkTile(callInfo, startingBot, 1, 0));
        
        var secondBot = Substitute.For<Bot>("O");
        secondBot
            .When(bot => bot.PlayMove(Arg.Any<Board>()))
            .Do(callInfo => MarkTile(callInfo, secondBot, 2, 1));
        
        // Act
        subject.Play(startingBot, secondBot);
    
        // Assert
        outputMock.Received().Print(
        "   | X |   " + "\n" +
            "---+---+---" + "\n" +
            "   |   | O " + "\n" +
            "---+---+---" + "\n" +
            "   |   |   ");
    }

    private static void MarkTile(CallInfo callInfo, Bot bot, int x, int y)
    {
        var tile = callInfo
            .Arg<Board>()
            .GetAvailableTiles()
            .Single(tile => tile.X == x && tile.Y == y);
        tile.Mark(bot.Marker);
    }
}