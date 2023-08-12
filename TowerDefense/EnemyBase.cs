

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public abstract class EnemyBase : SpriteBase
    {
        TimeSpan moveTime = TimeSpan.FromSeconds(1);
        TimeSpan moveWait;

        public int Speed;
        public int Difficulty;
        public int LastUpdated = 0;
        public int PathPosition = 0;
        public int Rank;

        public List<Vertex<System.Drawing.Point>> Path;


        private Vector2 origin;

        public override Vector2 Origin => origin;

        protected EnemyBase(Texture2D tex, Rectangle pos, Color color, float rotation, Vector2 origin, int difficulty, List<Vertex<System.Drawing.Point>> path, int rank) 
            : base(tex, pos, color, rotation)
        {
            this.origin = origin;
            Path = path;
            Difficulty = difficulty;
            Rank = rank;
        }

        public override void Update(GameTime time)
        {
            TimeSpan speed = TimeSpan.FromMilliseconds(Speed);

            moveWait += time.ElapsedGameTime;

            if (moveWait > moveTime - speed)
            {
                Pos = new Rectangle(Path[PathPosition].Value.ToPoint(), Pos.Size);

                if (PathPosition < Path.Count - 1)
                {
                    PathPosition++;
                }

                moveWait = TimeSpan.Zero;
            }


        }
    }
}
