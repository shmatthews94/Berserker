using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace PlatformerMac
{
	public class Player : Sprite
    {
		private bool moving;
		private bool grounded;
		private int speed;
		private int x_accel;
		private int y_accel;
		private double friction;
		public double x_vel;
		public double y_vel;
		public int movedX;
		public int movedY;
		private bool pushing;
		public double gravity = 0.1;
		public int maxFallSpeed = 10;
		private int jumpPoint = 0;
        
        public Player(int x, int y, int width, int height)
        {
            this.spriteX = x;
            this.spriteY = y;
            this.spriteWidth = width;
            this.spriteHeight = height;
			grounded = false;
			moving = false;
			pushing = false;

			// Movement
			speed = 5;
			friction = .15;
			x_accel = 0;
			y_accel = 0;
			x_vel = 0;
			y_vel = 0;
			movedX = 0;
        }

        public int getX()
		{
            return spriteX;
        }
        public int getY()
        {
            return spriteY;
        }
        public void setX(int x)
        {
            spriteX = x;
        }
        public void setY(int y)
        {
            spriteY = y;
        }

        public void LoadContent(ContentManager content)
        {
            image = content.Load<Texture2D>("viking character.png");
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(image, new Rectangle(spriteX, spriteY, spriteWidth, spriteHeight), Color.White);
        }

		public void Update(Controls controls, GameTime gameTime)
		{
			Move (controls);
		}

		public void Move(Controls controls)
		{

			// Sideways Acceleration
			if (controls.onPress(Keys.Right, Buttons.DPadRight))
				x_accel += speed;
			else if (controls.onRelease(Keys.Right, Buttons.DPadRight))
				x_accel -= speed;
			if (controls.onPress(Keys.Left, Buttons.DPadLeft))
				x_accel -= speed;
			else if (controls.onRelease(Keys.Left, Buttons.DPadLeft))
				x_accel += speed;

			if (controls.onPress(Keys.Up, Buttons.DPadUp))
				y_accel -= speed;
			else if (controls.onRelease(Keys.Up, Buttons.DPadUp))
				y_accel += speed;
			if (controls.onPress(Keys.Down, Buttons.DPadDown))
				y_accel += speed;
			else if (controls.onRelease(Keys.Down, Buttons.DPadDown))
				y_accel -= speed;
			
			double playerFriction = pushing ? (friction * 3) : friction;
			x_vel = x_vel * (1 - playerFriction) + x_accel * .10;
			y_vel = y_vel * (1 - playerFriction) + y_accel * .10;
			movedX = Convert.ToInt32(x_vel);
			spriteX += movedX;
			movedY = Convert.ToInt32(y_vel);
			spriteY += movedY;
			if (spriteX >= 550)
				spriteX = 550;
			else if (spriteX <= 0)
				spriteX = 0;
			if (spriteY >= 550)
				spriteY = 550;
			else if (spriteY <= 0)
				spriteY = 0;

			if (spriteX >= 550)
				spriteX = 550;
			else if (spriteX <= 0)
				spriteX = 0;
			if (spriteY >= 550)
				spriteY = 550;
			else if (spriteY <= 0)
				spriteY = 0;
			
			// Gravity

			// Check up/down collisions, then left/right

		}
			
    }
}
