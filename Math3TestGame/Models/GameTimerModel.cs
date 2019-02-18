using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Math3TestGame.Models
{
    public delegate void TimerEnded();

    public class GameTimerModel : IDrawableModel
    {
        public event TimerEnded OnTimerEnded;

        public Rectangle Rect { get; set; }

        private int totalSeconds = 60;

        private DateTime start;

        private string strSec = "60";

        private Vector2 position;

        private GameConfigs gc;

        private bool enabled = false;

        public GameTimerModel()
        {
            gc = GameConfigs.GetInstance();
            Rect = new Rectangle(gc.GetRealPoint(7.5f, 0.5f), new Point(0, 0));
            position = new Vector2(Rect.X, Rect.Y);
        }
        
        public void Start()
        {
            enabled = true;
            start = DateTime.Now;
        }

        public void Draw(SpriteBatch sb)
        {
            if(enabled) sb.DrawString(gc.DefaultFont, "Time: " + strSec, position, Color.White);
        }

        public void Update(int dt)
        {
            int sec = totalSeconds - (DateTime.Now - start).Seconds - 1;
            strSec = sec > 9 ? sec.ToString() : "0" + sec;
            if (sec <= 0 && enabled)
            {
                enabled = false;
                if (OnTimerEnded != null) OnTimerEnded();
            }
        }
    }
}
