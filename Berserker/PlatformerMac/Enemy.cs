﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace PlatformerMac
{
	class Enemy : Sprite
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

		public Enemy(int x, int y, int width, int height)
		{
			this.spriteX = x;
			this.spriteY = y;
			this.spriteWidth = width;
			this.spriteHeight = height;
			grounded = false;
			moving = false;
			pushing = false;

			// Movement
			speed = 1;
			friction = .15;
			x_accel = 0;
			y_accel = 0;
			x_vel = 0;
			y_vel = 0;
			movedX = 0;
		}

		public int getX(){
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

		public void Update(Controls controls, GameTime gameTime, int x, int y)
		{
			Move (x, y);
		}

		public void Move(int x, int y)
		{

			// Sideways Acceleration


			double playerFriction = pushing ? (friction * 3) : friction;
			x_vel = speed * (1 - playerFriction) + x_accel * .10;
			y_vel = speed * (1 - playerFriction) + y_accel * .10;
			if (x < this.spriteX)
				x_vel *= -1;
			if (y < this.spriteY)
				y_vel *= -1;
			movedX = Convert.ToInt32(x_vel);
			spriteX += movedX;
			movedY = Convert.ToInt32(y_vel);
			spriteY += movedY;
			// Gravity

			// Check up/down collisions, then left/right

		}

	}
}