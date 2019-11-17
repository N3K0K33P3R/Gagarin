using Microsoft.Xna.Framework.Graphics;
using MonoFlash.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empty.UI
{
    class Property : Sprite
    {
        public Text text;
        public List<Property> props;

        public Property()
        {
            props = new List<Property>();
            SetProperty(Game1.stoneTexture, Resources.Stone.ToString());
            SetProperty(Game1.woodTexture, Resources.Timber.ToString());
            SetProperty(Game1.ironTexture, Resources.Iron.ToString());
        }

        public void SetProperty(Texture2D texture, string textForSet)
        {
            Image image = new Image(texture);
            text = new Text(Game1.fontForProperties, textForSet, Microsoft.Xna.Framework.Color.Black);
            text.x += text.height + 25;
            int localY = 15 + ((int)text.height + 5) * props.Count;
            text.y = localY;
            image.ScaleX = text.height / image.height;
            image.ScaleY = text.height / image.width;
            image.x += text.height / 2 + 15;
            image.y += text.height / 2 + localY;
            AddChild(image);
            AddChild(text);
            props.Add(this);
        }
    }
}
