using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math3TestGame.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Math3TestGame.Models
{
    public class PlayButtonModel : IDrawableModel
    {
        private int AnimationStep = 0;

        public PlayButtonState State { get; set; } = PlayButtonState.NONE;

        private TextureHelper tHelper;
        private GameConfigs gc;

        private int ddt = 0;


        public Rectangle Rect { get; set; }

        public PlayButtonModel()
        {
            tHelper = TextureHelper.GetInstance();
            gc = GameConfigs.GetInstance();
            Rect = new Rectangle(gc.Center.X - 101, gc.Center.Y - 25, 202, 50);
        }

        public void Update(int dt)
        {
            ddt += dt;

            if(ddt > gc.ADTime)
            {
                ddt = 0;

                if (State == PlayButtonState.NONE && AnimationStep > 0) AnimationStep--;
                else if(State == PlayButtonState.HOVER && AnimationStep < 4)  AnimationStep++;   
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(gc.DefaultSpriteMap, Rect, tHelper.GetTextureRegion(SpriteName.PlayButton, AnimationStep), Color.White);
        }
    }

    public enum PlayButtonState
    {
        HOVER = 0,
        NONE = 1
    }
}
