namespace Tic_A_Tac
{
	public class Board : GameObject
	{
		private int rows;
		private int columns;
		private int xPos;
		private int yPos;
		private const int width = 5;
		private Dictionary<string, string> theme;
		private bool hasColumnHeaders;
		private bool hasRowHeaders;
		private char columnHeaderChar;
		private char rowHeaderChar;
		private bool renderFirstBoard = true;
		private bool renderAllIndexes = true;
		private readonly List<int> indexesToRender = [];
		private int selectedIndex = 0;
		public enum Mark
		{
			Empty,
			X,
			O,
			Blocked,
		}
		private readonly List<Mark> board;
		private int playerScore = 0;
		private int computerScore = 0;

		private Board(BoardBuilder builder)
		{
			this.rows = builder.rows;
			this.columns = builder.columns;
			this.theme = builder.theme;
			this.hasColumnHeaders = builder.hasColumnHeaders;
			this.hasRowHeaders = builder.hasRowHeaders;
			this.columnHeaderChar = builder.columnHeaderChar;
			this.rowHeaderChar = builder.rowHeaderChar;
			this.xPos = builder.xPos;
			this.yPos = builder.yPos;
			board = [.. Enumerable.Repeat(Mark.Empty, 25)];
			board[12] = Mark.Blocked;
		}

		public class BoardBuilder
		{
			public int rows;
			public int columns;
			public Dictionary<string, string> theme = new Dictionary<string, string>
			{
				{ "topLeft", "╔" }, { "topRight", "╗" }, { "bottomLeft", "╚" }, { "bottomRight", "╝" },
				{ "horizontal", "═" }, { "vertical", "║" }, { "intersection", "╬" }, { "topIntersection", "╦" },
				{ "bottomIntersection", "╩" }, { "leftIntersection", "╠" }, { "rightIntersection", "╣" },
				{ "empty", " " }
			};
			public bool hasColumnHeaders = false;
			public bool hasRowHeaders = false;
			public char columnHeaderChar;
			public char rowHeaderChar;
			public int xPos = 0;
			public int yPos = 0;

			public BoardBuilder SetRows(int rows)
			{
				this.rows = rows;
				return this;
			}

			public BoardBuilder SetColumns(int columns)
			{
				this.columns = columns;
				return this;
			}

			public BoardBuilder SetTheme(Dictionary<string, string> theme)
			{
				this.theme = theme;
				return this;
			}

			public BoardBuilder AddColumnHeaders(char headerChar)
			{
				this.hasColumnHeaders = true;
				this.columnHeaderChar = headerChar;
				return this;
			}

			public BoardBuilder AddRowHeaders(char headerChar)
			{
				this.hasRowHeaders = true;
				this.rowHeaderChar = headerChar;
				return this;
			}

			public BoardBuilder SetPosition(int x, int y)
			{
				this.xPos = x;
				this.yPos = y;
				return this;
			}

			public Board Build()
			{
				return new Board(this);
			}
		}

		public void Init()
		{
			needsRender = true;
		}

		public void Calculate()
		{
			throw new NotImplementedException();
		}

		public override void Render()
		{
			if (!needsRender) return;

			if (renderFirstBoard)
			{
				Screen.Clear();
				DrawBase();
				renderFirstBoard = false;
			}

			if (renderAllIndexes)
			{
				for (int i = 0; i < board.Count; i++)
				{
					DrawIndex(i);
				}
				renderAllIndexes = false;
			}

			if (indexesToRender.Count > 0)
			{
				foreach (int i in indexesToRender)
				{
					DrawIndex(i);
				}

				indexesToRender.Clear();
			}

			needsRender = false;
		}

		private void PrintColumnHeaders(int startX, int startY)
		{
			if (hasColumnHeaders)
			{
				Screen.SetCursorPosition(startX, startY);
				for (int col = 0; col < columns; col++)
				{
					Screen.Write($"  {columnHeaderChar++} ");
				}
				Screen.WriteLine();
			}
		}

		private void PrintTopBorder(int startX, int startY)
		{
			Screen.SetCursorPosition(startX, startY);
			Screen.Write(theme["topLeft"]);
			for (int col = 0; col < columns - 1; col++)
			{
				Screen.Write($"{theme["horizontal"]}{theme["horizontal"]}{theme["horizontal"]}{theme["topIntersection"]}");
			}
			Screen.WriteLine($"{theme["horizontal"]}{theme["horizontal"]}{theme["horizontal"]}{theme["topRight"]}");
		}

		private void PrintRowContent(int startX, int startY, int row)
		{
			if (hasRowHeaders)
			{
				Screen.SetCursorPosition(startX - 2, startY);
				Screen.Write($" {rowHeaderChar++} ");
			}

			Screen.SetCursorPosition(startX, startY);
			Screen.Write(theme["vertical"]);
			for (int col = 0; col < columns; col++)
			{
				Screen.Write($" {theme["empty"]} {theme["vertical"]}");
			}
			Screen.WriteLine();
		}

		private void PrintRowSeparator(int startX, int startY, int row)
		{
			if (row < rows - 1)
			{
				if (hasRowHeaders)
				{
					Screen.SetCursorPosition(startX, startY);
				}
				Screen.Write(theme["leftIntersection"]);
				for (int col = 0; col < columns - 1; col++)
				{
					Screen.Write($"{theme["horizontal"]}{theme["horizontal"]}{theme["horizontal"]}{theme["intersection"]}");
				}
				Screen.WriteLine($"{theme["horizontal"]}{theme["horizontal"]}{theme["horizontal"]}{theme["rightIntersection"]}");
			}
			else
			{
				if (hasRowHeaders)
				{
					Screen.SetCursorPosition(startX, startY);
				}
				Screen.Write(theme["bottomLeft"]);
				for (int col = 0; col < columns - 1; col++)
				{
					Screen.Write($"{theme["horizontal"]}{theme["horizontal"]}{theme["horizontal"]}{theme["bottomIntersection"]}");
				}
				Screen.WriteLine($"{theme["horizontal"]}{theme["horizontal"]}{theme["horizontal"]}{theme["bottomRight"]}");
			}
		}

		public void DrawBase()
		{
			int startX = xPos; // Starting X position for the board
			int startY = yPos; // Starting Y position for the board

			PrintColumnHeaders(startX, startY);
			startY++;

			PrintTopBorder(startX, startY);
			startY++;

			for (int row = 0; row < rows; row++)
			{
				PrintRowContent(startX, startY, row);
				startY++;
				PrintRowSeparator(startX, startY, row);
				startY++;
			}
		}

		private void DrawIndex(int i)
		{
			int row = i / width; // y value
			int column = i % width; // x value
			var x = (column * 3) + column + 1;
			var y = row * 2 + 2;
			switch (board[i])
			{
				case Mark.Empty:
					DrawEmptyCell(x, y, i == selectedIndex);
					break;
				case Mark.X:
					DrawX(x, y, i == selectedIndex);
					break;
				case Mark.O:
					DrawO(x, y, i == selectedIndex);
					break;
				case Mark.Blocked:
					DrawBlocked(x, y, i == selectedIndex);
					break;
			}
		}

		const string empty = "   ";
		private void DrawEmptyCell(int x, int y, bool selected = false)
		{
			int relativeX = x + xPos;
			int relativeY = y + yPos;

			Screen.SetCursorPosition(relativeX, relativeY);
			if (selected)
			{
				Screen.WriteBlueBackground(empty);
			}
			else
			{
				Screen.Write(empty);
			}
		}

		const string xxx = " X ";
		private void DrawX(int x, int y, bool selected = false)
		{
			int relativeX = x + xPos;
			int relativeY = y + yPos;

			Screen.SetCursorPosition(relativeX, relativeY);
			if (selected)
			{
				Screen.WriteYellowBackground(xxx);
			}
			else
			{
				Screen.Write(xxx);
			}
		}

		const string ooo = " O ";
		private void DrawO(int x, int y, bool selected = false)
		{
			int relativeX = x + xPos;
			int relativeY = y + yPos;

			Screen.SetCursorPosition(relativeX, relativeY);
			if (selected)
			{
				Screen.WriteYellowBackground(ooo);
			}
			else
			{
				Screen.Write(ooo);
			}
		}

		const string block = "▓▓▓";
		private void DrawBlocked(int x, int y, bool selected = false)
		{
			int relativeX = x + xPos;
			int relativeY = y + yPos;

			Screen.SetCursorPosition(relativeX, relativeY);
			if (selected)
			{
				Screen.WriteRed(block);
			}
			else
			{
				Screen.Write(block);
			}
		}

		public void MakeMoves(int playerMove, int computerMove)
		{
			if (playerMove == computerMove)
			{
				board[playerMove] = Mark.Blocked;
			}
			else
			{
				board[playerMove] = Mark.X;
				board[computerMove] = Mark.O;
			}
			indexesToRender.Add(playerMove);
			indexesToRender.Add(computerMove);
			CalculateTotals();
			needsRender = true;
		}

		public List<int> GetAvailableMoves()
		{
			List<int> available = [];

			for (int i = 0; i < board.Count; i++)
			{
				if (board[i] == Mark.Empty) available.Add(i);
			}

			return available;
		}

		public void MoveIndexUp()
		{
			int index = selectedIndex - 5;
			if (index < 0) return;
			indexesToRender.Add(index);
			indexesToRender.Add(selectedIndex);
			selectedIndex = index;
			needsRender = true;
		}

		public void MoveIndexDown()
		{
			int index = selectedIndex + 5;
			if (index >= 25) return;
			indexesToRender.Add(index);
			indexesToRender.Add(selectedIndex);
			selectedIndex = index;
			needsRender = true;
		}

		public void MoveIndexLeft()
		{
			int relativeX = selectedIndex % 5;
			if (relativeX == 0) return;
			indexesToRender.Add(selectedIndex);
			selectedIndex--;
			indexesToRender.Add(selectedIndex);
			needsRender = true;
		}

		public void MoveIndexRight()
		{
			int relativeX = selectedIndex % 5;
			if (relativeX == 4) return;
			indexesToRender.Add(selectedIndex);
			selectedIndex++;
			indexesToRender.Add(selectedIndex);
			needsRender = true;
		}

		public bool IsSelectIndexAvailable()
		{
			return board[selectedIndex] == Mark.Empty;
		}

		public int GetSelectedIndex()
		{
			return selectedIndex;
		}

		private void CalculateTotals()
		{

			int player = 0;
			int computer = 0;

			// iterate through board
			for (int i = 0; i < board.Count; i++)
			{
				Mark mark = board[i];
				// Check that we only calculate x's and o's
				if (mark != Mark.O && mark != Mark.X)
				{
					continue;
				}

				int x = i % 5;
				int y = i / 5;

				// look right for 3
				int second = x + 1;
				int third = x + 2;

				// if not off board
				if (second < 5 && third < 5)
				{
					// if 3 in a row on true indexes
					if (board[i + 1] == mark && board[i + 2] == mark)
					{
						if (mark == Mark.X)
						{
							player += 1;
						}
						else
						{
							computer += 1;
						}
					}
				}

				// look down for 3
				int down1 = y + 1;
				int down2 = y + 2;

				// if not off board
				if (down1 < 5 && down2 < 5)
				{
					// if 3 in a row on true indexes
					if (board[i + 5] == mark && board[i + 10] == mark)
					{
						// add 1 to score
						if (mark == Mark.X)
						{
							player += 1;
						}
						else
						{
							computer += 1;
						}
					}
				}
			}
			playerScore = player;
			computerScore = computer;
		}

		public (int, int) GetFinalScores()
		{
			return (playerScore, computerScore);
		}
	}
}
