using Math3TestGame.Models.GameModels;
using Math3TestGame.Models.Interfaces;
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
    public class Label:AUIControl
    {
        public Label(SpriteFont font, string text, int x, int y):this(font, text, x, y, Color.White) {}

        public Label(SpriteFont font, string text, int x, int y, Color color):base(x, y, 0, 0)
        {
            Font = font;
            Text = text;
            TextPosition = new Vector2(x, y);
            TextColor = color;
        }

        public override void Update(int dt)
        {

        }
    }
}
