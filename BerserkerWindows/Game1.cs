using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Berserker
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Animation idleAnim;
        Animation attackDownAnim;
        Animation walkDownAnim;
        Animation walkLeftAnim;
        Animation walkRightAnim;
        Animation walkUpAnim;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Texture2D animTex = Content.Load<Texture2D>("ax");
            idleAnim = new Animation(animTex, new Point(8, 6), new Point(32, 32), new Point(0,0), new Point(7,0), new TimeSpan(1000000), true);
            attackDownAnim = new Animation(animTex, new Point(8, 6), new Point(32, 32), new Point(0, 1), new Point(7, 1), new TimeSpan(1000000), false);
            walkDownAnim = new Animation(animTex, new Point(8, 6), new Point(32, 32), new Point(0, 2), new Point(7, 2), new TimeSpan(1000000), true);
            walkLeftAnim = new Animation(animTex, new Point(8, 6), new Point(32, 32), new Point(0, 3), new Point(7, 3), new TimeSpan(1000000), true);
            walkRightAnim = new Animation(animTex, new Point(8, 6), new Point(32, 32), new Point(0, 4), new Point(7, 4), new TimeSpan(1000000), true);
            walkUpAnim = new Animation(animTex, new Point(8, 6), new Point(32, 32), new Point(0, 5), new Point(7, 5), new TimeSpan(1000000), true);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            idleAnim.Update(gameTime);
            attackDownAnim.Update(gameTime);
            walkDownAnim.Update(gameTime);
            walkLeftAnim.Update(gameTime);
            walkRightAnim.Update(gameTime);
            walkUpAnim.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            idleAnim.Draw(spriteBatch, new Vector2(250,200));
            attackDownAnim.Draw(spriteBatch, new Vector2(300, 200));
            walkDownAnim.Draw(spriteBatch, new Vector2(350,200));
            walkLeftAnim.Draw(spriteBatch, new Vector2(400,200));
            walkRightAnim.Draw(spriteBatch, new Vector2(450,200));
            walkUpAnim.Draw(spriteBatch, new Vector2(500,200));

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
