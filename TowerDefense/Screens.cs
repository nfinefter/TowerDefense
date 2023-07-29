using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public class MenuScreen : Screen
    {
        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public override Screenum Update(GameTime gameTime)
        {
            if (Buttons[0].Pos.Contains(Mouse.GetState().Position) && ScreenManager.Instance.CurrentState == ButtonState.Pressed && ScreenManager.Instance.PrevState == ButtonState.Released)
            {
                return Screenum.Game;
            }
            return Screenum.Menu;
        }
    }
    public class GameScreen : Screen
    {
        public List<Player> monkeys = new List<Player>();

        public override void Draw(SpriteBatch spriteBatch)
        {
            void DrawProjectiles()
            {
                for (int i = 0; i < Player.projectiles.Count; i++)
                {
                    var temp = Player.projectiles[i].Rotation;
                    Player.projectiles[i].Rotation -= 90;
                    Player.projectiles[i].Draw(spriteBatch);
                    Player.projectiles[i].Rotation += 90;
                }

            }

            for (int i = 0; i < Sprites.Count; i++)
            {
                Sprites[i].Draw(spriteBatch);
            }
            for (int i = 0; i < Buttons.Count; i++)
            {
                Buttons[i].Draw(spriteBatch);
            }


            DrawProjectiles();
        }

        public override Screenum Update(GameTime gameTime)
        {
            for (int i = 0; i < Buttons.Count; i++)
            {
                Buttons[i].Update(gameTime);
            }
            return Screenum.Game;
        }
    }

}
