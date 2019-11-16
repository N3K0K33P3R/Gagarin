using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoFlashLib;
using MonoGame_Textbox;

namespace MonoFlash.Engine
{
    public enum TextAlign
    {
        Left,
        Center,
        Right
    }

    public class TextBoxHandler : Sprite
    {
        private readonly TextAlign align;
        private Rectangle drawRect;
        private readonly SpriteFont font;
        private ButtonState prevState;
        private readonly TextBox textBox;

        public TextBoxHandler(SpriteFont font, int maxChar, string defaultText, GraphicsDevice gd, Color textColor,
            Color selectionColor, Color cursorColor, TextAlign align = TextAlign.Left)
        {
            drawRect = new Rectangle(0, 0, 500, 500);
            textBox = new TextBox(drawRect, maxChar, defaultText, gd, font, textColor, cursorColor, selectionColor, 30);
            this.font = font;
            this.align = align;
        }

        public string GetText()
        {
            return textBox.Text.String;
        }

        public void SetText(string text)
        {
            textBox.Text.String = text;
        }

        public void SetActive(bool actived)
        {
            textBox.Active = actived;
        }

        public override void Update(float delta)
        {
            textBox.Update();
            if (PARAMS.IsActive && Mouse.GetState().LeftButton == ButtonState.Pressed &&
                prevState != ButtonState.Pressed)
            {
                var touchRect = new Rectangle(Mouse.GetState().Position, new Point(1, 1));
                if (drawRect.Intersects(touchRect))
                    textBox.Active = true;
                else
                    textBox.Active = false;
            }

            prevState = Mouse.GetState().LeftButton;

            //drawRect.Width = (int)Assets.defaultFont.MeasureString(textBox.Text.String).X * 10;
            //drawRect.Height = (int)Assets.defaultFont.MeasureString(textBox.Text.String).Y* 10;
            switch (align)
            {
                case TextAlign.Left:
                    drawRect.X = (int) globalX;
                    drawRect.Y = (int) globalY;
                    break;
                case TextAlign.Center:
                    drawRect.X = (int) (globalX - font.MeasureString(textBox.Text.String).X / 2);
                    drawRect.Y = (int) globalY;
                    break;
                case TextAlign.Right:
                    drawRect.X = (int) (globalX - font.MeasureString(textBox.Text.String).X);
                    drawRect.Y = (int) globalY;
                    break;
            }

            base.Update(delta);
        }

        public void SetTextColor(uint color, uint cursorColor, uint selectionColor)
        {
            textBox.Renderer.Color = Colors.hexToRGB(color);
            textBox.Cursor.Color = Colors.hexToRGB(cursorColor);
            textBox.Cursor.Selection = Colors.hexToRGB(selectionColor);
        }

        public override void Draw(SpriteBatch sb, GameTime gameTime)
        {
            textBox.Area = drawRect;
            textBox.Draw(sb);
            base.Draw(sb);
        }
    }
}