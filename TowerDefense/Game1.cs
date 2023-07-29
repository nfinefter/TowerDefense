global using HelperLibrary;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGame.Extended;
using MonoGame.Extended.Timers;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;

using WeightedDirectedGraphs;

namespace TowerDefense
{
    public class Game1 : Game
    {
        public static int size = 40;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        int updates = 0;
        int draws = 0;
        TimeSpan bloonKillerDelay = TimeSpan.FromSeconds(5);
        TimeSpan timer;
        bool killing;
        GameScreen Game = new GameScreen();
        MenuScreen Menu = new MenuScreen();
        public Rectangle Start;

        int moneyHealthSizer = 50;
        int moneyHealthRightScreenBuffer = 250;


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

            Game.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Texture2D pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White });

            Button sellButton = new Button(pixel, new Rectangle(Game.MoneyIMG.Pos.X + 10, GraphicsDevice.Viewport.Height - 60, 110, 50), Color.Red, 0, default, Game.Sell);
            Button upgradeButton = new Button(pixel, new Rectangle(sellButton.Pos.X + 120, GraphicsDevice.Viewport.Height - 60, 110, 50), Color.Green, 0, default, Game.Upgrade);

            ScreenManager.Instance.Init((Screenum.Game, Game), (Screenum.Menu, Menu));

            ScreenManager.Instance.currentScreen = Screenum.Menu;

            ContentManager.Instance[Textures.Dart] = Game.DartImage;
            ContentManager.Instance[Textures.Jesus] = Game.JesusImage;
            ContentManager.Instance[Textures.Bloon] = Game.BloonImage;

            spriteBatch = new SpriteBatch(GraphicsDevice);

            //sellButton = new Rectangle(moneyIMG.Pos.X + 10, GraphicsDevice.Viewport.Height - 60, 110, 50);
            //upgradeButton = new Rectangle(sellButton.Pos.X + 120, GraphicsDevice.Viewport.Height - 60, 110, 50);

            //selectedMonkey = monkeys[0];
            //Menu.Buttons.Add(new Rectangle((GraphicsDevice.Viewport.X + GraphicsDevice.Viewport.Width) / 2, (GraphicsDevice.Viewport.Y + GraphicsDevice.Viewport.Height) / 2, 100, 100));
            //Menu.Buttons.Add(new Rectangle((GraphicsDevice.Viewport.X + GraphicsDevice.Viewport.Width) / 2, (GraphicsDevice.Viewport.Y + GraphicsDevice.Viewport.Height) / 2 + 200, 100, 100));

            Game.Font = Content.Load<SpriteFont>("File");
            Game.MoneyIMG = new Sprite(Content.Load<Texture2D>("money"), new Rectangle(GraphicsDevice.Viewport.Width - moneyHealthRightScreenBuffer, 25, moneyHealthSizer, moneyHealthSizer), Color.Black, 0, Vector2.Zero);
            Game.HealthIMG = new Sprite(Content.Load<Texture2D>("health"), new Rectangle(GraphicsDevice.Viewport.Width - moneyHealthRightScreenBuffer, Game.MoneyIMG.Pos.Height * 2, moneyHealthSizer, moneyHealthSizer), Color.Black, 0, Vector2.Zero);
            Game.SelectedMonkey = Game.Monkeys[0];
            Game.DartImage = Content.Load<Texture2D>("Dart");
            Game.JesusImage = Content.Load<Texture2D>("DartMnokeySpriteSheetEdited");
            Game.BloonImage = Content.Load<Texture2D>("balloon");
            Game.MonkeySource = SourceRectangleFinder(Game.JesusImage, new Point(6, 1));
            Game.Saint = new Player(Game.JesusImage, new Rectangle(GraphicsDevice.Viewport.Width - 200, 200, 100, 100), Color.White, 0, new Vector2(Game.JesusImage.Width / 2, Game.JesusImage.Height / 2), Game.MonkeySource, 0, 0, 5, 25); ;
            Game.Bloons.Add(new Enemy(Game.BloonImage, Start, Color.Red, 0, new Vector2(Game.BloonImage.Width / 2, Game.BloonImage.Height / 2), 0, GameScreen.Path, 5));
            Game.Bloons.Add(new Enemy(Game.BloonImage, Start, Color.Red, 0, new Vector2(Game.BloonImage.Width / 2, Game.BloonImage.Height / 2), 0, GameScreen.Path, 3));
            Game.Bloons.Add(new Enemy(Game.BloonImage, Start, Color.Red, 0, new Vector2(Game.BloonImage.Width / 2, Game.BloonImage.Height / 2), 0, GameScreen.Path, 2));
            Game.Bloons.Add(new Enemy(Game.BloonImage, Start, Color.Red, 0, new Vector2(Game.BloonImage.Width / 2, Game.BloonImage.Height / 2), 0, GameScreen.Path, 1));
            Game.Monkeys.Add(new Player(Game.JesusImage, new Rectangle(GraphicsDevice.Viewport.Width - 200, 200, 100, 100), Color.White, 0, new Vector2(Game.JesusImage.Width / 2, Game.JesusImage.Height / 2), Game.MonkeySource, 0, 0, 5, 25));
            Game.GameWidth = GraphicsDevice.Viewport.Width;
            Game.GameHeight = GraphicsDevice.Viewport.Height;

            Menu.Sprites.Add(Game.MoneyIMG);
            Menu.Sprites.Add(Game.HealthIMG);
            Menu.Buttons.Add(new Button(pixel, new Rectangle((GraphicsDevice.Viewport.X + GraphicsDevice.Viewport.Width) / 2, (GraphicsDevice.Viewport.Y + GraphicsDevice.Viewport.Height) / 2, 100, 100), Color.Green, 0, "Start", default));
            Menu.Buttons.Add(new Button(pixel, new Rectangle((GraphicsDevice.Viewport.X + GraphicsDevice.Viewport.Width) / 2, (GraphicsDevice.Viewport.Y + GraphicsDevice.Viewport.Height) / 2 + 200, 100, 100), Color.Red, 0, "Back", default));

            Game.Sprites.Add(Game.MoneyIMG);
            Game.Sprites.Add(Game.HealthIMG);
            Game.Buttons.Add(sellButton);
            Game.Buttons.Add(upgradeButton);

            // TODO: use this.Content to load your game content here
        }


        protected override void Update(GameTime gameTime)
        {
            updates++;
            ScreenManager.Instance.Update(gameTime);


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Game.Update(gameTime);

            #region no

            //currState = Mouse.GetState().LeftButton;
            //for (int i = 1; i < monkeys.Count; i++)
            //{
            //    if (monkeys[i].Hitbox.Contains(Mouse.GetState().Position) && currState == ButtonState.Pressed)
            //    {
            //        selectedMonkey = monkeys[i];
            //    }
            //}
            //for (int i = 1; i < monkeys.Count; i++)
            //{
            //    monkeys[i].FindTarget(bloons);
            //    monkeys[i].Update(gameTime);
            //}
            //if (Vector2.Distance(new Vector2(monkeys[0].Pos.X, monkeys[0].Pos.Y), new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y)) < 100 && Mouse.GetState().LeftButton == ButtonState.Pressed && prevState == ButtonState.Released && dragging == false && money >= 100)
            //{
            //    monkeys.Add(new Player(jesusImage, monkeys[0].Pos, Color.White, 0, new Vector2(jesusImage.Width / 2, jesusImage.Height / 2), MonkeySource, 0, 0, 5, 25));
            //    money -= 100;

            //    dragging = true;

            //    if (Mouse.GetState().LeftButton != ButtonState.Pressed)
            //    {
            //        dragging = false;
            //    }
            //}

            //if (dragging)
            //{
            //    monkeys[monkeys.Count - 1].Pos = new Rectangle(Mouse.GetState().Position, new Point(monkeys[0].Pos.Width, monkeys[0].Pos.Height));
            //    PlaceMonke();
            //}

            //Sell();
            //Upgrade();

            //for (int i = 0; i < bloons.Count; i++)
            //{
            //    bloons[i].Update(gameTime);
            //}

            //Player.CheckKill(ref bloons);

            //for (int i = 0; i < bloons.Count; i++)
            //{
            //    if (EnemyManager.IsAttacked(bloons[i]))
            //    {
            //        bloons.RemoveAt(i);
            //    }
            //}

            //EnemyManager.EnemyCreate(gameTime, ref bloons);

            //if (Keyboard.GetState().IsKeyDown(Keys.Enter) && illegalPos == false)
            //{
            //    selectedMonkey.CreateDot();
            //    dragging = false;
            //    monkeys[monkeys.Count - 1].Placed = true;
            //}

            ScreenManager.Instance.Update(gameTime);

            base.Update(gameTime);
        }

        //void PlaceMonke()
        //{
        //    illegalPos = false;
        //    monkeys[monkeys.Count - 1].Color = Color.White;
        //    if (Vector2.Distance(new Vector2(monkeys[monkeys.Count - 1].Pos.X, monkeys[monkeys.Count - 1].Pos.Y), new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y)) < 20)
        //    {

        //        for (int i = 0; i < path.Count; i++)
        //        {
        //            //Red highlighting for whenever illegal monkey is trying to be placed

        //            //if (monkeys[monkeys.Count - 1].Hitbox.Intersects(path[i].Hitbox()))
        //            //{
        //            //    monkeys[monkeys.Count - 1].Color = Color.Red;
        //            //    illegalPos = true;
        //            //}
        //        }
        //    }
        //    if (monkeys[monkeys.Count - 1].Hitbox.Intersects(menuDimensions) || !new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height).Contains(monkeys[monkeys.Count - 1].Hitbox))
        //    {
        //        monkeys[monkeys.Count - 1].Color = Color.Red;
        //        illegalPos = true;
        //    }
        //    for (int j = 0; j < monkeys.Count - 1; j++)
        //    {
        //        if (monkeys[monkeys.Count - 1].Hitbox.Intersects(monkeys[j].Hitbox))
        //        {
        //            monkeys[monkeys.Count - 1].Color = Color.Red;
        //            illegalPos = true;
        //        }
        //    }

        //}
        //void Sell()
        //{
        //    if (sellButton.Contains(Mouse.GetState().Position) && currState == ButtonState.Pressed && prevState == ButtonState.Released)
        //    {
        //        if (monkeys.Contains(selectedMonkey) && monkeys.Count > 1)
        //        {
        //            ProjectileRemover(selectedMonkey);
        //            if (selectedMonkey.Pos != Saint.Pos && !dragging)
        //            {
        //                monkeys.Remove(selectedMonkey);
        //                money += (100 * selectedMonkey.Level) + 100;
        //                selectedMonkey = monkeys[0];
        //            }
        //        }
        //    }
        //}
        //void Upgrade()
        //{
        //    if (upgradeButton.Contains(Mouse.GetState().Position) && currState == ButtonState.Pressed && prevState == ButtonState.Released && money >= 100)
        //    {
        //        if (selectedMonkey != monkeys[0])
        //        {

        //            if (monkeys.Contains(selectedMonkey) && monkeys.Count > 0 && selectedMonkey.Level < 5 && !dragging)
        //            {
        //                selectedMonkey.DmgMultiplier++;
        //                money -= 100;
        //                selectedMonkey.Level++;
        //                PlayerManager.UpgradeDot(selectedMonkey);
        //            }
        //        }
        //    }

        //}
        void MonkeyKill(GameTime gameTime)
        {
            killing = monkeyKill(Game.Monkeys, killing);
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
        //void DrawProjectiles()
        //{
        //    for (int i = 0; i < Player.projectiles.Count; i++)
        //    {
        //        var temp = Player.projectiles[i].Rotation;
        //        Player.projectiles[i].Rotation -= 90;
        //        Player.projectiles[i].Draw(spriteBatch);
        //        Player.projectiles[i].Rotation += 90;
        //    }

        //}
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
                    Menu.Buttons[i].Draw(spriteBatch);
                    //spriteBatch.FillRectangle(Menu.Buttons[i], Color.Black, 0);
                }
            }

            if (ScreenManager.Instance.CurrentScreen == Game)
            {
                Game.Draw(spriteBatch);

                //for (int i = 0; i < pathDrawDelay; i++)
                //{
                //    spriteBatch.FillRectangle(new Rectangle(path[i].Value.X, path[i].Value.Y, Game1.size, Game1.size), Color.White, 0);
                //}

                //if (pathDrawDelay < path.Count)
                //{
                //    pathDrawDelay++;
                //}

                //for (int i = 0; i < monkeys.Count; i++)
                //{
                //    monkeys[i].Draw(spriteBatch);
                //    if (monkeys[i].Dot != null)
                //    {
                //        monkeys[i].Dot.DrawRectangle(spriteBatch);
                //    }
                //}

                //for (int i = 0; i < bloons.Count; i++)
                //{
                //    spriteBatch.Draw(bloons[i].Tex, bloons[i].Pos, bloons[i].Color);
                //}

                //spriteBatch.DrawString(spriteFont, $"{money}", new Vector2(moneyIMG.Pos.X + 50, moneyIMG.Pos.Y + 20), Color.Black);
                //spriteBatch.DrawString(spriteFont, $"{health}", new Vector2(healthIMG.Pos.X + 50, healthIMG.Pos.Y + 20), Color.Black);
                //spriteBatch.DrawString(spriteFont, "Cost :100", new Vector2(monkeys[0].Pos.X + 50, monkeys[0].Pos.Y + 20), Color.Black);

                //spriteBatch.DrawLine(new Vector2(MapXBorder, 0), new Vector2(MapXBorder, GraphicsDevice.Viewport.Height), Color.Black, 1, 0);

                //for (int i = 0; i < map.Graph.Count; i++)
                //{
                //    spriteBatch.DrawPoint(map.Graph[i].Value.X, map.Graph[i].Value.Y, Color.Black);
                //}

                //Draws Hitboxes
                //spriteBatch.DrawRectangle(monkeys[monkeys.Count - 1].Hitbox, Color.Yellow, 1, 0);
                
                //spriteBatch.DrawRectangle(selectedMonkey.Hitbox, Color.Black, 1, 0);

                //DrawProjectiles();

                MonkeyKill(gameTime);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}