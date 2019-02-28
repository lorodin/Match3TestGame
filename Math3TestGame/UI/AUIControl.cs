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
    public abstract class AUIControl
    {
        public string Name { get; set; }
        public SpriteName Background { get; set; }
        public SpriteName Image { get; set; }

        public string Text { get; set; }

        public Rectangle Region { get; set; }
        public Vector2 TextPosition { get; set; }
        public Color TextColor { get; set; } = Color.White;
        public SpriteFont Font { get; set; }
        public int SpriteAnimationStep { get; set; } = 0;
        public Color BackgroundColor { get; set; } = Color.White;
        public ButtonState ButtonState { get; set; } = ButtonState.NONE;

        protected GameConfigs gc;
        protected int ddt = 0;

        public AUIControl(int x, int y, int width, int height)
        {
            Region = new Rectangle(x, y, width, height);
            gc = GameConfigs.GetInstance();
            Background = SpriteName.None;
        }

        public abstract void Update(int dt);
    }

    public enum ButtonState
    {
        HOVER = 0,
        NONE = 1
    }
}
