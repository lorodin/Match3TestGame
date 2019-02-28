using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math3TestGame.Tools;
using Microsoft.Xna.Framework;

namespace Math3TestGame.Models.GameModels
{
    public class MulticolorGameObject : AGameObject
    {
        public MulticolorGameObject(AGameObject item) : base(item, SpriteName.GameObject6)
        {
        }

        public override Rectangle Region { get; set; }
        public override Point NewPosition { get; protected set; }
        public override int SpriteAnimationStep { get; protected set; }
        public override SpriteName SpriteName { get; protected set; }
        public override GameMatrix Parent { get; protected set; }

        public override void Kill()
        {
            if (hKilled >= 3 && vKilled >= 3)
            {
                Parent.ReplaceItem(this, BonusEffect.BANG);
                return;
            }

            AnimationState = SpriteAnimationState.HIDE;
        }
    }
}
