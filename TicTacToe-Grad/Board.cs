namespace TicTacToe_Grad;

public class Board
{
	public List<Tile> Tiles { get; } =
	[
		new(0, 0),
		new(0, 1),
		new(0, 2),
		new(1, 0),
		new(1, 1),
		new(1, 2),
		new(2, 0),
		new(2, 1),
		new(2, 2)
	];

	public IEnumerable<Tile> GetAvailableTiles() => Tiles.Where(tile => tile.Marker is null);
}

public class Tile(int x, int y)
{
	public int X { get; } = x;
	public int Y { get; } = y;

	public void Mark(string? marker)
	{
		Marker = marker;
	}

	public string? Marker { get; private set; }
}