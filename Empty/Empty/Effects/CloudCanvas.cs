using MonoFlash.Engine;
using System.Collections.Generic;

namespace Empty.Effects
{
	public class CloudCanvas : Sprite
	{
		private readonly List<Cloud> clouds;
		private          int         Timer { get; set; }

		/// <inheritdoc />
		public CloudCanvas()
		{
			clouds = new List<Cloud>();
		}

		/// <inheritdoc />
		public override void Update(float delta)
		{
			if (Timer == 0 && Values.GlobalSpeed > 0)
			{
				int textureId = Values.RANDOM.Next(0, Assets.clouds.Count);
				var cloud     = new Cloud(Assets.clouds[textureId]);
				AddChild(cloud);
				clouds.Add(cloud);
				Timer = Values.RANDOM.Next(50, 120);
			}

			for (int i = clouds.Count - 1; i >= 0; i--)
			{
				if (!(clouds[i].y > Values.SCREEN_HEIGHT + clouds[i].height))
				{
					continue;
				}

				RemoveChild(clouds[i]);
				clouds.Remove(clouds[i]);
			}

			Timer--;
			base.Update(delta);
		}
	}
}