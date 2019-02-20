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

namespace Math3TestGame.Models
{
    public class PlayButtonModel : IDrawableModel, IDynamic
    {
        public PlayButtonState PlayButtonState { get; set; } = PlayButtonState.NONE;

        private TextureHelper tHelper;
        private GameConfigs gc;

        private int ddt = 0;


        public Rectangle Region { get; set; }

        public SpriteName SpriteName { get; private set; } = SpriteName.PlayButton;

        public int SpriteAnimationStep { get; private set; } = 0;

        public SpriteAnimationState AnimationState { get; set; }

        public DynamicState State { get; set; }

        public PlayButtonModel()
        {
            tHelper = TextureHelper.GetInstance();
            gc = GameConfigs.GetInstance();
            Region = new Rectangle(gc.Center.X - 101, gc.Center.Y - 25, 202, 50);
        }

        public void Update(int dt)
        {
            ddt += dt;

            if(ddt > gc.ADTime)
            {
                ddt = 0;

                if (PlayButtonState == PlayButtonState.NONE && SpriteAnimationStep > 0) SpriteAnimationStep--;
                else if(PlayButtonState == PlayButtonState.HOVER && SpriteAnimationStep < 4)
                    SpriteAnimationStep++;   
            }
        }
    }

    public enum PlayButtonState
    {
        HOVER = 0,
        NONE = 1
    }
}
