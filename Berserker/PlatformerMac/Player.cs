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

        public Player(int x, int y, int width, int height)
        {
            this.spriteX = x;
            this.spriteY = y;
            this.spriteWidth = width;
            this.spriteHeight = height;

            // Movement
            speed = 5;
			health = 5;
        }

		public void decrementHealth() {
			this.health--;
		}

		public int getHealth() {
			return this.health;
		}

        public void LoadContent(ContentManager content)
        {
            image = content.Load<Texture2D>("viking character.png");
            attackL = content.Load<Texture2D>("slashLeft");
            attackR = content.Load<Texture2D>("slashRight");
            attackU = content.Load<Texture2D>("slashUp");
            attackD = content.Load<Texture2D>("slashDown");
            sAttackL = content.Load<Texture2D>("lance_left");
            sAttackR = content.Load<Texture2D>("lance_right");
            sAttackU = content.Load<Texture2D>("lance_up");
            sAttackD = content.Load<Texture2D>("lance_down");
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

                spearAttacking = false;
            }
        }

        public void Update(Controls controls, GameTime gameTime, List<Tree> Trees, List<Object> Objects)
        {
            Move(controls, Trees, Objects);
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

            if (controls.onPress(Keys.A, Buttons.A))
            {
                spearAttacking = true;
                for (int i = 0; i < Baddies.Count; i++)
                {
                    if (spearAttack.Intersects(Baddies[i].rectangle))
                    {
                        Baddies.Remove(Baddies[i]);
                        i--;
                    }
                }
            }

        }

        public void Attack(Controls controls, List<Enemy> Baddies)
        {
            moveX = 0;
            moveY = 0;
            if (facing == "left")
            {
                attack = new Rectangle(this.spriteX - 50, this.spriteY, 50, 50);
                moveX = -50;
                moveY = 0;
            }

            if (facing == "right")
            {
                attack = new Rectangle(this.spriteX + 50, this.spriteY, 50, 50);
                moveX = 50;
                moveY = 0;
            }

            if (facing == "up")
            {
                attack = new Rectangle(this.spriteX, this.spriteY - 50, 50, 50);
                moveX = 0;
                moveY = -50;
            }

            if (facing == "down")
            {
                attack = new Rectangle(this.spriteX, this.spriteY + 50, 50, 50);
                moveX = 0;
                moveY = 50;
            }

            if (controls.onPress(Keys.Space, Buttons.A))
            {
                normalAttacking = true;
                for (int i = 0; i < Baddies.Count; i++)
                {
                    if (attack.Intersects(Baddies[i].rectangle))
                    {
                        Baddies[i].decrementHealth();
                        if (Baddies[i].health == 0)
                        {
                            Baddies.RemoveAt(i);
                        }
                        else
                        {
                            Baddies[i].setX(Baddies[i].getX() + moveX);
                            Baddies[i].setY(Baddies[i].getY() + moveY);
                        }
                    }
                }
            }

        }

        public void Move(Controls controls, List<Tree> Trees, List<Object> Objects)
        {
            // Sideways Acceleration
            #region Movement and Tree Collision
            int prevSpriteX = spriteX;
            int prevSpriteY = spriteY;


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


            // OBJECT DETECTION

            for (int i = 0; i < Objects.Count; i++)
            {
                if (spriteX < Objects[i].getX() + Objects[i].getWidth() && spriteX + spriteWidth > Objects[i].getX() && spriteY < Objects[i].getY() + Objects[i].getHeight() && spriteHeight + spriteY > Objects[i].getY())
                    Objects.Remove(Objects[i]);
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
        private bool checkCollisions(Tree t)
        {
            if (Hitbox.Intersects(t.Hitbox))
                return true;
            return false;
        }
    }
}

