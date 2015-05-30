using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Berserker
{
    class Animation
    {
        //  fields
        Texture2D animationTexture;
        Point sheetSize;
        Point frameSize;
        Point currentFrame;
        Point startFrame;
        Point endFrame;

        //  Minimum time required between each frame of the animation
        TimeSpan frameInterval;
        //  Time since the currentFrame has been updated
        TimeSpan elapsedTime;
        //  Holds true if the animation loops indefinitely
        bool isLooping;
        //  Holds false if the animation has terminated
        bool active;

        //  Constructor
        public Animation(Texture2D animationTexture, Point sheetSize, Point frameSize,
            Point startFrame, Point endFrame, TimeSpan frameInterval, bool isLooping)
        {
            this.animationTexture = animationTexture;
            this.sheetSize = sheetSize;
            this.frameSize = frameSize;
            this.startFrame = startFrame;
            this.endFrame = endFrame;
            this.frameInterval = frameInterval;
            this.isLooping = isLooping;

            currentFrame = startFrame;
            elapsedTime = TimeSpan.Zero;
            active = true;
        }

        public void Update(GameTime gameTime)
        {
            if (!active) return;

            //  Checks to see if enough time passes to move on to the next frame
            //  Moves to the next row if it reaches the end of the row
            //  Loops back to the start of the animation if it is a loop and
            //  the animation has reached the last frame
            if (elapsedTime >= frameInterval)
            {
                elapsedTime = TimeSpan.Zero;
                currentFrame.X++;
                if (currentFrame.X >= sheetSize.X ||
                    (currentFrame.X > endFrame.X && currentFrame.Y == endFrame.Y))
                {
                    currentFrame.X = startFrame.X;
                    currentFrame.Y++;
                }
                if (currentFrame.Y >= sheetSize.Y || currentFrame.Y > endFrame.Y)
                {
                    if (isLooping)
                        currentFrame.Y = startFrame.Y;
                    else
                    {
                        currentFrame.X = endFrame.X;
                        currentFrame.Y = endFrame.Y;
                        active = false;
                    }
                }
            }
            elapsedTime += gameTime.ElapsedGameTime;
        }

        //  Draws the current frame where the parameter position indicates its location on the sceen
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(animationTexture, position, new Rectangle(
                frameSize.X * currentFrame.X, frameSize.Y * currentFrame.Y,
                frameSize.X, frameSize.Y),
                Color.White);
        }
    }
}
