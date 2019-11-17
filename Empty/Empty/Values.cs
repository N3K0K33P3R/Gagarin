using System;
using System.Collections.Generic;

namespace Empty
{
	public static class Values
	{
		public const            int          TILE_SIZE = 16;
		private static readonly List<string> NAMES     = new List<string> { "Троцкий", "Ульянов", "Маркс", "Энгельс", "Молотов" };
		public static readonly  Random       RANDOM    = new Random();
        public static float MapScale = 2;

        public static string GetRandomName() => NAMES[RANDOM.Next(0, NAMES.Count)];
	}
}