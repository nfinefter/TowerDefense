using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    enum Textures
    {
        Dart,
        Monkey,
        Bloon
    }
    internal class ContentManager
    {
        public Texture2D this[Textures wantedTexture]
        {
            get
            {
                return Textures[wantedTexture];
            }
            set
            {
                Textures[wantedTexture] = value;
            }
        }

        private Dictionary<Textures, Texture2D> Textures = new Dictionary<Textures, Texture2D>();
        
        private ContentManager()
        {
            
        }
        public static ContentManager Instance { get;} = new ContentManager();

    }
}
