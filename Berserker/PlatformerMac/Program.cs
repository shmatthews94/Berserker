#region File Description
//-----------------------------------------------------------------------------
// Program.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;

#if IPHONE
using MonoTouch.Foundation;
using MonoTouch.UIKit;
#endif

#endregion

namespace Berserker
{
	static class Program
	{	
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main (string[] args)
		{
			MonoMac.AppKit.NSApplication.Init ();
			
			using (var p = new MonoMac.Foundation.NSAutoreleasePool ()) {
				MonoMac.AppKit.NSApplication.SharedApplication.Delegate = new AppDelegate();
				MonoMac.AppKit.NSApplication.Main(args);
			}
		}
	}

	class AppDelegate : MonoMac.AppKit.NSApplicationDelegate
	{
		BerserkerGame game;
		public override void FinishedLaunching (MonoMac.Foundation.NSObject notification)
		{
			BerserkerGame game = new BerserkerGame();
			game.Run ();

		}
		
		public override bool ApplicationShouldTerminateAfterLastWindowClosed (MonoMac.AppKit.NSApplication sender)
		{
			return true;
		}
	}
}

