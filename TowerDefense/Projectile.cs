using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public sealed class Projectile : ProjectileBase
    {
        public override Rectangle? SourceRectangle => throw new NotImplementedException();

        public override Vector2 Origin => throw new NotImplementedException();

        public Projectile(Texture2D tex, Rectangle pos, Color color, float rotation, Vector2 origin, int damage, int speed)
            : base(tex, pos, color, rotation, origin, damage, speed)
        {
        }

        public override void Update()
        {
               //Do dart projectile stuff trig         
        }

    }
}
