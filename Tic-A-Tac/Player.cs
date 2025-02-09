namespace Tic_A_Tac
{
	public class Player : GameObject
	{

		private Board board;
		private InputController input;
		public int nextMove { get; private set; }

		public Player(Board boardRef, InputController inputController)
		{
			board = boardRef;
			input = inputController;

		}
		private enum LetterValues
		{
			A,
			B,
			C,
			D,
			E,
		}

		public override void Update()
		{
			// Detect keyboard inputs
			bool up = input.IsKeyPressed(InputController.KeyW) || input.IsKeyPressed(InputController.KeyUp);
			bool right = input.IsKeyPressed(InputController.KeyD) || input.IsKeyPressed(InputController.KeyRight);
			bool down = input.IsKeyPressed(InputController.KeyS) || input.IsKeyPressed(InputController.KeyDown);
			bool left = input.IsKeyPressed(InputController.KeyA) || input.IsKeyPressed(InputController.KeyLeft);

			// Move index in correct direction
			if (up)
			{
				board.MoveIndexUp();
			}
			if (right)
			{
				board.MoveIndexRight();
			}
			if (down)
			{
				board.MoveIndexDown();
			}
			if (left)
			{
				board.MoveIndexLeft();
			}

		}

		public bool ChooseCoord()
		{
			bool enterPressed = input.IsKeyPressed(InputController.KeyEnter);

			if (!enterPressed) return false;

			if (board.IsSelectIndexAvailable())
			{
				nextMove = board.GetSelectedIndex();
				return true;
			}
			return false;
		}

	}

	public enum PlayerMarker
	{
		X = 'X',
		O = 'O',
	}


}


