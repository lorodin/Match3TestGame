using Math3TestGame.Models.GameModels;
using Math3TestGame.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math3TestGame.Models.BonusEffects
{
    public class BangBonusEffect : IBonusEffect
    {
        public BonusEffect BonusType { get; private set; }

        public int AnimationStep { get; private set; } = 0;

        public DynamicState State { get; set; } = DynamicState.RUN;

        private int ddt = 0;

        private GameConfigs gc;

        public BangBonusEffect()
        {
            BonusType = BonusEffect.BANG;
            gc = GameConfigs.GetInstance();
        }

        public void Update(int dt)
        {
            ddt += dt;
            if(ddt >= gc.ADTime)
            {
                ddt = 0;
                AnimationStep++;
                if(AnimationStep >= 7)
                {
                    AnimationStep = 7;
                    State = DynamicState.END;
                }
            }
        }
    }
}
