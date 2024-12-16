using NSubstitute;
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
        Received.InOrder(() =>
        {
            outputMock.Print("   |   |   ");
            outputMock.Print("---+---+---");
            outputMock.Print("   |   |   ");
            outputMock.Print("---+---+---");
            outputMock.Print("   |   |   ");
        });
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
}