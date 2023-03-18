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
        public int MaxHealth;
        public int Health;
        public int Speed;
        public int Difficulty;

        protected EnemyBase(Texture2D tex, Rectangle pos, Color color, float rotation, Vector2 origin, int difficulty, int health, int speed) 
            : base(tex, pos, color, rotation, origin)
        {
            Health = health;
            Speed = speed;
            Difficulty = difficulty;
            MaxHealth = Difficulty * 10;
        }
    }
}
