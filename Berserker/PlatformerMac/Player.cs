using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using System.Collections.Specialized;
namespace Berserker
{
	public class Player : Sprite
	{
		private int speed;
		public int moveX;
		public int moveY;
		public int health;
		public int score;

		int prevSpriteX;
		int prevSpriteY;

		public bool rageMode = false;
		public Texture2D rageBar;
		public int rage = 50;
		public double counter;
		public int counter2;
		public Boolean playsound;

		TimeSpan attackDuration;

		public Rectangle attack;
		public Rectangle spearAttack;

		public String facing = "down";
		public Texture2D attackTex;
		public Texture2D attackL;
		public Texture2D attackR;
		public Texture2D attackU;
		public Texture2D attackD;
		public Texture2D sAttackL;
		public Texture2D sAttackR;
		public Texture2D sAttackU;
		public Texture2D sAttackD;

		bool normalAttacking = false;
		bool spearAttacking = false;

		TimeSpan spearCoolDown;

		public Player(int x, int y, int width, int height)
		{
			this.spriteX = x;
			this.spriteY = y;
			this.spriteWidth = width;
			this.spriteHeight = height;

			// Movement
			score = 0;
			speed = 3;
			health = 5;
			attackDuration = TimeSpan.Zero;
			spearCoolDown = new TimeSpan(50000000);
			playsound = true;
		}

		public void decrementHealth()
		{
			if (rageMode == false)
				this.health--;
			AudioManager.PlaySound ("Hurt");
		}

		public int getHealth()
		{
			return this.health;
		}

		public void incrementScore(int x)
		{
			score += x;
		}

		public int getScore()
		{
			return this.score;
		}

		public void LoadContent(Game game)
		{
			image = game.Content.Load<Texture2D> ("viking character");
			attackL = game.Content.Load<Texture2D>("slashLeft");
			attackR = game.Content.Load<Texture2D>("slashRight");
			attackU = game.Content.Load<Texture2D>("slashUp");
			attackD = game.Content.Load<Texture2D>("slashDown");
			sAttackL = game.Content.Load<Texture2D>("lance_left.png");
			sAttackR = game.Content.Load<Texture2D>("lance_right.png");
			sAttackU = game.Content.Load<Texture2D>("lance_up.png");
			sAttackD = game.Content.Load<Texture2D>("lance_down.png");
			rageBar = game.Content.Load<Texture2D> ("rage");
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
				if (attackDuration >= new TimeSpan(1000000))
				{
					normalAttacking = false;
					attackDuration = TimeSpan.Zero;
				}
			}

			if (spearAttacking)
			{
				if (facing == "left")
					sb.Draw(sAttackL, spearAttack, Color.White);

				if (facing == "right")
					sb.Draw(sAttackR, spearAttack, Color.White);

				if (facing == "up")
					sb.Draw(sAttackU, spearAttack, Color.White);

				if (facing == "down")
					sb.Draw(sAttackD, spearAttack, Color.White);

				if (attackDuration >= new TimeSpan(1000000))
				{
					spearAttacking = false;
					attackDuration = TimeSpan.Zero;
				}
			}
		}

		public void Update(Controls controls, GameTime gameTime, List<Tree> Trees, List<Enemy> Enemies, List<Object> Objects)
		{
			Move(controls, Trees, Enemies, Objects);
			if (!spearAttacking)
			{
				Attack(controls, Trees, Enemies);
			}
			if (!normalAttacking)
			{
				SpearAttack(controls, Enemies);
			}

			if (normalAttacking || spearAttacking)
			{
				attackDuration += gameTime.ElapsedGameTime;
			}
				
			if (rage >= 260)
			{
				rage = 260;
				rageMode = true;
			}

			if (rageMode == true) {
				if(playsound == true) {
					AudioManager.PlaySound ("rage1");
					playsound = false;
				}
				counter += gameTime.ElapsedGameTime.TotalMilliseconds;
				if (counter >= 9000) {
					rageMode = false;
					playsound = true;
					counter = 0;
					rage = 0;
				}
			}

			if (counter2 == 25) {
				rage -= 1;
				counter2 = 0;
			}
			counter2 += 1;
			spearCoolDown += gameTime.ElapsedGameTime;
		}


		public void SpearAttack(Controls controls, List<Enemy> Baddies)
		{
			if (facing == "left")
			{
				spearAttack = new Rectangle(this.spriteX - 115, this.spriteY, 115, 50);
			}

			if (facing == "right")
			{
				spearAttack = new Rectangle(this.spriteX + 50, this.spriteY, 115, 50);
			}

			if (facing == "up")
			{
				spearAttack = new Rectangle(this.spriteX, this.spriteY - 115, 50, 115);
			}

			if (facing == "down")
			{
				spearAttack = new Rectangle(this.spriteX, this.spriteY + 50, 50, 115);
			}
			if (!spearAttacking && spearCoolDown >= new TimeSpan(50000000))
			{
				if (controls.onPress(Keys.A, Buttons.A))
				{
					spearCoolDown = TimeSpan.Zero;
					spearAttacking = true;
					for (int i = 0; i < Baddies.Count; i++)
					{
						if (spearAttack.Intersects(Baddies[i].rectangle))
						{
							Baddies.Remove(Baddies[i]);
							this.incrementScore (100);
							i--;
						}
					}
				}
			}

		}

		public void Attack(Controls controls, List<Tree> Trees, List<Enemy> Baddies)
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
			if (!normalAttacking)
			{
				if (controls.onPress(Keys.Space, Buttons.A))
				{
					AudioManager.PlaySound ("Attack");
					normalAttacking = true;
					for (int i = 0; i < Baddies.Count; i++)
					{
						if (attack.Intersects(Baddies[i].rectangle))
						{
							Baddies[i].decrementHealth();
							if (Baddies[i].health == 0)
							{
								Baddies.RemoveAt(i);
								this.incrementScore (100);
								rage += 25;
							}
							else
							{
								Baddies[i].pushBack(this, Trees, facing);
							}
						}
					}
				}
			}
		}

		public void SmashAttack(Controls controls, List<Tree> Trees, List<Enemy> Baddies) {
			if (rageMode == true) {

			}
		}

		public void Move(Controls controls, List<Tree> Trees, List<Enemy> Enemies, List<Object> Objects)
		{
			// Sideways Acceleration
			#region Movement and Tree Collision
			prevSpriteX = spriteX;
			prevSpriteY = spriteY;

			if (rageMode == true)
			{
				speed = 6;
			}
			else
			{
				speed = 4;
			}

			if (controls.isPressed(Keys.Left, Buttons.DPadLeft))
			{
				facing = "left";
				spriteX -= speed;
			}
			if (controls.isPressed(Keys.Right, Buttons.DPadRight))
			{
				facing = "right";
				spriteX += speed;
			}
			resolveCollisionsX(Trees, Enemies);


			if (controls.isPressed(Keys.Up, Buttons.DPadUp))
			{
				facing = "up";
				spriteY -= speed;
			}
			if (controls.isPressed(Keys.Down, Buttons.DPadDown))
			{
				facing = "down";
				spriteY += speed;
			}

			resolveCollisionsY(Trees, Enemies);
			#endregion


			// OBJECT DETECTION

			for (int i = 0; i < Objects.Count; i++)
			{
				if (spriteX < Objects[i].getX() + Objects[i].getWidth() && spriteX + spriteWidth > Objects[i].getX() && spriteY < Objects[i].getY() + Objects[i].getHeight() && spriteHeight + spriteY > Objects[i].getY())
				{
					Objects.Remove(Objects[i]);
					rage += 100;
					this.incrementScore (500);
				}
			}

			#region Clamp Position to Screen
			if (spriteX >= 500)
				spriteX = 500;
			else if (spriteX <= 50)
				spriteX = 50;
			if (spriteY >= 500)
				spriteY = 500;
			else if (spriteY <= 50)
				spriteY = 50;
			#endregion
		}

		private void resolveCollisionsY(List<Tree> Trees, List<Enemy> Enemies)
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
			Enemies = Enemies.OrderBy(e => e.getY()).ToList();
			foreach (Enemy e in Enemies)
			{
				if (Math.Abs(spriteY - e.getY()) <= Math.Max(spriteWidth, e.getWidth()))
				{
					if (checkCollisions(e))
					{
						if (prevSpriteY < spriteY)
						{
							spriteY = e.getY() - spriteHeight;
						}
						else if (prevSpriteY > spriteY)
						{
							spriteY = e.getY() + e.getHeight();
						}
					}
				}
			}
		}

		public int getRage() {
			return this.rage;
		}


		private void resolveCollisionsX(List<Tree> Trees, List<Enemy> Enemies)
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
			Enemies = Enemies.OrderBy(e => e.getX()).ToList();
			foreach (Enemy e in Enemies)
			{
				if (Math.Abs(spriteX - e.getX()) <= Math.Max(spriteWidth, e.getWidth()))
				{
					if (checkCollisions(e))
					{
						if (prevSpriteX < spriteX)
						{
							spriteX = e.getX() - spriteWidth;
						}
						else if (prevSpriteX > spriteX)
						{
							spriteX = e.getX() + e.getWidth();
						}
					}
				}
			}
		}

		private bool checkCollisions(Sprite s)
		{
			if (Hitbox.Intersects(s.Hitbox))
				return true;
			return false;
		}

		internal void pushBack(List<Enemy> Enemies, List<Tree> Trees, string facing)
		{
			if (!rageMode)
			{
				switch (facing)
				{
				case "up":
					spriteY -= 50;
					resolveCollisionsY(Trees, Enemies);
					break;
				case "down":
					spriteY += 50;

					resolveCollisionsY(Trees, Enemies);
					break;
				case "left":
					spriteX -= 50;
					resolveCollisionsX(Trees, Enemies);
					break;
				case "right":
					spriteX += 50;
					resolveCollisionsX(Trees, Enemies);
					break;
				}
			}
		}
	}
}

