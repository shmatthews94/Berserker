using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Berserker
{
    class Boss : Enemy
    {
        int maxHealth;

        Random rand = new Random();

        Texture2D yeti;
        public TimeSpan FrameTime = new TimeSpan(500000);

        bool IsAttacking;

        #region Boss Animations
        Animation idleUp;
        Animation idleDown;
        Animation idleLeft;
        Animation idleRight;

        Animation attackUp;
        Animation attackDown;
        Animation attackLeft;
        Animation attackRight;

        Animation hurtUp;
        Animation hurtDown;
        Animation hurtLeft;
        Animation hurtRight;
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

        public Boss(int x, int y, int width, int height, int spawntime)
        {
            this.spriteX = x;
            this.spriteY = y;
            this.spriteWidth = width;
            this.spriteHeight = height;
            maxHealth = 15;
            health = maxHealth;
            // Movement
            speed = 1;
            movePattern = 0;
            elapsedWanderTime = TimeSpan.Zero;
            elapsedAttackTime = TimeSpan.Zero;
            targetWanderTime = new TimeSpan(10000);
            IsAttacking = false;
        }

        public override void LoadContent(Game game)
        {
            yeti = game.Content.Load<Texture2D>("yeti");
            initializeAnimations();
            facing = "down";
            PlayAnimation(idleDown);
            currentAnimation.Position = new Vector2(spriteX, spriteY);
        }

        public override void Attack(Controls controls, Player player, List<Enemy> Enemies, List<Tree> Trees)
        {
            int x = player.getX();
            int y = player.getY();
            if (Math.Abs(player.getX() - spriteX) + Math.Abs(player.getY() - spriteY) <= 150)
            {
                if (Math.Abs(this.spriteX - x) > Math.Abs(this.spriteY - y))
                {
                    if (x < this.spriteX)
                        facing = "left";
                    else if (x < this.spriteX)
                        facing = "right";
                }
                if (Math.Abs(this.spriteY - y) >= Math.Abs(this.spriteX - x))
                {
                    if (y < this.spriteY)
                        facing = "up";
                    else if (y > this.spriteY)
                        facing = "down";
                }

                if (facing == "left")
                {
                    attack = new Rectangle(this.spriteX - spriteWidth, this.spriteY, spriteWidth, spriteHeight);
                    PlayAnimation(attackLeft);
                }

                if (facing == "right")
                {
                    attack = new Rectangle(this.spriteX + spriteWidth, this.spriteY, spriteWidth, spriteHeight);
                    PlayAnimation(attackRight);
                }

                if (facing == "up")
                {
                    attack = new Rectangle(this.spriteX, this.spriteY - spriteHeight, spriteWidth, spriteHeight);
                    PlayAnimation(attackUp);
                }

                if (facing == "down")
                {
                    attack = new Rectangle(this.spriteX, this.spriteY + spriteHeight, spriteWidth, spriteHeight);
                    PlayAnimation(attackDown);
                }
                if (attack.Intersects(new Rectangle(player.getX(), player.getY(), player.getWidth(), player.getHeight())))
                {
                    player.decrementHealth();
                    player.pushBack(Enemies, Trees, facing);
                }
                IsAttacking = true;
            }
        }

        public void initializeAnimations()
        {
            idleUp = new Animation(yeti, new Point(200, 267), new Point(0, 0), new Vector2(0, 0), 1, FrameTime, true, spriteWidth, spriteHeight);
            idleDown = new Animation(yeti, new Point(200, 267), new Point(0, 267), new Vector2(0, 0), 1, FrameTime, true, spriteWidth, spriteHeight);
            idleLeft = new Animation(yeti, new Point(200, 267), new Point(600, 267), new Vector2(0, 0), 1, FrameTime, true, spriteWidth, spriteHeight);
            idleRight = new Animation(yeti, new Point(200, 267), new Point(0, 534), new Vector2(0, 0), 1, FrameTime, true, spriteWidth, spriteHeight);

            attackUp = new Animation(yeti, new Point(200, 267), new Point(0, 0), new Vector2(0, 0), 3, FrameTime, false, spriteWidth, spriteHeight);
            attackDown = new Animation(yeti, new Point(200, 267), new Point(1199, 0), new Vector2(0, 0), 3, FrameTime, false, spriteWidth, spriteHeight);
            attackLeft = new Animation(yeti, new Point(200, 267), new Point(600, 267), new Vector2(0, 0), 3, FrameTime, false, spriteWidth, spriteHeight);
            attackRight = new Animation(yeti, new Point(200, 267), new Point(0, 534), new Vector2(0, 0), 3, FrameTime, false, spriteWidth, spriteHeight);

            hurtUp = new Animation(yeti, new Point(200, 267), new Point(0, 0), new Vector2(0, 0), 3, FrameTime, false, spriteWidth, spriteHeight);
            hurtDown = new Animation(yeti, new Point(200, 267), new Point(0, 267), new Vector2(0, 0), 3, FrameTime, false, spriteWidth, spriteHeight);
            hurtLeft = new Animation(yeti, new Point(200, 267), new Point(1199, 267), new Vector2(0, 0), 3, FrameTime, false, spriteWidth, spriteHeight);
            hurtRight = new Animation(yeti, new Point(200, 267), new Point(599, 534), new Vector2(0, 0), 3, FrameTime, false, spriteWidth, spriteHeight);
        }

        public override void Update(Controls controls, GameTime gameTime, int x, int y, Player p, List<Enemy> Enemies, List<Tree> Trees)
        {
            if (IsPlaybackComplete())
            {
                IsAttacking = false;
                ResetAnimation();
                StopAnimation();
            }
            if (IsAttacking)
            {

                if (currentAnimation.CurrentFrame == 2)
                {
                    
                }
            }
            elapsedAttackTime += gameTime.ElapsedGameTime;
            if (elapsedAttackTime >= new TimeSpan(10000000))
            {
                Attack(controls, p, Enemies, Trees);
                elapsedAttackTime = TimeSpan.Zero;
            }

            if (!IsAttacking)
            {
                Move(gameTime, x, y, p, Trees);
            }

            UpdateAnimation(gameTime);
        }

        public override void Move(GameTime gameTime, int x, int y, Player p, List<Tree> Trees)
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

                //case 2:
                //    flee(x, y, p, Trees);
                //    break;
            }

            #endregion

            //if (Math.Abs(this.spriteX - x) > Math.Abs(this.spriteY - y))
            //{
            //    if (x < this.spriteX)
            //        facing = "left";
            //    else if (x > this.spriteX)
            //        facing = "right";
            //}
            //if (Math.Abs(this.spriteY - y) >= Math.Abs(this.spriteX - x))
            //{
            //    if (y < this.spriteY)
            //        facing = "up";
            //    else if (y > this.spriteY)
            //        facing = "down";
            //}

            switch (facing)
            {
                case "up":
                    PlayAnimation(idleUp);
                    break;
                case "down":
                    PlayAnimation(idleDown);
                    break;
                case "left":
                    PlayAnimation(idleLeft);
                    break;
                case "right":
                    PlayAnimation(idleRight);
                    break;
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


        public override void pursue(int x, int y, Player p, List<Tree> Trees)
        {
            if (Math.Abs(prevSpriteX - x) >= 15)
            {
                if (prevSpriteX > x)
                {
                    spriteX -= speed;
                    facing = "left";
                }
                if (prevSpriteX < x)
                {
                    spriteX += speed;
                    facing = "right";
                }
                resolveCollisionsX(p, Trees);
            }

            if (Math.Abs(prevSpriteX - x) >= 15)
            {
                if (prevSpriteY > y)
                {
                    spriteY -= speed;
                    if (Math.Abs(prevSpriteX - x) < Math.Abs(prevSpriteY - y))
                    {
                        facing = "up";
                    }
                }
                if (prevSpriteY < y)
                {
                    spriteY += speed;
                    if (Math.Abs(prevSpriteX - x) < Math.Abs(prevSpriteY - y))
                    {
                        facing = "down";
                    }
                }
                resolveCollisionsY(p, Trees);
            }
        }

        public override void wander(GameTime gameTime, Player p, List<Tree> Trees)
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

        public override bool playerNearby(int x, int y)
        {
            if (Math.Abs(spriteX - x) + Math.Abs(spriteY - y) <= 300)
                return true;
            return false;
        }

        public override void Draw(SpriteBatch sb)
        {
            DrawAnimation(sb);
        }
    }
}
