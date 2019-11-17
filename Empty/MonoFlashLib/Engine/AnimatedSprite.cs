using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFlash.Engine;
using System;
using System.Collections.Generic;

namespace MonoFlashLib.Engine
{
	public class AnimatedSprite : Sprite
	{
		private readonly List<Rectangle> frames;
		private readonly bool            isCyclic;
		private readonly Action          onEnd;
		private readonly int             scale;
		private readonly float           speed;
		private readonly Texture2D       texture;
		private          int             current;
		private          int             direction = 1;
		private          float           timer;
		public           Rectangle       defaultSprite;
		public           bool            isStarted = true;
		public           int             LoopCount;

		public AnimatedSprite(Texture2D texture, int scale = 1, float speed = 0.3f, bool isCyclic = false, Action onEnd = null)
		{
			this.texture  = texture;
			this.speed    = speed;
			this.isCyclic = isCyclic;
			this.onEnd    = onEnd;
			this.scale    = scale;
			frames        = new List<Rectangle>();
		}

		public override void Update(float delta)
		{
			base.Update(delta);

			if (isStarted)
			{
				if (timer >= 1)
				{
					LoopCount++;
					current += 1 * direction;

					if (current >= frames.Count || current < 0)
					{
						if (isCyclic)
						{
							current   =  frames.Count - 1;
							direction *= -1;
						}
						else
						{
							current = 0;
						}
					}

					timer = 0;

					onEnd?.Invoke();
				}

				timer += speed;
			}
		}

		public override void Draw(SpriteBatch sb, GameTime gameTime)
		{
			base.Draw(sb);
			Rectangle source = isStarted ? frames[current] : defaultSprite;

			sb.Draw(
				texture,
				new Vector2((float)globalX, (float)globalY),
				sourceRectangle: source,
				scale: new Vector2((float)Scale,                (float)Scale),
				origin: new Vector2(frames[current].Width / 2f, frames[current].Height / 2f),
				rotation: (float)rotation);
		}

		public void AddFrames(params Rectangle[] frames)
		{
			this.frames.AddRange(frames);
		}
	}
}