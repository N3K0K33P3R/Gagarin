using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoFlash.Engine;
using System;

namespace MonoFlashLib.Engine
{
	public class Slider : Sprite
	{
		private readonly Quad      main;
		private readonly Quad      slider;
		private          Rectangle collider;
		private          Rectangle colliderMouse;
		private          double    value;

		public double Value
		{
			get { return value; }
			set { value = value; }
		}

		public event Action<double> onValueChanged;

		public Slider(float width, float height, uint colorBack, uint colorSlider)
		{
			main   = new Quad(width, height / 10f, colorBack);
			main.y = height / 2 - main.height / 2;
			slider = new Quad(width / 20f, height, colorSlider);
			AddChild(main);
			AddChild(slider);
			collider      = new Rectangle(0, 0, (int)width, (int)height);
			colliderMouse = new Rectangle(0, 0, 1,          1);
			this.width    = width;
			this.height   = height;
		}

		public override void Update(float delta)
		{
			collider.X             = (int)globalX;
			collider.Y             = (int)globalY;
			colliderMouse.Location = Mouse.GetState().Position;

			if (PARAMS.IsActive &&
				Mouse.GetState().LeftButton == ButtonState.Pressed &&
				collider.Intersects(colliderMouse))
			{
				double position = colliderMouse.X - globalX;
				slider.x = position;
				value    = position / width;
				onValueChanged(value);
			}

			base.Update(delta);
		}
	}
}