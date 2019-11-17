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
        private bool main;
        public static Property mainProperty;

        public Property(bool main = true)
        {
            props = new List<Property>();
            this.main = main;
            if (main) mainProperty = this;
            UpdateMainProperties();
        }

        public void SetProperty(Texture2D texture, string textForSet, Microsoft.Xna.Framework.Color textColor)
        {
            Image image = new Image(texture);
            text = new Text(Game1.fontForProperties, textForSet, textColor);
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

        public void UpdateMainProperties()
        {
            if (main)
            {
                this.RemoveChildren();
                props.Clear();
                SetProperty(Game1.stoneTexture, Resources.Stone.ToString(), Microsoft.Xna.Framework.Color.Black);
                SetProperty(Game1.woodTexture, Resources.Timber.ToString(), Microsoft.Xna.Framework.Color.Black);
                SetProperty(Game1.ironTexture, Resources.Iron.ToString(), Microsoft.Xna.Framework.Color.Black);
            }
        }

        public void UpdateProperties()
        {
            
        }
    }
}
