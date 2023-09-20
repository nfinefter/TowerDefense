using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibrary
{
    public abstract class GameObject
    {
        public Rectangle Pos;
        public Color Color;
        public float Rotation;
        public abstract Vector2 Origin { get; }
        public abstract Rectangle? SourceRectangle { get; }

        public GameObject(Rectangle pos, Color color, float rotation)
        {
            Pos = pos;
            Color = color;
            Rotation = rotation;
        }
        public abstract void Update(GameTime gameTime);

        public void Lerp(Point initialPos, Point finalPos, double scale)
        {
            Pos.Location = new Point((int)((1 - scale) * initialPos.X + scale * finalPos.X), (int)((1 - scale) * initialPos.Y + scale * finalPos.Y));
        }
    }
}
