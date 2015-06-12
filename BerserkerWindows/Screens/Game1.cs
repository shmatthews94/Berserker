﻿#region Using Statements
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Specialized;
using Berserker;
using GameStateManagement;
#endregion

namespace Berserker
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	class Game1 : GameScreen
	{
		Player player1;
		public Texture2D healthbar;
		int spawncounter;
		int objectcounter;
		public int wave;
		public int enemycount;
		public static List<Enemy> Enemies = new List<Enemy>();
		public static List<Enemy> Wave1 = new List<Enemy>();
		public static List<Enemy> Wave2 = new List<Enemy>();
		public static List<Enemy> Wave3 = new List<Enemy>();
		public static List<Enemy> Wave4 = new List<Enemy>();
		public static List<Enemy> SpawnEnemies = new List<Enemy>();
		public static List<Tree> Trees = new List<Tree>();
		public static List<Tree> Trees1 = new List<Tree>();
		public static List<Tree> Trees2 = new List<Tree>();
		public static List<Object> Objects = new List<Object>();
		public static List<BorderTree> BorderTrees = new List<BorderTree>();
		public static List<List<Enemy>> EnemyWaves = new List<List<Enemy>> ();
		public static List<List<Tree>> TreeWaves = new List<List<Tree>> ();
		Controls controls = new Controls();
		Tree Castle1, Castle2, Castle3, Castle4;
		Background RegBackground, RageBackground;
		SpriteFont font;

		public Game1(int wave)
		{
			this.wave = wave;
			player1 = new Player (275, 275, 50, 50);
			spawncounter = 0;
			enemycount = 0;
			AudioManager.PlaySound("Soundtrack");
			Enemies.Clear();
			/*
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 600;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 600;   // set this value to the desired height of your window
            graphics.ApplyChanges();

            this.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 40.0f);
            */
			// ScreenManager.Game.Content.RootDirectory = "Content";
		}

		public Game1(int wave, Player player)
		{
			this.wave = wave;
			this.player1 = player;
			player.setX (275);
			player.setY (275);
			spawncounter = 0;
			enemycount = 0;
			AudioManager.PlaySound("Soundtrack");
			Enemies.Clear();
			/*
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 600;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 600;   // set this value to the desired height of your window
            graphics.ApplyChanges();

            this.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 40.0f);
            */
			// ScreenManager.Game.Content.RootDirectory = "Content";
		}


		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		public void LoadAssets()
		{
			// TODO: Add your initialization logic here

			RegBackground = new Background(-100, -100, 800, 800, 1);
			RageBackground = new Background(-100, -100, 800, 800, 2);
			Trees.Add (new Tree (150, 150, 50, 50, 1));
			Trees.Add (new Tree (100, 150, 50, 50, 1));
			Trees.Add (new Tree (200, 250, 50, 50, 1));
			Trees.Add (new Tree (200, 300, 50, 50, 1));
			Trees.Add (new Tree (350, 250, 50, 50, 1));
			Trees.Add (new Tree (350, 300, 50, 50, 1));
			Trees.Add (new Tree (400, 400, 50, 50, 1));
			Trees.Add (new Tree (450, 400, 50, 50, 1));
			Castle1 = new Tree(50, 50, 50, 50, 2);
			Castle2 = new Tree(500, 500, 50, 50, 2);
			Castle3 = new Tree(50, 500, 50, 50, 2);
			Castle4 = new Tree(500, 50, 50, 50, 2);
			BorderTrees.Add(new BorderTree(0, 0, 74, 600, 1));
			BorderTrees.Add(new BorderTree(511, 0, 89, 600, 2));
			BorderTrees.Add(new BorderTree(74, 0, 437, 65, 3));
			BorderTrees.Add(new BorderTree(74, 519, 437, 81, 4));

			RegBackground.LoadContent(ScreenManager.Game);
			RageBackground.LoadContent(ScreenManager.Game);


			Wave1.Add (new Enemy (50, 50, 50, 50, 1, 100));
			Wave1.Add (new Enemy (500, 50, 50, 50, 1, 100)); 
			Wave1.Add (new Enemy (50, 500, 50, 50, 1, 100));
			Wave1.Add (new Enemy (500, 500, 50, 50, 1, 100));
			Wave2.Add (new Enemy (50, 50, 50, 50, 5, 100));
			Wave2.Add (new Enemy (500, 50, 50, 50, 5, 100)); 
			Wave2.Add (new Enemy (50, 500, 50, 50, 5, 100));
			Wave2.Add (new Enemy (500, 500, 50, 50, 5, 100));
			EnemyWaves.Add (Wave1);
			EnemyWaves.Add (Wave2);
			player1.LoadContent (ScreenManager.Game);

			for (int i = 0; i < Trees.Count; i++)
			{
				Trees[i].LoadContent(ScreenManager.Game);
			}
			Castle1.LoadContent (ScreenManager.Game);
			Castle2.LoadContent (ScreenManager.Game);
			Castle3.LoadContent (ScreenManager.Game);
			Castle4.LoadContent (ScreenManager.Game);
			for(int j = 0; j < BorderTrees.Count; j++)
			{
				BorderTrees[j].LoadContent(ScreenManager.Game);
			}
			healthbar = Load<Texture2D>("healthbar");
			Console.WriteLine("Init");

			font = ScreenManager.Game.Content.Load<SpriteFont>("Fonts/MenuFont");

			for (int i = 0; i < EnemyWaves [wave].Count; i++) {
				SpawnEnemies.Add (EnemyWaves [wave] [i]);
			}

		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		public override void LoadContent()
		{
			SpriteBatch spritebatch = ScreenManager.SpriteBatch;
			// Create a new SpriteBatch, which can be used to draw textures.
			//spriteBatch = new SpriteBatch(GraphicsDevice);

			base.LoadContent();
			LoadAssets();
			base.LoadContent();
			#if ANDROID || IPHONE            
			LoadAssets();
			#endif            
			// Start the game

			// TODO: use this.Content to load your game content here
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			//set our keyboardstate tracker update can change the gamestate on every cycle
			controls.Update();

			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				ExitScreen();

			// TODO: Add your update logic here
			//Up, down, left, right affect the coordinates of the sprite


			if (SpawnEnemies.Count > 0) {
				if (SpawnEnemies [enemycount].getSpawn () == spawncounter) {
					Enemy newenemy = SpawnEnemies [enemycount];
					newenemy.LoadContent (ScreenManager.Game);
					Enemies.Add (newenemy);
					spawncounter = 0;
					SpawnEnemies.Remove(SpawnEnemies[enemycount]);
				}
			}

			if (Enemies.Count == 0 && SpawnEnemies.Count == 0) {
				ExitScreen ();
				wave++;
				ScreenManager.AddScreen (new WaveScreen(wave, player1), null);
			}

			Console.WriteLine();
			/*
            if (spawncounter % 61 == 0) {
                spawncounter1 -= 1;
                spawncounter2 -= 2;
                spawncounter3 -= 3;
                spawncounter4 -= 4;
            }
            */

			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				ExitScreen();

			// TODO: Add your update logic here
			//Up, down, left, right affect the coordinates of the sprite

			Console.WriteLine();
			/*
            double speed0 = 1 + (objectcounter / 250.0f);
            double speed1 = Math.Ceiling (speed0);
            int speed2 = Convert.ToInt32 (speed1);
            Console.WriteLine (speed2.ToString());

            if (spawncounter == spawncounter1)
            {
                Enemy newenemy = new Enemy(50, 50, 50, 50, speed2);
                newenemy.LoadContent(ScreenManager.Game);
                Enemies.Add (newenemy);
            }
            if (spawncounter == spawncounter2)
            {
                Enemy newenemy = new Enemy(500, 50, 50, 50, speed2);
                newenemy.LoadContent(ScreenManager.Game);
                Enemies.Add (newenemy);
            }
            if (spawncounter == spawncounter3)
            {
                Enemy newenemy = new Enemy(50, 500, 50, 50, speed2);
                newenemy.LoadContent(ScreenManager.Game);
                Enemies.Add (newenemy);

            }
            if (spawncounter == spawncounter4) {
                Enemy newenemy = new Enemy (500, 500, 50, 50, speed2);
                newenemy.LoadContent (ScreenManager.Game);
                Enemies.Add (newenemy);
                spawncounter = 0;
            }
            */
			if (objectcounter % 997 == 0)
			{
				Random rand = new Random();
				Object object1 = new Object((int)rand.Next(100, 450), (int)rand.Next(100, 450), 50, 50);
				object1.LoadContent(ScreenManager.Game);
				Objects.Add(object1);
			}
			player1.Update(controls, gameTime, Trees, Enemies, Objects);

			for (int i = 0; i < Enemies.Count; i++)
			{
				Enemies[i].Update(controls, gameTime, player1.getX(), player1.getY(), player1, Enemies, Trees);

			}
			//player1.Attack(controls, Trees, Enemies);
			//player1.SpearAttack(controls, Enemies);
			spawncounter++;
			objectcounter++;
			base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
		}


		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>


		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>


		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>

		public override void HandleInput (InputState input)
		{
			if (player1.getHealth () == 0) {
				ScreenManager.RemoveScreen (this);
				ScreenManager.AddScreen (new EndScreen (player1.getScore()), null);
			}
			base.HandleInput (input);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Draw(GameTime gameTime)
		{
			ScreenManager.SpriteBatch.Begin();
			if (player1.rageMode == false)
			{
				RegBackground.Draw(ScreenManager.SpriteBatch);

			}
			else if (player1.rageMode)
			{
				RageBackground.Draw(ScreenManager.SpriteBatch);
			}

			// TODO: Add your drawing code here


			player1.Draw (ScreenManager.SpriteBatch);

			for (int i = 0; i < Enemies.Count; i++)
			{
				Enemies[i].Draw(ScreenManager.SpriteBatch);
			}
			for (int i = 0; i < Trees.Count; i++)
			{
				Trees[i].Draw(ScreenManager.SpriteBatch);
			}
			for (int i = 0; i < BorderTrees.Count; i++)
			{
				BorderTrees[i].Draw(ScreenManager.SpriteBatch);
			}
			for (int i = 0; i < Objects.Count; i++)
			{
				Objects[i].Draw(ScreenManager.SpriteBatch);
			}
			Castle1.Draw (ScreenManager.SpriteBatch);
			Castle2.Draw (ScreenManager.SpriteBatch);
			Castle3.Draw (ScreenManager.SpriteBatch);
			Castle4.Draw (ScreenManager.SpriteBatch);
			ScreenManager.SpriteBatch.Draw (healthbar, new Rectangle(200, 0, 260, 50), Color.Black);
			ScreenManager.SpriteBatch.Draw(player1.rageBar, new Rectangle(200, 0, player1.getRage(), 50), Color.White);

			ScreenManager.SpriteBatch.Draw (healthbar, new Rectangle (0, 0, 150, 50), Color.Red);
			ScreenManager.SpriteBatch.Draw (healthbar, new Rectangle (0, 0, player1.getHealth()*30, 50), Color.DarkGreen);
			ScreenManager.SpriteBatch.DrawString (font, player1.getScore().ToString(), new Vector2 (500, 0), Color.Red);
			ScreenManager.SpriteBatch.End ();

			base.Draw(gameTime);
		}
	}
}