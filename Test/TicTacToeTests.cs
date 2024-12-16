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

    [Fact(DisplayName = "Starting game should print starting bot")]
    public void StartingGame_ShouldPrintStartingBot()
    {
        // Arrange
        var outputMock = Substitute.For<Output>();
        
        var subject = new TicTacToe(outputMock);
        
        // Act
        subject.Play(new Bot("X"), new Bot("O"));
        
        // Assert
        outputMock.Received().Print("Bot X starts the game");
    }
}