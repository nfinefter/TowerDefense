using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public static class PlayerManager
    {

        static readonly Color[] UpgradeColors =
        {
            Color.Blue,
            Color.Green,
            Color.Yellow,
            Color.Pink,
            Color.Black
        };

        public static void UpgradeDot(Player monkey)
        {
            monkey.Dot.Color = UpgradeColors[monkey.Level - 1];
            //D:
        }

    }
}
