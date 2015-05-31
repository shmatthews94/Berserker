using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Berserker
{
    public class Player : AnimatingSprite
    {
        #region Fields
        public int HP;
        public Vector2 Velocity;

        public string Direction;
        bool IsAttacking;

        AnimatingSprite attack;
        #endregion

        #region Movement Animations
        Animation idle;
        Animation attackDownAnim;

        Animation walkDown;
        Animation walkLeft;
        Animation walkRight;
        Animation walkUp;

        Animation facingDown;
        Animation facingLeft;
        Animation facingRight;
        Animation facingUp;
        #endregion

        #region Attack Animations
        Animation normalDown;
        Animation normalLeft;
        Animation normalRight;
        Animation normalUp;

        Animation smashDown;
        Animation smashLeft;
        Animation smashRight;
        Animation smashUp;

        Animation spearDown;
        Animation spearLeft;
        Animation spearRight;
        Animation spearUp;

        Animation shieldWall;
        #endregion

        public void Initialize(Vector2 pos, Texture2D tex)
        {
            Position = pos;
            Dimensions = new Point(32, 32);
            Texture = tex;

            HP = 5;
            Velocity = new Vector2(2.0f, 2.0f);
            Direction = "down";
            IsAttacking = false;

            initializePlayerAnimations();
            initializeAttackAnimations();

            PlayAnimation(idle);
            currentAnimation.Position = Position;
        }

        private void initializeAttackAnimations()
        {
            attack = new AnimatingSprite();
            attack.currentAnimation = null;

            normalDown = new Animation(Texture, new Point(8, 24), new Point(32, 16),
                new Point(0, 12), new Point(3, 12), new TimeSpan(1000000), false);
            normalLeft = new Animation(Texture, new Point(16, 12), new Point(16, 32),
                new Point(8, 6), new Point(11, 6), new TimeSpan(1000000), false);
            normalRight = new Animation(Texture, new Point(16, 12), new Point(16, 32),
                new Point(0, 7), new Point(3, 7), new TimeSpan(1000000), false);
            normalUp = new Animation(Texture, new Point(8, 24), new Point(32, 16),
                new Point(4, 15), new Point(7, 15), new TimeSpan(1000000), false);

            smashDown = new Animation(Texture, new Point(8, 12), new Point(32, 32),
                new Point(0, 8), new Point(3, 8), new TimeSpan(1000000), false);
            smashLeft = new Animation(Texture, new Point(8, 12), new Point(32, 32),
                new Point(4, 8), new Point(7, 8), new TimeSpan(1000000), false);
            smashRight = new Animation(Texture, new Point(8, 12), new Point(32, 32),
                new Point(0, 9), new Point(3, 9), new TimeSpan(1000000), false);
            smashUp = new Animation(Texture, new Point(8, 12), new Point(32, 32),
                new Point(4, 9), new Point(7, 9), new TimeSpan(1000000), false);

            spearDown = new Animation(Texture, new Point(8, 12), new Point(32, 32),
                new Point(0, 10), new Point(3, 10), new TimeSpan(1000000), false);
            spearLeft = new Animation(Texture, new Point(8, 12), new Point(32, 32),
                new Point(4, 10), new Point(7, 10), new TimeSpan(1000000), false);
            spearRight = new Animation(Texture, new Point(8, 12), new Point(32, 32),
                new Point(0, 11), new Point(3, 11), new TimeSpan(1000000), false);
            spearUp = new Animation(Texture, new Point(8, 12), new Point(32, 32),
                new Point(4, 11), new Point(7, 11), new TimeSpan(1000000), false);

            shieldWall = new Animation(Texture, new Point(8, 12), new Point(32, 32),
                new Point(0, 12), new Point(0, 12), new TimeSpan(1000000), false);

            attack.AddAnimation(normalDown);
            attack.AddAnimation(normalLeft);
            attack.AddAnimation(normalRight);
            attack.AddAnimation(normalUp);

            attack.AddAnimation(smashDown);
            attack.AddAnimation(smashLeft);
            attack.AddAnimation(smashRight);
            attack.AddAnimation(smashUp);

            attack.AddAnimation(spearDown);
            attack.AddAnimation(spearLeft);
            attack.AddAnimation(spearRight);
            attack.AddAnimation(spearUp);

            attack.AddAnimation(shieldWall);
        }

        private void initializePlayerAnimations()
        {
            idle = new Animation(Texture, new Point(8, 12), new Point(32, 32),
                new Point(0, 0), new Point(7, 0), new TimeSpan(1000000), true);
            attackDownAnim = new Animation(Texture, new Point(8, 12), new Point(32, 32),
                new Point(0, 1), new Point(7, 1), new TimeSpan(1000000), false);
            walkDown = new Animation(Texture, new Point(8, 12), new Point(32, 32),
                new Point(0, 2), new Point(7, 2), new TimeSpan(1000000), true);
            walkLeft = new Animation(Texture, new Point(8, 12), new Point(32, 32),
                new Point(0, 3), new Point(7, 3), new TimeSpan(1000000), true);
            walkRight = new Animation(Texture, new Point(8, 12), new Point(32, 32),
                new Point(0, 4), new Point(7, 4), new TimeSpan(1000000), true);
            walkUp = new Animation(Texture, new Point(8, 12), new Point(32, 32),
                new Point(0, 5), new Point(7, 5), new TimeSpan(1000000), true);

            facingDown = new Animation(Texture, new Point(8, 12), new Point(32, 32),
                new Point(0, 2), new Point(0, 2), new TimeSpan(1000000), true);
            facingLeft = new Animation(Texture, new Point(8, 12), new Point(32, 32),
                new Point(0, 3), new Point(0, 3), new TimeSpan(1000000), true);
            facingRight = new Animation(Texture, new Point(8, 12), new Point(32, 32),
                new Point(0, 4), new Point(0, 4), new TimeSpan(1000000), true);
            facingUp = new Animation(Texture, new Point(8, 12), new Point(32, 32),
                new Point(0, 5), new Point(0, 5), new TimeSpan(1000000), true);

            AddAnimation(idle);
            AddAnimation(attackDownAnim);
            AddAnimation(walkDown);
            AddAnimation(walkLeft);
            AddAnimation(walkRight);
            AddAnimation(walkUp);
        }

        public void Update(GameTime gameTime, Input input)
        {
            if (attack.IsPlaybackComplete() && IsAttacking)
            {
                IsAttacking = false;
                attack.ResetAnimation();
                attack.StopAnimation();
            }
            handleInput(input);
            UpdateAttackAnimation(gameTime);
            UpdateAnimation(gameTime);
        }

        private void UpdateAttackAnimation(GameTime gameTime)
        {
            if (IsAttacking)
            {
                if (Direction == "down")
                    attack.currentAnimation.Position = new Vector2(Position.X, Position.Y + Height());
                if (Direction == "right")
                    attack.currentAnimation.Position = new Vector2(Position.X + Width(), Position.Y);
                if (Direction == "left")
                    attack.currentAnimation.Position = new Vector2(Position.X - attack.Width(), Position.Y);
                if (Direction == "up")
                    attack.currentAnimation.Position = new Vector2(Position.X, Position.Y - attack.Height());
                attack.currentAnimation.Update(gameTime);
            }
        }

        private void handleInput(Input input)
        {
            if (input.isPressed(Keys.A))
            {
                IsAttacking = true;

                if (Direction == "down")
                {
                    attack.PlayAnimation(normalDown);
                    PlayAnimation(facingDown);
                }
                else if (Direction == "left")
                {
                    attack.PlayAnimation(normalLeft);
                    PlayAnimation(facingLeft);
                }
                else if (Direction == "right")
                {
                    attack.PlayAnimation(normalRight);
                    PlayAnimation(facingRight);
                }
                else if (Direction == "up")
                {
                    attack.PlayAnimation(normalUp);
                    PlayAnimation(facingUp);
                }
            }
            if (!IsAttacking)
                if (input.isPressed(Keys.S))
                {
                    IsAttacking = true;

                    if (Direction == "down")
                    {
                        attack.PlayAnimation(smashDown);
                        PlayAnimation(facingDown);
                    }
                    else if (Direction == "left")
                    {
                        attack.PlayAnimation(smashLeft);
                        PlayAnimation(facingLeft);
                    }
                    else if (Direction == "right")
                    {
                        attack.PlayAnimation(smashRight);
                        PlayAnimation(facingRight);
                    }
                    else if (Direction == "up")
                    {
                        attack.PlayAnimation(smashUp);
                        PlayAnimation(facingUp);
                    }
                }
            if (IsAttacking) return;

            if (input.CurrentKeyboardState.Equals(new KeyboardState()))
            {
                if (Direction == "down")
                    PlayAnimation(idle);
                else if (Direction == "left")
                    PlayAnimation(facingLeft);
                else if (Direction == "right")
                    PlayAnimation(facingRight);
                else if (Direction == "up")
                    PlayAnimation(facingUp);
            }
            if (input.isPressed(Keys.Left))
            {
                Direction = "left";
                Position.X -= Velocity.X;
                PlayAnimation(walkLeft);
            }
            if (input.isPressed(Keys.Right))
            {
                Direction = "right";
                Position.X += Velocity.X;
                PlayAnimation(walkRight);
            }
            if (input.isPressed(Keys.Up))
            {
                Direction = "up";
                Position.Y -= Velocity.Y;
                if (!input.isPressed(Keys.Left) && !input.isPressed(Keys.Right))
                    PlayAnimation(walkUp);
            }
            if (input.isPressed(Keys.Down))
            {
                Direction = "down";
                Position.Y += Velocity.Y;
                if (!input.isPressed(Keys.Left) && !input.isPressed(Keys.Right))
                    PlayAnimation(walkDown);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            currentAnimation.Draw(spriteBatch);
            if (attack.currentAnimation != null)
                attack.currentAnimation.Draw(spriteBatch);
        }
    }
}
