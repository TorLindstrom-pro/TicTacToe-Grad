namespace TicTacToe_Grad;

public class Program
{
	public static void Main(string[] args)
	{
		var ticTacToe = new TicTacToe(new Output());
		
		ticTacToe.Play(new Bot("X"), new Bot("O"));
	}
}