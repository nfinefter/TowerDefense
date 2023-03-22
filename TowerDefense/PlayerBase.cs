using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TowerDefense
{
    public abstract class PlayerBase : AnimatingSpriteBase
    {
        int RectIndex;

        public override Rectangle? SourceRectangle => SourceRectangles[RectIndex];
        
        public int Level;

        public int XP;

        public int Damage => Level * DmgMultiplier;

        public int DmgMultiplier;

        public PlayerBase(Texture2D tex, Rectangle pos, Color color, float rotation, Vector2 origin, int level, int xp, int dmgMultiplier)
            : base(tex, pos, color, rotation, origin)
        {
            Level = level;
            XP = xp;
            DmgMultiplier = dmgMultiplier;
        }
    }
}
