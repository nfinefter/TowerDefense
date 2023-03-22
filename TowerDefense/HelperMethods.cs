global using static TowerDefense.HelperMethods;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    static class HelperMethods
    {
        public static List<Rectangle> SourceRectangleFinder(Texture2D image, Point frames)
        {
            List<Rectangle> images = new List<Rectangle>();

            Point moveBetweenImg = new Point(image.Width / frames.X, image.Height / frames.Y);

            for (int i = 0; i < frames.X; i+= moveBetweenImg.X)
            {
                for (int j= 0; j < frames.Y; j+= moveBetweenImg.Y)
                {
                    images.Add(new Rectangle(i, j, moveBetweenImg.X, moveBetweenImg.Y));
                }
            }

            return images;
        }
    }
}
