namespace TicTacToe_Grad;

public class TicTacToe(Output output)
{
	public void Play(Bot bot, Bot bot1)
	{
		output.Print("   |   |   ");
		output.Print("---+---+---");
		output.Print("   |   |   ");
		output.Print("---+---+---");
		output.Print("   |   |   ");
		output.Print("Bot " + bot.Marker + " starts the game");

		var board = new Board();
		
		bot.PlayMove(board);
		
		output.Print(" " + board.Tiles[0] + " |   |   ");
		output.Print("---+---+---");
		output.Print("   |   |   ");
		output.Print("---+---+---");
		output.Print("   |   |   ");
	}
}