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
        public Vector2 Speed;

        protected ProjectileBase(Texture2D tex, Rectangle pos, Color color, float rotation, Vector2 origin, int damage, Vector2 speed)
            : base(tex, pos, color, rotation)
        {
            Damage = damage;
            Speed = speed;
        }
    }
}
