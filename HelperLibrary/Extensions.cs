using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeightedDirectedGraphs;

namespace HelperLibrary
{
    public static class Extensions
    {
        public static System.Drawing.Point ToPoint(this Microsoft.Xna.Framework.Point point)
        {
            return new System.Drawing.Point(point.X, point.Y);
        }

        public static Microsoft.Xna.Framework.Point ToPoint(this System.Drawing.Point point)
        {
            return new Microsoft.Xna.Framework.Point(point.X, point.Y);
        }

        //public static Rectangle Hitbox(this Vertex<System.Drawing.Point> vert)
        //{
        //    return new Rectangle(vert.Value.X, vert.Value.Y, Game1.size, Game1.size);
        //}
    }
}
