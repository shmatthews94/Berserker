using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Berserker
{
    public class Animation
    {
        //  fields
        Texture2D animationTexture;
        Point sheetSize;
        Point frameSize;
        public Point CurrentFrame;
        public Point StartFrame;
        public Point EndFrame;

        //  Minimum time required between each frame of the animation
        TimeSpan frameInterval;
        //  Time since the currentFrame has been updated
        public TimeSpan ElapsedTime;
        //  Holds true if the animation loops indefinitely
        public bool IsLooping;
        //  Holds false if the animation has terminated
        public bool IsActive;
        public bool Complete;

        public Vector2 Position;

        //  Constructor
        public Animation(Texture2D animationTexture, Point sheetSize, Point frameSize, Point startFrame,
            Point endFrame, TimeSpan frameInterval, bool isLooping)
        {
            this.animationTexture = animationTexture;
            this.sheetSize = sheetSize;
            this.frameSize = frameSize;            
            this.frameInterval = frameInterval;

            IsLooping = isLooping;

            StartFrame.X = startFrame.X;
            StartFrame.Y = startFrame.Y;
            EndFrame.X = endFrame.X;
            EndFrame.Y = endFrame.Y;
            CurrentFrame.X = StartFrame.X;
            CurrentFrame.Y = StartFrame.Y;

            ElapsedTime = TimeSpan.Zero;
            IsActive = true;
            Complete = false;
        }

        public void Update(GameTime gameTime)
        {
            if (!IsActive) return;

            //  Checks to see if enough time passes to move on to the next frame
            //  Moves to the next row if it reaches the end of the row
            //  Loops back to the start of the animation if it is a loop and
            //  the animation has reached the last frame
            if (ElapsedTime >= frameInterval)
            {
                ElapsedTime = TimeSpan.Zero;
                CurrentFrame.X++;
                if (CurrentFrame.X >= sheetSize.X ||
                    (CurrentFrame.X > EndFrame.X && CurrentFrame.Y == EndFrame.Y))
                {
                    CurrentFrame.X = StartFrame.X;
                    CurrentFrame.Y++;
                }
                if (CurrentFrame.Y >= sheetSize.Y || CurrentFrame.Y > EndFrame.Y)
                {
                    if (IsLooping)
                        CurrentFrame.Y = StartFrame.Y;
                    else
                    {
                        CurrentFrame.X = EndFrame.X;
                        CurrentFrame.Y = EndFrame.Y;
                        IsActive = false;
                        Complete = true;
                    }
                }
            }
            ElapsedTime += gameTime.ElapsedGameTime;
        }

        //  Draws the current frame where the parameter position indicates its location on the sceen
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(animationTexture, Position, new Rectangle(
                frameSize.X * CurrentFrame.X, frameSize.Y * CurrentFrame.Y,
                frameSize.X, frameSize.Y),
                Color.White);
        }
    }
}
