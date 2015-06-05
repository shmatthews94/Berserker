#region Using Statements
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using System.Collections.Specialized;
#endregion

namespace Berserker
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		Player player1;
		Controls controls;
		int spawncounter;
		int objectcounter;
		public static List<Enemy> Enemies = new List<Enemy>();
		public static List<Tree> Trees = new List<Tree>();
		public static List<Object> Objects = new List<Object>();

		TimeSpan elapsedTime = TimeSpan.Zero;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferWidth = 600;  // set this value to the desired width of your window
			graphics.PreferredBackBufferHeight = 600;   // set this value to the desired height of your window
			graphics.ApplyChanges();
			Content.RootDirectory = "Content";
			this.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 40.0f);
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

			player1 = new Player(275, 275, 50, 50);
			Trees.Add (new Tree (150, 150, 50, 50, 1));
			Trees.Add (new Tree (100, 150, 50, 50, 1));
			Trees.Add (new Tree (200, 250, 50, 50, 1));
			Trees.Add (new Tree (200, 300, 50, 50, 1));
			Trees.Add (new Tree (350, 250, 50, 50, 1));
			Trees.Add (new Tree (350, 300, 50, 50, 1));
			Trees.Add (new Tree (400, 400, 50, 50, 1));
			Trees.Add (new Tree (450, 400, 50, 50, 1));

			Trees.Add(new Tree(0, 0, 50, 50, 1));
			Trees.Add(new Tree(0, 50, 50, 50, 1));
			Trees.Add(new Tree(0, 100, 50, 50, 1));
			Trees.Add(new Tree(0, 150, 50, 50, 1));
			Trees.Add(new Tree(0, 200, 50, 50, 1));
			Trees.Add(new Tree(0, 250, 50, 50, 1));
			Trees.Add(new Tree(0, 300, 50, 50, 1));
			Trees.Add(new Tree(0, 350, 50, 50, 1));
			Trees.Add(new Tree(0, 400, 50, 50, 1));
			Trees.Add(new Tree(0, 450, 50, 50, 1));
			Trees.Add(new Tree(0, 500, 50, 50, 1));
			Trees.Add(new Tree(0, 550, 50, 50, 1));
			Trees.Add(new Tree(0, 0, 50, 50, 1));
			Trees.Add(new Tree(50, 0, 50, 50, 1));
			Trees.Add(new Tree(100, 0, 50, 50, 1));
			Trees.Add(new Tree(150, 0, 50, 50, 1));
			Trees.Add(new Tree(200, 0, 50, 50, 1));
			Trees.Add(new Tree(250, 0, 50, 50, 1));
			Trees.Add(new Tree(300, 0, 50, 50, 1));
			Trees.Add(new Tree(350, 0, 50, 50, 1));
			Trees.Add(new Tree(400, 0, 50, 50, 1));
			Trees.Add(new Tree(450, 0, 50, 50, 1));
			Trees.Add(new Tree(500, 0, 50, 50, 1));
			Trees.Add(new Tree(550, 0, 50, 50, 1));
			Trees.Add(new Tree(550, 50, 50, 50, 1));
			Trees.Add(new Tree(550, 100, 50, 50, 1));
			Trees.Add(new Tree(550, 150, 50, 50, 1));
			Trees.Add(new Tree(550, 200, 50, 50, 1));
			Trees.Add(new Tree(550, 250, 50, 50, 1));
			Trees.Add(new Tree(550, 300, 50, 50, 1));
			Trees.Add(new Tree(550, 350, 50, 50, 1));
			Trees.Add(new Tree(550, 400, 50, 50, 1));
			Trees.Add(new Tree(550, 450, 50, 50, 1));
			Trees.Add(new Tree(550, 500, 50, 50, 1));
			Trees.Add(new Tree(550, 550, 50, 50, 1));
			Trees.Add(new Tree(0, 550, 50, 50, 1));
			Trees.Add(new Tree(50, 550, 50, 50, 1));
			Trees.Add(new Tree(100, 550, 50, 50, 1));
			Trees.Add(new Tree(150, 550, 50, 50, 1));
			Trees.Add(new Tree(200, 550, 50, 50, 1));
			Trees.Add(new Tree(250, 550, 50, 50, 1));
			Trees.Add(new Tree(300, 550, 50, 50, 1));
			Trees.Add(new Tree(350, 550, 50, 50, 1));
			Trees.Add(new Tree(400, 550, 50, 50, 1));
			Trees.Add(new Tree(450, 550, 50, 50, 1));
			Trees.Add(new Tree(500, 550, 50, 50, 1));
			Trees.Add(new Tree(550, 550, 50, 50, 1));
			Trees.Add(new Tree(50, 50, 50, 50, 2));
			Trees.Add(new Tree(500, 500, 50, 50, 2));
			Trees.Add(new Tree(50, 500, 50, 50, 2));
			Trees.Add(new Tree(500, 50, 50, 50, 2));
			base.Initialize();
			Console.WriteLine("Init");


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
			for (int i = 0; i < Trees.Count; i++)
			{
				Trees[i].LoadContent(this.Content);
			}

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

			Console.WriteLine();


			if (spawncounter == 150)
			{
				Enemy newenemy = new Enemy(110, 110, 50, 50);
				newenemy.LoadContent(this.Content);
				Enemies.Add (newenemy);
			}
			if (spawncounter == 300)
			{
				Enemy newenemy = new Enemy(490, 110, 50, 50);
				newenemy.LoadContent(this.Content);
				Enemies.Add (newenemy);
			}
			if (spawncounter == 450)
			{
				Enemy newenemy = new Enemy(110, 490, 50, 50);
				newenemy.LoadContent(this.Content);
				Enemies.Add (newenemy);

			}
			if (spawncounter == 600)
			{
				Enemy newenemy = new Enemy(490, 490, 50, 50);
				newenemy.LoadContent(this.Content);
				Enemies.Add (newenemy);
				spawncounter = 0;
			}
			if (objectcounter % 997 == 0)
			{
				Random rand = new Random();
				Object object1 = new Object((int)rand.Next(100, 450), (int)rand.Next(100, 450), 50, 50);
				object1.LoadContent(this.Content);
				Objects.Add(object1);
			}
			player1.Update(controls, gameTime, Trees, Objects);

			for (int i = 0; i < Enemies.Count; i++)
			{
				Enemies[i].Update(controls, gameTime, player1.getX(), player1.getY(), Trees);
			}
			player1.Attack(controls, Enemies);
			player1.SpearAttack(controls, Enemies);
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
			GraphicsDevice.Clear(Color.DarkGreen);



			// TODO: Add your drawing code here
			spriteBatch.Begin();
			player1.Draw(spriteBatch);

			for (int i = 0; i < Enemies.Count; i++)
			{
				Enemies[i].Draw(spriteBatch);
			}
			for (int i = 0; i < Trees.Count; i++)
			{
				Trees[i].Draw(spriteBatch);
			}
			for (int i = 0; i < Objects.Count; i++)
			{
				Objects[i].Draw(spriteBatch);
			}
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}

}

