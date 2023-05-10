using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGame.Extended;
using MonoGame.Extended.Timers;

using System;
using System.Collections.Generic;
using System.Threading;

using WeightedDirectedGraphs;

namespace TowerDefense
{
    public class Game1 : Game
    {
        public static int size = 40;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        SpriteBase moneyIMG = default;
        SpriteBase healthIMG = default;
        List<Player> monkeys = new List<Player>();
        List<Enemy> bloons = new List<Enemy>();
        List<Projectile> projectiles = new List<Projectile>();
        Texture2D monkeyImage;
        Texture2D bloonImage;
        Texture2D dartImage;
        List<Rectangle> MonkeySource;
        float money = 1000;
        int health = 100;
        SpriteFont spriteFont;
        int pathDrawDelay = 0;
        Rectangle sellButton;
        Rectangle upgradeButton;
        bool dragging = false;
        int wrongPath = 0;
        bool illegalPos = false;
        Player selectedMonkey;
        int updates = 0;
        int draws = 0;
        ButtonState prevState = ButtonState.Released;
        ButtonState currState;
        Rectangle menuDimensions;

        int moneyHealthSizer = 50;
        int moneyHealthRightScreenBuffer = 250;

        int MapXBorder;
        Map map = new Map();

        int timer = 0;

        List<Vertex<System.Drawing.Point>> path;

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

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteFont = Content.Load<SpriteFont>("File");

            monkeyImage = Content.Load<Texture2D>("DartMnokeySpriteSheetEdited");
            bloonImage = Content.Load<Texture2D>("balloon");
            dartImage = Content.Load<Texture2D>("Dart");
            spriteBatch = new SpriteBatch(GraphicsDevice);

            MonkeySource = SourceRectangleFinder(monkeyImage, new Point(6, 1));

            moneyIMG = new Sprite(Content.Load<Texture2D>("money"), new Rectangle(GraphicsDevice.Viewport.Width - moneyHealthRightScreenBuffer, 25, moneyHealthSizer, moneyHealthSizer), Color.Black, 0, Vector2.Zero);

            healthIMG = new Sprite(Content.Load<Texture2D>("health"), new Rectangle(GraphicsDevice.Viewport.Width - moneyHealthRightScreenBuffer, moneyIMG.Pos.Height * 2, moneyHealthSizer, moneyHealthSizer), Color.Black, 0, Vector2.Zero);

            monkeys.Add(new Player(monkeyImage, new Rectangle(GraphicsDevice.Viewport.Width - 200, 200, 100, 100), Color.White, 0, new Vector2(monkeyImage.Width / 2, monkeyImage.Height / 2), MonkeySource, 0, 0, 5));

            bloons.Add(new Enemy(bloonImage, new Rectangle(new Microsoft.Xna.Framework.Point(path[0].Value.X, path[0].Value.Y), new Point(40, 40)), Color.Green, 0, new Vector2(bloonImage.Width / 2, bloonImage.Height / 2), 0, 50, path));
            bloons.Add(new Enemy(bloonImage, new Rectangle(new Microsoft.Xna.Framework.Point(path[0].Value.X, path[0].Value.Y), new Point(40, 40)), Color.Yellow, 0, new Vector2(bloonImage.Width / 2, bloonImage.Height / 2), 0, 60, path));
            bloons.Add(new Enemy(bloonImage, new Rectangle(new Microsoft.Xna.Framework.Point(path[0].Value.X, path[0].Value.Y), new Point(40, 40)), Color.Red, 0, new Vector2(bloonImage.Width / 2, bloonImage.Height / 2), 0, 70, path));
            bloons.Add(new Enemy(bloonImage, new Rectangle(new Microsoft.Xna.Framework.Point(path[0].Value.X, path[0].Value.Y), new Point(40, 40)), Color.White, 0, new Vector2(bloonImage.Width / 2, bloonImage.Height / 2), 0, 100, path));

            sellButton = new Rectangle(moneyIMG.Pos.X + 10, GraphicsDevice.Viewport.Height - 60, 110, 50);
            upgradeButton = new Rectangle(sellButton.X + 120, GraphicsDevice.Viewport.Height - 60, 110, 50);

            selectedMonkey = monkeys[0];

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            updates++;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            currState = Mouse.GetState().LeftButton;

            if (Vector2.Distance(new Vector2(monkeys[0].Pos.X, monkeys[0].Pos.Y), new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y)) < 100 && Mouse.GetState().LeftButton == ButtonState.Pressed && prevState == ButtonState.Released && dragging == false)
            {
                monkeys.Add(new Player(monkeyImage, monkeys[0].Pos, Color.White, 0, new Vector2(monkeyImage.Width / 2, monkeyImage.Height / 2), MonkeySource, 0, 0, 5));
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
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && illegalPos == false)
            {
                dragging = false;
                monkeys[monkeys.Count - 1].Placed = true;
            }

            if (Vector2.Distance(new Vector2(monkeys[monkeys.Count - 1].Pos.X, monkeys[monkeys.Count - 1].Pos.Y), new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y)) < 20)
            {

                for (int i = 0; i < path.Count; i++)
                {
                    //Add red highlighting for whenever illegal monkey is trying to be placed

                    if (monkeys[monkeys.Count - 1].Hitbox.Intersects(path[i].Hitbox()))
                    {
                        monkeys[monkeys.Count - 1].Color = Color.Red;
                        wrongPath = i;
                        illegalPos = true;
                    }
                    else if (!monkeys[monkeys.Count - 1].Hitbox.Intersects(path[wrongPath].Hitbox()))
                    {
                        monkeys[monkeys.Count - 1].Color = Color.White;
                        illegalPos = false;
                    }
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

            for (int i = 1; i < monkeys.Count; i++)
            {
                if (monkeys[i].Hitbox.Contains(Mouse.GetState().Position) && currState == ButtonState.Pressed)
                {
                    selectedMonkey = monkeys[i];
                }

            }
            if (sellButton.Contains(Mouse.GetState().Position) && currState == ButtonState.Pressed && prevState == ButtonState.Released)
            {
                if (monkeys.Contains(selectedMonkey))
                {
                    monkeys.Remove(selectedMonkey);
                    money += 100;
                    selectedMonkey = monkeys[0];
                }

            }

            for (int i = 0; i < bloons.Count; i++)
            {
                bloons[i].Update();

                //if (timer - bloons[i].LastUpdated > bloons[i].Speed)
                //{
                //    bloons[i].LastUpdated = timer;

                //    bloons[i].Pos = new Rectangle(path[bloons[i].PathPosition].Value.ToPoint(), bloons[i].Pos.Size);


                //    if (bloons[i].PathPosition < path.Count - 1)
                //    {
                //        bloons[i].PathPosition++;
                //    }
                //}
            }
            //for (int j = 0; j < monkeys.Count; j++)
            //{

            //    if (monkeys[j].Placed == true)
            //    {
            //        if (projectiles.Count > 1)
            //        {
            //            projectiles.RemoveAt(projectiles.Count - 1);
            //        }
            //        double speed = Projectile.FindSpeed(monkeys[j].Pos.Location.ToVector2(), bloons[i].Pos.Location.ToVector2(), 20);
            //        projectiles.Add(new Projectile(dartImage, new Rectangle(monkeys[j].Pos.X, monkeys[j].Pos.Y, 25, 50), Color.Black, 0, Vector2.Zero, monkeys[j].Damage, speed, monkeys[j]));
            //        for (int k = 0; k < projectiles.Count; k++)
            //        {
            //            projectiles[k].Lerp(projectiles[k].ThrownFrom.Pos.Location, bloons[i].Pos.Location, projectiles[k].Scalar);
            //            for (int a = 0; a < bloons.Count; a++)
            //            {
            //                if (projectiles[k].Pos.Intersects(bloons[a].Pos))
            //                {
            //                    projectiles.RemoveAt(k);
            //                    bloons.RemoveAt(a);
            //                }
            //            }

            //        }
            //    }
            //}



          

            prevState = currState;

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

            for (int i = 0; i < pathDrawDelay; i++)
            {
                spriteBatch.FillRectangle(new Rectangle(path[i].Value.X, path[i].Value.Y, Game1.size, Game1.size), Color.White, 0);
            }

            if (pathDrawDelay < path.Count)
            {
                pathDrawDelay++;
            }

            moneyIMG.Draw(spriteBatch);

            healthIMG.Draw(spriteBatch);
            Console.WriteLine(monkeys[0].RectIndex);

            for (int i = 0; i < monkeys.Count; i++)
            {
                monkeys[i].Draw(spriteBatch);
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

            //for (int i = 0; i < path.Count; i++)
            //{
            //    Fix next time to draw texture tiles
            //    spriteBatch.FillRectangle(new Rectangle(path[i].Value.X, path[i].Value.Y, 50, 50), Color.White, 0);
            //}

            //Draws All Projectiles
            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Draw(spriteBatch);
            }

            //Draws Hitboxes
            //spriteBatch.DrawRectangle(monkeys[monkeys.Count - 1].Hitbox, Color.Yellow, 1, 0);
            spriteBatch.DrawRectangle(selectedMonkey.Hitbox, Color.Black, 1, 0);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}