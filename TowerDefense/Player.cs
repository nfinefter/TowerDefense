using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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

        public Sprite Dot;

        private Enemy Target;

        public static List<Projectile> projectiles = new List<Projectile>();

        public override Vector2 Origin => new Vector2(SourceRectangle.Value.Width / 2, SourceRectangle.Value.Height / 2);

        public Rectangle Hitbox => new Rectangle(Pos.X - (Pos.Width / 2), Pos.Y - Pos.Height / 2, Pos.Width, Pos.Height);

        public bool Placed = false;
        public Player(Texture2D tex, Rectangle pos, Color color, float rotation, Vector2 origin, List<Rectangle> sourceRectangle, int level, int xp, int dmgMultiplier, int range)
            : base(tex, pos, color, rotation, origin, sourceRectangle, level, xp, dmgMultiplier, range)
        {
            shootWait = TimeSpan.Zero;
        }

        public override void Update(GameTime time)
        {
            if (!Placed) return;

            base.Update(time);

            shootWait += time.ElapsedGameTime;
            
            if (shootWait > shootTime)
            {
                projectiles.Add(AddProjectile());

                shootWait = TimeSpan.Zero;
            }
            for (int i = 0; i < projectiles.Count; i++)
            {
                if (projectiles[i].ThrownFrom == this)
                {

                    double yLength = Math.Sin(projectiles[i].Rotation) * 20;
                    double xLength = Math.Cos(projectiles[i].Rotation) * 20;

                    projectiles[i].Pos.X += (int)xLength;
                    projectiles[i].Pos.Y += (int)yLength;
                }
            }

            

        }
        public Projectile AddProjectile()
        {
            ContentManager manager = ContentManager.Instance;

            float offset = 0;
            if (Pos.X == Target.Pos.X)
            {
                offset = .1f;
            }

            float rotation = MathF.Atan2(Target.Pos.Y - Pos.Y, Target.Pos.X + offset - Pos.X); 

       //     rotation -= MathHelper.ToRadians(90);

            return new Projectile(manager[Textures.Dart], Pos, Color.Black, rotation, Vector2.Zero, Damage, 20, this);
        }

        public void FindTarget(List<Enemy> enemies)
        {
            if (!Placed) return;

            if (enemies.Count == 0)
            {
                //Win;
                return;
            }

            float smallest = Vector2.Distance(enemies[0].Pos.Location.ToVector2(), Pos.Location.ToVector2());
            int index = 0;

            for (int i = 1; i < enemies.Count; i++)
            {
                float dist = Vector2.Distance(enemies[i].Pos.Location.ToVector2(), Pos.Location.ToVector2());

                if (smallest > dist)
                {
                    smallest = dist;
                    index = i;
                }
            }
            Target = enemies[index];
        }
        public static void CheckKill(ref List<Enemy> enemies)
        {
            for (int i = 0; i < projectiles.Count; i++)
            {
                for (int j = 0; j < enemies.Count; j++)
                {
                    if (projectiles[i].Pos.Intersects(enemies[j].Pos))
                    {
                        projectiles[i].DamageCalc();

                        Game1.money += ((float)enemies[j].Rank/ 5f);

                        enemies[j].Rank--;

                        EnemyManager.RankCheck(enemies[j]);

                        projectiles.RemoveAt(i);

                        if (enemies[j].Rank <= 0)
                        {
                            enemies.RemoveAt(j);
                        }
                        
                        break;
                    }
                
                }
            }
        }
        public void CreateDot()
        {
            Dot = new Sprite(null, new Rectangle(Pos.X + 50, Pos.Y, 10, 10), Color.Red, 0, Origin);
        }

    }
}
