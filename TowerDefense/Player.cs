using Assimp.Configs;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public sealed class Player : PlayerBase
    {
        TimeSpan shootTime = TimeSpan.FromSeconds(1);
        TimeSpan shootWait;

        private Enemy Target;

        public static List<Projectile> projectiles;

        public override Vector2 Origin => new Vector2(SourceRectangle.Value.Width / 2, SourceRectangle.Value.Height / 2);

        public Rectangle Hitbox => new Rectangle(Pos.X - (Pos.Width / 2), Pos.Y - Pos.Height / 2, Pos.Width, Pos.Height);

        public bool Placed = false;
        public Player(Texture2D tex, Rectangle pos, Color color, float rotation, Vector2 origin, List<Rectangle> sourceRectangle, int level, int xp, int dmgMultiplier, int range)
            : base(tex, pos, color, rotation, origin, sourceRectangle, level, xp, dmgMultiplier, range)
        {
            shootWait = TimeSpan.Zero;
        }

        //x=(1-s)a+sb

        public override void Update(GameTime time)
        {
            base.Update(time);

            shootWait += time.ElapsedGameTime;
            
            if (shootWait > shootTime)
            {
                //dostuff----> Move projectile
                //Never gets to here

                projectiles.Add(AddProjectile());

                shootWait = TimeSpan.Zero;
            }

        }
        public Projectile AddProjectile()
        {
            ContentManager manager = ContentManager.Instance;

            float slope = (Pos.Y - Target.Pos.Y) / (Pos.X - Target.Pos.X);
           
            float rotation = MathF.Atan(slope);
            
            if (float.IsPositiveInfinity(slope))
            {
                rotation = 0;
            }
            else if (float.IsNegativeInfinity(slope))
            {
                rotation = MathF.PI;
            }
           
            return new Projectile(manager[Textures.Dart], Pos, Color.Black, rotation, Vector2.Zero, Damage, Projectile.FindSpeed(Pos.Location.ToVector2(), Target.Pos.Location.ToVector2(), 20), this);
        }
        
    }
}
