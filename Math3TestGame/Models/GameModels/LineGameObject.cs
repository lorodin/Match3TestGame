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
    public class LineGameObject : AGameObject
    {
        public override Rectangle Region { get; set; }
        public override Point NewPosition { get; protected set; }
        public override int SpriteAnimationStep { get; protected set; }
        public override SpriteName SpriteName { get; protected set; }
        public override GameMatrix Parent { get; protected set; }
        
        public LineGameObject(AGameObject item, LineType lt) : base(item, item.SpriteName)
        {
            Bonus = lt == LineType.H ? BonusEffect.LINE_H : BonusEffect.LINE_V;
        }
        
        public override void Kill()
        {
            if (!CanKilled()) return;

            AudioHelper.GetInstance().Play(SongName.LAZER);

            switch (Bonus)
            {
                case BonusEffect.LINE_H:
                    if (Left != null) Left.Kill(new LineBonusEffect(LineBonusEffectDirection.RL));
                    if (Right != null) Right.Kill(new LineBonusEffect(LineBonusEffectDirection.LR));
                    break;
                case BonusEffect.LINE_V:
                    if (Top != null) Top.Kill(new LineBonusEffect(LineBonusEffectDirection.BT));
                    if (Bottom != null) Bottom.Kill(new LineBonusEffect(LineBonusEffectDirection.TB));
                    break;
            }

            AnimationState = SpriteAnimationState.HIDE;
        }
    }

    public enum LineType
    {
        V = 0,
        H = 1
    }
}
