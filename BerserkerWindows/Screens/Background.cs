using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace Berserker
{
	public class Background : Sprite
	{
		public int type;
		public Background (int x, int y, int width, int height, int t)
		{
			this.spriteX = x;
			this.spriteY = y;
			this.spriteWidth = width;
			this.spriteHeight = height;
			this.type = t;
		}
		public void LoadContent(Game game)
		{
			if (type == 1) {
				image = game.Content.Load<Texture2D> ("background.png");
			}
			if (type == 2) {
				image = game.Content.Load<Texture2D> ("ragebackground.png");
			}
		}

		public override void Draw(SpriteBatch sb)
		{
			sb.Draw(image, new Rectangle(spriteX, spriteY, spriteWidth, spriteHeight), Color.White);
		}
			
	}
}

