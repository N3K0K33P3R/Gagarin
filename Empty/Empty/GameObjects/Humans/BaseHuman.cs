using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFlash.Engine;
using MonoFlashLib.Engine;
using System.Collections.Generic;

namespace Empty.GameObjects.Humans
{
	public class BaseHuman : Sprite
	{
		private readonly AnimatedSprite animated;
		public           Point          tilePos;
		private          TextureAtlas   atlas;
		private          Rectangle[]    walkAnim;
		private          Rectangle[]    deathAnim;
		private          bool           isMoving;
		private          Stack<Point>   path;
		private          Point          lastPathPoint;

		public float  GunSkill    { get; set; }
		public float  RepairSkill { get; set; }
		public string Name        { get; set; }


		/// <inheritdoc />
		internal BaseHuman(Texture2D atlasTexture, int x, int y)
		{
			tilePos = new Point(x, y);
			this.x  = x * Values.TILE_SIZE;
			this.y  = y * Values.TILE_SIZE;
			var ta = new TextureAtlas(atlasTexture, 0, 16, 16, 16);
			animated = new AnimatedSprite(atlasTexture, 1, 0.1f);
			animated.AddFrames(ta.GetFrames(0, 5));
			AddChild(animated);
			animated.defaultSprite = ta.GetFrames(0, 1)[0];
			animated.y             = animated.x = 8;
			animated.isStarted     = true;

			SetTransition(Transitions.position);
			Name = Values.GetRandomName();
		}

		/// <inheritdoc />
		public override void Update(float delta)
		{
			base.Update(delta);
		}

		public void SetTilePos(int x, int y)
		{
			List<Point> p = FindPath(x, y);

			if (p == null)
			{
				return;
			}

			path = new Stack<Point>(FindPath(x, y));

			if (path.Count == 0)
			{
				return;
			}

			(int x1, int y1) = path.Pop();

			SetPosition(
				x1 * Values.TILE_SIZE,
				y1 * Values.TILE_SIZE,
				Maths.Lerp,
				0.1f,
				() =>
				{
					tilePos = new Point(x1, y1);
					OnMoveEnd();
				});

			isMoving           = true;
			animated.isStarted = true;
		}

		private void OnMoveEnd()
		{
			if (path.Count == 0)
			{
				isMoving           = false;
				animated.isStarted = false;
			}
			else
			{
				tilePos = path.Pop();

				SetPosition(
					tilePos.X * Values.TILE_SIZE,
					tilePos.Y * Values.TILE_SIZE,
					Maths.Lerp,
					0.1f,
					OnMoveEnd);
			}
		}




		private List<Point> FindPath(int x, int y)
		{
			TileType[,] mapEnum = Main.instance.GetMap();
			var         map     = new byte[mapEnum.GetLength(0), mapEnum.GetLength(1)];

			for (var i = 0; i < map.GetLength(0); i++)
			{
				for (var j = 0; j < map.GetLength(1); j++)
				{
					map[i, j] = (byte)mapEnum[i, j];
				}
			}

			var pathFinder = new PathFinder(map, 1, 2, 3);

			return pathFinder.FindPath(tilePos.X, tilePos.Y, x, y);
		}
	}
}