using NSubstitute;
using TicTacToe_Grad;

namespace Test;

public class UnitTest1
{
    [Fact(DisplayName = "Starting game should print empty board")]
    public void StartingGame_ShouldPrintEmptyBoard()
    {
        // Arrange
        var outputMock = Substitute.For<Output>();
        
        var subject = new TicTacToe(outputMock);
        
        // Act
        subject.Play(new Bot(), new Bot());
        
        // Assert
        outputMock.Received().PrintBoard();
    }
}