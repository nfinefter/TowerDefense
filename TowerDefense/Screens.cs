using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assimp;
using System.IO;
using WeightedDirectedGraphs;
using static System.Reflection.Metadata.BlobBuilder;
using MonoGame.Extended;
using System.Collections;

namespace TowerDefense
{
    public class MenuScreen : Screen
    {
        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public override Screenum Update(GameTime gameTime)
        {
            if (Buttons[0].Pos.Contains(Mouse.GetState().Position) && ScreenManager.Instance.CurrentState == ButtonState.Pressed && ScreenManager.Instance.PrevState == ButtonState.Released)
            {
                return Screenum.Game;
            }
            return Screenum.Menu;
        }
    }
    public class GameScreen : Screen
    {
        public List<Player> Monkeys = new List<Player>();
        public List<Enemy> Bloons = new List<Enemy>();

        public Enemy IscariotBloon;
        public Player SelectedMonkey;

        static public float Money = 1000;
        static public int Health = 100;
        bool dragging = false;

        public Texture2D JesusImage;
        public Texture2D BloonImage;
        public Texture2D DartImage;
        public Sprite MoneyIMG;
        public Sprite HealthIMG;
        public List<Rectangle> MonkeySource;
        public Player Saint;
        bool illegalPos = false;
        public static List<Vertex<System.Drawing.Point>> Path = new List<Vertex<System.Drawing.Point>>();
        public Rectangle GameDimensions;
        public int GameWidth;
        public int GameHeight;
        public int GameX;
        public int GameY;
        int pathDrawDelay = 0;
        public static Rectangle Start;
        public SpriteFont Font;
        bool killing;
        TimeSpan bloonKillerDelay = TimeSpan.FromSeconds(5);
        TimeSpan timer;

        public int MapXBorder;
        public Map map = new Map();

        public void Initialize()
        {
            MapXBorder = GameWidth - 250;

            Path = map.GeneratePath(MapXBorder, GameHeight);

            GameDimensions = new Rectangle(MapXBorder, 0, MapXBorder, GameHeight);

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
            }

            spriteBatch.DrawString(Font, $"{Money}", new Vector2(MoneyIMG.Pos.X + 50, MoneyIMG.Pos.Y + 20), Color.Black);
            spriteBatch.DrawString(Font, $"{Health}", new Vector2(HealthIMG.Pos.X + 50, HealthIMG.Pos.Y + 20), Color.Black);
            spriteBatch.DrawString(Font, "Cost :100", new Vector2(Monkeys[0].Pos.X + 50, Monkeys[0].Pos.Y + 20), Color.Black);

            for (int i = 0; i < pathDrawDelay; i++)
            {
                spriteBatch.FillRectangle(new Rectangle(Path[i].Value.X, Path[i].Value.Y, Game1.size, Game1.size), Color.White, 0);
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

            spriteBatch.DrawLine(new Vector2(MapXBorder, 0), new Vector2(MapXBorder, GameHeight), Color.Black, 1, 0);

            for (int i = 0; i < map.Graph.Count; i++)
            {
                spriteBatch.DrawPoint(map.Graph[i].Value.X, map.Graph[i].Value.Y, Color.Black);
            }

            for (int i = 0; i < Bloons.Count; i++)
            {
                spriteBatch.Draw(Bloons[i].Tex, Bloons[i].Pos, Bloons[i].Color);
            }
            spriteBatch.DrawRectangle(SelectedMonkey.Hitbox, Color.Black, 1, 0);

            for (int i = 0; i < Buttons.Count; i++)
            {
                Buttons[i].Draw(spriteBatch);
            }

            if (killing)
            {
                IscariotBloon.Draw(spriteBatch);
            }

            DrawProjectiles();
        }

        public override Screenum Update(GameTime gameTime)
        {
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
            }

            Player.CheckKill(ref Bloons);

            for (int i = 0; i < Bloons.Count; i++)
            {
                if (EnemyManager.IsAttacked(Bloons[i]))
                {
                    Bloons.RemoveAt(i);
                }
            }

            killing = MonkeyKill(Monkeys, killing);

            EnemyManager.EnemyCreate(gameTime, ref Bloons);

            return Screenum.Game;
        }

        public void Sell()
        {
            if (Monkeys.Contains(SelectedMonkey) && Monkeys.Count > 1)
            {
                ProjectileRemover(SelectedMonkey);
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
                if (Monkeys.Contains(SelectedMonkey) && Monkeys.Count > 0 && SelectedMonkey.Level < 5 && !dragging)
                {
                    SelectedMonkey.DmgMultiplier++;
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
                //for (int i = 0; i < Path.Count; i++)
                //{
                //    if (Monkeys[Monkeys.Count - 1].Hitbox.Intersects(Path[i].Hitbox()))
                //    {
                //        Monkeys[Monkeys.Count - 1].Color = Color.Red;
                //        illegalPos = true;
                //    }
                //}
            }
            if (Monkeys[Monkeys.Count - 1].Hitbox.Intersects(GameDimensions) || !new Rectangle(0, 0, GameWidth, GameHeight).Contains(Monkeys[Monkeys.Count - 1].Hitbox))
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
        public static bool MonkeyKill(List<Player> monkeys, bool killing)
        {
            if (killing) return true;

            Random rand = new Random();

            if (rand.Next(1, 1001) == 100)
            {
                if (monkeys.Count > 2)
                {
                    int i = rand.Next(1, monkeys.Count);

                    if (!monkeys[i].Placed) return false;

                    ProjectileRemover(monkeys[i]);
                    monkeys.RemoveAt(i);
                }
                return true;
            }
            return false;
        }


    }

}
