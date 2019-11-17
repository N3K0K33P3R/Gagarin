using Empty.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Empty.Building
{
	public abstract class Structure : IActionable
	{
		public Texture2D texture;
		public Rectangle Rect;

		public Vector2 Offset = new Vector2(0, -8);

		public Color StateColor;

		public Vector2 position;

		public int StoneCost;
		public int TimberCost;
		public int IronCost;

		public int WorkCost;

		public bool ShouldDelete = false;
		
		protected Structure(Texture2D texture, int stoneCost, int timberCost, int ironCost, int workCost, Vector2 offset = default)
		{
			Offset       = offset;
			this.texture = texture;
			StoneCost    = stoneCost;
			TimberCost   = timberCost;
			IronCost     = ironCost;
			WorkCost     = workCost;

			StateColor = Color.White;
			Rect       = new Rectangle(0, 0, texture.Width, texture.Height);
		}

		public abstract void OnAddOnGrid(ref TileType[,] types, int i, int j);

		public abstract bool IsCanPut(Island island);

		public void Draw(SpriteBatch sb)
		{
			sb.Draw(texture, position + Offset, StateColor);
		}

		/// <inheritdoc />
		public virtual void OnClick(int x, int y)
		{
		}
	}
}