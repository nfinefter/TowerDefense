using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibrary
{
    public abstract class Sprite : SpriteBase
    {
        Vector2 origin;
        public override Vector2 Origin { get => origin; }

        public Sprite(Texture2D tex, Rectangle pos, Color color, float rotation, Vector2 origin)
            : base(tex, pos, color, rotation)
        {
            this.origin = origin;
        }

        public override Rectangle? SourceRectangle => null;

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
