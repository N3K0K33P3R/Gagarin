using System;

namespace Empty.Helpers
{
	public static class RandomExtensions
	{
		// Return a random value between 0 inclusive and max exclusive.
		public static double NextDouble(this Random rand, double max) => rand.NextDouble() * max;

		// Return a random value between min inclusive and max exclusive.
		public static double NextDouble(
			this Random rand,
			double min,
			double max) =>
			min + rand.NextDouble() * (max - min);
	}
}