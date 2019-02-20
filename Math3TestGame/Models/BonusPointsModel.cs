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
    public class BonusPointsModel : IDynamic, IDrawableModel
    {
        public int Points { get; set; } = 0;

        public Rectangle Region { get; set; }

        public DynamicState State { get; set; }

        public SpriteName SpriteName { get; set; }

        public int SpriteAnimationStep { get; set; }

        public SpriteAnimationState AnimationState { get; set; }

        private GameConfigs gc;
        private Vector2 position;

        public BonusPointsModel()
        {
            gc = GameConfigs.GetInstance();
            Region = new Rectangle(gc.GetRealPoint(1, 0.5f), new Point(0, 0));
            position = new Vector2(Region.X, Region.Y);
        }
        
        public void Update(int dt)
        {
        }
    }
}
