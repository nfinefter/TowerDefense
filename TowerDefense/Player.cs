using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;

using static TowerDefense.GameScreen;

namespace TowerDefense
{
    public sealed class Player : PlayerBase
    {
        TimeSpan shootTime = TimeSpan.FromSeconds(1);
        TimeSpan shootWait;

        public Sprite Dot;

        private EnemyBase Target;

        public static List<Projectile> projectiles = new List<Projectile>();

        public override Vector2 Origin => new Vector2(SourceRectangle.Value.Width / 2, SourceRectangle.Value.Height / 2);

        public Rectangle Hitbox => new Rectangle(Pos.X - (Pos.Width / 2), Pos.Y - Pos.Height / 2, Pos.Width, Pos.Height);

        public bool Placed = false;
        public Player(Texture2D tex, Rectangle pos, Color color, float rotation, Vector2 origin, List<Rectangle> sourceRectangle, int level, int xp, int dmgMultiplier, int range)
            : base(tex, pos, color, rotation, origin, sourceRectangle, level, xp, range)
        {
            shootWait = TimeSpan.Zero;
        }

        public override void Update(GameTime time)
        {
            if (!Placed) return;

            base.Update(time);
            Console.WriteLine(shootWait);
            shootWait += time.ElapsedGameTime;
            
            if (shootWait > shootTime)
            {
                AddProjectile();

                shootWait = TimeSpan.Zero;
            }
        }
        public bool DartPredicate(Projectile me)
        {
            if (me.Pos.X < 0 || me.Pos.Y < 0 || me.Pos.X > GameDimensions.X + GameDimensions.Width || me.Pos.Y > GameDimensions.Y + GameDimensions.Height) return true;

            for (int i = 0; i < Bloons.Count; i++)
            {
                if (me.Pos.Intersects(Bloons[i].Pos))
                {
                    Money += Bloons[i].Rank / 5f;
                    for (int j = 0; j < Damage; j++)
                    {
                        Bloons[i].Rank--;
                    }
                    if (Bloons[i].Rank == 10 || Bloons[i].Rank  <= 0)
                    {
                        Bloons.RemoveAt(i);
                        return true;
                    }
                    
                    EnemyManager.RankCheck(Bloons[i]);
                    return true;
                }
            }
            return false;
        }

        public void AddProjectile()
        {
            if (Target == null)
            {
                return;
            }

            ContentManager manager = ContentManager.Instance;

            float offset = 0;
            if (Pos.X == Target.Pos.X)
            {
                offset = .1f;
            }

            float rotation = MathF.Atan2(Target.Pos.Y - Pos.Y, Target.Pos.X + offset - Pos.X); 

            projectiles.Add(new Projectile(manager[Textures.Dart], Pos, Color.Black, rotation, Vector2.Zero, Damage, 20, DartPredicate));
        }

        public void FindTarget(List<EnemyBase> enemies)
        {
            if (!Placed) return;

            if (enemies.Count == 0)
            {
                Target = null;
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
        public static void CheckKill(ref List<EnemyBase> enemies)
        {
            for (int i = 0; i < projectiles.Count; i++)
            {
                for (int j = 0; j < enemies.Count; j++)
                {
                    if (projectiles[i].Pos.Intersects(enemies[j].Pos))
                    {
                        GameScreen.Money += ((float)enemies[j].Rank/ 5f);

                        projectiles.RemoveAt(i);

                        if (enemies[j].Rank == 10)
                        {
                            enemies.RemoveAt(j);
                            return;
                        }

                        enemies[j].Rank--;

                        EnemyManager.RankCheck(enemies[j]);

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
