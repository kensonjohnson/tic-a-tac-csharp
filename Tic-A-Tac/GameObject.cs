namespace Tic_A_Tac
{
	public abstract class GameObject
	{
		protected bool needsRender;
		protected OutputController Screen => OutputController.Instance;
		protected GameObject()
		{
			needsRender = false;
		}



		public virtual void Update()
		{
			return;
		}
		public virtual void Render()
		{
			return;
		}
	}

}
