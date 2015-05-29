#region Using Statements
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

#endregion

namespace PlatformerMac
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Platformer : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		Player player1;
		Enemy enemy1;
		Tree tree1;
		Controls controls;
		public static List<Enemy> Enemies = new List<Enemy>();
		public Platformer()
		{
			graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferWidth = 600;  // set this value to the desired width of your window
			graphics.PreferredBackBufferHeight = 600;   // set this value to the desired height of your window
			graphics.ApplyChanges();
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			player1 = new Player(50, 50, 50, 50);
			enemy1 = new Enemy(100, 100, 50, 50);
			Enemies.Add (enemy1);
			tree1 = new Tree (250, 250, 50, 50);
			base.Initialize();
			Console.WriteLine ("Init");


			controls = new Controls();

		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			player1.LoadContent(this.Content);
			enemy1.LoadContent (this.Content);
			tree1.LoadContent (this.Content);
			// TODO: use this.Content to load your game content here
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			//set our keyboardstate tracker update can change the gamestate on every cycle
			controls.Update();


			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			// TODO: Add your update logic here
			//Up, down, left, right affect the coordinates of the sprite

			Console.WriteLine ();

			player1.Update(controls, gameTime);
			enemy1.Update (controls, gameTime, player1.getX(), player1.getY());
			player1.Attack (controls, Enemies);

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.DarkRed);

			// TODO: Add your drawing code here
			spriteBatch.Begin();
			player1.Draw(spriteBatch);
			for (int i = 0; i < Enemies.Count; i++) {
				Enemies [i].Draw (spriteBatch);
			}
			tree1.Draw (spriteBatch);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}

}

