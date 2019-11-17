﻿using Microsoft.Xna.Framework;
using System;

namespace Empty
{
	public class PolygonHelper
	{
		private static readonly int INF = 10000;

		public static bool IsPointInPolygon(Point p, Point[] polygon)
		{
			double minX = polygon[0].X;
			double maxX = polygon[0].X;
			double minY = polygon[0].Y;
			double maxY = polygon[0].Y;

			for (var i = 1; i < polygon.Length; i++)
			{
				Point q = polygon[i];
				minX = Math.Min(q.X, minX);
				maxX = Math.Max(q.X, maxX);
				minY = Math.Min(q.Y, minY);
				maxY = Math.Max(q.Y, maxY);
			}

			if (p.X < minX || p.X > maxX || p.Y < minY || p.Y > maxY)
			{
				return false;
			}

			// http://www.ecse.rpi.edu/Homepages/wrf/Research/Short_Notes/pnpoly.html
			var inside = false;

			for (int i = 0,
					 j = polygon.Length - 1;
				 i < polygon.Length;
				 j = i++)
			{
				if (polygon[i].Y > p.Y != polygon[j].Y > p.Y &&
					p.X < (polygon[j].X - polygon[i].X) * (p.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X)
				{
					inside = !inside;
				}
			}

			return inside;
		}

		// Returns true if the point p lies  
		// inside the polygon[] with n vertices 
		public static bool IsInside(Point[] polygon, int n, Point p)
		{
			// There must be at least 3 vertices in polygon[] 
			if (n < 3)
			{
				return false;
			}

			// Create a point for line segment from p to infinite 
			var extreme = new Point(INF, p.Y);

			// Count intersections of the above line  
			// with sides of polygon 
			int count = 0,
				i     = 0;

			do
			{
				int next = (i + 1) % n;

				// Check if the line segment from 'p' to  
				// 'extreme' intersects with the line  
				// segment from 'polygon[i]' to 'polygon[next]' 
				if (DoIntersect(
					polygon[i],
					polygon[next],
					p,
					extreme))
				{
					// If the point 'p' is colinear with line  
					// segment 'i-next', then check if it lies  
					// on segment. If it lies, return true, otherwise false 
					if (Orientation(polygon[i], p, polygon[next]) == 0)
					{
						return OnSegment(
							polygon[i],
							p,
							polygon[next]);
					}

					count++;
				}

				i = next;
			}
			while (i != 0);

			// Return true if count is odd, false otherwise 
			return count % 2 == 1; // Same as (count%2 == 1) 
		}


		// Given three colinear points p, q, r,  
		// the function checks if point q lies 
		// on line segment 'pr' 
		private static bool OnSegment(Point p, Point q, Point r) =>
			q.X <= Math.Max(p.X, r.X) &&
			q.X >= Math.Min(p.X, r.X) &&
			q.Y <= Math.Max(p.Y, r.Y) &&
			q.Y >= Math.Min(p.Y, r.Y);

		// To find orientation of ordered triplet (p, q, r). 
		// The function returns following values 
		// 0 --> p, q and r are colinear 
		// 1 --> Clockwise 
		// 2 --> Counterclockwise 
		private static int Orientation(Point p, Point q, Point r)
		{
			int val = (q.Y - p.Y) * (r.X - q.X) -
					  (q.X - p.X) * (r.Y - q.Y);

			if (val == 0)
			{
				return 0; // colinear 
			}

			return val > 0 ? 1 : 2; // clock or counterclock wise 
		}

		// The function that returns true if  
		// line segment 'p1q1' and 'p2q2' intersect. 
		private static bool DoIntersect(
			Point p1,
			Point q1,
			Point p2,
			Point q2)
		{
			// Find the four orientations needed for  
			// general and special cases 
			int o1 = Orientation(p1, q1, p2);
			int o2 = Orientation(p1, q1, q2);
			int o3 = Orientation(p2, q2, p1);
			int o4 = Orientation(p2, q2, q1);

			// General case 
			if (o1 != o2 && o3 != o4)
			{
				return true;
			}

			// Special Cases 
			// p1, q1 and p2 are colinear and 
			// p2 lies on segment p1q1 
			if (o1 == 0 && OnSegment(p1, p2, q1))
			{
				return true;
			}

			// p1, q1 and p2 are colinear and 
			// q2 lies on segment p1q1 
			if (o2 == 0 && OnSegment(p1, q2, q1))
			{
				return true;
			}

			// p2, q2 and p1 are colinear and 
			// p1 lies on segment p2q2 
			if (o3 == 0 && OnSegment(p2, p1, q2))
			{
				return true;
			}

			// p2, q2 and q1 are colinear and 
			// q1 lies on segment p2q2 
			if (o4 == 0 && OnSegment(p2, q1, q2))
			{
				return true;
			}

			// Doesn't fall in any of the above cases 
			return false;
		}
	}
}