#region Using Statements
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using System.Collections.Specialized;
using Berserker;
using GameStateManagement;
#endregion

namespace Berserker
{
<<<<<<< HEAD:Berserker/PlatformerMac/Screens/Game1.cs
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	class Game1 : GameScreen
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		Player player1;
		public Texture2D healthbar;
		int spawncounter;
		int objectcounter;
		public static List<Enemy> Enemies = new List<Enemy>();
		public static List<Tree> Trees = new List<Tree>();
		public static List<Object> Objects = new List<Object>();
		Texture2D tree;
		Texture2D viking;
		Texture2D enemy;
		Controls controls = new Controls();
		Tree Castle1, Castle2, Castle3, Castle4;
		TimeSpan elapsedTime = TimeSpan.Zero;
		SpriteFont font;

		public Game1()
		{
			player1 = new Player (275, 275, 50, 50);
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
			Castle1 = new Tree(50, 50, 50, 50, 2);
			Castle2 = new Tree(500, 500, 50, 50, 2);
			Castle3 = new Tree(50, 500, 50, 50, 2);
			Castle4 = new Tree(500, 50, 50, 50, 2);
			player1.LoadContent (ScreenManager.Game);

			for (int i = 0; i < Trees.Count; i++)
			{
				Trees[i].LoadContent(ScreenManager.Game);
			}
			Castle1.LoadContent (ScreenManager.Game);
			Castle2.LoadContent (ScreenManager.Game);
			Castle3.LoadContent (ScreenManager.Game);
			Castle4.LoadContent (ScreenManager.Game);
			healthbar = Load<Texture2D>("healthbar");
			Console.WriteLine("Init");

			font = ScreenManager.Game.Content.Load<SpriteFont>("Fonts/MenuFont");



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

			Console.WriteLine();


			if (spawncounter == 120)
			{
				Enemy newenemy = new Enemy(50, 50, 50, 50);
				newenemy.LoadContent(ScreenManager.Game);
				Enemies.Add (newenemy);
			}
			if (spawncounter == 240)
			{
				Enemy newenemy = new Enemy(500, 50, 50, 50);
				newenemy.LoadContent(ScreenManager.Game);
				Enemies.Add (newenemy);
			}
			if (spawncounter == 360)
			{
				Enemy newenemy = new Enemy(50, 500, 50, 50);
				newenemy.LoadContent(ScreenManager.Game);
				Enemies.Add (newenemy);

			}
			if (spawncounter == 480)
			{
				Enemy newenemy = new Enemy(500, 500, 50, 50);
				newenemy.LoadContent(ScreenManager.Game);
				Enemies.Add (newenemy);
				spawncounter = 0;
=======

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player1;
        Controls controls;
        public Texture2D healthbar;
        int spawncounter;
        int objectcounter;
        public static List<Enemy> Enemies = new List<Enemy>();
        public static List<Tree> Trees = new List<Tree>();
		public static List<BorderTree> BorderTrees = new List<BorderTree>();
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
            Trees.Add(new Tree(150, 150, 50, 50, 1));
            Trees.Add(new Tree(100, 150, 50, 50, 1));
            Trees.Add(new Tree(200, 250, 50, 50, 1));
            Trees.Add(new Tree(200, 300, 50, 50, 1));
            Trees.Add(new Tree(350, 250, 50, 50, 1));
            Trees.Add(new Tree(350, 300, 50, 50, 1));
            Trees.Add(new Tree(400, 400, 50, 50, 1));
            Trees.Add(new Tree(450, 400, 50, 50, 1));

      
            Trees.Add(new Tree(50, 50, 50, 50, 2));
            Trees.Add(new Tree(500, 500, 50, 50, 2));
            Trees.Add(new Tree(50, 500, 50, 50, 2));
            Trees.Add(new Tree(500, 50, 50, 50, 2));

			BorderTrees.Add(new BorderTree(0, 0, 74, 600, 1));
			BorderTrees.Add(new BorderTree(511, 0, 89, 600, 2));
			BorderTrees.Add(new BorderTree(74, 0, 437, 65, 3));
			BorderTrees.Add(new BorderTree(74, 519, 437, 81, 4));
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
			for(int j = 0; j < BorderTrees.Count; j++)
			{
				BorderTrees[j].LoadContent(this.Content);
>>>>>>> 4da44638673fba4111112a4cebe6eb6175f19396:Berserker/PlatformerMac/Game1.cs
			}
            healthbar = Content.Load<Texture2D>("healthbar");
            player1.rageBar = Content.Load<Texture2D>("rage");

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
                Enemy newenemy = new Enemy(100, 100, 50, 50);
                newenemy.LoadContent(this.Content);
                Enemies.Add(newenemy);
            }
            if (spawncounter == 300)
            {
                Enemy newenemy = new Enemy(500, 100, 50, 50);
                newenemy.LoadContent(this.Content);
                Enemies.Add(newenemy);
            }
            if (spawncounter == 450)
            {
                Enemy newenemy = new Enemy(100, 500, 50, 50);
                newenemy.LoadContent(this.Content);
                Enemies.Add(newenemy);

            }
            if (spawncounter == 600)
            {
                Enemy newenemy = new Enemy(500, 450, 50, 50);
                newenemy.LoadContent(this.Content);
                Enemies.Add(newenemy);
                spawncounter = 0;
            }
            if (objectcounter % 997 == 0)
            {
                Random rand = new Random();
                Object object1 = new Object((int)rand.Next(100, 450), (int)rand.Next(100, 450), 50, 50);
                object1.LoadContent(this.Content);
                Objects.Add(object1);
            }
            player1.Update(controls, gameTime, Trees, Enemies, Objects);

            for (int i = 0; i < Enemies.Count; i++)
            {
                Enemies[i].Update(controls, gameTime, player1.getX(), player1.getY(), player1, Enemies, Trees);
            }
            player1.Attack(controls, Trees, Enemies);
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
            if (player1.rageMode == false)
            {
				GraphicsDevice.Clear(new Color(227, 219, 219));
            }
            else if (player1.rageMode)
            {
                GraphicsDevice.Clear(Color.Red);
            }


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
			for (int i = 0; i < BorderTrees.Count; i++)
			{
<<<<<<< HEAD:Berserker/PlatformerMac/Screens/Game1.cs
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
			player1.Attack(controls, Trees, Enemies);
			player1.SpearAttack(controls, Enemies);
			spawncounter++;
			objectcounter++;
			base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
		}

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

			if (player1.rageMode == false)
			{
				ScreenManager.GraphicsDevice.Clear(Color.DarkGreen);
			}
			else if (player1.rageMode)
			{
				ScreenManager.GraphicsDevice.Clear(Color.Red);
			}

			// TODO: Add your drawing code here
			ScreenManager.SpriteBatch.Begin();

			player1.Draw (ScreenManager.SpriteBatch);

			for (int i = 0; i < Enemies.Count; i++)
			{
				Enemies[i].Draw(ScreenManager.SpriteBatch);
			}
			for (int i = 0; i < Trees.Count; i++)
			{
				Trees[i].Draw(ScreenManager.SpriteBatch);
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
=======
				BorderTrees[i].Draw(spriteBatch);
			}
            for (int i = 0; i < Objects.Count; i++)
            {
                Objects[i].Draw(spriteBatch);
            }

            spriteBatch.Draw(player1.rageBar, new Rectangle(200, 555, player1.rage, 50), Color.White);
            spriteBatch.Draw(healthbar, new Rectangle(0, 0, 150, 50), new Rectangle(0, 0, 500, 50), Color.DarkGreen);
            spriteBatch.Draw(healthbar, new Rectangle(0, 0, player1.getHealth() * 30, 50), new Rectangle(0, 0, 500, 50), Color.Red);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
>>>>>>> 4da44638673fba4111112a4cebe6eb6175f19396:Berserker/PlatformerMac/Game1.cs

}

