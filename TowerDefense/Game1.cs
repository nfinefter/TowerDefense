using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;

namespace TowerDefense
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        SpriteBase moneyIMG = default;
        SpriteBase healthIMG = default;
        List<Player> monkeys = new List<Player>();
        List<Enemy> bloons = new List<Enemy>();
        List<Projectile> projectiles= new List<Projectile>();

        int updates = 0;
        int draws = 0;


        int moneyHealthSizer = 50;
        int moneyHealthRightScreenBuffer = 150;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;            
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            TargetElapsedTime = TimeSpan.FromMilliseconds(17);

            graphics.PreferredBackBufferHeight= 980;
            graphics.PreferredBackBufferWidth = 1900;
            graphics.ApplyChanges();
          
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Texture2D monkeyImage = Content.Load<Texture2D>("DartMnokeySpriteSheetEdited");
            Texture2D bloonImage = Content.Load<Texture2D>("balloon");

            spriteBatch = new SpriteBatch(GraphicsDevice);

            List<Rectangle> MonkeySource = SourceRectangleFinder(monkeyImage, new Point(6, 1));

            moneyIMG = new Sprite(Content.Load<Texture2D>("money"), new Rectangle(GraphicsDevice.Viewport.Width - moneyHealthRightScreenBuffer, 25, moneyHealthSizer, moneyHealthSizer), Color.Black, 0, Vector2.Zero);

            healthIMG = new Sprite(Content.Load<Texture2D>("health"), new Rectangle(GraphicsDevice.Viewport.Width - moneyHealthRightScreenBuffer, moneyIMG.Pos.Height * 2, moneyHealthSizer, moneyHealthSizer), Color.Black, 0, Vector2.Zero);

            monkeys.Add(new Player(monkeyImage, new Rectangle(100, 100, 100, 100), Color.White, 0, new Vector2(monkeyImage.Width / 2, monkeyImage.Height / 2), MonkeySource, 0, 0, 5));

            bloons.Add(new Enemy(bloonImage, new Rectangle(20, 20, 20, 40), Color.White, 0, new Vector2(bloonImage.Width / 2, bloonImage.Height / 2), 0, 1));

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            updates++;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            monkeys[0].test += gameTime.ElapsedGameTime;
            monkeys[0].Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            draws++;
            if (updates != draws)
            {
                Console.WriteLine($"Draws is {draws}, Updates is {updates}");
                updates = draws = 0;
            }
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            moneyIMG.Draw(spriteBatch);

            healthIMG.Draw(spriteBatch);
            Console.WriteLine(monkeys[0].RectIndex);
       //     Console.WriteLine(monkeys[0].SourceRectangle);
            monkeys[0].Draw(spriteBatch);

            bloons[0].Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}