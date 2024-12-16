namespace TicTacToe_Grad;

public class Output
{
	public virtual void Print(string line)
	{
		Console.WriteLine(line);
	}

	public virtual void HangOnScreen()
	{
		Thread.Sleep(2000);
	}
}