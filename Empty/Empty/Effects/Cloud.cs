using Empty.Helpers;
using Microsoft.Xna.Framework.Graphics;
using MonoFlash.Engine;

namespace Empty.Effects
{
	public class Cloud : Image
	{
		private double LocalSpeed { get; set; }

		/// <inheritdoc />
		public Cloud(Texture2D texture2D) : base(texture2D)
		{
			y          = -texture2D.Height;
			x          = Values.RANDOM.Next(0, Values.SCREEN_WIDTH);
			LocalSpeed = Values.RANDOM.NextDouble(-0.1, 0.1);
		}

		/// <inheritdoc />
		public override void Update(float delta)
		{
			y += Values.GlobalSpeed + LocalSpeed;
			base.Update(delta);
		}
	}
}