using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;



namespace Berserker
{
	public class BorderTree : Sprite
	{
			public int type;
			public BorderTree (int x, int y, int width, int height, int t)
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
					image = game.Content.Load<Texture2D> ("border left1.png");
				}
				if (type == 2) {
					image = game.Content.Load<Texture2D> ("border right1.png");
				}
				if (type == 3) {
					image = game.Content.Load<Texture2D> ("border top1.png");
				}
				if (type == 4) {
					image = game.Content.Load<Texture2D> ("border bottom1.png");
				}


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

