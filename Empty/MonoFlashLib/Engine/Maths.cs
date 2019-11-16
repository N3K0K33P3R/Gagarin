using System;

namespace MonoFlash.Engine
{
	public class Maths
	{
		public static double GetClosestAngle(double a, double b)
		{
			double target1 = b;
			double target2 = -(Math.PI * 2 - b);

			if (Math.Abs(a - target1) < Math.Abs(a - target2))
			{
				return target1;
			}

			return target2;
		}

		public static double Clamp(double start, double target, double speed)
		{
			if (start > target)
			{
				start -= speed;

				if (start < target)
				{
					start = target;
				}

				return start;
			}

			start += speed;

			if (start > target)
			{
				start = target;
			}

			return start;
		}

		public static double Lerp(double t, double a, double b) => a + (b - a) * t;

		public static double easeOutBack(double t, double b, double f)
		{
			var    s = 1.70158f;
			double c = f - b;
			float  d = 1;
			return c * ((t = t / d - 1) * t * ((s + 1) * t + s) + 1) + b;
		}

		/**
         *
         * @param	t - counter
         * @param	b - start position
         * @param	f - targer position
         * @return
         */
		public static double easeOutCubic(double t, double b, double f)
		{
			double c = f - b;
			var    d = 1;
			return c * ((t = t / d - 1) * t * t + 1) + b;
		}

		/**
         *
         * @param	t - counter
         * @param	b - start position
         * @param	f - targer position
         * @return
         */
		public static double easeOutBounce(double t, double b, double f)
		{
			double c = f - b;
			var    d = 1;

			if ((t /= d) < 1 / 2.75)
			{
				return c * (7.5625f * t * t) + b;
			}

			if (t < 2 / 2.75)
			{
				return c * (7.5625 * (t -= 1.5 / 2.75) * t + .75) + b;
			}

			if (t < 2.5 / 2.75)
			{
				return c * (7.5625 * (t -= 2.25 / 2.75) * t + .9375) + b;
			}

			return c * (7.5625 * (t -= 2.625 / 2.75) * t + .984375) + b;
		}

		/**
		 *
		 * @param	t - counter
		 * @param	b - start position
		 * @param	f - targer position
		 * @return
		 */
		public static double easeInOutQuint(double t, double b, double f)
		{
			double c = f - b;
			var    d = 1;

			if ((t /= d / 2f) < 1)
			{
				return c / 2 * t * t * t * t * t + b;
			}

			return c / 2 * ((t -= 2) * t * t * t * t + 2) + b;
		}

		/**
		 *
		 * @param	t - counter
		 * @param	b - start position
		 * @param	f - targer position
		 * @return
		 */

		public static double easeInElastic(double t, double b, double f)
		{
			double c = f - b;
			var    d = 1;
			var    s = 1.70158;
			double p = 0;
			double a = c;

			if (t == 0)
			{
				return b;
			}

			if ((t /= d) == 1)
			{
				return b + c;
			}

			if (p != 0)
			{
				p = d * .3;
			}

			if (a < Math.Abs(c))
			{
				a = c;
				s = p / 4;
			}
			else
			{
				s = p / (2 * Math.PI) * Math.Asin(c / a);
			}

			return -(a * Math.Pow(2, 10 * (t -= 1)) * Math.Sin((t * d - s) * (2 * Math.PI) / p)) + b;
		}

		/**
		 *
		 * @param	t - counter
		 * @param	b - start position
		 * @param	f - targer position
		 * @return
		 */

		public static double easeOutExpo(double t, double b, double f)
		{
			double c = f - b;
			var    d = 1;
			return t == d ? b + c : c * (-Math.Pow(2, -10 * t / d) + 1) + b;
		}

		/**
		 *
		 * @param	t - counter
		 * @param	b - start position
		 * @param	f - targer position
		 * @return
		 */
		public static double easeInOutCubic(double t, double b, double f)
		{
			double c = f - b;
			var    d = 1;

			if ((t /= d / 2f) < 1)
			{
				return c / 2 * t * t * t + b;
			}

			return c / 2 * ((t -= 2) * t * t + 2) + b;
		}

		/**
		 *
		 * @param	t - counter
		 * @param	b - start position
		 * @param	f - targer position
		 * @return
		 */

		public static double easeOutCirc(double t, double b, double f)
		{
			double c = f - b;
			var    d = 1;
			return c * Math.Sqrt(1 - (t = t / d - 1) * t) + b;
		}

		/**
		 *
		 * @param	t - counter
		 * @param	b - start position
		 * @param	f - targer position
		 * @return
		 */

		public static double easeInOutQuad(double t, double b, double f)
		{
			double c = f - b;
			var    d = 1;

			if ((t /= d / 2f) < 1)
			{
				return c / 2 * t * t + b;
			}

			return -c / 2 * (--t * (t - 2) - 1) + b;
		}

		/**
         *
         * @param	t - counter
         * @param	b - start position
         * @param	f - targer position
         * @return
         */

		public static double easeInExpo(double t, double b, double f)
		{
			double c = f - b;
			var    d = 1;
			return t == 0 ? b : c * Math.Pow(2, 10 * (t / d - 1)) + b;
		}
	}
}