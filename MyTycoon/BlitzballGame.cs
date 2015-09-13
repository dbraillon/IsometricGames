#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Isometric.Core;
using System.IO;
#endregion

namespace Isometric.Blitzball
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class BlitzballGame : Game
    {
        public GraphicsDeviceManager GraphicsDeviceManager { get; set; }
        public Graphics Graphics { get; set; }
        public Field Field { get; set; }
        public Item Player { get; set; }

        public BlitzballGame()
            : base()
        {
            GraphicsDeviceManager = new GraphicsDeviceManager(this);
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
            Graphics = new Graphics(GraphicsDevice);
            Graphics.XCam = 244;
            Graphics.YCam = -212;
            Field = new Field(11, 17, 2);
            Player = new Item(5, 0, 1, 64);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Load field
            FileInfo fieldFile = new FileInfo(Path.Combine(Content.RootDirectory, "field.txt"));
            Field.Load2dFieldFile(fieldFile);
            Graphics.RegisterField(Field);

            // TODO: use this.Content to load your game content here
            Graphics.RegisterTexture(1, Content.Load<Texture2D>("water"));
            Graphics.RegisterTexture(2, Content.Load<Texture2D>("water"));
            Graphics.RegisterTexture(4, Content.Load<Texture2D>("water-dark"));
            Graphics.RegisterTexture(5, Content.Load<Texture2D>("water-dark"));
            Graphics.RegisterTexture(6, Content.Load<Texture2D>("water-dark"));
            Graphics.RegisterTexture(7, Content.Load<Texture2D>("water-dark"));
            Graphics.RegisterTexture(8, Content.Load<Texture2D>("water-dark"));
            Graphics.RegisterTexture(9, Content.Load<Texture2D>("water-dark"));
            //Graphics.RegisterItem(Player, Content.Load<Texture2D>("2dYellowCube-Medium"));

            Graphics.RegisterFont(Content.Load<SpriteFont>("Arial"));
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (Keyboard.GetState().IsKeyDown(Keys.Left)) Player.X -= 1;
            if (Keyboard.GetState().IsKeyDown(Keys.Right)) Player.X += 1;
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && !Keyboard.GetState().IsKeyDown(Keys.LeftControl)) Player.Y -= 1;
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && Keyboard.GetState().IsKeyDown(Keys.LeftControl)) Graphics.Zoom += 0.01;
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && !Keyboard.GetState().IsKeyDown(Keys.LeftControl)) Player.Y += 1;
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && Keyboard.GetState().IsKeyDown(Keys.LeftControl)) Graphics.Zoom -= 0.01;

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
            Graphics.Draw(true);
            
            base.Draw(gameTime);
        }
    }
}
