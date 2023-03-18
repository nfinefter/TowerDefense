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

        public override Rectangle? SourceRectangle => throw new NotImplementedException();

        public Enemy(Texture2D tex, Rectangle pos, Color color, float rotation, Vector2 origin, int difficulty, int health, int speed)
            : base(tex, pos, color, rotation, origin, difficulty, health, speed)
        {

        }

        public void Attacked(int dmg)
        {
            dmgTaken = dmg;
        }

        public override void Update()
        {
            //Do health stuff

            Health -= dmgTaken;

            if (MaxHealth % Health >= 10)
            {
                Difficulty--;            
            }
        }
    }
}
