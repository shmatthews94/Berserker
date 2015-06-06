using System;
using System.Linq;
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
		public int health;
		public String facing;
		public bool normalAttacking = false;
		public Rectangle attack;
		public Texture2D attackL;
		public Texture2D attackR;
		public Texture2D attackU;
		public Texture2D attackD;

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
			health = 3;
			// Movement
			speed = 1;
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

		public void Attack(Controls controls, Player player, int counter)
		{
			if (counter % 50 == 0) {
				if (facing == "left")
				{
					attack = new Rectangle(this.spriteX - 50, this.spriteY, 50, 50);
				}

				if (facing == "right")
				{
					attack = new Rectangle(this.spriteX + 50, this.spriteY, 50, 50);
				}

				if (facing == "up")
				{
					attack = new Rectangle(this.spriteX, this.spriteY - 50, 50, 50);
				}

				if (facing == "down")
				{
					attack = new Rectangle(this.spriteX, this.spriteY + 50, 50, 50);
				}
				normalAttacking = true;
				if (attack.Intersects(new Rectangle(player.getX(), player.getY(), player.getWidth(), player.getHeight())))
				{
					player.decrementHealth ();
				}
			}
		}

		public void LoadContent(ContentManager content)
		{
			image = content.Load<Texture2D>("enemy.png");
			attackL = content.Load<Texture2D>("slashLeft");
			attackR = content.Load<Texture2D>("slashRight");
			attackU = content.Load<Texture2D>("slashUp");
			attackD = content.Load<Texture2D>("slashDown");
		}

		public void Draw(SpriteBatch sb)
		{
			sb.Draw(image, new Rectangle(spriteX, spriteY, spriteWidth, spriteHeight), Color.White);
			if (normalAttacking)
			{
				if (facing == "left")
					sb.Draw(attackL, attack, Color.White);

				if (facing == "right")
					sb.Draw(attackR, attack, Color.White);

				if (facing == "up")
					sb.Draw(attackU, attack, Color.White);

				if (facing == "down")
					sb.Draw(attackD, attack, Color.White);

				normalAttacking = false;

			}
		}

		public void Update(Controls controls, GameTime gameTime, int x, int y, List<Tree> Trees)
		{
			Move (x, y, Trees);
		}

		public void Move(int x, int y, List<Tree> Trees)
		{


            // Sideways Acceleration
            #region Movement and Tree Collision
            int prevSpriteX = spriteX;
            int prevSpriteY = spriteY;
            if (prevSpriteX > x)
            {
                spriteX -= speed;
            }
            if (prevSpriteX < x)
            {
                spriteX += speed;
            }
			if (Math.Abs (this.spriteX - x) > Math.Abs (this.spriteY - y)) {
				if (x < this.spriteX)
					facing = "left";
				else
					facing = "right";
			}
			if (Math.Abs (this.spriteY - y) >= Math.Abs (this.spriteX - x)) {
				if (y < this.spriteY)
					facing = "up";
				else
					facing = "down";
			}

            Trees = Trees.OrderBy(t => t.getX()).ToList();
            foreach (Tree t in Trees)
            {
                if (Math.Abs(spriteX - t.getX()) <= Math.Max(spriteWidth, t.getWidth()))
                {
                    if (checkCollisions(t))
                    {
                        spriteX = prevSpriteX;
                    }
                }
            }

            if (prevSpriteY > y)
            {
                spriteY -= speed;
            }
            if (prevSpriteY < y)
            {
                spriteY += speed;
            }

            Trees = Trees.OrderBy(t => t.getY()).ToList();
            foreach (Tree t in Trees)
            {
                if (Math.Abs(spriteY - t.getY()) <= Math.Max(spriteWidth, t.getWidth()))
                {
                    if (checkCollisions(t))
                    {
                        spriteY = prevSpriteY;
                    }
                }
            }
            #endregion

            #region Clamp position to screen
            if (spriteX >= 500)
				spriteX = 500;
			else if (spriteX <= 50)
				spriteX = 50;
			if (spriteY >= 500)
				spriteY = 500;
			else if (spriteY <= 50)
				spriteY = 50;
            #endregion

            // Gravity
		}

        private bool checkCollisions(Tree t)
        {
            if (Hitbox.Intersects(t.Hitbox))
                return true;
            return false;
        }
	}
}
