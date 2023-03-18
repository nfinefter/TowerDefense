using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    sealed class Enemy : EnemyBase
    {
        private int dmgTaken;

        public Enemy(Texture2D tex, Rectangle pos, Color color, float rotation, Vector2 origin, int maxHealth, int health, int speed)
            : base(tex, pos, color, rotation, origin, maxHealth, health, speed)
        {
        }

        public override Rectangle? SourceRectangle => throw new NotImplementedException();

        public void Attacked(int dmg)
        {
            dmgTaken = dmg;
        }

        public override void Update()
        {
            //Do health stuff

            Health -= dmgTaken;

        }
    }
}
