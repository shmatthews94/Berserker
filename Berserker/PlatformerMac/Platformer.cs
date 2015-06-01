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
		Tree tree1, tree2, tree3;
		Controls controls;
		int spawncounter;
		int objectcounter;
		public static List<Enemy> Enemies = new List<Enemy>();
		public static List<Tree> Trees = new List<Tree>();
		public static List<Object> Objects = new List<Object>();
		public Platformer()
		{
			graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferWidth = 600;  // set this value to the desired width of your window
			graphics.PreferredBackBufferHeight = 600;   // set this value to the desired height of your window
			graphics.ApplyChanges();
			Content.RootDirectory = "Content";
			this.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 50.0f);
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
			tree1 = new Tree (250, 250, 50, 50);
			tree2 = new Tree (300, 250, 50, 50);
			tree3 = new Tree (250, 300, 50, 50);
			Trees.Add (tree1);
			Trees.Add (tree2);
			Trees.Add (tree3);
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
			tree1.LoadContent (this.Content);
			tree2.LoadContent (this.Content);
			tree3.LoadContent (this.Content);
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

			if (spawncounter == 150) {
				Enemy newenemy = new Enemy (100, 100, 50, 50);
				newenemy.LoadContent(this.Content);
				Enemies.Add(newenemy);
			}
			if (spawncounter == 300) {
				Enemy newenemy = new Enemy (500, 100, 50, 50);
				newenemy.LoadContent(this.Content);
				Enemies.Add(newenemy);
			}
			if (spawncounter == 450) {
				Enemy newenemy = new Enemy (100, 500, 50, 50);
				newenemy.LoadContent(this.Content);
				Enemies.Add(newenemy);
			}
			if (spawncounter == 600) {
				Enemy newenemy = new Enemy (500, 500, 50, 50);
				newenemy.LoadContent(this.Content);
				Enemies.Add(newenemy);
				spawncounter = 0;
			}
			if (objectcounter % 353 == 0) {
				Random rand = new Random ();
				Object object1 = new Object((int)rand.Next(0, 550), (int)rand.Next(0, 550), 50, 50);
				object1.LoadContent(this.Content);
				Objects.Add(object1);
			}
			player1.Update(controls, gameTime, Trees, Objects);
			for (int i = 0; i < Enemies.Count; i++) {
				Enemies[i].Update (controls, gameTime, player1.getX (), player1.getY (), Trees);
			}
			player1.Attack (controls, Enemies);
			spawncounter++;
			objectcounter++;
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
			for (int i = 0; i < Trees.Count; i++) {
				Trees [i].Draw (spriteBatch);
			}
			for (int i = 0; i < Objects.Count; i++) {
				Objects [i].Draw (spriteBatch);
			}
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}

}

