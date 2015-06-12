#region File Description
//-----------------------------------------------------------------------------
// BackgroundScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GameStateManagement;
using Microsoft.Xna.Framework.Input.Touch;

#if MONOMAC
using MonoMac.AppKit;
using MonoMac.Foundation;
#endif

#if IPHONE
using MonoTouch.UIKit;
using MonoTouch.Foundation;
#endif
#endregion

namespace Berserker
{
	class EndScreen : GameScreen
	{
		#region Fields
		Texture2D background;
		SpriteFont font;
		int playerscore;
		bool isLoading;
		Game1 gameplayScreen;
		System.Threading.Thread thread;
		#endregion

		#region Initialization
		public EndScreen (int score)
		{
			EnabledGestures = GestureType.Tap;

			TransitionOnTime = TimeSpan.FromSeconds (0);
			TransitionOffTime = TimeSpan.FromSeconds (0.5);
			this.playerscore = score;
			AudioManager.PlaySound ("Valhalla");
		}
		#endregion

		#region Loading
		public override void LoadContent ()
		{
			background = Load<Texture2D> ("Textures/Backgrounds/EndScreen");
			font = Load<SpriteFont> ("Fonts/MenuFont");
		}
		#endregion

		public override void Update (GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			// If additional thread is running, skip
			if (null != thread) {
				// If additional thread finished loading and the screen is not exiting
				if (thread.ThreadState == System.Threading.ThreadState.Stopped && !IsExiting) {
					isLoading = false;

					// Exit the screen and show the gameplay screen 
					// with pre-loaded assets
					ExitScreen ();
					ScreenManager.AddScreen (gameplayScreen, null);
				}
			}
			base.Update (gameTime, otherScreenHasFocus, coveredByOtherScreen);
		}

		#region Handle input
		public override void HandleInput (InputState input)
		{
			if (isLoading == true) {
				#if ANDROID || IPHONE
				// Exit the screen and show the gameplay screen 
				// with pre-loaded assets
				ExitScreen ();
				ScreenManager.AddScreen (gameplayScreen, null);
				#endif				
				base.HandleInput (input);
				return;
			}
			PlayerIndex player;
			if (input.IsNewKeyPress (Microsoft.Xna.Framework.Input.Keys.Space, ControllingPlayer, out player) ||
				input.IsNewKeyPress (Microsoft.Xna.Framework.Input.Keys.Enter, ControllingPlayer, out player) ||
				input.MouseGesture.HasFlag(MouseGestureType.LeftClick)||
				input.IsNewButtonPress (Microsoft.Xna.Framework.Input.Buttons.Start, ControllingPlayer, out player)) {
				// Create a new instance of the gameplay screen
				ScreenManager.AddScreen(new Game1(0), null);
				// Start loading the resources in additional thread
				#if MONOMAC
				// create a new thread using BackgroundWorkerThread as method to execute
				thread = new System.Threading.Thread (new System.Threading.ThreadStart (gameplayScreen.LoadAssets));
				#endif
				isLoading = true;
				// start it
				//thread.Start ();

			}

			foreach (var gesture in input.Gestures) {
				if (gesture.GestureType == GestureType.Tap) {
					// Create a new instance of the gameplay screen
					gameplayScreen.ScreenManager = ScreenManager;

					#if ANDROID || IPHONE	
					isLoading = true;									
					#else				
					// Start loading the resources in additional thread
					thread = new System.Threading.Thread (new System.Threading.ThreadStart (gameplayScreen.LoadAssets));
					isLoading = true;
					thread.Start ();	
					#endif										

				}
			}

			base.HandleInput (input);
		}

		void LoadAssetsWorkerThread ()
		{

			#if MONOMAC || IPHONE			
			// Create an Autorelease Pool or we will leak objects.
			using (var pool = new NSAutoreleasePool()) {
			#else				

			#endif				
			// Make sure we invoke this on the Main Thread or OpenGL will throw an error
			#if MONOMAC
			MonoMac.AppKit.NSApplication.SharedApplication.BeginInvokeOnMainThread (delegate {
			#endif
			#if IPHONE
			var invokeOnMainThredObj = new NSObject();
			invokeOnMainThredObj.InvokeOnMainThread(delegate {
			#endif
			gameplayScreen.LoadAssets ();
			#if MONOMAC || IPHONE						
			});

			}				
			#endif				

		}
		#endregion

		#region Render
		public override void Draw (GameTime gameTime)
		{
			SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

			spriteBatch.Begin ();

			// Draw Background
			spriteBatch.Draw (background, new Rectangle(0, 0, 600, 600), new Color (255, 255, 255, TransitionAlpha));
			ScreenManager.SpriteBatch.DrawString (font, "YOUR SCORE: ", new Vector2 (160, 200), Color.Red);
			ScreenManager.SpriteBatch.DrawString (font, playerscore.ToString(), new Vector2 (260, 250), Color.Red);
			ScreenManager.SpriteBatch.DrawString (font, "PRESS SPACE TO DIE AGAIN", new Vector2 (50, 550), Color.Red);
			// If loading gameplay screen resource in the 
			// background show "Loading..." text

			spriteBatch.End ();
		}
		#endregion
	}
}
