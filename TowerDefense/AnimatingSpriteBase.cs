using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

namespace TowerDefense
{
    public abstract class AnimatingSpriteBase : SpriteBase
    {
        protected abstract List<Rectangle> SourceRectangles { get; }

        int RectIndex;
        public override Rectangle? SourceRectangle => SourceRectangles[RectIndex];

        public AnimatingSpriteBase(Texture2D tex, Rectangle pos, Color color, float rotation, Vector2 origin/*, List<Rectangle> sourceRectangle*/)
            : base(tex, pos, color, rotation, origin)
        {
            //SourceRectangles = sourceRectangle;
        }
                
        public override void Update()
        {

            RectIndex++;
            if (RectIndex >= SourceRectangles.Count)
            {
                RectIndex = 0;
            }

        }

    }
}
