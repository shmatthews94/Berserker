using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Berserker
{
    public class Animation
    {
        //  fields
        Texture2D animationTexture;
        Point sheetSize;
        public int CurrentFrame;
        public Point StartFramePos;
        public Vector2 Offset;

        public int TotalFrames;

        public int Width;
        public int Height;

        //  Minimum time required between each frame of the animation
        TimeSpan frameInterval;
        //  Time since the currentFrame has been updated
        public TimeSpan ElapsedTime;
        //  Holds true if the animation loops indefinitely
        public bool IsLooping;
        //  Holds false if the animation has terminated
        public bool IsActive;
        public bool Complete;

        public Point FrameSize;

        public Vector2 Position;

        public Rectangle srcRect
        {
            get
            {
                return new Rectangle(StartFramePos.X + CurrentFrame * FrameSize.X, StartFramePos.Y, FrameSize.X, FrameSize.Y);
            }
        }

        public Rectangle destRect
        {
            get
            {
                return new Rectangle((int)Position.X + (int)Offset.X, (int)Position.Y + (int)Offset.Y, Width, Height);
            }
        }

        //  Constructor
        public Animation(Texture2D animationTexture, Point frameSize, Point startFramePos, Vector2 offset,
            int numFrames, TimeSpan frameInterval, bool isLooping, int width, int height)
        {
            this.animationTexture = animationTexture;
            FrameSize = frameSize;
            this.frameInterval = frameInterval;

            FrameSize.X = frameSize.X;
            FrameSize.Y = frameSize.Y;

            Offset.X = offset.X;
            Offset.Y = offset.Y;

            IsLooping = isLooping;

            StartFramePos.X = startFramePos.X;
            StartFramePos.Y = startFramePos.Y;
            CurrentFrame = 0;

            ElapsedTime = TimeSpan.Zero;
            IsActive = true;
            Complete = false;

            Position = new Vector2();

            Width = width;
            Height = height;

            TotalFrames = numFrames;
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
                CurrentFrame++;
                if (CurrentFrame >= TotalFrames)
                {
                    if (IsLooping)
                        CurrentFrame = 0;
                    else
                    {
                        CurrentFrame = TotalFrames - 1;
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
            //spriteBatch.Draw(animationTexture, new Vector2((int)Position.X, (int)Position.Y), new Rectangle(
            //    Width * CurrentFrame.X, Height * CurrentFrame.Y,
            //    Width, Height),
            //    Color.White);
            spriteBatch.Draw(animationTexture, destRect, srcRect, Color.White);
        }
    }
}
