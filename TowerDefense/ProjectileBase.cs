//using Assimp.Configs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public abstract class ProjectileBase : SpriteBase
    {
        public int Damage;
        private double Speed;

        private double scalar = 0;
        public double Scalar
        {
            get
            {
                return scalar += Speed;
            }
        }

        public Player ThrownFrom;
        protected ProjectileBase(Texture2D tex, Rectangle pos, Color color, float rotation, Vector2 origin, int damage, double speed)
            : base(tex, pos, color, rotation)
        {
            Damage = damage;
            Speed = speed;
        }

        public static double FindingTime(Vector2 pos1, Vector2 pos2, double idealSpeed)
        {
            return idealSpeed / Vector2.Distance(pos1, pos2);
        }


    }
}
