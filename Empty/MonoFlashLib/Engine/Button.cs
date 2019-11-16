using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoFlashLib;
using System;

namespace MonoFlash.Engine
{
	public class Button : Sprite
	{
		private readonly uint                color1;
		private readonly uint                color2;
		private readonly AnimationController colorAC;
		private readonly uint                colorHover;
		private readonly uint                textBase;
		private readonly uint                textHover;
		private readonly Text                textV;
		private          uint                colorInnerCurrent;
		private          Action<Button>      onClick;
		private          ButtonState         prevState;
		private          Rectangle           rect;
		private          bool                shouldAction = false;

		private bool startedHover,
					 wasAbandonedByMouse = true;

		private string text;
		private uint   textCurrent;
		public  int    id;

		public Button(
			string text,
			int width,
			int height,
			SpriteFont font,
			uint colorBorder,
			uint colorInner,
			uint colorHover,
			uint textBase,
			uint textHover)
		{
			this.text = text;
			rect      = new Rectangle((int)x, (int)y, width, height);
			textV     = new Text(font, text, Color.Black);
			AddChild(textV);
			textV.x         = width / 2 - textV.width / 2;
			textV.y         = height / 2 - textV.height / 2;
			this.width      = width;
			this.height     = height;
			color1          = colorBorder;
			color2          = colorInner;
			this.colorHover = colorHover;
			this.textBase   = textBase;
			this.textHover  = textHover;
			colorAC         = new AnimationController(0);
			prevState       = ButtonState.Pressed;
		}

		public override void Update(float delta)
		{
			rect.X = (int)globalX;
			rect.Y = (int)globalY;
			double i = colorAC.MakeStep(delta);
			colorInnerCurrent = Colors.colorLerp(color2,   colorHover, (float)i);
			textCurrent       = Colors.colorLerp(textBase, textHover,  (float)i);
			textV.color       = Colors.hexToRGB(textCurrent);
			var touchRect = new Rectangle(Mouse.GetState().Position, new Point(1, 1));

			if (PARAMS.IsActive &&
				Mouse.GetState().LeftButton == ButtonState.Pressed &&
				prevState != ButtonState.Pressed)
			{
				//Проверка координат, было ли нажатие совершено по самой кнопке

				if (rect.Intersects(touchRect) && onClick != null)
				{
					onClick(this);
				}
			}
			else if (rect.Intersects(touchRect) && !startedHover && wasAbandonedByMouse)
			{
				wasAbandonedByMouse = false;
				OnHover();
			}
			else if (!rect.Intersects(touchRect) && !wasAbandonedByMouse)
			{
				wasAbandonedByMouse = true;
				UnHover();
			}

			prevState = Mouse.GetState().LeftButton;
			base.Update(delta);
		}


		public override void Draw(SpriteBatch sb, GameTime gameTime)
		{
			Color     color1   = Colors.hexToRGB(colorInnerCurrent);
			Color     color2   = Colors.hexToRGB(this.color1);
			Rectangle drawRect = rect;
			drawRect.X = (int)globalX;
			drawRect.Y = (int)globalY;

			sb.FillRectangle(drawRect, color1, 1);
			sb.DrawRectangle(drawRect, color2, 1);
			base.Draw(sb);
		}


		public void SetText(string text)
		{
			this.text = text;
			textV.SetText(text);
			textV.x = width / 2 - textV.width / 2;
			textV.y = height / 2 - textV.height / 2;
		}

		public void AddEventListener(Action<Button> action)
		{
			onClick += action;
		}

		private void OnHover()
		{
			startedHover = true;
			colorAC.StartAnimation(Maths.easeInOutQuad, colorAC.i, 1, finished: FinishedOnHover);
		}

		private void FinishedOnHover()
		{
			startedHover = false;
		}

		private void UnHover()
		{
			startedHover = false;
			colorAC.StartAnimation(Maths.easeInOutQuad, colorAC.i, 0);
		}
	}
}