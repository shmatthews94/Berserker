using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace Berserker
{
	public class Controls
	{
		public KeyboardState CurrentKeyboardState;
		public KeyboardState PreviousKeyboardState;
		public GamePadState CurrentGamePadState;
		public GamePadState PreviousGamePadState;

		public Controls()
		{
			this.CurrentKeyboardState = Keyboard.GetState();
			this.PreviousKeyboardState = Keyboard.GetState();
//			this.gp = GamePad.GetState(PlayerIndex.One);
//			this.gpo = GamePad.GetState(PlayerIndex.One);
			//Console.WriteLine (Sdl.SDL_JoystickName (0));

		}

		public void Update()
		{
			PreviousKeyboardState = CurrentKeyboardState;
			//gpo = gp;
			CurrentKeyboardState = Keyboard.GetState();
			//this.gp = GamePad.GetState(PlayerIndex.One);
		}

		public bool isPressed(Keys key, Buttons button)
		{
			//Console.WriteLine (button);
			return CurrentKeyboardState.IsKeyDown(key);// || gp.IsButtonDown(button);
		}

		public bool onPress(Keys key, Buttons button)
		{
//			if ((gp.IsButtonDown (button) && gpo.IsButtonUp (button))) {
//				Console.WriteLine (button);
//			}
			return (CurrentKeyboardState.IsKeyDown(key) && PreviousKeyboardState.IsKeyUp(key));// ||
				//(gp.IsButtonDown(button) && gpo.IsButtonUp(button));
		}

		public bool onRelease(Keys key, Buttons button)
		{
			//Console.WriteLine (button);
			return (CurrentKeyboardState.IsKeyUp(key) && PreviousKeyboardState.IsKeyDown(key));// ||
				//(gp.IsButtonUp(button) && gpo.IsButtonDown(button));
		}

		public bool isHeld(Keys key, Buttons button)
		{
			//Console.WriteLine (button);
			return (CurrentKeyboardState.IsKeyDown(key) && PreviousKeyboardState.IsKeyDown(key));// ||
				//(gp.IsButtonDown(button) && gpo.IsButtonDown(button));
		}
	
	}
}

