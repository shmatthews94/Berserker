using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace Berserker
{
	public class Enemy : Sprite
	{
		private int speed;
		private double friction;
		public double x_vel;
		public double y_vel;
		public int movedX;
		public int movedY;
		private bool pushing;

		public Rectangle rectangle
		{
			get
			{
				return new Rectangle(spriteX, spriteY, spriteWidth, spriteHeight);
			}
		}

		public Enemy(int x, int y, int width, int height)
		{
			this.spriteX = x;
			this.spriteY = y;
			this.spriteWidth = width;
			this.spriteHeight = height;
			///grounded = false;
			///moving = false;
			pushing = false;

			// Movement
			speed = 1;
			friction = .15;
			///x_accel = 0;
			///y_accel = 0;
			x_vel = 1;
			y_vel = 1;
			movedX = 0;
		}


		public void LoadContent(ContentManager content)
		{
			image = content.Load<Texture2D>("enemy.png");
		}

		public void Draw(SpriteBatch sb)
		{
			sb.Draw(image, new Rectangle(spriteX, spriteY, spriteWidth, spriteHeight), Color.White);
		}

		public void Update(Controls controls, GameTime gameTime, int x, int y, List<Tree> Trees)
		{
			Move (x, y, Trees);
		}


		public void Move(int x, int y, List<Tree> Trees)
		{

			// Sideways Acceleration

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
			double playerFriction = pushing ? (friction * 3) : friction;
			x_vel = x_vel * (1 - playerFriction);
			y_vel = y_vel * (1 - playerFriction);
			if (x < this.spriteX)
				x_vel *= -1;
			if (y < this.spriteY)
				y_vel *= -1;
			movedX = Convert.ToInt32(x_vel);
			spriteX += movedX;
			movedY = Convert.ToInt32(y_vel);
			spriteY += movedY;
			x_vel = 1;
			y_vel = 1;

			if (spriteX >= 500)
				spriteX = 500;
			else if (spriteX <= 50)
				spriteX = 50;
			if (spriteY >= 500)
				spriteY = 500;
			else if (spriteY <= 50)
				spriteY = 50;

			// Gravity

			// Check up/down collisions, then left/right

		}

	}
}
