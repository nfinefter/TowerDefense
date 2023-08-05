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
        public int AtkDelay = 500;

        bool Display = false;
        public override Vector2 Origin => Vector2.Zero;

        public override Rectangle? SourceRectangle => new Rectangle(0, 0, 0, 0);

        public KillerEnemy(Texture2D tex, Rectangle pos, Color color, float rotation, Vector2 origin, int difficulty, int rank)
            : base(tex, pos, color, rotation, origin, difficulty, rank)
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
                }
            }
        }

        public void KillMonkey(int index)
        {
            ProjectileRemover(GameScreen.Monkeys[index]);
            GameScreen.Monkeys.RemoveAt(index);
            Display = !Display;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Display)
            {
                spriteBatch.Draw(Tex, Pos, SourceRectangle, Color, Rotation, Origin, SpriteEffects.None, 0);
                Display = !Display;
            }
        }


    }
}
