using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoFlash.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Empty.Building
{

    public abstract class Structure
    {
        public Texture2D texture;
        public Rectangle Rect;

        public Color StateColor;

        public Vector2 position;

        public int StoneCost = 0;
        public int TimberCost = 0;
        public int IronCost = 0;

        public int WorkCost = 0;

        protected Structure(Texture2D texture, int stoneCost, int timberCost, int ironCost, int workCost)
        {
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
            var pos = Vector2.One;
            if (StateColor == Color.White)
                pos = position * Values.MAP_SCALE;
            else
                pos = position;

            var size = (Vector2.One * Values.MAP_SCALE);
            sb.Draw(texture, position: pos, scale: size, color: StateColor);
        }

    }
}
