using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public abstract class PlayerBase : AnimatingSpriteBase
    {
        protected PlayerBase(Texture2D tex, Rectangle pos, Color color, float rotation, Vector2 origin) : base(tex, pos, color, rotation, origin)
        {
        }
    }
}
