using System;
using System.Collections.Generic;

namespace Empty
{
	public static class Values
	{
		public const            int          TILE_SIZE     = 16;
		public const            int          SCREEN_WIDTH  = 1600;
		public const            int          SCREEN_HEIGHT = 800;
		public const            double       GlobalSpeed   = 1;
		private static readonly List<string> NAMES         = new List<string> { "Троцкий", "Ульянов", "Маркс", "Энгельс", "Молотов" };
		public static readonly  Random       RANDOM        = new Random();
		public static           float        MAP_SCALE     = 1;

        public static string GetRandomName() => NAMES[RANDOM.Next(0, NAMES.Count)];
	}
}