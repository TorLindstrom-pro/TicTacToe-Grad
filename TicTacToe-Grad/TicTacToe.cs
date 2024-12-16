namespace TicTacToe_Grad;

public class TicTacToe(Output output)
{
	public void Play(Bot bot, Bot bot1)
	{
		var board = new Board();
		
		PrintBoard(board);
		
		output.Print("Bot " + bot.Marker + " starts the game");
		
		bot.PlayMove(board);
		
		PrintBoard(board);
	}

	private void PrintBoard(Board board)
	{
		PrintRow(0);
		output.Print("---+---+---");
		PrintRow(1);
		output.Print("---+---+---");
		PrintRow(2);
		
		return;

		void PrintRow(int index)
		{
			var paddedTileMarkers = board.Tiles
				.Where(tile => tile.Y == index)
				.Select(tile => " " + (tile.Marker ?? " ") + " ");
			var formattedRow = string.Join("|", paddedTileMarkers);
			output.Print(formattedRow);
		}
	}
}