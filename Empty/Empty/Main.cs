using Empty.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFlash.Engine;

namespace Empty
{
	internal class Main : Sprite
	{
		private Island island;
        private UI.Building.Interface buildingInterface;

		/// <inheritdoc />
		public Main()
		{
			island = new Island();
			AddChild(island);
			AddChild(new UI.Property());
            buildingInterface = new UI.Building.Interface(TestAction);
            AddChild(buildingInterface);

		}
        private void TestAction(UI.Building.Interface.BuildType bt)
        {
            Resources.Stone -= 1;
            Resources.Timber -= 1;
            Resources.Iron -= 1;
            UI.Property.mainProperty.UpdateMainProperties();
            UI.Building.Interface.UpdateInterface();
        }

        /// <inheritdoc />
        public override void Draw(SpriteBatch sb, GameTime gameTime = null)
		{
			sb.Begin();
			base.Draw(sb, gameTime);
			sb.End();
		}
	}
}