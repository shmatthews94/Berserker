#region File Description
//-----------------------------------------------------------------------------
// CatapultGame.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using Berserker;
using GameStateManagement;
#endregion

namespace Berserker
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class BerserkerGame : Game
	{
		#region Fields
		GraphicsDeviceManager graphics;
		ScreenManager screenManager;
		public const int screenwidth = 800;
		public const int screenheight = 800;
		#endregion

		#region Initialization Methods
		public BerserkerGame()
		{
			this.Window.AllowUserResizing = true;
			this.Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);
			graphics = new GraphicsDeviceManager(this);
			//graphics.SynchronizeWithVerticalRetrace = false;
			graphics.PreferredBackBufferWidth = screenwidth;  // set this value to the desired width of your window
			graphics.PreferredBackBufferHeight = screenheight;
			Content.RootDirectory = "Content";

			// Frame rate is 30 fps by default for Windows Phone.
			TargetElapsedTime = TimeSpan.FromTicks(333333);

			//Create a new instance of the Screen Manager
			screenManager = new ScreenManager(this);
			Components.Add(screenManager);
			IsMouseVisible = true;
			#if !WINDOWS && !XBOX && !MONOMAC
			//Switch to full screen for best game experience
				// graphics.IsFullScreen = true;
			#endif

			//Add two new screens
			// screenManager.AddScreen(new BackgroundScreen(), null);
			screenManager.AddScreen(new MainMenuScreen(), null);

			AudioManager.Initialize(this);
		}

		void Window_ClientSizeChanged(object sender, EventArgs e)
		{
			// Make changes to handle the new window size.            
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			base.Initialize();
		}
		#endregion

		#region Loading
		protected override void LoadContent()
		{
			AudioManager.LoadSounds();
			base.LoadContent();
		}
		#endregion
	}
}
