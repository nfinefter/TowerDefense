using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ScreenDemo
{
    public enum Screenum
    {
        Game,
        Menu,
        Pause,
        whatever,
        thisisanexample
    }

    public abstract class Screen
    {
        public abstract Screenum Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);
    }

    public class MenuScreen : Screen
    {

    }

    public class ScreenManager
    {
        public Screenum currentScreen;

        Dictionary<Screenum, Screen> backingScreens = new Dictionary<Screenum, Screen>();

        private ScreenManager() { }

        public static ScreenManager Instance { get; private set; }

        public void Init(params (Screenum, Screen)[] screens)
        {
            foreach (var screenPair in screens)
            {
                backingScreens.Add(screenPair.Item1, screenPair.Item2);
            }
        }

        public Screen CurrentScreen => backingScreens[currentScreen];

        public void Update(GameTime gameTime) => currentScreen = CurrentScreen.Update(gameTime);
        public void Draw(SpriteBatch spriteBatch) => CurrentScreen.Draw(spriteBatch);


    }



}
