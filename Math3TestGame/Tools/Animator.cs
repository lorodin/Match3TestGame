using Math3TestGame.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math3TestGame.Tools
{
    public class Animator
    {
        public AnimatorState State { get; private set; } = AnimatorState.Free;
        
        private Queue<IAnimation> animations = new Queue<IAnimation>();

        private Queue<Func<IAnimation>> aActions = new Queue<Func<IAnimation>>();

        private IAnimation currentAnimation;

        
        public Animator()
        {

        }

        public Animator Next(Func<IAnimation> anim)
        {
            aActions.Enqueue(anim);
            return this;
        }

        public void RunAnimation(IAnimation animation, List<GameObjectModel> gameObjects)
        {
            if (this.State != AnimatorState.Free) return;

            State = animation.AnimatorState;

            currentAnimation = animation.Start(gameObjects);
        }


        public void Update(int dt)
        {
            if(currentAnimation == null)
            {
                State = AnimatorState.Free;
                return;
            }

            currentAnimation.Update(dt);

            if(currentAnimation.State == AnimationState.END)
            {
                currentAnimation = currentAnimation.Next();
                State = currentAnimation != null ? currentAnimation.AnimatorState : AnimatorState.Free;
            }

        }
        
    }

    public enum AnimatorState
    {
        Free = 0,
        WaitKilled = 1,
        DropDownItems = 2,
        WaitShowItems = 3,
        Swaping = 4
    }
}
