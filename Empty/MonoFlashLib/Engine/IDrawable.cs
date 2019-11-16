using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MonoFlash.Engine
{
	public interface IDrawable
	{
		double x { get; set; }
		double y { get; set; }

		double globalX { get; }
		double globalY { get; }

		double width  { get; }
		double height { get; }

		double rotation { get; set; }

		double Alpha { get; set; }

		double Scale  { get; set; }
		double ScaleX { get; set; }
		double ScaleY { get; set; }

		List<IDrawable> childs { get; }

		IDrawable parent { get; set; }


		void Update(float delta);

		void Draw(SpriteBatch sb, GameTime gameTime);

		void AddChild(IDrawable child);

		void RemoveChild(IDrawable child);

		void RemoveChildAt(int id);

		void RemoveChildren();

		void AddChildAt(IDrawable child, int id);
	}
}