using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public abstract class PlayerBase : AnimatingSpriteBase
    {
        int RectIndex;

        public override Rectangle? SourceRectangle => SourceRectangles[RectIndex];
        
        public int Level;

        public int XP;

        public int Damage => Level * 10;

        public PlayerBase(Texture2D tex, Rectangle pos, Color color, float rotation, Vector2 origin, int level, int xp)
            : base(tex, pos, color, rotation, origin)
        {
            Level = level;
            XP = xp;
        }



    }
}
