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
		public int health;

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
			health = 3;
			// Movement
			speed = 1;
			friction = .15;
			///x_accel = 0;
			///y_accel = 0;
			x_vel = 1;
			y_vel = 1;
			movedX = 0;
		}

		public int getHealth() {
			return this.health;
		}

		public void setHealth(int x) {
			this.health = x;
		}

		public void decrementHealth() {
			this.health = this.health - 1;
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

			x_vel = 1;
			y_vel = 1;
			double playerFriction = pushing ? (friction * 3) : friction;
			x_vel = x_vel * (1 - playerFriction);
			y_vel = y_vel * (1 - playerFriction);
			if (x < this.spriteX)
				x_vel *= -1;
			if (y < this.spriteY)
				y_vel *= -1;

			for (int i = 0; i < Trees.Count; i++) {
				Rectangle player = new Rectangle (this.spriteX, this.spriteY, this.spriteWidth, this.spriteHeight);
				Rectangle tree = new Rectangle (Trees [i].getX (), Trees [i].getY (), Trees [i].getWidth (), Trees [i].getHeight ());
				if (player.Intersects (tree)) {
					Rectangle intersection = Rectangle.Intersect (player, tree);
					if (intersection.Height > intersection.Width) {

						if (this.spriteX >= Trees [i].getX ()) {
							if (this.x_vel < 0) {
								this.x_vel = 0;
								this.spriteX = Trees [i].getX () + Trees[i].getWidth();
							}
						}
						else {
							if (this.x_vel > 0) {
								this.x_vel = 0;
								this.spriteX = Trees [i].getX () - this.spriteWidth;
							}
						}
					} else {
						if (this.spriteY >= Trees [i].getY ()) {
							if (this.y_vel < 0) {
								this.y_vel = 0;
								this.spriteY = Trees [i].getY () + Trees [i].getHeight();
							}
						}
						else {
							if (this.y_vel > 0) {
								this.y_vel = 0;
								this.spriteY = Trees [i].getY () - this.spriteHeight;
							}
						}
					}
				}
			}

			movedX = Convert.ToInt32(x_vel);
			spriteX += movedX;
			movedY = Convert.ToInt32(y_vel);
			spriteY += movedY;

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
