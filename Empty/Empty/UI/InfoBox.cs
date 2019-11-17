using Empty.GameObjects.Humans;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFlash.Engine;

namespace Empty.UI
{
	public class InfoBox : Sprite
	{
		private BaseHuman human;
		public  bool      Enabled { get; set; }

		/// <inheritdoc />
		public InfoBox(BaseHuman human)
		{
			this.human = human;
		}

		/// <inheritdoc />
		public override void Draw(SpriteBatch sb, GameTime gameTime = null)
		{
			sb.FillRectangle((float)globalX, (float)globalY, 36, 36, Colors.hexToRGB(0x7f8c8d), 0);
			base.Draw(sb, gameTime);
		}
	}
}