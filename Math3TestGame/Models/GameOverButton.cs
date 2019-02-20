using Math3TestGame.Models.GameModels;
using Math3TestGame.Models.Interfaces;
using Math3TestGame.Tools;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math3TestGame.Models
{
    public class GameOverButton : IDrawableModel, IDynamic
    {
        public Rectangle Region { get; set; }

        public SpriteName SpriteName { get; private set; }

        public int SpriteAnimationStep { get; set; }

        public SpriteAnimationState AnimationState { get; set; }

        public DynamicState State { get; set; }

        private GameConfigs gc;

        public ButtonState ButtonState { get; set; }

        private int ddt = 0;

        public GameOverButton()
        {
            gc = GameConfigs.GetInstance();
            Region = new Rectangle(gc.Center.X - 50, gc.Center.Y + 20, 100, 30);
            SpriteName = SpriteName.GameOverButton;
        }

        public void Update(int dt)
        {
            ddt += dt;

            if(ddt > gc.ADTime)
            {
                ddt = 0;
                if(ButtonState == ButtonState.HOVER && SpriteAnimationStep != 2)
                {
                    SpriteAnimationStep++;
                    if (SpriteAnimationStep > 2) SpriteAnimationStep = 2;
                }
                else if(SpriteAnimationStep != 0)
                {
                    SpriteAnimationStep--;
                    if (SpriteAnimationStep < 0) SpriteAnimationStep = 0;
                }
            }
        }
    }
}
