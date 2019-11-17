using Empty.GameObjects.Humans;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFlash.Engine;
using System.Linq;

namespace Empty.GameObjects
{
	public class OurIsland : Island
	{
		private BaseHuman selectedHuman;
		
		public void OnClick(Point tile)
		{
			if (selectedHuman == null)
			{
				BaseHuman human = humans.FirstOrDefault(h => h.tilePos == (tile));

				if (human != null)
				{
					selectedHuman = human;
				}
			}
			else
			{
				(int x1, int x2) = (tile);
				selectedHuman.SetTilePos(x1, x2);
				selectedHuman = null;
			}
		}

		/// <inheritdoc />
		public override void Draw(SpriteBatch sb, GameTime gameTime = null)
		{
			sb.DrawRectangle(node*Values.TILE_SIZE, Vector2.One * Values.TILE_SIZE, re, 1, 0);
			base.Draw(sb, gameTime);
		}
	}
}