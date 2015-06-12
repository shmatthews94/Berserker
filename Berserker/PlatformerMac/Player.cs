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
        public int health;
        public int score;
        public bool playsound;

        int prevSpriteX;
        int prevSpriteY;

        bool IsAttacking;


        public TimeSpan FrameTime = new TimeSpan(600000);

        public bool rageMode = false;
        public Texture2D rageBar;
        public int rage = 50;
        public double counter;
        public int counter2;

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

        public Texture2D sheet;

        bool normalAttacking = false;
        bool spearAttacking = false;

        #region Player Animations
        Animation idleUp;
        Animation idleDown;
        Animation idleLeft;
        Animation idleRight;

        Animation walkUp;
        Animation walkDown;
        Animation walkLeft;
        Animation walkRight;

        Animation normalUp;
        Animation normalDown;
        Animation normalLeft;
        Animation normalRight;

        Animation spearUp;
        Animation spearDown;
        Animation spearLeft;
        Animation spearRight;
        #endregion

        #region Animation fields and methods
        public List<Animation> Animations = new List<Animation>();
        public Animation currentAnimation = null;

        public void PlayAnimation(Animation animation)
        {
            if (currentAnimation != animation)
            {
                currentAnimation = animation;
                ResetAnimation();
            }
        }

        public void AddAnimation(Animation animation)
        {
            if (animation != null)
                Animations.Add(animation);
        }

        public void ResetAnimation()
        {
            if (currentAnimation != null)
            {
                currentAnimation.IsActive = true;
                currentAnimation.Complete = false;
                currentAnimation.ElapsedTime = TimeSpan.Zero;
                currentAnimation.CurrentFrame = 0;
                currentAnimation.Position.X = spriteX;
                currentAnimation.Position.Y = spriteY;
            }
        }

        public void StopAnimation()
        {
            currentAnimation = null;
        }

        public bool IsPlaybackComplete()
        {
            if (currentAnimation == null)
                return true;
            if (!currentAnimation.IsActive || currentAnimation.Complete)
                return true;
            return false;
        }

        public void UpdateAnimation(GameTime gameTime)
        {
            currentAnimation.Position.X = spriteX;
            currentAnimation.Position.Y = spriteY;
            currentAnimation.Update(gameTime);
        }

        public void DrawAnimation(SpriteBatch spriteBatch)
        {
            currentAnimation.Draw(spriteBatch);
        }
        #endregion

        TimeSpan spearCoolDown;
        TimeSpan spearCooldownTime = new TimeSpan(30000000);

        public Player(int x, int y, int width, int height)
        {
            this.spriteX = x;
            this.spriteY = y;
            this.spriteWidth = width;
            this.spriteHeight = height;

            // Movement
            playsound = true;
            score = 0;
            speed = 3;
            health = 5;
            attackDuration = TimeSpan.Zero;
            spearCoolDown = spearCooldownTime;
            IsAttacking = false;
        }

        public void Reset()
        {
            PlayAnimation(idleDown);
            facing = "down";
            spearCoolDown = spearCooldownTime;
            IsAttacking = false;
        }

        private void initializeAnimations()
        {
            idleUp = new Animation(sheet, new Point(50, 100), new Point(150, 0), new Vector2(0, 0), 1, FrameTime, true, spriteWidth, spriteHeight);
            idleDown = new Animation(sheet, new Point(50, 100), new Point(100, 0), new Vector2(0, 0), 1, FrameTime, true, spriteWidth, spriteHeight);
            idleLeft = new Animation(sheet, new Point(50, 100), new Point(50, 0), new Vector2(0, 0), 1, FrameTime, true, spriteWidth, spriteHeight);
            idleRight = new Animation(sheet, new Point(50, 100), new Point(0, 0), new Vector2(0, 0), 1, FrameTime, true, spriteWidth, spriteHeight);

            walkUp = new Animation(sheet, new Point(50, 100), new Point(200, 0), new Vector2(0, 0), 4, FrameTime, true, spriteWidth, spriteHeight);
            walkDown = new Animation(sheet, new Point(50, 100), new Point(400, 0), new Vector2(0, 0), 4, FrameTime, true, spriteWidth, spriteHeight);
            walkLeft = new Animation(sheet, new Point(60, 100), new Point(840, 0), new Vector2(-5, 0), 4, FrameTime, true, spriteWidth + 10, spriteHeight);
            walkRight = new Animation(sheet, new Point(60, 100), new Point(600, 0), new Vector2(-5, 0), 4, FrameTime, true, spriteWidth + 10, spriteHeight);

            normalUp = new Animation(sheet, new Point(90, 110), new Point(360, 100), new Vector2(-20, -5), 4, FrameTime, false, spriteWidth + 40, spriteHeight + 10);
            normalDown = new Animation(sheet, new Point(70, 100), new Point(1080, 0), new Vector2(-10, 0), 4, FrameTime, false, spriteWidth + 20, spriteHeight);
            normalLeft = new Animation(sheet, new Point(90, 100), new Point(0, 100), new Vector2(-20, 0), 4, FrameTime, false, spriteWidth + 40, spriteHeight);
            normalRight = new Animation(sheet, new Point(90, 100), new Point(1360, 0), new Vector2(-20, 0), 4, FrameTime, false, spriteWidth + 40, spriteHeight);

            spearUp = new Animation(sheet, new Point(70, 240), new Point(720, 100), new Vector2(-10, -115), 4, FrameTime, false, spriteWidth + 20, spriteHeight + 115);
            spearDown = new Animation(sheet, new Point(70, 290), new Point(1000, 100), new Vector2(-10, 0), 4, FrameTime, false, spriteWidth + 20, spriteHeight + 115);
            spearLeft = new Animation(sheet, new Point(250, 100), new Point(1000, 390), new Vector2(-115, 0), 4, FrameTime, false, spriteWidth + 115, spriteHeight);
            spearRight = new Animation(sheet, new Point(250, 100), new Point(0, 390), new Vector2(0, 0), 4, FrameTime, false, spriteWidth + 115, spriteHeight);

            AddAnimation(idleUp);
            AddAnimation(idleDown);
            AddAnimation(idleRight);
            AddAnimation(idleLeft);

            AddAnimation(walkUp);
            AddAnimation(walkDown);
            AddAnimation(walkRight);
            AddAnimation(walkLeft);

            AddAnimation(normalUp);
            AddAnimation(normalDown);
            AddAnimation(normalLeft);
            AddAnimation(normalRight);

            AddAnimation(spearUp);
            AddAnimation(spearDown);
            AddAnimation(spearLeft);
            AddAnimation(spearRight);
        }

        public void decrementHealth()
        {
            if (rageMode == false)
                this.health--;
            AudioManager.PlaySound("Hurt");
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
            image = game.Content.Load<Texture2D>("viking character");
            attackL = game.Content.Load<Texture2D>("slashLeft");
            attackR = game.Content.Load<Texture2D>("slashRight");
            attackU = game.Content.Load<Texture2D>("slashUp");
            attackD = game.Content.Load<Texture2D>("slashDown");
            sAttackL = game.Content.Load<Texture2D>("lanceLeft");
            sAttackR = game.Content.Load<Texture2D>("lanceRight");
            sAttackU = game.Content.Load<Texture2D>("lanceUp");
            sAttackD = game.Content.Load<Texture2D>("lanceDown");
            rageBar = game.Content.Load<Texture2D>("rage");
            sheet = game.Content.Load<Texture2D>("sheet");

            initializeAnimations();
            PlayAnimation(walkDown);
            currentAnimation.Position = new Vector2(spriteX, spriteY);
        }

        public void Draw(SpriteBatch sb)
        {
            //sb.Draw(image, new Rectangle(spriteX, spriteY, spriteWidth, spriteHeight), Color.White);
            //sb.Draw(sheet, new Rectangle(300, 300, 100, 100), Color.White);
            currentAnimation.Draw(sb);
            //if (normalAttacking)
            //{
            //    if (facing == "left")
            //        sb.Draw(attackL, attack, Color.White);

            //    if (facing == "right")
            //        sb.Draw(attackR, attack, Color.White);

            //    if (facing == "up")
            //        sb.Draw(attackU, attack, Color.White);

            //    if (facing == "down")
            //        sb.Draw(attackD, attack, Color.White);
            //}

            //if (spearAttacking)
            //{
            //    if (facing == "left")
            //        sb.Draw(sAttackL, spearAttack, Color.White);

            //    if (facing == "right")
            //        sb.Draw(sAttackR, spearAttack, Color.White);

            //    if (facing == "up")
            //        sb.Draw(sAttackU, spearAttack, Color.White);

            //    if (facing == "down")
            //        sb.Draw(sAttackD, spearAttack, Color.White);
            //}
        }

        public void Update(Controls controls, GameTime gameTime, List<Tree> Trees, List<Enemy> Enemies, List<Object> Objects)
        {
            if (IsPlaybackComplete() && IsAttacking)
            {
                IsAttacking = false;
                ResetAnimation();
                StopAnimation();
            }

            Move(controls, Trees, Enemies, Objects);
            UpdateAnimation(gameTime);

            if (normalAttacking || spearAttacking)
            {
                attackDuration += gameTime.ElapsedGameTime;
            }

            if (rage >= 260)
            {
                if (playsound)
                {
                    AudioManager.PlaySound("RageMode1");
                    playsound = false;
                    rage = 260;
                    rageMode = true;
                }
            }

            if (rageMode == true)
            {
                counter += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (counter >= 9000)
                {
<<<<<<< HEAD
                    rageMode = false;
                    playsound = true;
=======
					playsound = true;
					rageMode = false;
>>>>>>> origin/master
                    counter = 0;
                    rage = 0;
                }
            }

            if (counter2 == 25)
            {
                rage -= 1;
                counter2 = 0;
            }
            counter2 += 1;
            spearCoolDown += gameTime.ElapsedGameTime;
        }


        public void SpearAttack(Controls controls, List<Enemy> Baddies)
        {
            if (spearCoolDown >= spearCooldownTime)
            {
                AudioManager.PlaySound("Spear");
                spearCoolDown = TimeSpan.Zero;
                if (facing == "left")
                {
                    spearAttack = new Rectangle(this.spriteX - 115, this.spriteY, 115, 50);
                    PlayAnimation(spearLeft);
                }

                if (facing == "right")
                {
                    spearAttack = new Rectangle(this.spriteX + 50, this.spriteY, 115, 50);
                    PlayAnimation(spearRight);
                }

                if (facing == "up")
                {
                    spearAttack = new Rectangle(this.spriteX, this.spriteY - 115, 50, 115);
                    PlayAnimation(spearUp);
                }

                if (facing == "down")
                {
                    spearAttack = new Rectangle(this.spriteX, this.spriteY + 50, 50, 115);
                    PlayAnimation(spearDown);
                }
                for (int i = 0; i < Baddies.Count; i++)
                {
                    if (spearAttack.Intersects(Baddies[i].rectangle))
                    {
                        Baddies.Remove(Baddies[i]);
                        this.incrementScore(100);
                        i--;
                    }
                }
            }
        }

        public void Attack(Controls controls, List<Tree> Trees, List<Enemy> Baddies)
        {
            AudioManager.PlaySound("Attack");
            if (facing == "left")
            {
                attack = new Rectangle(this.spriteX - 50, this.spriteY, 50, 50);
                PlayAnimation(normalLeft);
            }

            if (facing == "right")
            {
                attack = new Rectangle(this.spriteX + 50, this.spriteY, 50, 50);
                PlayAnimation(normalRight);
            }

            if (facing == "up")
            {
                attack = new Rectangle(this.spriteX, this.spriteY - 50, 50, 50);
                PlayAnimation(normalUp);
            }

            if (facing == "down")
            {
                attack = new Rectangle(this.spriteX, this.spriteY + 50, 50, 50);
                PlayAnimation(normalDown);
            }
            for (int i = 0; i < Baddies.Count; i++)
            {
                if (attack.Intersects(Baddies[i].rectangle))
                {
                    Baddies[i].decrementHealth();
                    if (Baddies[i].health == 0)
                    {
                        Baddies.RemoveAt(i);
                        this.incrementScore(100);
                        rage += 25;
                    }
                    else
                    {
                        Baddies[i].pushBack(this, Trees, facing);
                    }
                }
            }
        }

        public void Move(Controls controls, List<Tree> Trees, List<Enemy> Enemies, List<Object> Objects)
        {
            // Sideways Acceleration
            if (currentAnimation == null)
            {
                PlayAnimation(idleDown);
                currentAnimation.Position.X = spriteX;
                currentAnimation.Position.Y = spriteY;
            }
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
            if (!IsAttacking)
            {
                if (controls.onPress(Keys.Space, Buttons.A))
                {
                    IsAttacking = true;
                    Attack(controls, Trees, Enemies);
                }
            }

            if (!IsAttacking && spearCoolDown >= spearCooldownTime)
            {
                if (controls.onPress(Keys.A, Buttons.A))
                {
                    IsAttacking = true;
                    SpearAttack(controls, Enemies);
                }
            }

            if (IsAttacking)
                return;

            if (controls.isPressed(Keys.Left, Buttons.DPadLeft))
            {
                facing = "left";
                spriteX -= speed;
                PlayAnimation(walkLeft);
            }
            if (controls.isPressed(Keys.Right, Buttons.DPadRight))
            {
                facing = "right";
                spriteX += speed;
                PlayAnimation(walkRight);
            }
            resolveCollisionsX(Trees, Enemies);


            if (controls.isPressed(Keys.Up, Buttons.DPadUp))
            {
                facing = "up";
                spriteY -= speed;
                PlayAnimation(walkUp);
            }
            if (controls.isPressed(Keys.Down, Buttons.DPadDown))
            {
                facing = "down";
                spriteY += speed;
                PlayAnimation(walkDown);
            }

            resolveCollisionsY(Trees, Enemies);

            if (controls.kb.Equals(new KeyboardState()))
            {
                if (facing == "left")
                {
                    PlayAnimation(idleLeft);
                }

                if (facing == "right")
                {
                    PlayAnimation(idleRight);
                }

                if (facing == "up")
                {
                    PlayAnimation(idleUp);
                }

                if (facing == "down")
                {
                    PlayAnimation(idleDown);
                }
            }
            #endregion


            // OBJECT DETECTION

            for (int i = 0; i < Objects.Count; i++)
            {
                if (spriteX < Objects[i].getX() + Objects[i].getWidth() && spriteX + spriteWidth > Objects[i].getX() && spriteY < Objects[i].getY() + Objects[i].getHeight() && spriteHeight + spriteY > Objects[i].getY())
                {
                    Objects.Remove(Objects[i]);
                    rage += 100;
                    this.incrementScore(600);
                }
            }

            #region Clamp Position to Screen
            if (spriteX >= 600)
                spriteX = 600;
            else if (spriteX <= 50)
                spriteX = 50;
            if (spriteY >= 600)
                spriteY = 600;
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

        public int getRage()
        {
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

