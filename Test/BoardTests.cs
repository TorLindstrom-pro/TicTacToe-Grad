using FluentAssertions;
using TicTacToe_Grad;

namespace Test;

public class BoardTests
{
	[Fact(DisplayName = "New board returns all tiles as available")]
	void GettingAvailableTilesFromNewBoard_ShouldReturnAllTiles()
	{
		// Arrange
		var board = new Board();
		
		// Act
		var availableTiles = board.GetAvailableTiles();
		
		// Assert
		availableTiles.Count().Should().Be(9);
	}
	
	[Fact(DisplayName = "A tile can be marked")]
	void Mark_ShouldMarkTheTileWithTheMarker()
	{
		// Arrange
		var tile = new Tile();
		
		// Act
		tile.Mark("X");
		
		// Assert
		tile.Marker.Should().Be("X");
	}
}