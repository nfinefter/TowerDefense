using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibrary
{
    public abstract class SpriteBase : GameObject
    {
        public Texture2D Tex;
        public SpriteBase(Texture2D tex, Rectangle pos, Color color, float rotation) : 
            base(pos, color, rotation)
        {
            Tex = tex;
            Pos = pos;
            Color = color;
            Rotation = rotation;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Tex, Pos, SourceRectangle, Color, Rotation, Origin, SpriteEffects.None, 0);
        }

        public void DrawRectangle(SpriteBatch spriteBatch)
        {
            //spriteBatch.FillRectangle(new Rectangle(Pos.X, Pos.Y, 10, 10), Color, 0);
        }

        public void Lerp(Point initialPos, Point finalPos, double scale)
        {
            Pos.Location = new Point((int)((1 - scale) * initialPos.X + scale * finalPos.X), (int)((1 - scale) * initialPos.Y + scale * finalPos.Y));
        }
    }
}
