using FluentAssertions;
using NSubstitute;
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
        
        var startingBot = Substitute.For<Bot>("X");
        
        startingBot
            .When(bot => bot.PlayMove(Arg.Any<Board>()))
            .Do(callInfo => throw new AbortTestException());
        
        // Act
        Assert.Throws<AbortTestException>(() => subject.Play(startingBot, new Bot("O")));
        
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
        
        var startingBot = Substitute.For<Bot>(startingBotMarker);
        startingBot
            .When(bot => bot.PlayMove(Arg.Any<Board>()))
            .Do(callInfo => throw new AbortTestException());
        
        // Act
        Assert.Throws<AbortTestException>(() => subject.Play(startingBot, new Bot("O")));
        
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
            .Do(callInfo =>
            {
                if (callInfo.Arg<Board>().GetAvailableTiles().Count() == 9)
                    MarkTile(callInfo, startingBot, 1, 0);
            });
        
        var secondBot = Substitute.For<Bot>("X");
        secondBot
            .When(bot => bot.PlayMove(Arg.Any<Board>()))
            .Do(callInfo => throw new AbortTestException());
        
        // Act
        Assert.Throws<AbortTestException>(() => subject.Play(startingBot, secondBot));
    
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
            .Do(callInfo =>
            {
                if (callInfo.Arg<Board>().GetAvailableTiles().Count() == 9)
                    MarkTile(callInfo, startingBot, 1, 0);
                else
                    throw new AbortTestException();
            });
        
        var secondBot = Substitute.For<Bot>("O");
        secondBot
            .When(bot => bot.PlayMove(Arg.Any<Board>()))
            .Do(callInfo =>
            {
                if (callInfo.Arg<Board>().GetAvailableTiles().Count() == 8)
                    MarkTile(callInfo, secondBot, 2, 1);
            });
        
        // Act
        Assert.Throws<AbortTestException>(() => subject.Play(startingBot, secondBot));
    
        // Assert
        outputMock.Received().Print(
        "   | X |   " + "\n" +
            "---+---+---" + "\n" +
            "   |   | O " + "\n" +
            "---+---+---" + "\n" +
            "   |   |   ");
    }

    [Fact(DisplayName = "Bot playing three in a row vertically wins")]
    public void ThreeInARowVertically_ShouldReturnWinner()
    {
        // Arrange
        var outputMock = Substitute.For<Output>();
        
        var subject = new TicTacToe(outputMock);
    
        var startingBot = Substitute.For<Bot>("X");
        startingBot
            .When(bot => bot.PlayMove(Arg.Any<Board>()))
            .Do(callInfo =>
            {
                switch (callInfo.Arg<Board>().GetAvailableTiles().Count())
                {
                    case 9:
                        MarkTile(callInfo, startingBot, 0, 0);
                        break;
                    case 7:
                        MarkTile(callInfo, startingBot, 0, 1);
                        break;
                    case 5:
                        MarkTile(callInfo, startingBot, 0, 2);
                        break;
                }
            });
        
        var secondBot = Substitute.For<Bot>("O");
        secondBot
            .When(bot => bot.PlayMove(Arg.Any<Board>()))
            .Do(callInfo =>
            {
                switch (callInfo.Arg<Board>().GetAvailableTiles().Count())
                {
                    case 8:
                        MarkTile(callInfo, secondBot, 2, 0);
                        break;
                    case 6:
                        MarkTile(callInfo, secondBot, 1, 1);
                        break;
                }
            });
        
        // Act
        var result = subject.Play(startingBot, secondBot);
    
        // Assert
        outputMock.Received().Print(
        " X |   | O " + "\n" +
            "---+---+---" + "\n" +
            " X | O |   " + "\n" +
            "---+---+---" + "\n" +
            " X |   |   ");

        result.Should().Be(startingBot);
    }

    [Fact(DisplayName = "Second bot playing three in a row vertically wins")]
    public void SecondBotPlaysThreeInARowVertically_ShouldReturnSecondBotAsWinner()
    {
        // Arrange
        var outputMock = Substitute.For<Output>();
        
        var subject = new TicTacToe(outputMock);
    
        var startingBot = Substitute.For<Bot>("X");
        startingBot
            .When(bot => bot.PlayMove(Arg.Any<Board>()))
            .Do(callInfo =>
            {
                switch (callInfo.Arg<Board>().GetAvailableTiles().Count())
                {
                    case 9:
                        MarkTile(callInfo, startingBot, 1, 0);
                        break;
                    case 7:
                        MarkTile(callInfo, startingBot, 2, 1);
                        break;
                    case 5:
                        MarkTile(callInfo, startingBot, 1, 2);
                        break;
                }
            });
        
        var secondBot = Substitute.For<Bot>("O");
        secondBot
            .When(bot => bot.PlayMove(Arg.Any<Board>()))
            .Do(callInfo =>
            {
                switch (callInfo.Arg<Board>().GetAvailableTiles().Count())
                {
                    case 8:
                        MarkTile(callInfo, secondBot, 0, 0);
                        break;
                    case 6:
                        MarkTile(callInfo, secondBot, 0, 1);
                        break;
                    case 4:
                        MarkTile(callInfo, secondBot, 0, 2);
                        break;
                }
            });
        
        // Act
        var result = subject.Play(startingBot, secondBot);
    
        // Assert
        outputMock.Received().Print(
        " O | X |   " + "\n" +
            "---+---+---" + "\n" +
            " O |   | X " + "\n" +
            "---+---+---" + "\n" +
            " O | X |   ");

        result.Should().Be(secondBot);
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

public class AbortTestException: Exception;