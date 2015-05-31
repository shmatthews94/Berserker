using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Berserker
{
    public class AnimatingSprite : GameObject
    {
        public List<Animation> Animations = new List<Animation>();
        public Animation currentAnimation = null;

        public int Width()
        {
            return Dimensions.X;
        }

        public int Height()
        {
            return Dimensions.Y;
        }

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
                Dimensions = new Point(currentAnimation.Width, currentAnimation.Height);
                currentAnimation.IsActive = true;
                currentAnimation.Complete = false;
                currentAnimation.ElapsedTime = TimeSpan.Zero;
                currentAnimation.CurrentFrame.X = currentAnimation.StartFrame.X;
                currentAnimation.CurrentFrame.Y = currentAnimation.StartFrame.Y;
                currentAnimation.Position = Position;
            }
        }

        public void StopAnimation()
        {
            currentAnimation = null;
            Dimensions = new Point(0, 0);
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
            currentAnimation.Position = Position;
            currentAnimation.Update(gameTime);
        }

        public void DrawAnimation(SpriteBatch spriteBatch)
        {
            currentAnimation.Draw(spriteBatch);
        }
    }
}
