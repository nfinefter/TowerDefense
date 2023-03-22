using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public abstract class ProjectileBase : SpriteBase
    {
        public int Damage;
        public int Speed;

        protected ProjectileBase(Texture2D tex, Rectangle pos, Color color, float rotation, Vector2 origin, int damage, int speed)
            : base(tex, pos, color, rotation, origin)
        {
            Damage = damage;
            Speed = speed;
        }
    }
}
