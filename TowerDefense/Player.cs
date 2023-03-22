using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public sealed class Player : PlayerBase
    {
        int RectIndex;

        public override Rectangle? SourceRectangle => SourceRectangles[RectIndex];

        protected override List<Rectangle> SourceRectangles => new List<Rectangle>();

        public Player(Texture2D tex, Rectangle pos, Color color, float rotation, Vector2 origin, int level, int xp, int dmgMultiplier)
            : base(tex, pos, color, rotation, origin, level, xp, dmgMultiplier)
        {
        }


    }
}
