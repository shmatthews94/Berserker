using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Berserker
{
    public class Input
    {
        public KeyboardState CurrentKeyboardState;
        public KeyboardState PreviousKeyboardState;

        public Input()
        {
            CurrentKeyboardState = Keyboard.GetState();
            PreviousKeyboardState = Keyboard.GetState();
        }

        public void Update()
        {
            PreviousKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Keyboard.GetState();
        }

        public bool isPressed(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key);
        }

        public bool onPress(Keys key, Buttons button)
        {
            return (CurrentKeyboardState.IsKeyDown(key) && PreviousKeyboardState.IsKeyUp(key));
        }

        public bool onRelease(Keys key, Buttons button)
        {
            return (CurrentKeyboardState.IsKeyUp(key) && PreviousKeyboardState.IsKeyDown(key));
        }

        public bool isHeld(Keys key, Buttons button)
        {
            return (CurrentKeyboardState.IsKeyDown(key) && PreviousKeyboardState.IsKeyDown(key));
        }

    }

}
