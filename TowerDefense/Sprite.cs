using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public sealed class Sprite : SpriteBase
    {

        public Sprite(Texture2D tex, Rectangle pos, Color color, float rotation, Vector2 origin)
            : base(tex, pos, color, rotation, origin)
        {
            
        }

        public override Rectangle? SourceRectangle => null;
    }
}
