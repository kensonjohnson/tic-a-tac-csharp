namespace Tic_A_Tac
{
	public class OutputController
	{
		private static OutputController _instance;

		// Private constructor to prevent instantiation
		private OutputController() { }

		public static OutputController Instance
		{
			get
			{
				// Ensures that only a single version of OutputController is called
				if (_instance == null)
				{
					_instance = new OutputController();
				}
				return _instance;
			}
		}

		public GameObject GameObject
		{
			get => default;
			set
			{
			}
		}

		/// <summary>
		/// Write the specified output to the console
		/// </summary>
		/// <param name="output">The output</param>
		public void Write(string output)
		{
			Console.Write(output);
		}

		/// <summary>
		/// Write the output with a new line
		/// </summary>
		/// <param name="output"></param>
		public void WriteLine(string output)
		{
			Console.WriteLine(output);
		}

		/// <summary>
		/// Write an empty new line
		/// </summary>
		public void WriteLine()
		{
			Console.WriteLine();
		}

		/// <summary>
		/// Clear the output
		/// </summary>
		public void Clear()
		{
			Console.Clear();
		}

		/// <summary>
		/// Move the cursor to the given x and y in the console window
		/// </summary>
		public void SetCursorPosition(int x, int y)
		{
			Console.SetCursorPosition(x, y);
		}

		/// <summary>
		/// Write the specified output to the console with a blue background
		/// </summary>
		/// <param name="output">The output</param>
		public void WriteBlueBackground(string output)
		{
			Console.BackgroundColor = ConsoleColor.Blue;
			Console.Write(output);
			Console.ResetColor();
		}

		/// <summary>
		/// Write the specified output to the console with a yellow background
		/// </summary>
		/// <param name="output">The output</param>
		public void WriteYellowBackground(string output)
		{
			Console.BackgroundColor = ConsoleColor.DarkYellow;
			Console.Write(output);
			Console.ResetColor();
		}

		/// <summary>
		/// Write the specified output to the console with a yellow background
		/// </summary>
		/// <param name="output">The output</param>
		public void WriteRed(string output)
		{
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.Write(output);
			Console.ResetColor();
		}

	}
}
