using Empty.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace Empty.Building
{
	public class Cannon : Structure
	{
		public Cannon(Texture2D texture, int stoneCost, int timberCost, int ironCost, int workCost, Vector2 offset = default) : base(
			texture,
			stoneCost,
			timberCost,
			ironCost,
			workCost,
			offset) { }

		public override bool IsCanPut(Island island) =>
			island.GetCellByMouse != TileType.Empty && island.FindHuman(Main.instance.MousePosition.ToPoint()) == null;


		public override void OnAddOnGrid(ref TileType[,] types, int i, int j) { }

		/// <inheritdoc />
		public override void OnClick(int x, int y)
		{
			Main.instance.Shot(this, new Point(x, y));
			Console.WriteLine("PUSH");
			base.OnClick(x, y);
		}
	}
}