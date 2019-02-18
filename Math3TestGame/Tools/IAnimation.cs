using Math3TestGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math3TestGame.Tools
{
    public interface IAnimation
    {
        AnimatorState AnimatorState { get; }
        AnimationState State { get; set; }
        IAnimation Start(List<GameObjectModel> objects);
        IAnimation OnEnd(Action action);
        void OnNext(Func<IAnimation> next);
        IAnimation Next();
        void Update(float dt);
    }

    public enum AnimationState
    {
        STOP = 0,
        RUN = 1,
        END = 2
    }
}
