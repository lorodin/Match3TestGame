using Math3TestGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math3TestGame.Tools
{
    public class AnimationKillItems : IAnimation
    {
        public AnimationState State { get; set; } = AnimationState.STOP;

        public AnimatorState AnimatorState { get; } = AnimatorState.WaitKilled;

        private Action onEnd;

        private List<GameObjectModel> gObjs;
        

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
            if (State == AnimationState.END || State == AnimationState.STOP) return;
            foreach(var go in gObjs)
            {
                if(go.State != GameObjectState.KILLED) return;
            }


            if (onEnd != null) onEnd();
            State = AnimationState.END;
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
