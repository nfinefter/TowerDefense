using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    sealed public class Player : PlayerBase
    {
        int RectIndex;

        public override Rectangle? SourceRectangle => SourceRectangles[RectIndex];

        protected override List<Rectangle> SourceRectangles => throw new NotImplementedException();

        public Player(Texture2D tex, Rectangle pos, Color color, float rotation, Vector2 origin, int level, int xp)
            : base(tex, pos, color, rotation, origin, level, xp)
        {
        }


    }
}
