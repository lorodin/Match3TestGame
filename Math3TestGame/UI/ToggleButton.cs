using Math3TestGame.Models.GameModels;
using Math3TestGame.Models.Interfaces;
using Math3TestGame.Tools;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math3TestGame.UI
{
    public class ToggleButton : AUIControl
    {
        
        public SpriteAnimationState AnimationState { get; set; }

        public ButtonState ButtonState { get; set; } = ButtonState.NONE;

        public ToggleButtonState ToggleState { get; set; } = ToggleButtonState.OFF;
        
        private int ddt = 0;
        
        public GameType GameType { get; private set; }

        public ToggleButton(GameType gameType, int x, int y, int width, int height):base(x, y, width, height)
        {
            switch (gameType)
            {
                case GameType.G8x8:
                    Text = "8x8";
                    break;
                case GameType.G6x6:
                    Text = "6x6";
                    break;
                case GameType.G6x8:
                    Text = "6x8";
                    break;
                case GameType.G8x9:
                    Text = "8x9";
                    break;
            }

            Font = gc.DefaultFont12;

            TextPosition = new Vector2(x + 10, y + 15);

            GameType = gameType;

            Background = SpriteName.ToggleButton;
        }

        public override void Update(int dt)
        {
            if(ToggleState == ToggleButtonState.ON)
            {
                SpriteAnimationStep = 4;
                ddt = 0;
                return;
            }

            ddt += dt;

            if (ddt > gc.ADTime)
            {
                ddt = 0;

                if (ButtonState == ButtonState.NONE && SpriteAnimationStep > 0) SpriteAnimationStep--;
                else if (ButtonState == ButtonState.HOVER && SpriteAnimationStep < 3)
                    SpriteAnimationStep++;
            }
        }
    }

    public enum ToggleButtonState
    {
        OFF = 0,
        ON = 1
    }
}
