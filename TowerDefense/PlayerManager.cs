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
        public static void UpgradeDot(Player monkey)
        {
            if (monkey.Level == 1)
            {
                monkey.Dot.Color = Color.Red;
            }
            else if (monkey.Level == 2)
            {
                monkey.Dot.Color = Color.Blue;
            }
            else if (monkey.Level == 3)
            {
                monkey.Dot.Color = Color.Green;
            }
            else if (monkey.Level == 4)
            {
                monkey.Dot.Color = Color.Yellow;
            }
            else if (monkey.Level == 5)
            {
                monkey.Dot.Color = Color.Pink;
            }
            else if (monkey.Level == 6)
            {
                monkey.Dot.Color = Color.Black;
            }

        }

    }
}
