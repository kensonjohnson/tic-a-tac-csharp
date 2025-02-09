using Tic_A_Tac;

namespace Tic_A_TacTests
{
	[TestClass]
	public sealed class TestPlayer
	{
		private InputController inputController;

		[TestInitialize]
		public void init()
		{
			inputController = new InputController();
		}

		[TestMethod]
		public void CanCreatePlayer()
		{
			var player = new Player(PlayerMarker.O, inputController);
			Assert.IsNotNull(player);

		}
	}

}
