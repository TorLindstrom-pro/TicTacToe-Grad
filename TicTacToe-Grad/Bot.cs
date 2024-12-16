namespace TicTacToe_Grad;

public class Bot(string marker)
{
	public string Marker { get; } = marker;

	public virtual void PlayMove(Board any)
	{
		any.GetAvailableTiles()
			.Where(tile => tile.X == 0)
			.ToList()
			.ForEach(tile => tile.Mark(Marker));
	}
}