using MonoFlash.Engine;
using System;

namespace Empty.GameObjects
{
	public class EnemyIsland : Island
	{
		private readonly AnimationController acY;
		private readonly AnimationController acSpeed;
		public           float               Velocity     { get; set; }
		public           float               Acceleration { get; set; }

		public bool IsDefeated { get; set; }

		/// <inheritdoc />
		public EnemyIsland(int w = IslandSize, int h = IslandSize) : base(w, h)
		{
			y            = -h * 2 * Values.TILE_SIZE;
			Velocity     = (float)Values.GlobalSpeed;
			Acceleration = -0.1f;

			acY     = new AnimationController((float)y);
			acSpeed = new AnimationController((float)Values.GlobalSpeed);

			acY.StartAnimation(Maths.easeInOutQuad, y, 0, 0.005);
			acSpeed.StartAnimation(Maths.easeInOutQuad, Values.GlobalSpeed, 0, 0.005);
		}

		/// <inheritdoc />
		public override void Update(float delta)
		{
			if (IsDefeated)
			{
				y += Values.GlobalSpeed;
			}
			else
			{
				y = acY.MakeStep(delta);
			}

			Values.GlobalSpeed = acSpeed.MakeStep(delta);
			base.Update(delta);
		}

		public void Kill()
		{
			IsDefeated = true;
			acSpeed.StartAnimation(Maths.easeInOutQuad, 0, 1, 0.005);
		}

		private double GetTime() => -(Math.Sqrt(2 * Acceleration * x + Velocity * Velocity) + Velocity) / Acceleration;
	}
}