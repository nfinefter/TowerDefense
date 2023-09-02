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
            if (Pos.X  < 0 || Pos.Y < 0 || Pos.X > GameScreen.GameDimensions.X + GameScreen.GameDimensions.Width|| Pos.Y > GameScreen.GameDimensions.Y + GameScreen.GameDimensions.Height)
            {
                Player.projectiles.Remove(this);
            }

            double yLength = Math.Sin(Rotation) * 20;
            double xLength = Math.Cos(Rotation) * 20;

            Pos.X += (int)xLength;
            Pos.Y += (int)yLength;
        }

        public void DamageCalc()
        {
            Damage = ThrownFrom.DmgMultiplier * 5;
        }
    }
}
