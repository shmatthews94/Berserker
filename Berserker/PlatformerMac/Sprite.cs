using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Audio;
namespace PlatformerMac
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
		public void setX(int x)
		{
			spriteX = x;
		}
		public void setY(int y)
		{
			spriteY = y;
		}
	}
}

