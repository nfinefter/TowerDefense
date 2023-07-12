global using HelperLibrary;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGame.Extended;
using MonoGame.Extended.Timers;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

using WeightedDirectedGraphs;

namespace TowerDefense
{
    public class Game1 : Game
    {
        public static int size = 40;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        Sprite moneyIMG = default;
        Sprite healthIMG = default;
        List<Player> monkeys = new List<Player>();
        List<Enemy> bloons = new List<Enemy>();
        Texture2D jesusImage;
        Texture2D bloonImage;
        public static Texture2D dartImage;
        List<Rectangle> MonkeySource;
        static public float money = 1000;
        static public int health = 100;
        SpriteFont spriteFont;
        int pathDrawDelay = 0;
        Rectangle sellButton;
        Rectangle upgradeButton;
        bool dragging = false;
        bool illegalPos = false;
        Player selectedMonkey;
        int updates = 0;
        int draws = 0;
        ButtonState prevState = ButtonState.Released;
        ButtonState currState;
        Rectangle menuDimensions;
        TimeSpan bloonKillerDelay = TimeSpan.FromSeconds(5);
        TimeSpan timer;
        bool killing;
        GameScreen Game = new GameScreen();
        MenuScreen Menu = new MenuScreen();
        public static Rectangle Start;
        Player Saint;
        bool GameStarted = false;

        int moneyHealthSizer = 50;
        int moneyHealthRightScreenBuffer = 250;

        int MapXBorder;
        Map map = new Map();

        public static List<Vertex<System.Drawing.Point>> path;

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

            graphics.PreferredBackBufferHeight = 980;
            graphics.PreferredBackBufferWidth = 1900;
            graphics.ApplyChanges();

            MapXBorder = GraphicsDevice.Viewport.Width - 250;

            path = map.GeneratePath(MapXBorder, GraphicsDevice.Viewport.Height);

            menuDimensions = new Rectangle(MapXBorder, 0, MapXBorder, GraphicsDevice.Viewport.Height);

            Start = new Rectangle(new Microsoft.Xna.Framework.Point(path[0].Value.X, path[0].Value.Y), new Point(40, 40));

            base.Initialize();
        }

        protected override void LoadContent()
        {

            spriteFont = Content.Load<SpriteFont>("File");

            jesusImage = Content.Load<Texture2D>("DartMnokeySpriteSheetEdited");
            bloonImage = Content.Load<Texture2D>("balloon");
            dartImage = Content.Load<Texture2D>("Dart");

            ScreenManager.Instance.Init((Screenum.Game, Game), (Screenum.Menu, Menu));

            ScreenManager.Instance.currentScreen = Screenum.Menu;

            ContentManager.Instance[Textures.Dart] = dartImage;
            ContentManager.Instance[Textures.Jesus] = jesusImage;
            ContentManager.Instance[Textures.Bloon] = bloonImage;

            spriteBatch = new SpriteBatch(GraphicsDevice);

            MonkeySource = SourceRectangleFinder(jesusImage, new Point(6, 1));

            moneyIMG = new Sprite(Content.Load<Texture2D>("money"), new Rectangle(GraphicsDevice.Viewport.Width - moneyHealthRightScreenBuffer, 25, moneyHealthSizer, moneyHealthSizer), Color.Black, 0, Vector2.Zero);

            healthIMG = new Sprite(Content.Load<Texture2D>("health"), new Rectangle(GraphicsDevice.Viewport.Width - moneyHealthRightScreenBuffer, moneyIMG.Pos.Height * 2, moneyHealthSizer, moneyHealthSizer), Color.Black, 0, Vector2.Zero);

            monkeys.Add(new Player(jesusImage, new Rectangle(GraphicsDevice.Viewport.Width - 200, 200, 100, 100), Color.White, 0, new Vector2(jesusImage.Width / 2, jesusImage.Height / 2), MonkeySource, 0, 0, 5, 25));

            Saint = new Player(jesusImage, new Rectangle(GraphicsDevice.Viewport.Width - 200, 200, 100, 100), Color.White, 0, new Vector2(jesusImage.Width / 2, jesusImage.Height / 2), MonkeySource, 0, 0, 5, 25);

            bloons.Add(new Enemy(bloonImage, Start, Color.Red, 0, new Vector2(bloonImage.Width / 2, bloonImage.Height / 2), 0, path, 5));
            bloons.Add(new Enemy(bloonImage, Start, Color.Red, 0, new Vector2(bloonImage.Width / 2, bloonImage.Height / 2), 0, path, 3));
            bloons.Add(new Enemy(bloonImage, Start, Color.Red, 0, new Vector2(bloonImage.Width / 2, bloonImage.Height / 2), 0, path, 2));
            bloons.Add(new Enemy(bloonImage, Start, Color.Red, 0, new Vector2(bloonImage.Width / 2, bloonImage.Height / 2), 0, path, 1));

            sellButton = new Rectangle(moneyIMG.Pos.X + 10, GraphicsDevice.Viewport.Height - 60, 110, 50);
            upgradeButton = new Rectangle(sellButton.X + 120, GraphicsDevice.Viewport.Height - 60, 110, 50);

            selectedMonkey = monkeys[0];

            Menu.Sprites.Add(moneyIMG);
            Menu.Sprites.Add(healthIMG);

            Menu.Buttons.Add(new Rectangle((GraphicsDevice.Viewport.X + GraphicsDevice.Viewport.Width) / 2, (GraphicsDevice.Viewport.Y + GraphicsDevice.Viewport.Height) / 2, 100, 100));
            Menu.Buttons.Add(new Rectangle((GraphicsDevice.Viewport.X + GraphicsDevice.Viewport.Width) / 2, (GraphicsDevice.Viewport.Y + GraphicsDevice.Viewport.Height) / 2 + 200, 100, 100));


            // TODO: use this.Content to load your game content here
        }


        protected override void Update(GameTime gameTime)
        {
            updates++;
            ScreenManager.Instance.Update(gameTime);
            #region no

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

          
            currState = Mouse.GetState().LeftButton;
            for (int i = 1; i < monkeys.Count; i++)
            {
                if (monkeys[i].Hitbox.Contains(Mouse.GetState().Position) && currState == ButtonState.Pressed)
                {
                    selectedMonkey = monkeys[i];
                }
            }
            for (int i = 1; i < monkeys.Count; i++)
            {
                monkeys[i].FindTarget(bloons);
                monkeys[i].Update(gameTime);
            }
            if (Vector2.Distance(new Vector2(monkeys[0].Pos.X, monkeys[0].Pos.Y), new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y)) < 100 && Mouse.GetState().LeftButton == ButtonState.Pressed && prevState == ButtonState.Released && dragging == false && money >= 100)
            {
                monkeys.Add(new Player(jesusImage, monkeys[0].Pos, Color.White, 0, new Vector2(jesusImage.Width / 2, jesusImage.Height / 2), MonkeySource, 0, 0, 5, 25));
                money -= 100;

                dragging = true;

                if (Mouse.GetState().LeftButton != ButtonState.Pressed)
                {
                    dragging = false;
                }
            }

            if (dragging)
            {
                monkeys[monkeys.Count - 1].Pos = new Rectangle(Mouse.GetState().Position, new Point(monkeys[0].Pos.Width, monkeys[0].Pos.Height));
                PlaceMonke();
            }

            Sell();
            Upgrade();

            for (int i = 0; i < bloons.Count; i++)
            {
                bloons[i].Update(gameTime);
            }
          
            Player.CheckKill(ref bloons);

            for (int i = 0; i < bloons.Count; i++)
            {
                if (EnemyManager.IsAttacked(bloons[i]))
                {
                    bloons.RemoveAt(i);
                }
            }

            EnemyManager.EnemyCreate(gameTime, ref bloons);

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && illegalPos == false)
            {
                selectedMonkey.CreateDot();
                dragging = false;
                monkeys[monkeys.Count - 1].Placed = true;
            }

            if (health <= 0)
            {
                while (true) ;
                //You lose
            }

            prevState = currState;

            ScreenManager.Instance.Update(gameTime);

            base.Update(gameTime);
        }

        void PlaceMonke()
        {
            illegalPos = false;
            monkeys[monkeys.Count - 1].Color = Color.White;
            if (Vector2.Distance(new Vector2(monkeys[monkeys.Count - 1].Pos.X, monkeys[monkeys.Count - 1].Pos.Y), new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y)) < 20)
            {

                for (int i = 0; i < path.Count; i++)
                {
                    //Red highlighting for whenever illegal monkey is trying to be placed

                    //if (monkeys[monkeys.Count - 1].Hitbox.Intersects(path[i].Hitbox()))
                    //{
                    //    monkeys[monkeys.Count - 1].Color = Color.Red;
                    //    illegalPos = true;
                    //}
                }
            }
            if (monkeys[monkeys.Count - 1].Hitbox.Intersects(menuDimensions) || !new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height).Contains(monkeys[monkeys.Count - 1].Hitbox))
            {
                monkeys[monkeys.Count - 1].Color = Color.Red;
                illegalPos = true;
            }
            for (int j = 0; j < monkeys.Count - 1; j++)
            {
                if (monkeys[monkeys.Count - 1].Hitbox.Intersects(monkeys[j].Hitbox))
                {
                    monkeys[monkeys.Count - 1].Color = Color.Red;
                    illegalPos = true;
                }
            }

        }
        void Sell()
        {
            if (sellButton.Contains(Mouse.GetState().Position) && currState == ButtonState.Pressed && prevState == ButtonState.Released)
            {
                if (monkeys.Contains(selectedMonkey) && monkeys.Count > 1)
                {
                    ProjectileRemover(selectedMonkey);
                    if (selectedMonkey.Pos != Saint.Pos && !dragging)
                    {
                        monkeys.Remove(selectedMonkey);
                        money += (100 * selectedMonkey.Level) + 100;
                        selectedMonkey = monkeys[0];
                    }
                }
            }
        }
        void Upgrade()
        {
            if (upgradeButton.Contains(Mouse.GetState().Position) && currState == ButtonState.Pressed && prevState == ButtonState.Released && money >= 100)
            {
                if (selectedMonkey != monkeys[0])
                {

                    if (monkeys.Contains(selectedMonkey) && monkeys.Count > 0 && selectedMonkey.Level < 5 && !dragging)
                    {
                        selectedMonkey.DmgMultiplier++;
                        money -= 100;
                        selectedMonkey.Level++;
                        PlayerManager.UpgradeDot(selectedMonkey);
                    }
                }
            }

        }
        void MonkeyKill(GameTime gameTime)
        {
            killing = monkeyKill(monkeys, killing);
            if (killing)
            {
                spriteBatch.Draw(ContentManager.Instance[Textures.Bloon], new Rectangle((GraphicsDevice.Viewport.X + GraphicsDevice.Viewport.Width) / 2 - 200, (GraphicsDevice.Viewport.Y + GraphicsDevice.Viewport.Height) / 2 - 200, 500, 500), Color.Purple);

                timer += gameTime.ElapsedGameTime;
                if (timer > bloonKillerDelay)
                {
                    killing = false;
                    timer = TimeSpan.Zero;
                }
            }
        }
        void DrawProjectiles()
        {
            for (int i = 0; i < Player.projectiles.Count; i++)
            {
                var temp = Player.projectiles[i].Rotation;
                Player.projectiles[i].Rotation -= 90;
                Player.projectiles[i].Draw(spriteBatch);
                Player.projectiles[i].Rotation += 90;
            }

        }
        #endregion
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

            if (ScreenManager.Instance.CurrentScreen == Menu)
            {
                for (int i = 0; i < Menu.Buttons.Count; i++)
                {
                    spriteBatch.FillRectangle(Menu.Buttons[i], Color.Black, 0);
                }
            }

            if (ScreenManager.Instance.CurrentScreen == Game)
            {
                for (int i = 0; i < pathDrawDelay; i++)
                {
                    spriteBatch.FillRectangle(new Rectangle(path[i].Value.X, path[i].Value.Y, Game1.size, Game1.size), Color.White, 0);
                }

                if (pathDrawDelay < path.Count)
                {
                    pathDrawDelay++;
                }

                for (int i = 0; i < Game.Sprites.Count; i++)
                {
                    Game.Sprites[i].Draw(spriteBatch);
                }

                for (int i = 0; i < monkeys.Count; i++)
                {
                    monkeys[i].Draw(spriteBatch);
                    if (monkeys[i].Dot != null)
                    {
                        monkeys[i].Dot.DrawRectangle(spriteBatch);
                    }
                }

                for (int i = 0; i < bloons.Count; i++)
                {
                    spriteBatch.Draw(bloons[i].Tex, bloons[i].Pos, bloons[i].Color);
                }

                spriteBatch.DrawString(spriteFont, $"{money}", new Vector2(moneyIMG.Pos.X + 50, moneyIMG.Pos.Y + 20), Color.Black);
                spriteBatch.DrawString(spriteFont, $"{health}", new Vector2(healthIMG.Pos.X + 50, healthIMG.Pos.Y + 20), Color.Black);
                spriteBatch.DrawString(spriteFont, "Cost :100", new Vector2(monkeys[0].Pos.X + 50, monkeys[0].Pos.Y + 20), Color.Black);

                spriteBatch.FillRectangle(sellButton, Color.Red, 0);
                Vector2 size = spriteFont.MeasureString("Sell");
                spriteBatch.DrawString(spriteFont, "Sell", new Vector2(sellButton.X + sellButton.Width / 2 - (size.X / 2), sellButton.Y + sellButton.Height / 2 - (size.Y / 2)), Color.Black);

                spriteBatch.FillRectangle(upgradeButton, Color.Green, 0);
                size = spriteFont.MeasureString("Upgrade");
                spriteBatch.DrawString(spriteFont, "Upgrade", new Vector2(upgradeButton.X + upgradeButton.Width / 2 - (size.X / 2), upgradeButton.Y + upgradeButton.Height / 2 - (size.Y / 2)), Color.Black);

                spriteBatch.DrawLine(new Vector2(MapXBorder, 0), new Vector2(MapXBorder, GraphicsDevice.Viewport.Height), Color.Black, 1, 0);

                for (int i = 0; i < map.Graph.Count; i++)
                {
                    spriteBatch.DrawPoint(map.Graph[i].Value.X, map.Graph[i].Value.Y, Color.Black);
                }

                //Draws Hitboxes
                //spriteBatch.DrawRectangle(monkeys[monkeys.Count - 1].Hitbox, Color.Yellow, 1, 0);
                spriteBatch.DrawRectangle(selectedMonkey.Hitbox, Color.Black, 1, 0);

                DrawProjectiles();

                MonkeyKill(gameTime);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}