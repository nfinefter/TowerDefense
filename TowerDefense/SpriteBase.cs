using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public abstract class SpriteBase
    {
        public Texture2D Tex;
        public Rectangle Pos;
        public Color Color;
        public float Rotation;
        public Vector2 Origin { get; }
        public abstract Rectangle? SourceRectangle { get; }

        public SpriteBase(Texture2D tex, Rectangle pos, Color color, float rotation, Vector2 origin)
        {
            Tex = tex;
            Pos = pos;
            Color = color;
            Rotation = rotation;
            Origin = origin;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Tex, Pos, SourceRectangle, Color, Rotation, Origin, SpriteEffects.None, 0);
        }

        public abstract void Update();
        
    }
}