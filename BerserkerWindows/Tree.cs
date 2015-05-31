using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Berserker
{
    public class Tree : GameObject
    {
        public void Initialize(int x, int y, int width, int height)
        {
            Position.X = x;
            Position.Y = y;
            Dimensions.X = width;
            Dimensions.Y = height;
        }
        public int getX()
        {
            return (int)Position.X;
        }
        public int getY()
        {
            return (int)Position.Y;
        }
        public void setX(int x)
        {
            Position.X = x;
        }
        public void setY(int y)
        {
            Position.Y = y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Draw(spriteBatch);
        }

    }

}
