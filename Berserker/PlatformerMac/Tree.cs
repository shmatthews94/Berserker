using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;


namespace PlatformerMac
{
	public class Tree : Sprite
	{
		public Tree (int x, int y, int width, int height)
		{
			this.spriteX = x;
			this.spriteY = y;
			this.spriteWidth = width;
			this.spriteHeight = height;
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
		public void LoadContent(ContentManager content)
		{
			image = content.Load<Texture2D>("tree.png");
		}

		public void Draw(SpriteBatch sb)
		{
			sb.Draw(image, new Rectangle(spriteX, spriteY, spriteWidth, spriteHeight), Color.White);
		}

		public void Update(Controls controls, GameTime gameTime)
		{
			
		}

	}
}

