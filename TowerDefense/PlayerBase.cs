using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

namespace TowerDefense
{
    public abstract class PlayerBase : AnimatingSpriteBase
    {
        private List<Rectangle> sourceRectangles;

        protected override List<Rectangle> SourceRectangles => sourceRectangles;

        public int Level;

        public int XP;

        public int Damage => Level * DmgMultiplier;

        public int DmgMultiplier;

        public PlayerBase(Texture2D tex, Rectangle pos, Color color, float rotation, Vector2 origin, List<Rectangle> sourceRectangle, int level, int xp, int dmgMultiplier)
            : base(tex, pos, color, rotation, origin, sourceRectangle)
        {
            Level = level;
            XP = xp;
            DmgMultiplier = dmgMultiplier;
            sourceRectangles = sourceRectangle;
        }
    }
}
