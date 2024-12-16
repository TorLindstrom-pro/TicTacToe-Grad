using System.Text;

namespace TicTacToe_Grad;

public class TicTacToe(Output output)
{
	public Bot Play(Bot startingBot, Bot secondBot)
	{
		var board = new Board();
		
		PrintBoard(board);
		
		output.Print("Bot " + startingBot.Marker + " starts the game");

		var currentBot = startingBot;
		var waitingBot = secondBot;
		
		while (!LastMoveWon())
		{
			currentBot.PlayMove(board);
			PrintBoard(board);
			
			(currentBot, waitingBot) = (waitingBot, currentBot);
		}
		
		return currentBot;
		return waitingBot;

		bool LastMoveWon() => board.Tiles
			.Where(tile => tile.X == 0)
			.All(tile => tile.Marker == waitingBot.Marker);
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