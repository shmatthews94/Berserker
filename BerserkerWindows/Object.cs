using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

using Berserker;
using GameStateManagement;

namespace Berserker
{
	public class Object : Sprite
	{
		public Object (int x, int y, int width, int height)
		{
			this.spriteX = x;
			this.spriteY = y;
			this.spriteWidth = width;
			this.spriteHeight = height;
		}
		public void LoadContent(Game game)
		{
			image = game.Content.Load<Texture2D>("shroom.png");
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

