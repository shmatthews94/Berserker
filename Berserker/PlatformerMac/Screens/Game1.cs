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
		public static List<Enemy> Wave5 = new List<Enemy>();
		public static List<Enemy> Wave6 = new List<Enemy>();
		public static List<Enemy> Wave7 = new List<Enemy>();
		public static List<Enemy> Wave8 = new List<Enemy>();
		public static List<Enemy> Wave9 = new List<Enemy>();
		public static List<Enemy> Wave10 = new List<Enemy>();

		public static List<Enemy> SpawnEnemies = new List<Enemy>();
		public static List<Tree> Trees = new List<Tree>();
		public static List<Tree> Trees1 = new List<Tree>();
		public static List<Tree> Trees2 = new List<Tree>();
		public static List<Tree> Trees3 = new List<Tree>();
		public static List<Tree> Trees4 = new List<Tree>();
		public static List<Tree> Trees9 = new List<Tree>();
		public static List<Tree> Trees10 = new List<Tree>();
		public static List<Object> Objects = new List<Object>();
		public static List<BorderTree> BorderTrees = new List<BorderTree>();
		public static List<BorderTree> Bodies = new List<BorderTree>();
		public static List<List<Enemy>> EnemyWaves = new List<List<Enemy>> ();
		public static List<List<Tree>> TreeWaves = new List<List<Tree>> ();
		Controls controls = new Controls();
		Tree Castle1, Castle2, Castle3, Castle4;
		Background RegBackground, RageBackground;
		SpriteFont font;

        Texture2D hud;

		public Game1(int wave)
		{
			this.wave = wave;
			player1 = new Player (350, 350, 50, 50);
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
			if (this.player1.rageMode == true) {
				this.player1.rageMode = false;
				this.player1.rage = 0;
			}

			player1.setX (350);
			player1.setY (350);
            player1.Reset();
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
            #region load trees and background
            RegBackground = new Background(0, 0, BerserkerGame.screenwidth, BerserkerGame.screenheight, 1);
			RageBackground = new Background(0, 0, BerserkerGame.screenwidth, BerserkerGame.screenheight, 2);

			Castle1 = new Tree(50, 50, 50, 50, 2);
			Castle2 = new Tree(600, 50, 50, 50, 2);
			Castle3 = new Tree(600, 600, 50, 50, 2);
			Castle4 = new Tree(50, 600, 50, 50, 2);
			if (BorderTrees.Count == 0) {
				BorderTrees.Add(new BorderTree(12, 18, 23, 61, 5));
				BorderTrees.Add(new BorderTree(27, 48, 61, 45, 4));
				BorderTrees.Add(new BorderTree(39, 74, 44, 97, 7));
				BorderTrees.Add(new BorderTree(9, 111, 23, 61, 5));
				BorderTrees.Add(new BorderTree(45, 147, 23, 61, 1));
				BorderTrees.Add(new BorderTree(-19, 205, 108, 103, 2));
				BorderTrees.Add(new BorderTree(47, 253, 27, 101, 5));
				BorderTrees.Add(new BorderTree(2, 327, 61, 45, 4));
				BorderTrees.Add(new BorderTree(45, 332, 23, 61, 5));
				BorderTrees.Add(new BorderTree(11, 330, 27, 101, 1));
				BorderTrees.Add(new BorderTree(6, 445, 71, 64, 6));
				BorderTrees.Add(new BorderTree(10, 475, 32, 92, 3));
				BorderTrees.Add(new BorderTree(-26, 572, 68, 57, 2));
				BorderTrees.Add(new BorderTree(42, 409, 27, 101, 5));
				BorderTrees.Add(new BorderTree(14, 638, 61, 45, 4));
				BorderTrees.Add(new BorderTree(2, 653, 23, 69, 2));
				BorderTrees.Add(new BorderTree(26, 707, 27, 101, 5));
				BorderTrees.Add(new BorderTree(70, 670, 32, 92, 2));
				BorderTrees.Add(new BorderTree(119, 691, 27, 101, 1));
				BorderTrees.Add(new BorderTree(110, 752, 61, 45, 4));
				BorderTrees.Add(new BorderTree(188, 681, 77, 75, 2));
				BorderTrees.Add(new BorderTree(194, 730, 23, 61, 1));
				BorderTrees.Add(new BorderTree(213, 748, 61, 45, 4));
				BorderTrees.Add(new BorderTree(265, 708, 23, 69, 3));
				BorderTrees.Add(new BorderTree(293, 710, 27, 101, 1));
				BorderTrees.Add(new BorderTree(345, 721, 23, 61, 1));
				BorderTrees.Add(new BorderTree(432, 695, 27, 101, 1));
				BorderTrees.Add(new BorderTree(364, 718, 108, 103, 2));
				BorderTrees.Add(new BorderTree(470, 684, 44, 97, 7));
				BorderTrees.Add(new BorderTree(497, 745, 61, 45, 4));
				BorderTrees.Add(new BorderTree(533, 699, 23, 61, 5));
				BorderTrees.Add(new BorderTree(567, 687, 77, 75, 6));
				BorderTrees.Add(new BorderTree(633, 706, 23, 69, 3));
				BorderTrees.Add(new BorderTree(641, 706, 27, 101, 5));
				BorderTrees.Add(new BorderTree(588, 754, 61, 45, 4));
				BorderTrees.Add(new BorderTree(716, -12, 108, 103, 6));
				BorderTrees.Add(new BorderTree(729, 28, 27, 101, 1));
				BorderTrees.Add(new BorderTree(765, 56, 32, 92, 7));
				BorderTrees.Add(new BorderTree(708, 141, 61, 45, 4));
				BorderTrees.Add(new BorderTree(762, 113, 27, 101, 1));
				BorderTrees.Add(new BorderTree(710, 219, 41, 88, 3));
				BorderTrees.Add(new BorderTree(765, 244, 27, 101, 5));
				BorderTrees.Add(new BorderTree(729, 302, 26, 61, 5));
				BorderTrees.Add(new BorderTree(760, 325, 26, 61, 5));
				BorderTrees.Add(new BorderTree(742, 381, 23, 69, 3));
				BorderTrees.Add(new BorderTree(745, 454, 61, 45, 4));
				BorderTrees.Add(new BorderTree(713, 507, 108, 103, 6));
				BorderTrees.Add(new BorderTree(717, 535, 27, 101, 1));
				BorderTrees.Add(new BorderTree(739, 738, 61, 45, 4));
				BorderTrees.Add(new BorderTree(766, 563, 41, 88, 3));
				BorderTrees.Add(new BorderTree(708, 629, 108, 103, 6));
				BorderTrees.Add(new BorderTree(714, 718, 23, 69, 3));
				BorderTrees.Add(new BorderTree(766, 683, 27, 101, 1));
				BorderTrees.Add(new BorderTree(735, 757, 61, 45, 4));
				BorderTrees.Add(new BorderTree(151, -31, 108, 103, 2));
				BorderTrees.Add(new BorderTree(162, 8, 23, 69, 3));
				BorderTrees.Add(new BorderTree(121, 33, 61, 45, 4));
				BorderTrees.Add(new BorderTree(220, 4, 41, 88, 3));
				BorderTrees.Add(new BorderTree(276, -42, 27, 101, 5));
				BorderTrees.Add(new BorderTree(298, 16, 23, 61, 5));
				BorderTrees.Add(new BorderTree(246, 34, 61, 45, 4));
				BorderTrees.Add(new BorderTree(334, 35, 23, 69, 7));
				BorderTrees.Add(new BorderTree(417, -7, 108, 103, 6));
				BorderTrees.Add(new BorderTree(406, 12, 23, 69, 3));
				BorderTrees.Add(new BorderTree(486, 41, 61, 45, 4));
				BorderTrees.Add(new BorderTree(561, 1, 41, 88, 7));
				BorderTrees.Add(new BorderTree(628, 18, 23, 61, 5));
				BorderTrees.Add(new BorderTree(657, 5, 27, 101, 5));
				BorderTrees.Add(new BorderTree(642, 48, 23, 61, 5));

				for (int i = 0; i < BorderTrees.Count; i++) {
					double newX = (BorderTrees [i].getX () * 7 / 8);
					double newY = (BorderTrees [i].getY () * 7 / 8);
					BorderTrees [i].setX (Convert.ToInt32(newX));
					BorderTrees [i].setY (Convert.ToInt32(newY));
				}
			}

			if(Bodies.Count == 0){
				Bodies.Add(new BorderTree(-31, -33, 126, 102, 12));
				Bodies.Add(new BorderTree(-5, 0, 25, 96, 10));
				Bodies.Add(new BorderTree(-12, 49, 122, 137, 14));
				Bodies.Add(new BorderTree(-16, 152, 133, 67, 9));
				Bodies.Add(new BorderTree(9, 204, 133, 66, 8));
				Bodies.Add(new BorderTree(-25, 246, 126, 102, 12));
				Bodies.Add(new BorderTree(3, 358, 126, 102, 12));
				Bodies.Add(new BorderTree(-17, 404, 122, 137, 13));
				Bodies.Add(new BorderTree(17, 462, 133, 66, 8));
				Bodies.Add(new BorderTree(1, 515, 25, 96, 10));
				Bodies.Add(new BorderTree(-14, 565, 122, 137, 12));
				Bodies.Add(new BorderTree(-29, 650, 126, 102, 9));
				Bodies.Add(new BorderTree(101, 643, 122, 137, 14));
				Bodies.Add(new BorderTree(28, 730, 133, 66, 9));
				Bodies.Add(new BorderTree(172, 683, 25, 115, 10));
				Bodies.Add(new BorderTree(197, 683, 126, 102, 8));
				Bodies.Add(new BorderTree(207, 735, 133, 66, 12));
				Bodies.Add(new BorderTree(316, 691, 122, 137, 14));
				Bodies.Add(new BorderTree(409, 758, 133, 66, 8));
				Bodies.Add(new BorderTree(435, 680, 126, 102, 11));
				Bodies.Add(new BorderTree(466, 709, 126, 102, 12));
				Bodies.Add(new BorderTree(571, 678, 122, 137, 13));
				Bodies.Add(new BorderTree(598, 744, 153, 137, 8));
				Bodies.Add(new BorderTree(674, -11, 126, 102, 11));
				Bodies.Add(new BorderTree(687, 42, 133, 66, 9));
				Bodies.Add(new BorderTree(708, 34, 25, 115, 10));
				Bodies.Add(new BorderTree(758, 25, 25, 115, 10));
				Bodies.Add(new BorderTree(732, 74, 25, 115, 10));
				Bodies.Add(new BorderTree(706, 133, 122, 137, 13));
				Bodies.Add(new BorderTree(664, 336, 126, 102, 14));
				Bodies.Add(new BorderTree(662, 193, 126, 102, 11));
				Bodies.Add(new BorderTree(673, 258, 133, 68, 9));
				Bodies.Add(new BorderTree(685, 321, 126, 102, 11));
				Bodies.Add(new BorderTree(773, 347, 25, 115, 10));
				Bodies.Add(new BorderTree(664, 336, 122, 137, 13));
				Bodies.Add(new BorderTree(702, 464, 133, 68, 9));
				Bodies.Add(new BorderTree(701, 517, 126, 102, 11));
				Bodies.Add(new BorderTree(726, 536, 25, 115, 10));
				Bodies.Add(new BorderTree(710, 587, 122, 137, 13));
				Bodies.Add(new BorderTree(670, 636, 122, 137, 14));
				Bodies.Add(new BorderTree(753, 698, 25, 115, 10));
				Bodies.Add(new BorderTree(56, 5, 126, 102, 8));
				Bodies.Add(new BorderTree(59, 57, 133, 66, 9));
				Bodies.Add(new BorderTree(176, -38, 122, 137, 13));
				Bodies.Add(new BorderTree(176, 28, 133, 66, 9));
				Bodies.Add(new BorderTree(272, -22, 126, 102, 11));
				Bodies.Add(new BorderTree(349, 17, 133, 66, 8));
				Bodies.Add(new BorderTree(424, -20, 122, 137, 14));
				Bodies.Add(new BorderTree(500, -2, 126, 102, 9));
				Bodies.Add(new BorderTree(555, -19, 122, 137, 13));

				for (int i = 0; i < Bodies.Count; i++) {
					double newX = Bodies [i].getX () * 7 / 8;
					double newY = Bodies [i].getY () * 7 / 8;
					Bodies [i].setX (Convert.ToInt32(newX));
					Bodies [i].setY (Convert.ToInt32(newY));
				}
			}

			RegBackground.LoadContent(ScreenManager.Game);
			RageBackground.LoadContent(ScreenManager.Game);
            #endregion

            if (Wave1.Count == 0) {
				if (Wave1.Count == 0) {
					Wave1.Add (new Enemy (50, 50, 50, 50, 1, 100));
					Wave1.Add (new Enemy (600, 50, 50, 50, 1, 100)); 
					Wave1.Add (new Enemy (50, 600, 50, 50, 1, 100));
					Wave1.Add (new Enemy (600, 600, 50, 50, 3, 100));

					Wave2.Add (new Enemy (50, 50, 50, 50, 3, 100));
					Wave2.Add (new Enemy (600, 50, 50, 50, 3, 100));
					Wave2.Add (new Enemy (50, 600, 50, 50, 3, 100));
					Wave2.Add (new Enemy (600, 600, 50, 50, 3, 100));
					Wave2.Add (new Enemy (600, 50, 50, 50, 3, 100));
					Wave2.Add (new Enemy (50, 600, 50, 50, 3, 5));
					Wave2.Add (new Enemy (600, 600, 50, 50, 3, 100));
					Wave2.Add (new Enemy (50, 50, 50, 50, 3, 5));

					Wave3.Add (new Enemy (50, 50, 50, 50, 4, 50));
					Wave3.Add (new Enemy (600, 50, 50, 50, 4, 5)); 
					Wave3.Add (new Enemy (50, 600, 50, 50, 4, 5));
					Wave3.Add (new Enemy (600, 600, 50, 50, 4, 5));
					Wave3.Add (new Enemy (600, 600, 50, 50, 5, 100));
					Wave3.Add (new Enemy (600, 50, 50, 50, 5, 50)); 
					Wave3.Add (new Enemy (50, 50, 50, 50, 5, 100));
					Wave3.Add (new Enemy (50, 600, 50, 50, 5, 50));
					Wave3.Add (new Enemy (50, 50, 50, 50, 5, 100));
					Wave3.Add (new Enemy (600, 600, 50, 50, 5, 5));

					Wave4.Add (new Enemy (50, 50, 50, 50, 6, 25));
					Wave4.Add (new Enemy (600, 50, 50, 50, 6, 25)); 
					Wave4.Add (new Enemy (50, 600, 50, 50, 6, 25));
					Wave4.Add (new Enemy (600, 600, 50, 50, 6, 25));
					Wave4.Add (new Enemy (600, 600, 50, 50, 7, 50));
					Wave4.Add (new Enemy (50, 600, 50, 50, 7, 5));
					Wave4.Add (new Enemy (50, 50, 50, 50, 7, 50));
					Wave4.Add (new Enemy (600, 60, 50, 50, 7, 5));
					Wave4.Add (new Enemy (50, 50, 50, 50, 8, 75));
					Wave4.Add (new Enemy (600, 50, 50, 50, 8, 75)); 
					Wave4.Add (new Enemy (50, 600, 50, 50, 8, 75));
					Wave4.Add (new Enemy (600, 600, 50, 50, 8, 75));

					Wave5.Add (new Enemy (50, 50, 50, 50, 9, 75));
					Wave5.Add (new Enemy (600, 50, 50, 50, 9, 75)); 
					Wave5.Add (new Enemy (50, 600, 50, 50, 9, 75));
					Wave5.Add (new Enemy (600, 600, 50, 50, 9, 75));

					Wave6.Add (new Enemy (50, 50, 50, 50, 10, 50));
					Wave6.Add (new Enemy (600, 50, 50, 50, 10, 50));
					Wave6.Add (new Enemy (50, 600, 50, 50, 10, 75));
					Wave6.Add (new Enemy (600, 600, 50, 50, 10, 75));
					Wave6.Add (new Enemy (600, 50, 50, 50, 10, 100));
					Wave6.Add (new Enemy (50, 600, 50, 50, 10, 5));
					Wave6.Add (new Enemy (600, 600, 50, 50, 11, 100));
					Wave6.Add (new Enemy (50, 50, 50, 50, 11, 5));

					Wave7.Add (new Enemy (50, 50, 50, 50, 11, 50));
					Wave7.Add (new Enemy (600, 50, 50, 50, 11, 5)); 
					Wave7.Add (new Enemy (50, 600, 50, 50, 11, 5));
					Wave7.Add (new Enemy (600, 600, 50, 50, 11, 5));
					Wave7.Add (new Enemy (600, 600, 50, 50, 11, 100));
					Wave7.Add (new Enemy (600, 50, 50, 50, 12, 50)); 
					Wave7.Add (new Enemy (50, 50, 50, 50, 12, 100));
					Wave7.Add (new Enemy (50, 600, 50, 50, 12, 50));
					Wave7.Add (new Enemy (50, 50, 50, 50, 12, 100));
					Wave7.Add (new Enemy (600, 600, 50, 50, 12, 5));

					Wave8.Add (new Enemy (50, 50, 50, 50, 13, 25));
					Wave8.Add (new Enemy (600, 50, 50, 50, 13, 25)); 
					Wave8.Add (new Enemy (50, 600, 50, 50, 13, 25));
					Wave8.Add (new Enemy (600, 600, 50, 50, 13, 25));
					Wave8.Add (new Enemy (600, 600, 50, 50, 13, 50));
					Wave8.Add (new Enemy (50, 600, 50, 50, 13, 5));
					Wave8.Add (new Enemy (50, 50, 50, 50, 13, 50));
					Wave8.Add (new Enemy (600, 60, 50, 50, 13, 5));
					Wave8.Add (new Enemy (50, 50, 50, 50, 14, 75));
					Wave8.Add (new Enemy (600, 50, 50, 50, 14, 75)); 
					Wave8.Add (new Enemy (50, 600, 50, 50, 14, 75));
					Wave8.Add (new Enemy (600, 600, 50, 50, 14, 75));

					Wave9.Add (new Enemy (50, 50, 50, 50, 1, 100));
					Wave9.Add (new Enemy (600, 50, 50, 50, 1, 100)); 
					Wave9.Add (new Enemy (50, 600, 50, 50, 1, 100));
					Wave9.Add (new Enemy (600, 600, 50, 50, 1, 100));
					Wave9.Add (new Enemy (50, 50, 50, 50, 5, 50));
					Wave9.Add (new Enemy (600, 50, 50, 50, 5, 50)); 
					Wave9.Add (new Enemy (50, 600, 50, 50, 5, 50));
					Wave9.Add (new Enemy (600, 600, 50, 50, 5, 50));
					Wave9.Add (new Enemy (50, 600, 50, 50, 10, 25));
					Wave9.Add (new Enemy (600, 600, 50, 50, 10, 25));
					Wave9.Add (new Enemy (50, 50, 50, 50, 10, 25));
					Wave9.Add (new Enemy (600, 50, 50, 50, 10, 25)); 

					int i = 0;
					while (i < 30) {
						Wave10.Add (new Enemy (50, 50, 50, 50, i++, 100-(3*i)));
						Wave10.Add (new Enemy (600, 50, 50, 50, i++, 100-(3*i))); 
						Wave10.Add (new Enemy (50, 600, 50, 50, i++, 100-(3*i)));
						Wave10.Add (new Enemy (600, 600, 50, 50, i++, 100-(3*i)));
					}

				}
			}
			EnemyWaves.Add (Wave1);
			EnemyWaves.Add (Wave2);
			EnemyWaves.Add (Wave3);
			EnemyWaves.Add (Wave4);
			EnemyWaves.Add (Wave5);
			EnemyWaves.Add (Wave6);
			EnemyWaves.Add (Wave7);
			EnemyWaves.Add (Wave8);
			EnemyWaves.Add (Wave9);
			EnemyWaves.Add (Wave10);
			player1.LoadContent (ScreenManager.Game);
			if (Trees1.Count == 0) {
				Trees1.Add (new Tree (150, 150, 50, 50, 1));
				Trees1.Add (new Tree (100, 150, 50, 50, 1));
				Trees1.Add (new Tree (200, 250, 50, 50, 1));
				Trees1.Add (new Tree (200, 300, 50, 50, 1));
				Trees1.Add (new Tree (450, 350, 50, 50, 1));
				Trees1.Add (new Tree (450, 400, 50, 50, 1));
				Trees1.Add (new Tree (500, 500, 50, 50, 1));
				Trees1.Add (new Tree (550, 500, 50, 50, 1));

				Trees2.Add (new Tree (150, 150, 50, 50, 1));
				Trees2.Add (new Tree (200, 150, 50, 50, 1));
				Trees2.Add (new Tree (350, 150, 50, 50, 1));
				Trees2.Add (new Tree (400, 150, 50, 50, 1));
				Trees2.Add (new Tree (250, 300, 50, 50, 1));
				Trees2.Add (new Tree (250, 350, 50, 50, 1));
				Trees2.Add (new Tree (400, 300, 50, 50, 1));
				Trees2.Add (new Tree (400, 350, 50, 50, 1));
				Trees2.Add (new Tree (250, 500, 50, 50, 1));
				Trees2.Add (new Tree (300, 500, 50, 50, 1));
				Trees2.Add (new Tree (450, 500, 50, 50, 1));
				Trees2.Add (new Tree (500, 500, 50, 50, 1));

				Trees3.Add (new Tree (150, 150, 50, 50, 1));
				Trees3.Add (new Tree (200, 200, 50, 50, 1));
				Trees3.Add (new Tree (250, 250, 50, 50, 1));
				Trees3.Add (new Tree (300, 300, 50, 50, 1));
				Trees3.Add (new Tree (200, 300, 50, 50, 1));
				Trees3.Add (new Tree (300, 200, 50, 50, 1));
				Trees3.Add (new Tree (550, 550, 50, 50, 1));
				Trees3.Add (new Tree (500, 500, 50, 50, 1));
				Trees3.Add (new Tree (450, 450, 50, 50, 1));
				Trees3.Add (new Tree (400, 400, 50, 50, 1));
				Trees3.Add (new Tree (400, 500, 50, 50, 1));
				Trees3.Add (new Tree (500, 400, 50, 50, 1));

				Trees4.Add (new Tree (150, 150, 50, 50, 1));
				Trees4.Add (new Tree (100, 200, 50, 50, 1));
				Trees4.Add (new Tree (150, 250, 50, 50, 1));
				Trees4.Add (new Tree (150, 350, 50, 50, 1));
				Trees4.Add (new Tree (150, 400, 50, 50, 1));
				Trees4.Add (new Tree (150, 450, 50, 50, 1));
				Trees4.Add (new Tree (200, 300, 50, 50, 1));
				Trees4.Add (new Tree (200, 500, 50, 50, 1));
				Trees4.Add (new Tree (250, 250, 50, 50, 1));
				Trees4.Add (new Tree (250, 400, 50, 50, 1));
				Trees4.Add (new Tree (250, 550, 50, 50, 1));
				Trees4.Add (new Tree (300, 250, 50, 50, 1));
				Trees4.Add (new Tree (350, 250, 50, 50, 1));
				Trees4.Add (new Tree (400, 250, 50, 50, 1));
				Trees4.Add (new Tree (400, 400, 50, 50, 1));
				Trees4.Add (new Tree (400, 550, 50, 50, 1));
				Trees4.Add (new Tree (450, 300, 50, 50, 1));
				Trees4.Add (new Tree (450, 500, 50, 50, 1));
				Trees4.Add (new Tree (500, 150, 50, 50, 1));
				Trees4.Add (new Tree (500, 250, 50, 50, 1));
				Trees4.Add (new Tree (500, 350, 50, 50, 1));
				Trees4.Add (new Tree (500, 400, 50, 50, 1));
				Trees4.Add (new Tree (500, 450, 50, 50, 1));
				Trees4.Add (new Tree (550, 200, 50, 50, 1));

				Trees9.Add (new Tree (300, 300, 50, 50, 1));

				Trees10.Add (new Tree (150, 150, 50, 50, 1));
				Trees10.Add (new Tree (200, 200, 50, 50, 1));
				Trees10.Add (new Tree (250, 250, 50, 50, 1));
				Trees10.Add (new Tree (500, 500, 50, 50, 1));
				Trees10.Add (new Tree (450, 450, 50, 50, 1));
				Trees10.Add (new Tree (400, 400, 50, 50, 1));

				Trees10.Add (new Tree (500, 150, 50, 50, 1));
				Trees10.Add (new Tree (450, 200, 50, 50, 1));
				Trees10.Add (new Tree (400, 250, 50, 50, 1));
				Trees10.Add (new Tree (150, 500, 50, 50, 1));
				Trees10.Add (new Tree (200, 450, 50, 50, 1));
				Trees10.Add (new Tree (250, 400, 50, 50, 1));
			}
			TreeWaves.Add (Trees1);
			TreeWaves.Add (Trees2);
			TreeWaves.Add (Trees3);
			TreeWaves.Add (Trees4);
			TreeWaves.Add (Trees1);
			TreeWaves.Add (Trees2);
			TreeWaves.Add (Trees3);
			TreeWaves.Add (Trees4);
			TreeWaves.Add (Trees9);
			TreeWaves.Add (Trees10);

			Castle1.LoadContent (ScreenManager.Game);
			Castle2.LoadContent (ScreenManager.Game);
			Castle3.LoadContent (ScreenManager.Game);
			Castle4.LoadContent (ScreenManager.Game);
			for(int j = 0; j < BorderTrees.Count; j++)
			{
				BorderTrees[j].LoadContent(ScreenManager.Game);
			}
			for(int j = 0; j < Bodies.Count; j++){
				Bodies[j].LoadContent(ScreenManager.Game);
			}
			healthbar = Load<Texture2D>("healthbar");
			Console.WriteLine("Init");

            hud = Load<Texture2D>("hud");

			font = ScreenManager.Game.Content.Load<SpriteFont>("Fonts/MenuFont");
			for (int i = 0; i < TreeWaves [wave].Count; i++) {
				Trees.Add (TreeWaves [wave] [i]);
			}
			for (int i = 0; i < Trees.Count; i++)
			{
				Trees[i].LoadContent(ScreenManager.Game);
			}

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
				Trees.Clear ();
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
			if (objectcounter % 997 == 0 && player1.rageMode == false)
			{
				Random rand = new Random();
				Object object1 = new Object((int)rand.Next(100, 550), (int)rand.Next(100, 550), 50, 50);
				object1.LoadContent(ScreenManager.Game);
				Objects.Add(object1);
			}
			player1.Update(controls, gameTime, Trees, Enemies, Objects);

			for (int i = 0; i < Enemies.Count; i++)
			{
				Enemies[i].Update(controls, gameTime, player1.getX(), player1.getY(), player1, Enemies, Trees);

			}
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
				Enemies.Clear ();
				SpawnEnemies.Clear ();
				Trees.Clear ();
				Wave1.Clear ();
				Wave2.Clear ();
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

            List<Sprite> AllSprites = new List<Sprite>();

            AllSprites.Add(player1);
            foreach (Enemy e in Enemies)
            {
                AllSprites.Add(e);
            }
            foreach (Tree t in Trees) {
                AllSprites.Add(t);
            }
            foreach (BorderTree t in BorderTrees)
            {
                AllSprites.Add(t);
            }
            foreach (Object o in Objects)
            {
                AllSprites.Add(o);
            }
            AllSprites.Add(Castle1);
            AllSprites.Add(Castle2);
            AllSprites.Add(Castle3);
            AllSprites.Add(Castle4);

            AllSprites = AllSprites.OrderBy(s => s.getY()).ToList();

            foreach (Sprite s in AllSprites)
            {
                s.Draw(ScreenManager.SpriteBatch);
            }

            /*
			player1.Draw (ScreenManager.SpriteBatch);

			for (int i = 0; i < Enemies.Count; i++)
			{
				Enemies[i].Draw(ScreenManager.SpriteBatch);
			}
			for (int i = 0; i < Trees.Count; i++)
			{
				Trees[i].Draw(ScreenManager.SpriteBatch);
			}
			if(player1.rageMode)
			{
				for (int i = 0; i < Bodies.Count; i++)
				{
					Bodies[i].Draw(ScreenManager.SpriteBatch);
				}
			}else{
				for (int i = 0; i < BorderTrees.Count; i++)
				{
					BorderTrees[i].Draw(ScreenManager.SpriteBatch);
				}
			}
			for (int i = 0; i < Objects.Count; i++)
			{
				Objects[i].Draw(ScreenManager.SpriteBatch);
			}
			Castle1.Draw (ScreenManager.SpriteBatch);
			Castle2.Draw (ScreenManager.SpriteBatch);
			Castle3.Draw (ScreenManager.SpriteBatch);
			Castle4.Draw (ScreenManager.SpriteBatch);
             */

			ScreenManager.SpriteBatch.Draw(hud, new Rectangle(0, 0, BerserkerGame.screenwidth, 40), Color.White);
			ScreenManager.SpriteBatch.Draw(healthbar, new Rectangle(10, 5, 150, 30), Color.Red);
			ScreenManager.SpriteBatch.Draw(healthbar, new Rectangle(10, 5, player1.getHealth() * 30, 30), Color.DarkGreen);

			ScreenManager.SpriteBatch.DrawString(font, "HP", new Vector2(50, 0), Color.White);

			ScreenManager.SpriteBatch.Draw(healthbar, new Rectangle(170, 5, 260, 30), Color.Black);
			ScreenManager.SpriteBatch.Draw(healthbar, new Rectangle(170, 5, player1.getRage(), 30), Color.Red);
			ScreenManager.SpriteBatch.Draw(player1.rageBar, new Rectangle(170, 5, 260, 30), Color.White);


			ScreenManager.SpriteBatch.DrawString (font, "SCORE: " + player1.getScore().ToString(), new Vector2 (440, 0), Color.White);
			ScreenManager.SpriteBatch.End ();

			base.Draw(gameTime);
		}
	}
}