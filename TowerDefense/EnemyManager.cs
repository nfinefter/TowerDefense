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
        public static int JudisSpawnDelay = 10;
        public static bool IsAttacked(EnemyBase enemy)
        {
            if (enemy.PathPosition + 1 == enemy.Path.Count)
            {
                GameScreen.Health -= enemy.Rank;
                return true;
            }
            return false;
        }
        public static void RankCheck(EnemyBase enemy)
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
            else if (enemy.Rank == 10)
            {
                enemy.Color = Color.Purple;
                enemy.Speed = 1000;
            }//cry
        }

        static TimeSpan moveTime = TimeSpan.FromSeconds(1);
        static TimeSpan moveWait;
        static public int min = 1;
        static public int max = 10;
        static public int minRank = 1;

        public static void EnemyCreate(GameTime time, ref List<EnemyBase> bloons)
        {
            Random rand = new Random();

            TimeSpan spawnSpeed = TimeSpan.FromMilliseconds(rand.Next(min, max));
            moveWait += time.ElapsedGameTime;

            if (moveWait > moveTime - spawnSpeed)
            {
                JudisCreation(ref bloons);
                bloons.Add(new Enemy(ContentManager.Instance[Textures.Bloon], GameScreen.Start, Color.White, 0, Vector2.Zero, 0, GameScreen.Path, rand.Next(minRank, 7)));
                if (rand.Next(0, 5000) == 1)
                {
                    min += 1;
                    max += 1;
                }
                moveWait = TimeSpan.Zero;
            }
            if (bloons.Count > 1 && bloons.Count % 500 == 0 && minRank <= 5)
            {
                minRank++;
            }
        }
        public static void JudisCreation(ref List<EnemyBase> bloons)
        {
            Random rand = new Random();

            if (GameScreen.Monkeys.Count > 1 && rand.Next(1, JudisSpawnDelay) == 5)
            {
                bloons.Add(new KillerEnemy(ContentManager.Instance[Textures.Bloon], GameScreen.Start, Color.Purple, 0, Vector2.Zero, 0, GameScreen.Path, 10));
            }
        }
    }
}
