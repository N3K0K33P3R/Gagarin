using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;

namespace MonoFlash.Engine
{
	public class Text : Sprite
	{
		public Color         color;
		public SpriteFont    font;
		public StringBuilder text;
		public object        textObject;

		public Text(SpriteFont font, object text, Color color)
		{
			this.font  = font;
			textObject = text;
			this.color = color;
			this.text  = new StringBuilder();
			this.text.Append(text);
			width  = font.MeasureString(text.ToString()).X;
			height = font.MeasureString(text.ToString()).Y;
		}

		public override void Draw(SpriteBatch sb, GameTime gameTime)
		{
			//Console.WriteLine(color);
			color.A = colorAlpha.A;
			base.Draw(sb);
			sb.DrawString(font, text.ToString(), new Vector2((int)globalX, (int)globalY), color);
		}

		public void SetText(params object[] list)
		{
			text.Clear();

			foreach (object item in list)
			{
				if (item != null)
				{
					text.Append(item + " ");
				}
			}

			width  = font.MeasureString(text).X;
			height = font.MeasureString(text).Y;
		}
	}
}