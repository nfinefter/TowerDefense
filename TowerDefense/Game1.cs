using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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


        int moneyHealthSizer = 50;
        int moneyHealthRightScreenBuffer = 150;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;            
            graphics.PreferredBackBufferHeight= 980;
            graphics.PreferredBackBufferWidth = 1900;
            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

          
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Texture2D monkeyImage = Content.Load<Texture2D>("DartMnokeySpriteSheetEdited");
            Texture2D bloonImage = Content.Load<Texture2D>("balloon");

            spriteBatch = new SpriteBatch(GraphicsDevice);

            moneyIMG = new Sprite(Content.Load<Texture2D>("money"), new Rectangle(GraphicsDevice.Viewport.Width - moneyHealthRightScreenBuffer, 25, moneyHealthSizer, moneyHealthSizer), Color.Black, 0, Vector2.Zero);

            healthIMG = new Sprite(Content.Load<Texture2D>("health"), new Rectangle(GraphicsDevice.Viewport.Width - moneyHealthRightScreenBuffer, moneyIMG.Pos.Height * 2, moneyHealthSizer, moneyHealthSizer), Color.Black, 0, Vector2.Zero);

            monkeys.Add(new Player(monkeyImage, new Rectangle(0, 0, 100, 100), Color.White, 0, new Vector2(monkeyImage.Width / 2, monkeyImage.Height / 2), 0, 0));

            bloons.Add(new Enemy(bloonImage, new Rectangle(20, 20, 20, 20), Color.White, 0, new Vector2(bloonImage.Width / 2, bloonImage.Height / 2), 0, 1));

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();



            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            moneyIMG.Draw(spriteBatch);

            healthIMG.Draw(spriteBatch);

            monkeys[0].Draw(spriteBatch);

            bloons[0].Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}