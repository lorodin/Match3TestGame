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
    public class GameOverDialogModel : IDrawableModel
    {
        public Rectangle Region { get; set; }

        public SpriteName SpriteName { get; private set; }

        public int SpriteAnimationStep { get; private set; } = 0;

        public SpriteAnimationState AnimationState { get; set; }

        private GameConfigs gc;

        public GameOverDialogModel()
        {
            gc = GameConfigs.GetInstance();
            SpriteName = SpriteName.GameOverBG;
            Region = new Rectangle(gc.Center.X - 200, gc.Center.Y - 100, 400, 200);
        }
    }
}
