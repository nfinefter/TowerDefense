using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public abstract class Screen
    {
        public List<Sprite> Sprites= new List<Sprite>();

        public List<Rectangle> Buttons= new List<Rectangle>();

    }
}
