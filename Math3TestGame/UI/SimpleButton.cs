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
    public class SimpleButton : AUIControl
    {


        public SimpleButton(SpriteFont font, string text, int x, int y, int width, int height, Color textColor):base(x, y, width, height)
        {
            Text = text;
            Font = font;
            TextColor = textColor;
            int textLength = text.Length;
            int textWidth = (int)Math.Round(font.Spacing * textLength);
            Point center = new Point(x + width / 2, y + height / 2);
            TextPosition = new Vector2(1 + center.X - font.LineSpacing / 2 , center.Y - font.LineSpacing / 2);
            Background = SpriteName.ToggleButton;
            SpriteAnimationStep = 3;
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
