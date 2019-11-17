using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoFlash.Engine
{
	/// <summary>
	/// an enum of all available mouse buttons.
	/// </summary>
	public enum MouseButtons
	{
		LeftButton,
		MiddleButton,
		RightButton,
		ExtraButton1,
		ExtraButton2
	}


	public class InputHelper
	{
		private bool   _cursorIsVisible;
		private Sprite _cursorSprite;

		private Viewport _viewport;

		public Vector2 Cursor;

		public GamePadState GamePadState { get; private set; }

		public KeyboardState KeyboardState { get; private set; }

		public MouseState MouseState { get; private set; }

		public GamePadState PreviousGamePadState { get; private set; }

		public KeyboardState PreviousKeyboardState { get; private set; }

		public MouseState PreviousMouseState { get; private set; }

		public bool ShowCursor
		{
			get { return _cursorIsVisible && IsCursorValid; }
			set { _cursorIsVisible = value; }
		}

		public bool IsCursorValid { get; private set; }

		/// <summary>
		/// Constructs a new input state.
		/// </summary>
		public InputHelper()
		{
			KeyboardState = new KeyboardState();
			GamePadState  = new GamePadState();
			MouseState    = new MouseState();

			PreviousKeyboardState = new KeyboardState();
			PreviousGamePadState  = new GamePadState();
			PreviousMouseState    = new MouseState();

			_cursorIsVisible = false;
			IsCursorValid    = true;

			Cursor = Vector2.Zero;
		}

		public void LoadContent(Viewport viewport)
		{
			_viewport = viewport;
		}

		/// <summary>
		/// Reads the latest state of the keyboard and gamepad and mouse/touchpad.
		/// </summary>
		public void Update(GameTime gameTime)
		{
			PreviousKeyboardState = KeyboardState;
			PreviousGamePadState  = GamePadState;
			PreviousMouseState    = MouseState;

			KeyboardState = Keyboard.GetState();
			GamePadState  = GamePad.GetState(PlayerIndex.One);
			MouseState    = Mouse.GetState();

			// Update cursor
			Vector2 oldCursor = Cursor;

			if (GamePadState.IsConnected && GamePadState.ThumbSticks.Left != Vector2.Zero)
			{
				Cursor += GamePadState.ThumbSticks.Left *
						  new Vector2(300f, -300f) *
						  (float)gameTime.ElapsedGameTime.TotalSeconds;

				Cursor = Vector2.Clamp(Cursor, Vector2.Zero, new Vector2(_viewport.Width, _viewport.Height));
				Mouse.SetPosition((int)Cursor.X, (int)Cursor.Y);
			}
			else
			{
				Cursor.X = MouseState.X;
				Cursor.Y = MouseState.Y;
				Cursor   = Vector2.Clamp(Cursor, Vector2.Zero, new Vector2(_viewport.Width, _viewport.Height));
			}

			if (_viewport.Bounds.Contains(MouseState.X, MouseState.Y))
			{
				IsCursorValid = true;
			}
			else
			{
				IsCursorValid = false;
			}
		}


		/// <summary>
		/// Helper for checking if a key was newly pressed during this update.
		/// </summary>
		public bool IsNewKeyPress(Keys key) => KeyboardState.IsKeyDown(key) && PreviousKeyboardState.IsKeyUp(key);

		public bool IsNewKeyRelease(Keys key) => PreviousKeyboardState.IsKeyDown(key) && KeyboardState.IsKeyUp(key);

		/// <summary>
		/// Helper for checking if a button was newly pressed during this update.
		/// </summary>
		public bool IsNewButtonPress(Buttons button) => GamePadState.IsButtonDown(button) && PreviousGamePadState.IsButtonUp(button);

		public bool IsNewButtonRelease(Buttons button) => PreviousGamePadState.IsButtonDown(button) && GamePadState.IsButtonUp(button);

		/// <summary>
		/// Helper for checking if a mouse button was newly pressed during this update.
		/// </summary>
		public bool IsNewMouseButtonPress(MouseButtons button)
		{
			switch (button)
			{
				case MouseButtons.LeftButton:
					return MouseState.LeftButton == ButtonState.Pressed &&
						   PreviousMouseState.LeftButton == ButtonState.Released;
				case MouseButtons.RightButton:
					return MouseState.RightButton == ButtonState.Pressed &&
						   PreviousMouseState.RightButton == ButtonState.Released;
				case MouseButtons.MiddleButton:
					return MouseState.MiddleButton == ButtonState.Pressed &&
						   PreviousMouseState.MiddleButton == ButtonState.Released;
				case MouseButtons.ExtraButton1:
					return MouseState.XButton1 == ButtonState.Pressed &&
						   PreviousMouseState.XButton1 == ButtonState.Released;
				case MouseButtons.ExtraButton2:
					return MouseState.XButton2 == ButtonState.Pressed &&
						   PreviousMouseState.XButton2 == ButtonState.Released;
				default: return false;
			}
		}

		/// <summary>
		/// Checks if the requested mouse button is released.
		/// </summary>
		/// <param name="button">The button.</param>
		public bool IsNewMouseButtonRelease(MouseButtons button)
		{
			switch (button)
			{
				case MouseButtons.LeftButton:
					return PreviousMouseState.LeftButton == ButtonState.Pressed &&
						   MouseState.LeftButton == ButtonState.Released;
				case MouseButtons.RightButton:
					return PreviousMouseState.RightButton == ButtonState.Pressed &&
						   MouseState.RightButton == ButtonState.Released;
				case MouseButtons.MiddleButton:
					return PreviousMouseState.MiddleButton == ButtonState.Pressed &&
						   MouseState.MiddleButton == ButtonState.Released;
				case MouseButtons.ExtraButton1:
					return PreviousMouseState.XButton1 == ButtonState.Pressed &&
						   MouseState.XButton1 == ButtonState.Released;
				case MouseButtons.ExtraButton2:
					return PreviousMouseState.XButton2 == ButtonState.Pressed &&
						   MouseState.XButton2 == ButtonState.Released;
				default: return false;
			}
		}

		/// <summary>
		/// Checks if the mouse wheel has been scrolled up
		/// </summary>
		public bool IsNewScrollWheelUp() => MouseState.ScrollWheelValue - PreviousMouseState.ScrollWheelValue > 0;

		/// <summary>
		/// Checks if the mouse wheel has been scrolled down
		/// </summary>
		public bool IsNewScrollWheelDown() => PreviousMouseState.ScrollWheelValue - MouseState.ScrollWheelValue > 0;

		/// <summary>
		/// Checks for a "menu select" input action.
		/// </summary>
		public bool IsMenuSelect() =>
			IsNewKeyPress(Keys.Space) ||
			IsNewKeyPress(Keys.Enter) ||
			IsNewButtonPress(Buttons.A) ||
			IsNewMouseButtonPress(MouseButtons.LeftButton);

		public bool IsMenuHold() =>
			IsNewButtonPress(Buttons.A) ||
			IsNewMouseButtonPress(MouseButtons.LeftButton);

		public bool IsMenuRelease() =>
			GamePadState.IsButtonUp(Buttons.A) &&
			MouseState.LeftButton == ButtonState.Released;

		/// <summary>
		/// Checks for a "menu cancel" input action.
		/// </summary>
		public bool IsMenuCancel() =>
			IsNewKeyPress(Keys.Escape) ||
			IsNewKeyPress(Keys.Back) ||
			IsNewButtonPress(Buttons.B) ||
			IsNewButtonPress(Buttons.Back);

		public bool IsMenuUp() =>
			IsNewKeyPress(Keys.Up) ||
			IsNewKeyPress(Keys.PageUp) ||
			IsNewButtonPress(Buttons.DPadUp) ||
			IsNewButtonPress(Buttons.RightThumbstickUp) ||
			IsNewScrollWheelUp();

		public bool IsMenuDown() =>
			IsNewKeyPress(Keys.Down) ||
			IsNewKeyPress(Keys.PageDown) ||
			IsNewButtonPress(Buttons.DPadDown) ||
			IsNewButtonPress(Buttons.RightThumbstickDown) ||
			IsNewScrollWheelDown();

		public bool IsScreenExit() =>
			IsNewKeyPress(Keys.Escape) ||
			IsNewKeyPress(Keys.Back) ||
			IsNewButtonPress(Buttons.Back);
	}
}