using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SharpFont.Cache;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeightedDirectedGraphs;

namespace TowerDefense
{
    public sealed class Enemy : EnemyBase
    {
        TimeSpan moveTime = TimeSpan.FromSeconds(1);
        TimeSpan moveWait;

        public List<Vertex<System.Drawing.Point>> Path;
        public override Rectangle? SourceRectangle => null;

        private Vector2 origin;

        public override Vector2 Origin => origin;

        public Enemy(Texture2D tex, Rectangle pos, Color color, float rotation, Vector2 origin, int difficulty, List<Vertex<System.Drawing.Point>> path, int rank)
            : base(tex, pos, color, rotation, origin, difficulty, rank)
        {
            this.origin = origin;
            Path = path;
            EnemyManager.RankCheck(this);
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

