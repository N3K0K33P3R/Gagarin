﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MonoFlash.Engine
{
	public static class Ext
	{
		public static async Task ForEachAsync<T>(this List<T> list, Func<T, Task> func)
		{
			foreach (T value in list)
			{
				await func(value);
			}
		}

		public static byte[] ReadAllBytes(this BinaryReader reader)
		{
			const int bufferSize = 4096;

			using (var ms = new MemoryStream())
			{
				var buffer = new byte[bufferSize];
				int count;

				while ((count = reader.Read(buffer, 0, buffer.Length)) != 0)
				{
					ms.Write(buffer, 0, count);
				}

				return ms.ToArray();
			}
		}

		public static double GetDist(this Point p1, Point p2) => Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));

		public static void Shuffle<T>(this IList<T> list, Random random)
		{
			int n = list.Count;

			while (n > 1)
			{
				n--;
				int k     = random.Next(n + 1);
				T   value = list[k];
				list[k] = list[n];
				list[n] = value;
			}
		}
	}
}