using Math3TestGame.Models.GameModels;
using Math3TestGame.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math3TestGame.Models.BonusEffects
{
    public delegate void BeforeLastStep(LineBonusEffect effect);

    public class LineBonusEffect : IBonusEffect
    {
        public BonusEffect BonusType { get; set; }

        public event BeforeLastStep BeforeLastStep;

        public int AnimationStep { get; private set; } = 0;

        public DynamicState State { get; set; }

        public LineBonusEffectDirection Direction { get; private set; }

        private int ddt = 0;

        private GameConfigs gc;

        public LineBonusEffect(LineBonusEffectDirection direction)
        {
            Direction = direction;
            gc = GameConfigs.GetInstance();
            State = DynamicState.RUN;
            BonusType = direction == LineBonusEffectDirection.BT || direction == LineBonusEffectDirection.TB ? BonusEffect.LINE_V : BonusEffect.LINE_H;
        }

        public void Update(int dt)
        {
            ddt += dt;
            if (ddt >= gc.ADTime)
            {
                ddt = 0;
                AnimationStep++;
                if(AnimationStep == 3)
                {
                    if (BeforeLastStep != null) BeforeLastStep(this);
                }
                if(AnimationStep >= 4)
                {
                    AnimationStep = 4;
                    State = DynamicState.END;
                }
            }
        }
    }
    public enum LineBonusEffectDirection
    {
        LR = 0,
        RL = 1,
        TB = 2,
        BT = 3
    }
}
