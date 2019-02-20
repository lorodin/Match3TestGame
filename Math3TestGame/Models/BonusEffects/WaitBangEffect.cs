using Math3TestGame.Models.GameModels;
using Math3TestGame.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math3TestGame.Models.BonusEffects
{
    public class WaitBangEffect : IBonusEffect
    {
        public DynamicState State { get; set; } = DynamicState.RUN;
        public BonusEffect BonusType { get; private set; }

        public int AnimationStep { get; private set; } = 0;

        private int dtt = 0;

        private GameConfigs gc;

        public WaitBangEffect()
        {
            gc = GameConfigs.GetInstance();
            BonusType = BonusEffect.WAIT_BANG;
        }

        public void Update(int dt)
        {
            dtt += dt;
            if (dtt >= gc.BOMB_TIME)
            {
                State = DynamicState.END;
            }
        }
    }
}
