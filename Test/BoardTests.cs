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

	[Fact(DisplayName = "Gettign available tiles does not return marked tiles")]
	void GettingAvailableTiles_ShouldOnlyReturnUnmarkedTiles()
	{
		// Arrange
		var board = new Board();
		
		var tile = board
			.GetAvailableTiles()
			.Single(tile => tile is { X: 1, Y: 1 });
		
		tile.Mark("X");
		
		// Act
		var availableTiles = board.GetAvailableTiles();
		
		// Assert
		var enumerable = availableTiles.ToList();
		
		enumerable.Count.Should().Be(8);
		enumerable.Should().NotContain(tile);
	}
	
	[Fact(DisplayName = "A tile can be marked")]
	void Mark_ShouldMarkTheTileWithTheMarker()
	{
		// Arrange
		var tile = new Tile(0, 0);
		
		// Act
		tile.Mark("X");
		
		// Assert
		tile.Marker.Should().Be("X");
	}
}