namespace Tic_A_Tac
{
	internal class ScoreBoard : GameObject
	{
		private int playerScore;
		private int computerScore;
		private readonly int xPos;
		private readonly int yPos;

		public ScoreBoard(int x, int y)
		{
			xPos = x;
			yPos = y;
		}

		public override void Render()
		{
			if (!needsRender) return;
			Screen.SetCursorPosition(xPos, yPos);
			Screen.Write(new string(' ', Console.WindowWidth));
			Screen.SetCursorPosition(xPos, yPos);
			Screen.Write("Player: " + playerScore + " Computer: " + computerScore);
			needsRender = false;
		}

		public void UpdateScores(int player, int computer)
		{
			playerScore = player;
			computerScore = computer;
			needsRender = true;
		}

		public void Init()
		{
			needsRender = true;
		}
	}
}
