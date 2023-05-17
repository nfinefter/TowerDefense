using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeightedDirectedGraphs;

namespace TowerDefense
{
    sealed class Enemy : EnemyBase
    {
        TimeSpan moveTime = TimeSpan.FromSeconds(1);
        TimeSpan moveWait;

        private int dmgTaken;

        List<Vertex<System.Drawing.Point>> Path;
        public override Rectangle? SourceRectangle => null;

        private Vector2 origin;

        public override Vector2 Origin => origin;

        public Enemy(Texture2D tex, Rectangle pos, Color color, float rotation, Vector2 origin, int difficulty, int speed, List<Vertex<System.Drawing.Point>> path)
            : base(tex, pos, color, rotation, origin, difficulty, speed)
        {
            this.origin = origin;
            Path = path;
        }

        public void Attacked(int dmg)
        {
            dmgTaken = dmg;
        }

        public override void Update(GameTime time)
        {
            //Do health stuff

            //Health -= dmgTaken;

            //if (MaxHealth % Health >= 10)
            //{
            //    Difficulty--;
            //}

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

