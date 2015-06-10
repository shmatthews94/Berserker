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
	class WaveScreen : GameScreen
	{
		#region Fields
		Texture2D background;
		SpriteFont font;
		bool isLoading;
		double timer;
		public int wave;
		Player player;
		Game1 gameplayScreen;
		System.Threading.Thread thread;
		#endregion

		#region Initialization
		public WaveScreen(int wave)
		{
			timer = 1000f; // 10 seconds
			this.wave = wave;
			EnabledGestures = GestureType.Tap;
			if (wave == 0) {
				AudioManager.PlaySound ("BePrepared");
			}
			TransitionOnTime = TimeSpan.FromSeconds (1);
			TransitionOffTime = TimeSpan.FromSeconds (1);
		}
		public WaveScreen(int wave, Player player)
		{
			timer = 1000f; // 10 seconds
			this.wave = wave;
			this.player = player;
			EnabledGestures = GestureType.Tap;
			AudioManager.PlaySound ("BePrepared");
			TransitionOnTime = TimeSpan.FromSeconds (1);
			TransitionOffTime = TimeSpan.FromSeconds (1);
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
					//ScreenManager.AddScreen (gameplayScreen, null);
				}
			}

			if (timer < 0.0f) {
				timer = 0;
				ExitScreen ();
				ScreenManager.RemoveScreen (this);
				if (wave == 0) {
					ScreenManager.AddScreen (new Game1 (wave), null);
				} else {
					ScreenManager.AddScreen (new Game1 (wave, player), null);
				}
				
			} else {
				timer -= gameTime.ElapsedGameTime.Milliseconds;
			}
			base.Update (gameTime, otherScreenHasFocus, coveredByOtherScreen);
		}

		#region Handle input
		public override void HandleInput (InputState input)
		{
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
			ScreenManager.GraphicsDevice.Clear (Color.Black);
			SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

			spriteBatch.Begin ();

			// Draw Background

			ScreenManager.SpriteBatch.DrawString (font, "WAVE " + (this.wave+1), new Vector2 (200, 250), Color.Red);
			ScreenManager.SpriteBatch.Draw(background, new Rectangle (0, 0, 600, 600), Color.Black*(TransitionAlpha));
			Console.WriteLine (TransitionAlpha.ToString());
			// If loading gameplay screen resource in the 
			// background show "Loading..." text

			spriteBatch.End ();
		}
		#endregion
	}
}
