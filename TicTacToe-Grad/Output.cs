namespace TicTacToe_Grad;

public class Output
{
	public void Print(string line)
	{
		Console.WriteLine(line);
	}

	public virtual void HangOnScreen()
	{
		Thread.Sleep(2000);
	}
}