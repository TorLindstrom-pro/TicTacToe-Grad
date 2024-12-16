namespace TicTacToe_Grad;

public class Board
{
	public IEnumerable<Tile> GetAvailableTiles()
	{
		return new List<Tile>()
		{
			new Tile(),
			new Tile(),
			new Tile(),
			new Tile(),
			new Tile(),
			new Tile(),
			new Tile(),
			new Tile(),
			new Tile()
		};
	}
}

public class Tile
{
}