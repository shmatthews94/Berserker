﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Berserker
{
    public class GameObject
    {
        public Vector2 Position;
        public Point Dimensions;
        public Rectangle Hitbox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Dimensions.X, Dimensions.Y);
            }
        }
        public Texture2D Texture;

        public virtual void Move(Vector2 amount)
        {
            Position += amount;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Vector2((int)Position.X, (int)Position.Y), Hitbox, Color.White);
        }
    }
}
