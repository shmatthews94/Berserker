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
					image = game.Content.Load<Texture2D> ("border tree type 1.png");
				}
				if (type == 2) {
					image = game.Content.Load<Texture2D> ("border tree type 2.png");
				}
				if (type == 3) {
					image = game.Content.Load<Texture2D> ("border tree type 3.png");
				}
				if (type == 4) {
					image = game.Content.Load<Texture2D> ("shrub.png");
				}
				if (type == 5){
					image = game.Content.Load<Texture2D> ("reverse border tree type 1.png");
				}
				if (type == 6){
					image = game.Content.Load<Texture2D> ("reverse border tree type 2.png");
				}
				if (type == 7){
					image = game.Content.Load<Texture2D>("reverse border tree type 3.png");
				}
				if(type == 8){
					image = game.Content.Load<Texture2D>("body.png");	
				}
				if(type == 9){
					image = game.Content.Load<Texture2D>("reversebody.png");
				}
				if(type == 10){
					image = game.Content.Load<Texture2D>("headonpike1.png");
				}
				if(type == 11){
					image = game.Content.Load<Texture2D>("bodyonpike.png");
				}
				if(type == 12){
					image = game.Content.Load<Texture2D>("reversebodyonpike.png");
				}
				if(type == 13){
					image = game.Content.Load<Texture2D>("diagonalbodyonpike.png");
				}
				if(type == 14){
					image = game.Content.Load<Texture2D>("reversediagonalbodyonpike.png");
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

