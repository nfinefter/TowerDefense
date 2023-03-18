using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TowerDefense
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        SpriteBase moneyIMG = default;
        SpriteBase healthIMG = default;
        SpriteBase dartIMG = default;

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

            moneyIMG = new Sprite(null, new Rectangle(GraphicsDevice.Viewport.Width - moneyHealthRightScreenBuffer, 25, moneyHealthSizer, moneyHealthSizer), Color.Black, 0, Vector2.Zero);

            healthIMG = new Sprite(null, new Rectangle(GraphicsDevice.Viewport.Width - moneyHealthRightScreenBuffer, moneyIMG.Pos.Height * 2, moneyHealthSizer, moneyHealthSizer), Color.Black, 0, Vector2.Zero);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            moneyIMG.Tex = Content.Load<Texture2D>("money");
            healthIMG.Tex = Content.Load<Texture2D>("health");

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

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}