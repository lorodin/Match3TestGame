using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math3TestGame.Models.BonusEffects;
using Math3TestGame.Tools;
using Microsoft.Xna.Framework;

namespace Math3TestGame.Models.GameModels
{
    public class BangGameObject : AGameObject
    {
        public override Rectangle Region { get; set; }
        public override Point NewPosition { get; protected set; }
        public override int SpriteAnimationStep { get; protected set; }
        public override SpriteName SpriteName { get; protected set; }
        public override GameMatrix Parent { get; protected set; }

        public BangGameObject(AGameObject item) : base(item, item.SpriteName)
        {
            Bonus = BonusEffect.BANG;
        }


        private void KillItem(AGameObject item)
        {
            switch (item.Bonus)
            {
                case BonusEffect.BANG:
                    ((BangGameObject)item).Kill();

                    break;
                case BonusEffect.NONE:
                    item.Kill();
                    break;
            }
        }

        public override void Kill()
        {
            if (!CanKilled()) return;

            //BonusEffects.Add(new WaitBangEffect());
            SetBonusEffect(new WaitBangEffect());
            /*
            if(Left != null)
            {
                if (Left.Top != null) Left.Top.Kill();
                Left.Kill();
            }
            if(Top != null)
            {
                if (Top.Right != null) Top.Right.Kill();
                Top.Kill();
            }
            if(Right != null)
            {
                if (Right.Bottom != null) Right.Bottom.Kill();
                Right.Kill();
            }
            if(Bottom != null)
            {
                if (Bottom.Left != null) Bottom.Left.Kill();
                Bottom.Kill();
            }

            AnimationState = SpriteAnimationState.HIDE;*/
        }
    }
}
