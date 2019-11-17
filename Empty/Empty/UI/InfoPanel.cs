using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFlash.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empty.UI
{
    class InfoPanel : Sprite
    {
        private int indent = 30;
        public static int widthPanel = 280;
        private List<Row> rows;
        private Quad background;

        public InfoPanel(GameObjects.Humans.BaseHuman human)
        {
            rows = new List<Row>();
            human.GunSkill = (float)0.2;
            human.RepairSkill = 1;
            rows.Add(new Row($"NAME: {human.Name}"));
            rows.Add(new Row($"GUN SKILL: ", human.GunSkill));
            rows.Add(new Row($"REPAIR SKILL: ", human.RepairSkill));
            this.width = widthPanel;
            this.height = 120;
            AddBackground();
            RedrawRows();
        }

        public void RedrawRows()
        {
            double setY = background.y + 15;
            foreach (Row row in rows)
            {
                row.x = background.x + 10;
                row.y = setY;
                setY += row.text.height + 10;
                AddChild(row);
            }
        }

        private void AddBackground()
        {
            background = new Quad((float)this.width, (float)this.height, 0x2c3e50);
            background.x = indent;
            background.y = Values.SCREEN_HEIGHT - this.height - indent;
            AddChild(background);
        }

        public override void Draw(SpriteBatch sb, GameTime gameTime = null)
        {
            
            base.Draw(sb, gameTime);
        }
    }

    class Row : Sprite
    {
        public Text text = null;

        public Row(string text)
        {
            DrawText(text);
        }

        public Row(string text, double value)
        {
            DrawText(text);
            DrawBar(value);
        }

        private void DrawBar(double value)
        {
            Quad back = new Quad(InfoPanel.widthPanel - (float)text.width - 40, (float)text.height, 0xe74c3c);
            back.x = text.x + text.width + 10;
            back.y = text.y;
            Quad fill = new Quad((float)back.width * (float)value - 2, (float)back.height - 2, 0xf39c12);
            fill.x = back.x + 1;
            fill.y = back.y + 1;
            AddChild(back);
            AddChild(fill);
        }

        private void DrawText(string textToWrite)
        {
            text = new Text(Game1.fontForProperties, textToWrite, Colors.hexToRGB(0xecf0f1));
            AddChild(text);
        }

        public override void Draw(SpriteBatch sb, GameTime gameTime = null)
        {
            base.Draw(sb, gameTime);
        }
    }
}
