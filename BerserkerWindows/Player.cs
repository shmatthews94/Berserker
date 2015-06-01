using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace Berserker
{
	public class Player : Sprite
    {
		private bool moving;
		private bool grounded;
		private double speed;
		private double x_accel;
		private double y_accel;
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

        public void LoadContent(ContentManager content)
        {
            image = content.Load<Texture2D>("viking character.png");
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(image, new Rectangle(spriteX, spriteY, spriteWidth, spriteHeight), Color.White);
        }

		public void Update(Controls controls, GameTime gameTime, List<Tree> Trees, List<Object> Objects)
		{
			Move (controls, Trees, Objects);
		}

		public void Attack(Controls controls, List<Enemy> Baddies)
		{
			if(controls.onPress(Keys.Space, Buttons.A))
				for (int i = 0; i < Baddies.Count; i++) {
					double xdist = Math.Pow(this.spriteX - Baddies[i].getX(), 2);
					double ydist = Math.Pow(this.spriteY - Baddies[i].getY(), 2);
					if (Math.Pow((xdist + ydist), .5) < 50)
						Baddies.Remove (Baddies [i]);
				}
		}


		public void Move(Controls controls, List<Tree> Trees, List<Object> Objects)
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

			// EDGE DETECTION

			for (int i = 0; i < Trees.Count; i++) {
				//left side
				if ((spriteX + spriteWidth == Trees [i].getX ()) && (spriteX < Trees[i].getX()) && (spriteY + spriteHeight > Trees [i].getY ()) && (spriteY < Trees [i].getY () + Trees [i].getHeight ())) {
					if (x_vel > 0) {
						spriteX = Trees [i].getX () - spriteWidth;
						x_vel = 0;
					}
				}
				//right side
				if ((spriteX > Trees [i].getX ()) && (spriteX == Trees [i].getX () + Trees [i].getWidth ()) && (spriteY + spriteHeight > Trees [i].getY ()) && (spriteY < Trees [i].getY () + Trees [i].getHeight ())) {
					if (x_vel < 0) {
						spriteX = Trees [i].getX () + Trees [i].getWidth ();
						x_vel = 0;
					}
				}
				//top side
				if ((spriteX + spriteWidth > Trees [i].getX ()) && (spriteX < Trees [i].getX () + Trees [i].getWidth ()) && (spriteY + spriteHeight == Trees [i].getY ()) && (spriteY < Trees [i].getY ())) {
					if (y_vel > 0) {
						spriteY = Trees [i].getY () - spriteHeight;
						y_vel = 0;
					}
				}
				//bottom side
				if ((spriteX + spriteWidth > Trees [i].getX ()) && (spriteX < Trees [i].getX () + Trees [i].getWidth ()) && (spriteY == Trees [i].getY () + Trees [i].getHeight ()) && (spriteY > Trees [i].getY ())) {
					if (y_vel < 0) {
						spriteY = Trees [i].getY () + Trees [i].getHeight ();
						y_vel = 0;
					}
				}
			}

			// OBJECT DETECTION

			for (int i = 0; i < Objects.Count; i++) {
				if (spriteX < Objects [i].getX () + Objects [i].getWidth () && spriteX + spriteWidth > Objects [i].getX () && spriteY < Objects [i].getY () + Objects [i].getHeight () && spriteHeight + spriteY > Objects [i].getY ())
					Objects.Remove (Objects [i]);
			}

			x_vel = x_vel * (1 - friction) + x_accel * .05;
			y_vel = y_vel * (1 - friction) + y_accel * .05;
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
