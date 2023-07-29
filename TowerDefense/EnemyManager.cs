using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public static class EnemyManager
    {
        public static bool IsAttacked(Enemy enemy)
        {
            if (enemy.PathPosition + 1 == enemy.Path.Count)
            {
                GameScreen.Health -= enemy.Rank;
                return true;
            }
            return false;
        }
        public static void RankCheck(Enemy enemy)
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
        static TimeSpan moveTime = TimeSpan.FromSeconds(1);
        static TimeSpan moveWait;
        static int min = 1;
        static int max = 10;
        static int minRank = 1;

        public static void EnemyCreate(GameTime time, ref List<Enemy> bloons)
        {
            Random rand = new Random();

            TimeSpan spawnSpeed = TimeSpan.FromMilliseconds(rand.Next(min, max));

            moveWait += time.ElapsedGameTime;

            if (moveWait > moveTime - spawnSpeed)
            {
                bloons.Add(new Enemy(ContentManager.Instance[Textures.Bloon], GameScreen.Start, Color.White, 0, new Vector2(ContentManager.Instance[Textures.Bloon].Width / 2, ContentManager.Instance[Textures.Bloon].Height / 2), 0, GameScreen.Path, rand.Next(minRank, 7)));
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
