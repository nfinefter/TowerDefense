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
        private Rectangle? sourceRectangle = null;

        public override Rectangle? SourceRectangle => sourceRectangle;

        private Vector2 origin;
        public override Vector2 Origin => origin;

        public Projectile(Texture2D tex, Rectangle pos, Color color, float rotation, Vector2 origin, int damage, double speed, Player thrownFrom)
            : base(tex, pos, color, rotation, origin, damage, speed, thrownFrom)
        {
            this.origin = origin;
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
