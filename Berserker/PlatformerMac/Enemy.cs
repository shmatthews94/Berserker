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

		int prevSpriteX;
		int prevSpriteY;

		TimeSpan elapsedWanderTime;
		TimeSpan targetWanderTime;

		public TimeSpan elapsedAttackTime;

		Random rand = new Random();
		int wanderDir;

		public Rectangle rectangle
		{
			get
			{
				return new Rectangle(spriteX, spriteY, spriteWidth, spriteHeight);
			}
		}

		public int movePattern
		{
			get;
			set;
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
			movePattern = 0;
			elapsedWanderTime = TimeSpan.Zero;
			elapsedAttackTime = TimeSpan.Zero;
			targetWanderTime = new TimeSpan(10000);
		}

		public Enemy(int x, int y, int width, int height, int s)
		{
			this.spriteX = x;
			this.spriteY = y;
			this.spriteWidth = width;
			this.spriteHeight = height;
			health = 3;
			// Movement
			this.speed = s;
			movePattern = 0;
			elapsedWanderTime = TimeSpan.Zero;
			elapsedAttackTime = TimeSpan.Zero;
			targetWanderTime = new TimeSpan(10000);
		}

		public int getHealth()
		{
			return this.health;
		}

		public void setHealth(int x)
		{
			this.health = x;
		}

		public void decrementHealth()
		{
			this.health = this.health - 1;
		}

		public void Attack(Controls controls, Player player, List<Enemy> Enemies, List<Tree> Trees)
		{
			if (Math.Abs(player.getX() - spriteX) + Math.Abs(player.getY() - spriteY) <= 100)
			{
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
					player.decrementHealth();
					player.pushBack(Enemies, Trees, facing);
				}
			}
		}

		public void LoadContent(Game game)
		{
			image = game.Content.Load<Texture2D>("enemy.png");
			attackL = game.Content.Load<Texture2D>("slashLeft");
			attackR = game.Content.Load<Texture2D>("slashRight");
			attackU = game.Content.Load<Texture2D>("slashUp");
			attackD = game.Content.Load<Texture2D>("slashDown");
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

		public void Update(Controls controls, GameTime gameTime, int x, int y, Player p, List<Enemy> Enemies, List<Tree> Trees)
		{
			Move(gameTime, x, y, p, Trees);

			elapsedAttackTime += gameTime.ElapsedGameTime;
			if (elapsedAttackTime >= new TimeSpan(10000000))
			{
				Attack(controls, p, Enemies, Trees);
				elapsedAttackTime = TimeSpan.Zero;
			}
		}

		public void Move(GameTime gameTime, int x, int y, Player p, List<Tree> Trees)
		{
			prevSpriteX = spriteX;
			prevSpriteY = spriteY;

			// Sideways Acceleration
			#region Movement and Tree Collision

			switch (movePattern)
			{
			case 0:
				wander(gameTime, p, Trees);
				if (playerNearby(x, y))
				{
					movePattern = 1;
				}
				break;

			case 1:
				pursue(x, y, p, Trees);
				break;

			case 2:
				flee(x, y, p, Trees);
				break;
			}

			#endregion

			if (Math.Abs(this.spriteX - x) > Math.Abs(this.spriteY - y))
			{
				if (x < this.spriteX)
					facing = "left";
				else
					facing = "right";
			}
			if (Math.Abs(this.spriteY - y) >= Math.Abs(this.spriteX - x))
			{
				if (y < this.spriteY)
					facing = "up";
				else
					facing = "down";
			}

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

		private bool playerNearby(int x, int y)
		{
			if (Math.Abs(spriteX - x) + Math.Abs(spriteY - y) <= 150)
				return true;
			return false;
		}

		private void flee(int x, int y, Player p, List<Tree> Trees)
		{
			throw new NotImplementedException();
		}

		private void pursue(int x, int y, Player p, List<Tree> Trees)
		{
			if (prevSpriteX > x)
			{
				spriteX -= speed;
			}
			if (prevSpriteX < x)
			{
				spriteX += speed;
			}

			resolveCollisionsX(p, Trees);

			if (prevSpriteY > y)
			{
				spriteY -= speed;
			}
			if (prevSpriteY < y)
			{
				spriteY += speed;
			}

			resolveCollisionsY(p, Trees);
		}

		private void wander(GameTime gameTime, Player p, List<Tree> Trees)
		{
			elapsedWanderTime += gameTime.ElapsedGameTime;
			if (elapsedWanderTime >= targetWanderTime)
			{
				wanderDir = rand.Next(1, 10);
				targetWanderTime = new TimeSpan(rand.Next(5000000, 20000000));
				elapsedWanderTime = TimeSpan.Zero;
			}
			switch (wanderDir)
			{
			case 1:
				spriteX -= speed;
				resolveCollisionsX(p, Trees);
				spriteY += speed;
				resolveCollisionsY(p, Trees);
				break;
			case 2:
				spriteY += speed;
				resolveCollisionsY(p, Trees);
				break;
			case 3:
				spriteX += speed;
				resolveCollisionsX(p, Trees);
				spriteY += speed;
				resolveCollisionsY(p, Trees);
				break;
			case 4:
				spriteX -= speed;
				resolveCollisionsX(p, Trees);
				break;
			case 5:
				break;
			case 6:
				spriteX += speed;
				resolveCollisionsX(p, Trees);
				break;
			case 7:
				spriteX -= speed;
				resolveCollisionsX(p, Trees);
				spriteY -= speed;
				resolveCollisionsY(p, Trees);
				break;
			case 8:
				spriteY -= speed;
				resolveCollisionsY(p, Trees);
				break;
			case 9:
				spriteX += speed;
				resolveCollisionsX(p, Trees);
				spriteY -= speed;
				resolveCollisionsY(p, Trees);
				break;
			}
		}

		private void resolveCollisionsX(Player p, List<Tree> Trees)
		{
			Trees = Trees.OrderBy(t => t.getX()).ToList();
			foreach (Tree t in Trees)
			{
				if (Math.Abs(spriteX - t.getX()) <= Math.Max(spriteWidth, t.getWidth()))
				{
					if (checkCollisions(t))
					{
						if (prevSpriteX < spriteX)
						{
							spriteX = t.getX() - spriteWidth;
						}
						else if (prevSpriteX > spriteX)
						{
							spriteX = t.getX() + t.getWidth();
						}
					}
				}
			}
			if (checkCollisions(p))
			{
				if (prevSpriteX < spriteX)
				{
					spriteX = p.getX() - spriteWidth;
				}
				else if (prevSpriteX > spriteX)
				{
					spriteX = p.getX() + p.getWidth();
				}
			}
		}

		private void resolveCollisionsY(Player p, List<Tree> Trees)
		{
			Trees = Trees.OrderBy(t => t.getY()).ToList();
			foreach (Tree t in Trees)
			{
				if (Math.Abs(spriteY - t.getY()) <= Math.Max(spriteWidth, t.getWidth()))
				{
					if (checkCollisions(t))
					{
						if (prevSpriteY < spriteY)
						{
							spriteY = t.getY() - spriteHeight;
						}
						else if (prevSpriteY > spriteY)
						{
							spriteY = t.getY() + t.getHeight();
						}
					}
				}
			}
			if (checkCollisions(p))
			{
				if (prevSpriteY < spriteY)
				{
					spriteY = p.getY() - spriteHeight;
				}
				else if (prevSpriteY > spriteY)
				{
					spriteY = p.getY() + p.getHeight();
				}
			}
		}

		private bool checkCollisions(Sprite s)
		{
			if (Hitbox.Intersects(s.Hitbox))
				return true;
			return false;
		}

		internal void pushBack(Player p, List<Tree> Trees, string facing)
		{
			elapsedAttackTime = TimeSpan.Zero;
			switch (facing)
			{
			case "up":
				spriteY -= 50;
				resolveCollisionsY(p, Trees);
				break;
			case "down":
				spriteY += 50;

				resolveCollisionsY(p, Trees);
				break;
			case "left":
				spriteX -= 50;
				resolveCollisionsX(p, Trees);
				break;
			case "right":
				spriteX += 50;
				resolveCollisionsX(p, Trees);
				break;
			}
		}
	}
}
