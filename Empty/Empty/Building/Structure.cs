using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoFlash.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Empty.GameObjects;

namespace Empty.Building
{

    public abstract class Structure
    {
        public Texture2D texture;
        public Rectangle Rect;

        public Vector2 Offset = new Vector2(0, -8);

        public Color StateColor;

        public Vector2 position;

        public int StoneCost = 0;
        public int TimberCost = 0;
        public int IronCost = 0;

        public int WorkCost = 0;

        protected Structure(Texture2D texture, int stoneCost, int timberCost, int ironCost, int workCost, Vector2 offset = default)
        {
            Offset = offset;
            this.texture = texture;
            StoneCost = stoneCost;
            TimberCost = timberCost;
            IronCost = ironCost;
            WorkCost = workCost;

            StateColor = Color.White;
            Rect = new Rectangle(0, 0, texture.Width, texture.Height);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, position + Offset, color: StateColor);
        }

        public abstract void OnAddOnGrid(ref TileType[,] types,int i, int j);

        public abstract bool IsCanPut(Island island );


    }
}
