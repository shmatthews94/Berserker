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
        //  Animation fields
        Texture2D animationTexture;
        Point sheetSize;
        TimeSpan frameInterval;
        TimeSpan elapsedTime;

        public Point CurrentFrame;
        public Point FrameSize;
        public Point StartFrame;
        public Point EndFrame;
        //  Constructor
        public Animation(Texture2D animSheet, Point frameSize, Point animSheetSize, TimeSpan interval, Point startIndex, Point endIndex)
        {
            animationTexture = animSheet;
            FrameSize = frameSize;
            sheetSize = animSheetSize;
            frameInterval = interval;

            StartFrame = startIndex;
            EndFrame = endIndex;

            CurrentFrame = StartFrame;
        }

        public void Update(GameTime gameTime)
        {
            //  Checks to see if enough time passes to move on to the next frame
            //  Moves to the next row if it reaches the end of the row
            //  Loops back to the start of the animation if the animation ends
            if (elapsedTime >= frameInterval)
            {
                elapsedTime = TimeSpan.Zero;
                CurrentFrame.X++;
                if (CurrentFrame.X >= sheetSize.X || (CurrentFrame.X >= EndFrame.X && CurrentFrame.Y == EndFrame.Y))
                {
                    CurrentFrame.X = StartFrame.X;
                    CurrentFrame.Y++;
                }
                if (CurrentFrame.Y >= sheetSize.Y || CurrentFrame.Y >= EndFrame.Y)
                {
                    CurrentFrame.Y = StartFrame.Y;
                }
            }
            elapsedTime += gameTime.ElapsedGameTime;
        }

        //  Draws the current frame where the parameter position indicates its location on the sceen
        //  May want to 
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(animationTexture, position, new Rectangle(
                FrameSize.X * CurrentFrame.X,
                FrameSize.Y * CurrentFrame.Y,
                FrameSize.X,
                FrameSize.Y),
                Color.White);
            spriteBatch.End();
        }
    }
}
