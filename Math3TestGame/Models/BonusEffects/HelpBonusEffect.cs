using Math3TestGame.Models.GameModels;
using Math3TestGame.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math3TestGame.Models.BonusEffects
{
    public class HelpBonusEffect : IBonusEffect
    {
        public BonusEffect BonusType { get; private set; }

        public int AnimationStep { get; private set; } = 0;

        public DynamicState State { get; set; }

        public Direction Direction { get; private set; }

        private int ddt = 0;

        private GameConfigs gc;

        public HelpBonusEffect(Direction direction)
        {
            Direction = direction;
            gc = GameConfigs.GetInstance();
            BonusType = BonusEffect.HELP;
            State = DynamicState.RUN;
        }

        public void Start()
        {
            State = DynamicState.RUN;
        }

        public void Update(int dt)
        {
            if (State != DynamicState.RUN) return;
            ddt += dt;
            if(ddt >= gc.HelpTime)
            {
                State = DynamicState.END;
            }
        }
    }
}
