using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;




namespace TowerDefense
{
    public sealed class Enemy : EnemyBase
    {
        public override Rectangle? SourceRectangle => null;

        public Enemy(Texture2D tex, Rectangle pos, Color color, float rotation, Vector2 origin, int difficulty, List<Vertex<System.Drawing.Point>> path, int rank)
            : base(tex, pos, color, rotation, origin, difficulty, path, rank)
        {
            EnemyManager.RankCheck(this);
        }


    }


}


