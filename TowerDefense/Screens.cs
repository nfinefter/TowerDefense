using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using WeightedDirectedGraphs;
using MonoGame.Extended;

namespace TowerDefense
{

    public class EndScreen : Screen
    {
        string loseMsg = "You lose";
        Button RestartButton;
        Button ExitButton;
        public EndScreen()
        {
            RestartButton = new Button(Game1.pixel, new Rectangle((GameScreen.GameDimensions.X + GameScreen.GameDimensions.Width) / 2, (GameScreen.GameDimensions.Y + GameScreen.GameDimensions.Height) / 2, 100, 100), Color.Green, 0, "Restart", default);
            ExitButton = new Button(Game1.pixel, new Rectangle((GameScreen.GameDimensions.X + GameScreen.GameDimensions.Width) / 2, (GameScreen.GameDimensions.Y + GameScreen.GameDimensions.Height) / 2 + 200, 100, 100), Color.Red, 0, "Exit", default);
        }
        public override void Draw(SpriteBatch graphics)
        {
            RestartButton.Draw(graphics);
            ExitButton.Draw(graphics);
            graphics.DrawString(GameScreen.Font, loseMsg, new Vector2((GameScreen.GameDimensions.X + GameScreen.GameDimensions.Width) / 2, (GameScreen.GameDimensions.Y + GameScreen.GameDimensions.Height) / 2), Color.Black);
        }
        public override Screenum Update(GameTime gameTime)
        {
            if (RestartButton.Pos.Contains(Mouse.GetState().Position) && ScreenManager.Instance.CurrentState == ButtonState.Pressed && ScreenManager.Instance.PrevState == ButtonState.Released)
            {
                return Screenum.Game;
            }
            else if (ExitButton.Pos.Contains(Mouse.GetState().Position) && ScreenManager.Instance.CurrentState == ButtonState.Pressed && ScreenManager.Instance.PrevState == ButtonState.Released)
            {
                return Screenum.Menu;
            }
            return Screenum.End;
        }
    }
    public class MenuScreen : Screen
    {
        Button StartButton;
        Button DontStartButton;

        public MenuScreen()
        {
            StartButton = new Button(Game1.pixel, new Rectangle((GameScreen.GameDimensions.X + GameScreen.GameDimensions.Width) / 2, (GameScreen.GameDimensions.Y + GameScreen.GameDimensions.Height) / 2, 100, 100), Color.Green, 0, "Start", default);
            DontStartButton = new Button(Game1.pixel, new Rectangle((GameScreen.GameDimensions.X + GameScreen.GameDimensions.Width) / 2, (GameScreen.GameDimensions.Y + GameScreen.GameDimensions.Height) / 2 + 200, 100, 100), Color.Red, 0, "Back", default);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            StartButton.Draw(spriteBatch);
            DontStartButton.Draw(spriteBatch);
            spriteBatch.DrawString(GameScreen.Font, "Start", new Vector2(StartButton.Pos.X, StartButton.Pos.Y), Color.Black);
            spriteBatch.DrawString(GameScreen.Font, "Don't Start", new Vector2(DontStartButton.Pos.X, DontStartButton.Pos.Y), Color.Black);
        }
        public override Screenum Update(GameTime gameTime)
        {
            if (StartButton.Pos.Contains(Mouse.GetState().Position) && ScreenManager.Instance.CurrentState == ButtonState.Pressed && ScreenManager.Instance.PrevState == ButtonState.Released)
            {
                return Screenum.Game;
            }
            return Screenum.Menu;
        }
    }
    public class GameScreen : Screen
    {
        public static List<Player> Monkeys = new List<Player>();
        public static List<EnemyBase> Bloons = new List<EnemyBase>();

        //public KillerEnemy IscariotBloon;
        public Player SelectedMonkey;

        public static int size = 40;
        public static float Money = 1000;
        public static int Health = 100;
        public static Player Saint;
        bool dragging = false;

        public Texture2D JesusImage;
        public Texture2D BloonImage;
        public Texture2D DartImage;
        public Texture2D PopeSoldierImage;
        public Sprite MoneyIMG;
        public Sprite HealthIMG;
        public List<Rectangle> MonkeySource;
        bool illegalPos = false;
        public static List<Vertex<System.Drawing.Point>> Path = new List<Vertex<System.Drawing.Point>>();
        public static Rectangle GameDimensions;
        int pathDrawDelay = 0;
        public static Rectangle Start;
        public static SpriteFont Font;

        public int MapXBorder;
        public Map map = new Map();



        public void Initialize()
        {
            MapXBorder = Game1.GameWidth - 250;

            Path = map.GeneratePath(MapXBorder, Game1.GameHeight);

            GameDimensions = new Rectangle(MapXBorder, 0, MapXBorder, Game1.GameHeight);

            Start = new Rectangle(new Microsoft.Xna.Framework.Point(Path[0].Value.X, Path[0].Value.Y), new Point(40, 40));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
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

            for (int i = 0; i < Sprites.Count; i++)
            {
                Sprites[i].Draw(spriteBatch);
            }
            for (int i = 0; i < Buttons.Count; i++)
            {
                Buttons[i].Draw(spriteBatch);
                spriteBatch.DrawString(Font, "Sell", new Vector2(Buttons[0].Pos.X + 20, Buttons[0].Pos.Y + 20), Color.Black);
                spriteBatch.DrawString(Font, "Upgrade", new Vector2(Buttons[1].Pos.X + 20, Buttons[1].Pos.Y + 20), Color.Black);
            }

            spriteBatch.DrawString(Font, $"{Money}", new Vector2(MoneyIMG.Pos.X + 50, MoneyIMG.Pos.Y + 20), Color.Black);
            spriteBatch.DrawString(Font, $"{Health}", new Vector2(HealthIMG.Pos.X + 50, HealthIMG.Pos.Y + 20), Color.Black);
            spriteBatch.DrawString(Font, "Cost :100", new Vector2(Monkeys[0].Pos.X + 50, Monkeys[0].Pos.Y + 20), Color.Black);


            for (int i = 0; i < pathDrawDelay; i++)
            {
                spriteBatch.FillRectangle(new Rectangle(Path[i].Value.X, Path[i].Value.Y, GameScreen.size, GameScreen.size), Color.White, 0);
            }
            if (pathDrawDelay < Path.Count)
            {
                pathDrawDelay++;
            }
            for (int i = 0; i < Monkeys.Count; i++)
            {
                Monkeys[i].Draw(spriteBatch);
                if (Monkeys[i].Dot != null)
                {
                    Monkeys[i].Dot.DrawRectangle(spriteBatch);
                }
            }

            spriteBatch.DrawLine(new Vector2(MapXBorder, 0), new Vector2(MapXBorder, Game1.GameHeight), Color.Black, 1, 0);

            for (int i = 0; i < map.Graph.Count; i++)
            {
                spriteBatch.DrawPoint(map.Graph[i].Value.X, map.Graph[i].Value.Y, Color.Black);
            }

            for (int i = 0; i < Bloons.Count; i++)
            {
                //spriteBatch.Draw(Bloons[i].Tex, Bloons[i].Pos, Bloons[i].Color);
                Bloons[i].Draw(spriteBatch);
            }
            spriteBatch.DrawRectangle(SelectedMonkey.Hitbox, Color.Black, 1, 0);


            DrawProjectiles();
        }
        public override Screenum Update(GameTime gameTime)
        {

            if (Health <= 99)
            {
                return Screenum.End;
            }

            for (int i = 0; i < Buttons.Count; i++)
            {
                Buttons[i].Update(gameTime);
            }

            for (int i = 1; i < Monkeys.Count; i++)
            {
                if (Monkeys[i].Hitbox.Contains(Mouse.GetState().Position) && ScreenManager.Instance.CurrentState == ButtonState.Pressed)
                {
                    SelectedMonkey = Monkeys[i];
                }
            }

            for (int i = 1; i < Monkeys.Count; i++)
            {
                Monkeys[i].FindTarget(Bloons);
                Monkeys[i].Update(gameTime);
            }

            if (Vector2.Distance(new Vector2(Monkeys[0].Pos.X, Monkeys[0].Pos.Y), new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y)) < 100 && Mouse.GetState().LeftButton == ButtonState.Pressed && ScreenManager.Instance.PrevState == ButtonState.Released && dragging == false && Money >= 100)
            {
                Monkeys.Add(new Player(JesusImage, Monkeys[0].Pos, Color.White, 0, new Vector2(JesusImage.Width / 2, JesusImage.Height / 2), MonkeySource, 0, 0, 5, 25));
                Money -= 100;

                dragging = true;

                if (Mouse.GetState().LeftButton != ButtonState.Pressed)
                {
                    dragging = false;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && illegalPos == false)
            {
                SelectedMonkey.CreateDot();
                dragging = false;
                Monkeys[Monkeys.Count - 1].Placed = true;
            }
            if (dragging)
            {
                Monkeys[Monkeys.Count - 1].Pos = new Rectangle(Mouse.GetState().Position, new Point(Monkeys[0].Pos.Width, Monkeys[0].Pos.Height));
                PlaceMonke();
            }

            for (int i = 0; i < Bloons.Count; i++)
            {
                Bloons[i].Update(gameTime);
                //IscariotBloon.Update(gameTime);
            }

            //Player.CheckKill(ref Bloons);

            //Func<Projectile, bool> dartPredicate = m =>
            //{
            //    var bloonCount = Bloons.Count;
            //    Player.CheckKill(ref Bloons);
            //    if (bloonCount > Bloons.Count)
            //    {
            //        return true;
            //    }
            //    return false;
            //};

            for (int i = 0; i < Bloons.Count; i++)
            {
                if (EnemyManager.IsAttacked(Bloons[i]))
                {
                    Bloons.RemoveAt(i);
                }
            }
            for (int i = 0; i < Player.projectiles.Count; i++)
            {
                Player.projectiles[i].Update(gameTime);
            }

            EnemyManager.EnemyCreate(gameTime, ref Bloons);

            return Screenum.Game;
        }

        public void Sell()
        {
            if (Monkeys.Contains(SelectedMonkey) && Monkeys.Count > 1)
            {
                if (SelectedMonkey.Pos != Saint.Pos && !dragging)
                {
                    Monkeys.Remove(SelectedMonkey);
                    Money += (100 * SelectedMonkey.Level) + 100;
                    SelectedMonkey = Monkeys[0];
                }
            }
        }
        public void Upgrade()
        {
            if (SelectedMonkey != Monkeys[0])
            {
                if (Monkeys.Contains(SelectedMonkey) && Monkeys.Count > 0 && SelectedMonkey.Level < 5 && !dragging && Money >= 100)
                {
                    Money -= 100;
                    SelectedMonkey.Level++;
                    PlayerManager.UpgradeDot(SelectedMonkey);
                }
            }
        }
        void PlaceMonke()
        {
            illegalPos = false;
            Monkeys[Monkeys.Count - 1].Color = Color.White;
            if (Vector2.Distance(new Vector2(Monkeys[Monkeys.Count - 1].Pos.X, Monkeys[Monkeys.Count - 1].Pos.Y), new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y)) < 20)
            {
                for (int i = 0; i < Path.Count; i++)
                {
                    if (Monkeys[Monkeys.Count - 1].Hitbox.Intersects(Path[i].Hitbox()))
                    {
                        Monkeys[Monkeys.Count - 1].Color = Color.Red;
                        illegalPos = true;
                    }
                }
            }
            if (Monkeys[Monkeys.Count - 1].Hitbox.Intersects(GameDimensions) || !new Rectangle(0, 0, Game1.GameWidth, Game1.GameHeight).Contains(Monkeys[Monkeys.Count - 1].Hitbox))
            {
                Monkeys[Monkeys.Count - 1].Color = Color.Red;
                illegalPos = true;
            }
            for (int j = 0; j < Monkeys.Count - 1; j++)
            {
                if (Monkeys[Monkeys.Count - 1].Hitbox.Intersects(Monkeys[j].Hitbox))
                {
                    Monkeys[Monkeys.Count - 1].Color = Color.Red;
                    illegalPos = true;
                }
            }
        }

    }

}
