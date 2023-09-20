using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


namespace HelperLibrary
{
    using static ScreenManager;
    using static System.Reflection.Metadata.BlobBuilder;

    public enum Screenum
    {
        Game,
        Menu,
        End,
    }


    public abstract class Screen
    {

        public abstract void Begin();

        public abstract Screenum Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);
    }

    public class MenuScreen : Screen
    {
        public override void Begin()
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public override Screenum Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
    public class GameScreen : Screen
    {
        public override void Begin()
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public override Screenum Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }

    public class ScreenManager
    {
        public ButtonState CurrentState { get; private set; }
        public ButtonState PrevState { get; private set; }
        public Screenum currentScreen;

        Dictionary<Screenum, Screen> backingScreens = new Dictionary<Screenum, Screen>();

        private ScreenManager() { }

        public static ScreenManager Instance { get; private set; } = new ScreenManager();

        public void Init(params (Screenum, Screen)[] screens)
        {
            foreach (var screenPair in screens)
            {
                backingScreens.Add(screenPair.Item1, screenPair.Item2);
            }
        }

        public Screen CurrentScreen => backingScreens[currentScreen];

        public void Update(GameTime gameTime)
        {
            CurrentState = Mouse.GetState().LeftButton;
            currentScreen = CurrentScreen.Update(gameTime);

            PrevState = CurrentState;
        }
        public void Draw(SpriteBatch spriteBatch) => CurrentScreen.Draw(spriteBatch);


    }



}
