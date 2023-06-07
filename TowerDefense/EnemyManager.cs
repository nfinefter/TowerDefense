using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public class EnemyManager
    {
        public bool IsAttacked(Enemy enemy)
        {
            if (enemy.PathPosition + 1 == enemy.Path.Count)
            {
                Game1.health -= enemy.Rank;
                return true;
            }
            return false;
        }
        public void RankCheck(Enemy enemy)
        {
            if (enemy.Rank == 1)
            {
                enemy.Color = Color.Red;
                enemy.Speed = 100;
            }
            else if (enemy.Rank == 2)
            {
                enemy.Color = Color.Blue;
                enemy.Speed = 200;
            }
            else if (enemy.Rank == 3)
            {
                enemy.Color = Color.Green;
                enemy.Speed = 300;
            }
            else if (enemy.Rank == 4)
            {
                enemy.Color = Color.Yellow;
                enemy.Speed = 400;
            }
            else if (enemy.Rank == 5)
            {
                enemy.Color = Color.Pink;
                enemy.Speed = 500;
            }
            else if (enemy.Rank == 6)
            {
                enemy.Color = Color.Black;
                enemy.Speed = 600;
            }
        }
        TimeSpan moveTime = TimeSpan.FromSeconds(1);
        TimeSpan moveWait;
        int min = 1;
        int max = 10;
        int minRank = 1;

        public void EnemyCreate(GameTime time, ref List<Enemy> bloons)
        {
            Random rand = new Random();

            TimeSpan spawnSpeed = TimeSpan.FromMilliseconds(rand.Next(min, max));

            moveWait += time.ElapsedGameTime;

            if (moveWait > moveTime - spawnSpeed)
            {
                bloons.Add(new Enemy(ContentManager.Instance[Textures.Bloon], Game1.Start, Color.White, 0, new Vector2(ContentManager.Instance[Textures.Bloon].Width / 2, ContentManager.Instance[Textures.Bloon].Height / 2), 0, Game1.path, rand.Next(minRank, 7)));
                min += 1;
                max += 1;
                moveWait = TimeSpan.Zero;
            }
            if (bloons.Count % 500 == 0 && minRank <= 5)
            {
                minRank++;
            }


        }
    }
}
