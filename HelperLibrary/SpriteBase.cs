using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoGame.Extended;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibrary
{
    public abstract class SpriteBase
    {
        public Texture2D Tex;
        public Rectangle Pos;
        public Color Color;
        public float Rotation;
        public abstract Vector2 Origin { get; }
        public abstract Rectangle? SourceRectangle { get; }

        public SpriteBase(Texture2D tex, Rectangle pos, Color color, float rotation)
        {
            Tex = tex;
            Pos = pos;
            Color = color;
            Rotation = rotation;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Tex, Pos, SourceRectangle, Color, Rotation, Origin, SpriteEffects.None, 0);
        }
        
        public void DrawRectangle(SpriteBatch spriteBatch)
        {
            spriteBatch.FillRectangle(new Rectangle(Pos.X, Pos.Y, 10, 10), Color, 0);
        }

        public abstract void Update(GameTime gameTime);

        public void Lerp(Point initialPos, Point finalPos, double scale)
        {
            Pos.Location = new Point((int)((1 - scale) * initialPos.X + scale * finalPos.X), (int)((1 - scale) * initialPos.Y + scale * finalPos.Y));
        }
        
    }
}