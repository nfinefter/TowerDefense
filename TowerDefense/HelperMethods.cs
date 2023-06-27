global using static TowerDefense.HelperMethods;

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
    static class HelperMethods
    {
        public static bool MonkeyKill(List<Player> monkeys)
        {
            Random rand = new Random();

            if (rand.Next(1, 10001) == 5000)
            {
                monkeys.RemoveAt(rand.Next(0, monkeys.Count));
                return true;
            }
            return false;
        }
        public static List<Rectangle> SourceRectangleFinder(Texture2D image, Point frames)
        {
            List<Rectangle> images = new List<Rectangle>();

            Point moveBetweenImg = new Point(0, 0);

            Point imageSize = new Point(image.Width / frames.X, image.Height / frames.Y);

            for (int i = 0; i < frames.X; i++)
            {
                for (int j = 0; j < frames.Y; j++)
                {
                    images.Add(new Rectangle(moveBetweenImg.X, moveBetweenImg.Y, image.Width / frames.X, image.Height / frames.Y));
                    moveBetweenImg.Y += imageSize.Y;
                }
                moveBetweenImg.Y = 0;
                moveBetweenImg.X += imageSize.X;
            }

            return images;
        }
        public static void Sell(Player selectedMonkey, ref int money)
        {

        }

      
    }

}

