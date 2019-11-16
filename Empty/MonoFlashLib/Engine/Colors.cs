using Microsoft.Xna.Framework;
using System;

namespace MonoFlash.Engine
{
	public class Colors
	{
		public static Color hexToRGB(uint hex)
		{
			uint r = (hex >> 16) & 0xFF;
			uint g = (hex >> 8) & 0xFF;
			uint b = hex & 0xFF;
			return new Color((int)r, (int)g, (int)b);
		}

		public static uint RGBtoHex(int r, int g, int b) => (uint)(((r & 0xFF) << 16) + ((g & 0xFF) << 8) + (b & 0xFF));

		public static uint uintLerp(uint start, uint target, float i)
		{
			if (Math.Abs((target - start) * i) < i)
			{
				return target;
			}

			if (start != target) { }

			return (uint)(start + i * (target - (int)start));
		}

		public static uint colorLerp(uint cur, uint target, float t)
		{
			if (t > 1)
			{
				t = 1;
			}

			Color _colorsCur    = hexToRGB(cur);
			Color _colorsTarget = hexToRGB(target);

			_colorsCur.R = (byte)uintLerp(_colorsCur.R, _colorsTarget.R, t);
			_colorsCur.G = (byte)uintLerp(_colorsCur.G, _colorsTarget.G, t);
			_colorsCur.B = (byte)uintLerp(_colorsCur.B, _colorsTarget.B, t);

			return RGBtoHex(_colorsCur.R, _colorsCur.G, _colorsCur.B);
		}
	}
}