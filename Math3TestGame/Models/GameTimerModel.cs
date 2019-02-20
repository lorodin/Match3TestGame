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
    //public delegate void TimerEnded();

    public class GameTimerModel : IDynamic, IDrawableModel
    {
        //public event TimerEnded OnTimerEnded;
        
        public SpriteName SpriteName { get; }
        public int SpriteAnimationStep { get; }
        public SpriteAnimationState AnimationState { get; set; }

        
        public Rectangle Region { get; set; }
        public DynamicState State { get; set; } = DynamicState.STOP;


        private int totalSeconds = 60;

        private DateTime start;

        public string StrSec { get; private set; } = "60";

        private Vector2 position;

        private GameConfigs gc;

        private bool enabled = false;

        public GameTimerModel()
        {
            gc = GameConfigs.GetInstance();
            Region = new Rectangle(gc.GetRealPoint(7.5f, 0.5f), new Point(0, 0));
            position = new Vector2(Region.X, Region.Y);
            State = DynamicState.END;
        }
        
        public void Start()
        {
            enabled = true;
            start = DateTime.Now;
            State = DynamicState.RUN;
        }

        public void Update(int dt)
        {
            int sec = totalSeconds - (DateTime.Now - start).Seconds - 1;

            StrSec = sec > 9 ? sec.ToString() : "0" + sec;

            if (sec <= 0 && enabled)
            {
                enabled = false;
                State = DynamicState.END;
                //if (OnTimerEnded != null) OnTimerEnded();
            }
        }
    }
}
