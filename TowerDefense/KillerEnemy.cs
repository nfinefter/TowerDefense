using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public sealed class KillerEnemy : EnemyBase
    {

        public int AtkDelay = 2000;
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

        public void KillMonkey(int index)
        {
            ProjectileRemover(GameScreen.Monkeys[index]);
            GameScreen.Monkeys.RemoveAt(index);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (PathPosition >= Path.Count - 1) return;

            if (DrawPope)
            {
                spriteBatch.Draw(ContentManager.Instance[Textures.PopeSoldier], new Rectangle(0, 0, ContentManager.Instance[Textures.PopeSoldier].Width, ContentManager.Instance[Textures.PopeSoldier].Height), Color.White);
                DrawPope = !DrawPope;
            }

            base.Draw(spriteBatch);
            
        }

    }
}
