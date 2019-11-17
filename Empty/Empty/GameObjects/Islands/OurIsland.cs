using Empty.Building;
using Empty.GameObjects.Humans;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFlash.Engine;
using System;
using System.Linq;

namespace Empty.GameObjects
{
	public class OurIsland : Island
	{
		private IActionable       selected;
		private Action<BaseHuman> showInfo;
		private Action            removeInfo;

		/// <inheritdoc />
		public OurIsland(Action<BaseHuman> showInfo, Action removeInfo, int w = IslandSize, int h = IslandSize) : base(w, h)
		{
			this.showInfo   = showInfo;
			this.removeInfo = removeInfo;
		}


		/// <inheritdoc />
		public override void Draw(SpriteBatch sb, GameTime gameTime = null)
		{
			sb.DrawRectangle(node * Values.TILE_SIZE, Vector2.One * Values.TILE_SIZE, re, 1, 0);

			if (selected is Cannon g)
			{
				var v1    = new Vector2(g.position.X + Values.TILE_SIZE / 2,              g.position.Y + Values.TILE_SIZE / 2);
				var v2    = new Vector2(node.X * Values.TILE_SIZE + Values.TILE_SIZE / 2, node.Y * Values.TILE_SIZE + Values.TILE_SIZE / 2);
				var angle = Ball.GetAngle(v1, v2);
				Trace(angle);

				Color color = angle < 0.5 && angle > -0.5 ? Color.Green : Color.Red;

				sb.DrawLine(
					v1,
					v2,
					color,
					0);
			}

			base.Draw(sb, gameTime);
		}

		public void OnClick(Point tile)
		{
			if (selected == null)
			{
				BaseHuman human     = FindHuman(tile);
				Structure structure = FindStructure(tile);

				if (human != null)
				{
					selected = human;
					showInfo(human);
				}
				else if (structure != null)
				{
					selected = structure;
				}
			}
			else
			{
				(int x1, int x2) = tile;
				selected.OnClick(x1, x2);
				selected = null;
				removeInfo();
			}
		}

		/// <inheritdoc />
		public override void Update(float delta)
		{
			base.Update(delta);
		}
	}
}