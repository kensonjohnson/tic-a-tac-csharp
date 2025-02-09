using System.Runtime.InteropServices;

namespace Tic_A_Tac
{
	public class InputController
	{
		// For a list of key-codes, look here: 
		// https://learn.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes
		public const int KeyEnter = 0x0D;
		public const int KeyEscape = 0x1B;
		public const int KeySpace = 0x20;
		public const int KeyLeft = 0x25;
		public const int KeyUp = 0x26;
		public const int KeyRight = 0x27;
		public const int KeyDown = 0x28;
		public const int KeyA = 0x41;
		public const int KeyD = 0x44;
		public const int KeyQ = 0x51;
		public const int KeyS = 0x53;
		public const int KeyW = 0x57;
		private readonly List<int> Keys = [
			KeyEnter,
			KeyEscape,
			KeySpace,
			KeyLeft,
			KeyUp,
			KeyRight,
			KeyDown,
			KeyA,
			KeyD,
			KeyQ,
			KeyS,
			KeyW
		];
		private readonly HashSet<int> inputs = [];

		public InputController()
		{
			// This is to clear out all currently registered inputs
			GetInputs();
		}

		/// <summary>
		/// Detect current state of key presses
		/// </summary>
		public void GetInputs()
		{
			inputs.Clear();

			// If no key is being pressed, do nothing
			if (!Console.KeyAvailable) return;

			// This is to prevent keystrokes from showing in terminal after closing app.
			// The `true` here is to "intercept" the keypress, and not allow it to act normally.
			Console.ReadKey(true);
			foreach (int key in Keys)
			{
				if (GetAsyncKeyState(key) is not 0)
				{
					inputs.Add(key);
				}
			}
		}

		/// <summary>
		/// Get state of key in question. Returns true if key is currently pressed, otherwise false.
		/// </summary>
		/// <param name="key"></param>
		public bool IsKeyPressed(int key)
		{
			return inputs.Contains(key);
		}


		[DllImport("user32.dll")]
		private static extern short GetAsyncKeyState(int vKey);
	}
}
