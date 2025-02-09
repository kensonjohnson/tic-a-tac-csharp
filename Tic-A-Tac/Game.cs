namespace Tic_A_Tac
{
	public class Game
	{
		private List<GameObject> GameObjects { get; set; }
		private readonly int targetFPS = 60;
		private bool Quit { get; set; }
		private ScoreBoard scoreBoard;
		private int playerScore;
		private int computerScore;
		protected OutputController Screen => OutputController.Instance;

		private enum GameState
		{
			WaitingForInitialization,
			ShowWelcome,
			WaitForPlayerToStart,
			DrawBoard,
			ComputerInput,
			PlayerInput,
			ProcessMoves,
			CalculateTotals,
			DisplayFinalMessage,
			ExitGame,
		}
		private GameState gameState = GameState.WaitingForInitialization;

		private InputController input;
		private Board board;
		private Computer computer;
		private Player player;

		public Game()
		{
			GameObjects = [];
			input = new InputController();
			board = new Board.BoardBuilder().SetRows(5).SetColumns(5).AddColumnHeaders('1').AddRowHeaders('A').SetPosition(5, 3).Build();
			computer = new(PlayerMarker.O);
			player = new(board, input);
			scoreBoard = new(4, 0);
			Initialize();
		}

		// Use this to set up needed game objects
		private void Initialize()
		{
			GameObjects.Add(board);
			GameObjects.Add(scoreBoard);
			gameState = GameState.ShowWelcome;
		}

		public void Run()
		{
			while (!Quit)
			{
				UpdateGameObjects();
				RenderGameObjects();
				Thread.Sleep(1000 / targetFPS);
			}
			Screen.Clear();
			Screen.WriteLine("Shutting down...");
		}

		private void UpdateGameObjects()
		{
			input.GetInputs();
			if (input.IsKeyPressed(InputController.KeyEscape))
			{
				Quit = true;
				return;
			}


			switch (gameState)
			{
				case GameState.ShowWelcome:
					ShowWelcome();
					gameState = GameState.WaitForPlayerToStart;
					break;

				case GameState.WaitForPlayerToStart:
					bool enterPressed = input.IsKeyPressed(InputController.KeyEnter);
					if (enterPressed) gameState = GameState.DrawBoard;
					break;

				case GameState.DrawBoard:
					board.Init();
					scoreBoard.Init();
					gameState = GameState.ComputerInput;
					break;

				case GameState.ComputerInput:
					computer.ChooseCoord(board);
					gameState = GameState.PlayerInput;
					break;

				case GameState.PlayerInput:
					player.Update();
					bool choiceMade = player.ChooseCoord();
					if (choiceMade) gameState = GameState.ProcessMoves;
					break;

				case GameState.ProcessMoves:
					board.MakeMoves(player.nextMove, computer.nextMove);
					var score = board.GetFinalScores();
					scoreBoard.UpdateScores(score.Item1, score.Item2);
					if (board.GetAvailableMoves().Count == 0)
					{
						gameState = GameState.DisplayFinalMessage;
					}
					else
					{
						gameState = GameState.ComputerInput;
					}
					break;

				case GameState.DisplayFinalMessage:
					var scores = board.GetFinalScores();
					Screen.SetCursorPosition(0, 17);
					Screen.WriteLine("     Game Over!");
					Screen.WriteLine();
					Screen.WriteLine($"     Player got: {scores.Item1} points!");
					Screen.WriteLine($"     Computer got: {scores.Item2} points!");
					Screen.WriteLine();
					if (scores.Item1 > scores.Item2)
					{
						Screen.WriteLine("     Player wins!");
					}
					else
					{
						Screen.WriteLine("     Computer wins!");
					}
					Screen.WriteLine();
					Screen.WriteLine("     Press any key to exit...");
					while (!Console.KeyAvailable) { }
					gameState = GameState.ExitGame;
					break;

				case GameState.ExitGame:
					Quit = true;
					break;
			}
		}

		private void RenderGameObjects()
		{
			foreach (GameObject gameObject in GameObjects)
			{
				gameObject.Render();
			}

			Screen.SetCursorPosition(0, 0);
		}

		private void ShowWelcome()
		{
			// NOTE: Temporary function
			Screen.Clear();


            // Set text color to blue
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("****************************************************************************");

            // Set text color to orange Yellow
            Console.ForegroundColor = ConsoleColor.Yellow;


            Console.WriteLine("\u001B[94m             __        __   _                            _        ");
            Console.WriteLine("__/\\__       \\ \\      / /__| | ___ ___  _ __ ___   ___  | |_ ___  ");
            Console.WriteLine("\\    /        \\ \\ /\\ / / _ \\ |/ __/ _ \\| '_ ` _ \\ / _\u001B[92m \\ | __/ _ \\ ");
            Console.WriteLine("/_  _\\         \\ V  V /  __/ | (_| (_) | | | | | |  __/ | || (_) |");
            Console.WriteLine("  \\/            \\_/\\_/ \\___|_|\\___\\___/|_| |_| |_|\\___|  \\__\\___/ ");
            Console.WriteLine("                                        \u001B[93m                          ");
            Console.WriteLine(" _____ ___ ____      _      _____  _    ____             ");
            Console.WriteLine("|_   _|_ _/ ___|    / \\    |_   _|/ \\  / ___|      __/\\__");
            Console.WriteLine("  | |  | | |       / _ \\     | | / _ \\| |   \u001B[91m       \\    /");
            Console.WriteLine("  | |  | | |___   / ___ \\    | |/ ___ \\ |___       /_  _\\");
            Console.WriteLine("  |_| |___\\____| /_/   \\_\\   |_/_/   \\_\\____|        \\/  ");
            Console.WriteLine("                                                         \u001B[0m");

            // Set text color to green
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("*****************************************************************************");

          

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Get ready to play an exciting game of Tic A Tac!");
            Console.WriteLine();

            // Reset text color to default
            Console.ResetColor();
            GameRules();

            Console.WriteLine();
            Screen.WriteBlueBackground("Press enter to continue...");
		}
        public void GameRules()
        {
            Console.WriteLine("Tic A Tac Game Rules:");
            Console.WriteLine("1. The game is played on a  5x5 grid.");
            Console.WriteLine("2. One player is 'X' and the other player is 'O'. Players take turns placing their symbols on the grid.");
            Console.WriteLine("3. The goal is to be the first player to get five of the symbol in a row or column.");
            Console.WriteLine("4. Players alternate turns until one player wins or the grid is full.");
            Console.WriteLine("5. A player wins by placing five of their symbols in a horizontal or vertical row.");
            Console.WriteLine("6. If all cells are filled and no player has achieved this, the game is a draw.");
            Console.WriteLine("7. If both players place their symbols in the same cell, that cell becomes blocked.");
        }
    }
}
