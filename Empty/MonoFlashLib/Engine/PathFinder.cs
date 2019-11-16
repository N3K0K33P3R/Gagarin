using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoFlashLib.Engine
{
    public class PathFinder
    {
        private List<PathTile> checkedTiles;
        private readonly byte[,] map;
        private List<PathTile> tilesToCheck;

        private int x1, y1, x2, y2;

        private byte[] allowedToWalk;

        public PathFinder(byte[,] map, params byte[] allowedToWalk)
        {
            this.map = map;
            this.allowedToWalk = allowedToWalk;
        }

        private PathTile Contains(PathTile tile, List<PathTile> list)
        {
            foreach (var item in list)
                if (item.x == tile.x && item.y == tile.y)
                    return item;
            return null;
        }

        private void CheckTilesAround(PathTile tile)
        {
            for (var i = -1; i <= 1; i++)
            for (var j = -1; j <= 1; j++)
                if (!(i != 0 && j != 0))
                {
                    var temp = new PathTile(tile.x + i, tile.y + j, tile.costDir, tile);
                    temp.AddEur(x2, y2);
                    if (i != 0 && j != 0) continue;
                    if (Contains(temp, tilesToCheck) != null)
                    {
                    }
                    else if (Contains(temp, checkedTiles) != null)
                    {
                        var cont = Contains(temp, checkedTiles);
                        if (temp.FullCost < cont.FullCost) cont = temp;
                    }
                    else if (temp.x >= 0 && temp.x < map.GetLength(0) && temp.y >= 0 && temp.y < map.GetLength(1))
                    {
                        if (allowedToWalk.Contains(map[temp.x, temp.y]))
                            temp.costDir += 1;
                        else
                            continue;
                        tilesToCheck.Add(temp);
                    }
                }

            tilesToCheck.Remove(tile);
            checkedTiles.Add(tile);
        }

        public Task<List<Point>> FindPathAsync(int x1, int y1, int x2, int y2)
        {
            this.x1 = x1;
            this.x2 = x2;
            this.y1 = y1;
            this.y2 = y2;
            var task = new Task<List<Point>>(CallFindWithLocalParams);
            task.Start();
            return task;
        }

        private List<Point> CallFindWithLocalParams()
        {
            return FindPath(x1, y1, x2, y2);
        }

        public List<Point> FindPath(int x1, int y1, int x2, int y2)
        {
            tilesToCheck = new List<PathTile>();
            checkedTiles = new List<PathTile>();

            var first = new PathTile(x1, y1, 0, null);
            tilesToCheck.Add(first);

            while (tilesToCheck.Count > 0)
            {
                var min = tilesToCheck[0];
                foreach (var item in tilesToCheck)
                    if (item.FullCost < min.FullCost)
                        min = item;


                if (min.x == x2 && min.y == y2)
                {
                    var result = new List<Point>();
                    var temp = min;
                    while (temp != null)
                    {
                        var point = new Point(temp.x, temp.y);
                        result.Add(point);
                        temp = temp.prev;
                    }

                    return result;
                }

                CheckTilesAround(min);
            }

            return null;
        }

        public class PathTile
        {
            public float costDir;
            public float costEur;
            public PathTile prev;
            public int x, y;


            public PathTile(int x, int y, float cost, PathTile prev)
            {
                this.x = x;
                this.y = y;
                costDir = cost;
                this.prev = prev;
            }

            public float FullCost => costDir + costEur;

            public void AddEur(int x, int y)
            {
                costEur = (float) Math.Sqrt((this.x - x) * (this.x - x) + (this.y - y) * (this.y - y));
            }
        }
    }
}