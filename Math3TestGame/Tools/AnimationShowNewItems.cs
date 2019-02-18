using Math3TestGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math3TestGame.Tools
{
    public class AnimationShowNewItems : IAnimation
    {
        public AnimationState State { get; set; } = AnimationState.STOP;

        private List<GameObjectModel> gObjs;

        public AnimatorState AnimatorState { get; } = AnimatorState.WaitShowItems;

        private Action onEnd;

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
            foreach(var g in gObjs)
            {
                if (g.State != GameObjectState.NONE) return;
            }
            //gObjs.Clear();
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
