using Math3TestGame.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math3TestGame.UI
{
    public class CheckBox : AUIControl
    {
        public CheckBox(SpriteFont font, string text, int x, int y, int width, int height, Color color):base(x, y, width, height)
        {
            Background = SpriteName.ToggleButton;
            Image = gc.SoundOn ? SpriteName.Check : SpriteName.None;
            Text = text;
            Font = font;
            TextPosition = new Vector2(x, y + Font.LineSpacing / 3);
            Region = new Rectangle(x + Font.LineSpacing * Text.Length / 2 , y, 30, 30);
            TextColor = color;
        }

        public override void Update(int dt)
        {
            ddt += dt;

            if (ddt > gc.ADTime)
            {
                ddt = 0;

                if (ButtonState == ButtonState.NONE && SpriteAnimationStep < 3) SpriteAnimationStep++;
                else if (ButtonState == ButtonState.HOVER && SpriteAnimationStep > 0) SpriteAnimationStep--;
            }
        }
    }
}
