using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;

namespace HelperLibrary
{
    public abstract class AnimatingSpriteBase : SpriteBase
    {
        public TimeSpan test;

        protected abstract List<Rectangle> SourceRectangles { get; }

        internal int RectIndex = 0;
        public override Rectangle? SourceRectangle => SourceRectangles[RectIndex];



        public AnimatingSpriteBase(Texture2D tex, Rectangle pos, Color color, float rotation, Vector2 origin, List<Rectangle> sourceRectangle)
            : base(tex, pos, color, rotation)
        {
            //SourceRectangles = sourceRectangle;
        }
                
        public override void Update(GameTime time)
        {
            if (test < TimeSpan.FromMilliseconds(1000)) return;
            test = TimeSpan.Zero;
            RectIndex++;
            if (RectIndex >= SourceRectangles.Count)
            {
                RectIndex = 0;
            }

        }

    }
}
