using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static TowerDefense.GameScreen;
namespace TowerDefense
{
    public sealed class KillerEnemy : EnemyBase
    {
        public int DrawDelay = 20;
        public int DrawCounter = 0;
        public int AtkDelay = 2000;

        private Player Target;
        public override Vector2 Origin => Vector2.Zero;
        public bool DrawPope = false;
        public override Rectangle? SourceRectangle => null;
        public KillerEnemy(Texture2D tex, Rectangle pos, Color color, float rotation, Vector2 origin, int difficulty, List<Vertex<System.Drawing.Point>> path, int rank)
            : base(tex, pos, color, rotation, origin, difficulty, path, rank)
        {

        }

        public override void Update(GameTime gameTime)
        {
            Random rand = new Random();

            if (GameScreen.Monkeys.Count > 1 && rand.Next(1, AtkDelay) == 100)
            {
                int i = rand.Next(1, GameScreen.Monkeys.Count);

                if (GameScreen.Monkeys[i].Placed)
                {
                    KillMonkey(i);
                    DrawPope = true;
                }
            }
            base.Update(gameTime);
        }

        public bool DartPredicate(Projectile me)
        {
            FindTarget();
            if (me.Pos.X < 0 || me.Pos.Y < 0 || me.Pos.X > GameDimensions.X + GameDimensions.Width || me.Pos.Y > GameDimensions.Y + GameDimensions.Height) return true;

            for (int i = 0; i < Monkeys.Count; i++)
            {
                if (me.Pos.Intersects(Monkeys[i].Pos))
                {
                    Monkeys.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
        public void FindTarget()
        {
            if (Monkeys.Count == 0)
            {
                Target = null;
                return;
            }

            float smallest = Vector2.Distance(Monkeys[0].Pos.Location.ToVector2(), Pos.Location.ToVector2());
            int index = 0;

            for (int i = 1; i < Monkeys.Count; i++)
            {
                float dist = Vector2.Distance(Monkeys[i].Pos.Location.ToVector2(), Pos.Location.ToVector2());

                if (smallest > dist)
                {
                    smallest = dist;
                    index = i;
                }
            }
            Target = Monkeys[index];
        }
        public void KillMonkey(int index)
        {
            GameScreen.Monkeys.RemoveAt(index);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (PathPosition >= Path.Count - 1) return;

            if (DrawPope && DrawCounter <= DrawDelay)
            {
                DrawCounter++;
                spriteBatch.Draw(ContentManager.Instance[Textures.PopeSoldier], new Rectangle(0, 0, ContentManager.Instance[Textures.PopeSoldier].Width, ContentManager.Instance[Textures.PopeSoldier].Height), Color.White);
            }
            else
            {
                DrawCounter = 0;
                DrawPope = false;
            }

            base.Draw(spriteBatch);
            
        }

    }
}
