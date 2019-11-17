using Empty.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Empty.Building
{
	internal class Wall : Structure
	{
		public Wall(Texture2D texture, int stoneCost, int timberCost, int ironCost, int workCost, Vector2 offset = default) : base(
			texture,
			stoneCost,
			timberCost,
			ironCost,
			workCost,
			offset) { }

		public override bool IsCanPut(Island island) => island.GetCellByMouse != TileType.Empty && island.FindHuman(Main.instance.MousePosition.ToPoint()) == null;


		public override void OnAddOnGrid(ref TileType[,] types, int i, int j) { }
	}
}