using Math3TestGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math3TestGame.Tools
{
    public class EndAnimation : IAnimation
    {
        public AnimatorState AnimatorState { get; } = AnimatorState.Free;

        public AnimationState State { get; set; } = AnimationState.END;
        
        public IAnimation OnEnd(Action action)
        {
            return this;
        }

        public IAnimation Start(List<GameObjectModel> a)
        {
            return this;
        }

        public void Update(float dt)
        {

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
