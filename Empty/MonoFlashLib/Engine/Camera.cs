using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoFlash.Engine
{
	public class Camera
	{
		protected float   _rotation;
		protected float   _zoom;
		public    Vector2 _pos;
		public    Matrix  _transform;

		public float Zoom
		{
			//Устанавливаем увеличение
			get { return _zoom; }
			set
			{
				_zoom = value;

				if (_zoom < 0.1f)
				{
					_zoom = 0.1f;
				}
			}
		}

		public float Rotation
		{
			//Поворот
			get { return _rotation; }
			set { _rotation = value; }
		}

		// Устанавливаем позицию
		public Vector2 Pos
		{
			get { return _pos; }
			set { _pos = value; }
		}

		public Camera()
		{
			//Устанавливаем увеличение, поворот и позицию камеры
			_zoom     = 1.0f;
			_rotation = 0.0f;
			_pos      = Vector2.Zero;
		}

		// Перемещаем камеру (хз почему я не использовал это для слежения за игроком, а ставил координаты напрямую. Такой вот я человек)
		public void Move(Vector2 amount)
		{
			_pos += amount;
		}


		//Матрица трансформацийй
		public Matrix get_transformation(GraphicsDevice graphicsDevice)
		{
			var ViewportWidth  = 1280f;
			var ViewportHeight = 720f;

			//Создаём трансляцию
			_transform =
				Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0)) *
				Matrix.CreateRotationZ(Rotation) *
				Matrix.CreateScale(new Vector3(Zoom,                       Zoom,                  1)) *
				Matrix.CreateTranslation(new Vector3(ViewportWidth * 0.5f, ViewportHeight * 0.5f, 0));

			return _transform;
		}
	}
}