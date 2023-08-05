using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeightedDirectedGraphs;

namespace TowerDefense
{
    public static class LocalExtensions
    {
        public static Rectangle Hitbox(this Vertex<System.Drawing.Point> vert)
        {
            return new Rectangle(vert.Value.X, vert.Value.Y, GameScreen.size, GameScreen.size);
        }
    }
}
