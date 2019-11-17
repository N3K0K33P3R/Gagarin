using Empty;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


public class CameraNew
{
	private float currentMouseWheelValue,
				  previousMouseWheelValue,
				  previousZoom,
				  prevMouseX,
				  prevMouseY;

	private Rectangle restrictions;
	private Rectangle Bounds      { get; set; }
	private Rectangle VisibleArea { get; set; }
	private float     Senetive    { get; set; }

	public float   Zoom      { get; set; }
	public Vector2 Position  { get; set; }
	public Matrix  Transform { get; protected set; }

	public CameraNew(Viewport viewport)
	{
		Bounds   = viewport.Bounds;
		Zoom     = 1f;
		Position = Vector2.Zero;

		restrictions = new Rectangle(0, 0, 550, 270);
	}

	public void MoveCamera(Vector2 movePosition)
	{
		Vector2 newPosition = Position + movePosition;


		if (!restrictions.Contains(newPosition))
		{
			(float lx, float ly) = newPosition;

			if (lx < restrictions.X)
			{
				lx = restrictions.X;
			}
			else if (ly < restrictions.Y)
			{
				ly = restrictions.X;
			}
			else if (lx > restrictions.Right)
			{
				lx = restrictions.Right;
			}
			else if (ly > restrictions.Bottom)
			{
				ly = restrictions.Bottom;
			}

			newPosition = new Vector2(lx, ly);
		}

		Position = newPosition;
	}


	public void UpdateCamera(Viewport bounds)
	{
		Bounds = bounds.Bounds;
		UpdateMatrix();

		Vector2 cameraMovement = Vector2.Zero;
		float   moveSpeed      = (Senetive + 1) / Zoom;


		if (Mouse.GetState().RightButton == ButtonState.Pressed)
		{
			cameraMovement.X = moveSpeed * (prevMouseX - Mouse.GetState().X);
			cameraMovement.Y = moveSpeed * (prevMouseY - Mouse.GetState().Y);
		}

		prevMouseX = Mouse.GetState().X;
		prevMouseY = Mouse.GetState().Y;


		if (Keyboard.GetState().IsKeyDown(Keys.Down))
		{
			Position = Vector2.Zero;
		}


		previousMouseWheelValue = currentMouseWheelValue;
		currentMouseWheelValue  = Mouse.GetState().ScrollWheelValue;

		if (currentMouseWheelValue > previousMouseWheelValue)
		{
			Zoom += 0.05f;
			//Values.MAP_SCALE += .01f;
			//Position += Bounds.Size.ToVector2() *.001f;
		}

		if (currentMouseWheelValue < previousMouseWheelValue && Zoom > 1)
		{
			Zoom -= 0.05f;
			//Position -= Bounds.Size.ToVector2() *.001f;
		}

		previousZoom     = Zoom;
		Values.MAP_SCALE = Zoom;

		MoveCamera(cameraMovement);
	}


	private void UpdateVisibleArea()
	{
		Matrix inverseViewMatrix = Matrix.Invert(Transform);

		Vector2 tl = Vector2.Transform(Vector2.Zero,                             inverseViewMatrix);
		Vector2 tr = Vector2.Transform(new Vector2(Bounds.X,     0),             inverseViewMatrix);
		Vector2 bl = Vector2.Transform(new Vector2(0,            Bounds.Y),      inverseViewMatrix);
		Vector2 br = Vector2.Transform(new Vector2(Bounds.Width, Bounds.Height), inverseViewMatrix);

		var min = new Vector2(
			MathHelper.Min(tl.X, MathHelper.Min(tr.X, MathHelper.Min(bl.X, br.X))),
			MathHelper.Min(tl.Y, MathHelper.Min(tr.Y, MathHelper.Min(bl.Y, br.Y))));

		var max = new Vector2(
			MathHelper.Max(tl.X, MathHelper.Max(tr.X, MathHelper.Max(bl.X, br.X))),
			MathHelper.Max(tl.Y, MathHelper.Max(tr.Y, MathHelper.Max(bl.Y, br.Y))));

		VisibleArea = new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));
	}

	private void UpdateMatrix()
	{
		Transform = Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
					Matrix.CreateScale(Zoom) *
					Matrix.CreateTranslation(new Vector3(Bounds.Width * 0.5f, Bounds.Height * 0.5f, 0));

		UpdateVisibleArea();
	}
}