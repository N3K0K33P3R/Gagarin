using Empty.GameObjects.Humans;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFlash.Engine;
using System.Collections.Generic;

namespace Empty.UI
{
	internal class InfoPanel : Sprite
	{
		private readonly int       indent = 30;
		private readonly List<Row> rows;
		private          Quad      background;
		public static    int       widthPanel = 280;

		public InfoPanel(BaseHuman human)
		{
			rows              = new List<Row>();
			human.GunSkill    = (float)0.2;
			human.RepairSkill = 1;
			rows.Add(new Row($"NAME: {human.Name}"));
			rows.Add(new Row("GUN SKILL: ",    human.GunSkill));
			rows.Add(new Row("REPAIR SKILL: ", human.RepairSkill));
			width  = widthPanel;
			height = 90;
			AddBackground();
			RedrawRows();
		}

		public override void Draw(SpriteBatch sb, GameTime gameTime = null)
		{
			base.Draw(sb, gameTime);
		}

		public void RedrawRows()
		{
			double setY = background.y + 15;

			foreach (Row row in rows)
			{
				row.x =  background.x + 10;
				row.y =  setY;
				setY  += row.text.height + 10;
				AddChild(row);
			}
		}

		private void AddBackground()
		{
			background   = new Quad((float)width, (float)height, 0x2c3e50);
			background.x = indent;
			background.y = Values.SCREEN_HEIGHT - height - indent;
			AddChild(background);
		}
	}


	internal class Row : Sprite
	{
		public Text text;

		public Row(string text)
		{
			DrawText(text);
		}

		public Row(string text, double value)
		{
			DrawText(text);
			DrawBar(value);
		}

		public override void Draw(SpriteBatch sb, GameTime gameTime = null)
		{
			base.Draw(sb, gameTime);
		}

		private void DrawBar(double value)
		{
			var back = new Quad(InfoPanel.widthPanel - (float)text.width - 40, (float)text.height, 0xe74c3c);
			back.x = text.x + text.width + 10;
			back.y = text.y;
			var fill = new Quad((float)back.width * (float)value - 2, (float)back.height - 2, 0xf39c12);
			fill.x = back.x + 1;
			fill.y = back.y + 1;
			AddChild(back);
			AddChild(fill);
		}

		private void DrawText(string textToWrite)
		{
			text = new Text(Game1.fontForProperties, textToWrite, Colors.hexToRGB(0xecf0f1));
			AddChild(text);
		}
	}
}