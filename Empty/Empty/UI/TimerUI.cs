﻿using MonoFlash.Engine;

namespace Empty.UI
{
	public class TimerUI : Sprite
	{
		private readonly Quad  quad;
		private readonly float baseWidth = Values.SCREEN_WIDTH / 3;

		/// <inheritdoc />
		public TimerUI()
		{
			quad   = new Quad(baseWidth, Values.SCREEN_HEIGHT / 40, 0x2ecc71);
			quad.x = Values.SCREEN_WIDTH / 2 - baseWidth / 2;
			AddChild(quad);
		}

		/// <inheritdoc />
		public override void Update(float delta)
		{
			quad.x = Values.SCREEN_WIDTH / 2 - quad.width / 2;
			base.Update(delta);
		}

		public void SetTimer(float timer)
		{
			quad.width = baseWidth * timer;
		}
	}
}