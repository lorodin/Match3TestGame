using Math3TestGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math3TestGame.Tools
{
    public class AnimationMoveItems : IAnimation
    {
        public AnimationState State { get; set; } = AnimationState.STOP;

        private Action onEnd;

        public AnimatorState AnimatorState { get; } = AnimatorState.DropDownItems;

        private GameConfigs gc;

        private List<GameObjectModel> gObjs;

        public AnimationMoveItems()
        {
            gc = GameConfigs.GetInstance();
        }

        public IAnimation OnEnd(Action action)
        {
            onEnd = action;
            return this;
        }

        public IAnimation Start(List<GameObjectModel> objects)
        {
            State = AnimationState.RUN;
            gObjs = objects;
            return this;
        }

        public void Update(float dt)
        {
            if (State == AnimationState.STOP || State == AnimationState.END) return;

            float ds = dt * gc.DefaultSpeed;
            int countComplete = 0;

            foreach(var m in gObjs)
            {

                if(m.Rect.Y == m.NewPosition.Y)
                {
                    countComplete++;
                    continue;
                }
                if(m.Rect.Y + ds > m.NewPosition.Y)
                {
                    m.Move(0, m.NewPosition.Y - m.Rect.Y);
                    continue;
                }
                m.Move(0, ds);
            }

            if(countComplete == gObjs.Count)
            {
                if (onEnd != null) onEnd();
                State = AnimationState.END;
            }
        }

        private Func<IAnimation> next;

        public void OnNext(Func<IAnimation> next)
        {
            this.next = next;
        }

        public IAnimation Next()
        {
            return next != null ? next() : null;
        }
    }
}
