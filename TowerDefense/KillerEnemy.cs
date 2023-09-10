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
        public int DrawDelay = 50;
        public int DrawCounter = 0;
        public int AtkDelay = 2000;
        bool dartShot = false;
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

            if (Monkeys.Count > 1)
            {
                int i = rand.Next(1, Monkeys.Count);

                if (Monkeys[i].Placed)     
                {
                    FindTarget();
                    if (!dartShot)
                    {
                        AddProjectile();
                        dartShot = true;
                    }
                    DrawPope = true;
                }
            }
            base.Update(gameTime);
        }

        public bool DartPredicate(Projectile me)
        {
            if (me.Pos.Intersects(GameDimensions)) return true;
            if (me.Pos.X < 0 || me.Pos.Y < 0 || me.Pos.X > GameDimensions.X + GameDimensions.Width || me.Pos.Y > GameDimensions.Y + GameDimensions.Height) return true;

            for (int i = 0; i < Monkeys.Count; i++)
            {
                if (me.Pos.Intersects(Monkeys[i].Pos))
                {
                    KillMonkey(i);
                    return true;
                }
            }
            return false;
        }
        public void FindTarget()
        {
            if (Monkeys.Count <= 1)
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
            if (Target == Saint)
            {
                Target = null;
            }
        }
        public void KillMonkey(int index)
        {
            Monkeys.RemoveAt(index);
        }
        public void AddProjectile()
        {
            if (Target == null)
            {
                return;
            }

            ContentManager manager = ContentManager.Instance;

            float offset = 0;
            if (Pos.X == Target.Pos.X)
            {
                offset = .1f;
            }

            float rotation = MathF.Atan2(Target.Pos.Y - Pos.Y, Target.Pos.X + offset - Pos.X);

            Player.projectiles.Add(new Projectile(manager[Textures.Dart], Pos, Color.Black, rotation, Vector2.Zero, 0, 20, DartPredicate));
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (PathPosition >= Path.Count - 1) return;

            if (DrawPope && DrawCounter <= DrawDelay)
            {
                DrawCounter++;
                //spriteBatch.Draw(ContentManager.Instance[Textures.PopeSoldier], new Rectangle(0, 0, ContentManager.Instance[Textures.PopeSoldier].Width, ContentManager.Instance[Textures.PopeSoldier].Height), Color.White);
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
