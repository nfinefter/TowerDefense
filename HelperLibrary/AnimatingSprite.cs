using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibrary
{
    public sealed class AnimatingSprite : AnimatingSpriteBase
    {
        protected override List<Rectangle> SourceRectangles { get; }

        private Vector2 origin;

        public override Vector2 Origin => origin;

        public AnimatingSprite(Texture2D tex, Rectangle pos, Color color, float rotation, Vector2 origin, List<Rectangle> sourceRectangle)
            : base(tex, pos, color, rotation, origin, sourceRectangle)
        {
            SourceRectangles = sourceRectangle;

            this.origin = origin;
        }

       
    }
}
