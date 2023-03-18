using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public sealed class AnimatingSprite : AnimatingSpriteBase
    {
        protected override List<Rectangle> SourceRectangles { get; }
        
        public AnimatingSprite(Texture2D tex, Rectangle pos, Color color, float rotation, Vector2 origin, List<Rectangle> sourceRectangle)
            : base(tex, pos, color, rotation, origin)
        {
            SourceRectangles = sourceRectangle;
        }
    }
}
