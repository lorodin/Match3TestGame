using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math3TestGame.Models.GameModels;
using Math3TestGame.Models.Interfaces;
using Math3TestGame.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Math3TestGame.UI
{
    public class PlayButton : AUIControl
    {
        public ButtonState PlayButtonState { get; set; } = ButtonState.NONE;
        
        private int ddt = 0;
        
        public PlayButton(int x, int y):base(x, y, 202, 50)
        {
            Background = SpriteName.PlayButton;
        }

        public override void Update(int dt)
        {
            ddt += dt;

            if(ddt > gc.ADTime)
            {
                ddt = 0;

                if (PlayButtonState == ButtonState.NONE && SpriteAnimationStep > 0) SpriteAnimationStep--;
                else if(PlayButtonState == ButtonState.HOVER && SpriteAnimationStep < 4) SpriteAnimationStep++;   
            }
        }
    }

}
