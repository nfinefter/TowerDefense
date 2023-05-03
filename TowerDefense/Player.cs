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
        public override Vector2 Origin => new Vector2(SourceRectangle.Value.Width / 2, SourceRectangle.Value.Height / 2);

        public Rectangle Hitbox => new Rectangle(Pos.X - (Pos.Width / 2), Pos.Y - Pos.Height / 2, Pos.Width, Pos.Height);

        public bool Placed = false;
        public Player(Texture2D tex, Rectangle pos, Color color, float rotation, Vector2 origin, List<Rectangle> sourceRectangle, int level, int xp, int dmgMultiplier)
            : base(tex, pos, color, rotation, origin, sourceRectangle, level, xp, dmgMultiplier)
        {
        }

        //x=(1-s)a+sb
    }
}
