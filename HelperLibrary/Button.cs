using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibrary
{
    public class Button : SpriteBase
    {
        public Action PressedAction;
        public string Text;
        public bool Click { get; private set; }
        public bool WasClicked { get; private set; }

        public override Vector2 Origin => Vector2.Zero;

        public override Rectangle? SourceRectangle => new Rectangle(0, 0 ,0 ,0);

        public Button(Texture2D tex, Rectangle pos, Color color, float rotation, string text, Action pressedAction)
         : base(tex, pos, color, rotation)
        {
            Text = text;
            PressedAction = pressedAction;
        }

        public override void Update(GameTime gameTime)
        { 
            if (Pos.Contains(Mouse.GetState().Position) && ScreenManager.Instance.CurrentState == ButtonState.Pressed && ScreenManager.Instance.PrevState != ScreenManager.Instance.CurrentState)
            {
                Click = true;
            }

            if (Click)
            {
                Click = !Click;
                PressedAction();
            }
        }

    }
}
