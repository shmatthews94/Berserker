using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Berserker
{
    class Enemy : AnimatingSprite
    {
        public int HP;
        public Vector2 Velocity;

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

        public void Initialize(Vector2 pos, Texture2D tex, float speed)
        {
            Position = pos;
            Texture = tex;

            HP = 5;
            Velocity = new Vector2(speed, speed);

            initializeMovementAnimations();
            PlayAnimation(idle);
        }

        private void initializeMovementAnimations()
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

        public void Update(GameTime gameTime)
        {
            UpdateAnimation(gameTime);
        }

        public void Move()
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            currentAnimation.Draw(spriteBatch);
        }
    }
}
