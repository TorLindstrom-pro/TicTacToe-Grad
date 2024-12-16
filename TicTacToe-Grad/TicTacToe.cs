using System.Text;

namespace TicTacToe_Grad;

public class TicTacToe(Output output)
{
	public Bot Play(Bot startingBot, Bot secondBot)
	{
		var board = new Board();
		
		PrintBoard(board);
		
		output.Print("Bot " + startingBot.Marker + " starts the game");
		
		startingBot.PlayMove(board);
		
		PrintBoard(board);
		
		secondBot.PlayMove(board);
		startingBot.PlayMove(board);
		secondBot.PlayMove(board);
		startingBot.PlayMove(board);
		
		PrintBoard(board);

		return startingBot;
	}

	private void PrintBoard(Board board)
	{
		var stringBuilder = new StringBuilder();
		
		stringBuilder
			.Append(FormatRow(0))
			.Append("\n")
			.Append("---+---+---")
			.Append("\n")
			.Append(FormatRow(1))
			.Append("\n")
			.Append("---+---+---")
			.Append("\n")
			.Append(FormatRow(2));

		output.Print(stringBuilder.ToString());
		
		return;

		string FormatRow(int index)
		{
			var paddedTileMarkers = board.Tiles
				.Where(tile => tile.Y == index)
				.Select(tile => " " + (tile.Marker ?? " ") + " ");
			var formattedRow = string.Join("|", paddedTileMarkers);

			return formattedRow;
		}
	}
}