﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Audio;
namespace Berserker
{
	public class Sprite
	{
		protected int spriteX, spriteY;
		protected int spriteWidth, spriteHeight;
		protected Texture2D image;

		public Sprite ()
		{
		}
		public int getX()
		{
			return spriteX;
		}
		public int getY()
		{
			return spriteY;
		}
		public int getHeight()
		{
			return spriteHeight;
		}
		public int getWidth() 
		{
			return spriteWidth;
		}
		public void setX(int x)
		{
			spriteX = x;
		}
		public void setY(int y)
		{
			spriteY = y;
		}
		public void setHeight(int height)
		{
			spriteHeight = height;
		}
		public void setWidth(int width)
		{
			spriteWidth = width;
		}
        public Rectangle Hitbox
        {
            get
            {
                return new Rectangle(spriteX, spriteY, spriteWidth, spriteHeight);
            }
        }
        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(image, new Vector2(spriteX, spriteY), Color.White);
        }
	}
}

