using Math3TestGame.Models;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math3TestGame.Tools
{
    public class AnimationSwapObjects : IAnimation
    {
        public AnimationState State { get; set; } = AnimationState.STOP;

        public AnimatorState AnimatorState { get; } = AnimatorState.Swaping;

        private GameObjectModel m1;
        private GameObjectModel m2;

        private Action onEnd;

        private GameConfigs gc;

        private PointF speed1;
        private PointF speed2;
        
        public AnimationSwapObjects()
        {
            gc = GameConfigs.GetInstance();
        }

        public IAnimation OnEnd(Action action)
        {
            onEnd = action;
            return this;
        }

        public IAnimation Start(List<GameObjectModel> go)
        {
            m1 = go[0];
            m2 = go[1];

            m1.NewPosition = new Point(m2.Rect.X, m2.Rect.Y);
            m2.NewPosition = new Point(m1.Rect.X, m1.Rect.Y);

            speed1 = new PointF(m1.Rect.X == m1.NewPosition.X ? 0 : gc.DefaultSpeed,
                                m1.Rect.Y == m1.NewPosition.Y ? 0 : gc.DefaultSpeed);

            speed2 = new PointF(-1 * speed1.X, -1 * speed1.Y);

            State = AnimationState.RUN;

            return this;
        }


        private bool MoveAndCompleted(float dt, GameObjectModel m)
        {
            if (m.NewPosition.X == m.Rect.X && m.NewPosition.Y == m.Rect.Y) return true;

            if(m.NewPosition.X > m.Rect.X)
            {
                if(m.NewPosition.X < m.Rect.X + dt * gc.DefaultSpeed)
                {
                    m.Move(m.NewPosition.X - m.Rect.X, 0);
                    return true;
                }

                m.Move(gc.DefaultSpeed * dt, 0);

                return false;
            }else if(m.NewPosition.X < m.Rect.X)
            {
                if(m.NewPosition.X > m.Rect.X - dt * gc.DefaultSpeed)
                {
                    m.Move(m.Rect.X - m.NewPosition.X, 0);
                    return true;
                }

                m.Move(-gc.DefaultSpeed * dt, 0);

                return false;
            }

            if (m.NewPosition.Y > m.Rect.Y)
            {
                if (m.NewPosition.Y < m.Rect.Y + gc.DefaultSpeed * dt)
                {
                    m.Move(0, m.NewPosition.Y - m.Rect.Y);
                    return true;
                }

                m.Move(0, gc.DefaultSpeed * dt);

                return false;
            }
            else if (m.NewPosition.Y < m.Rect.Y)
            {
                if (m.NewPosition.Y > m.Rect.Y + gc.DefaultSpeed * dt)
                {
                    m.Move(0, m.Rect.Y - m.NewPosition.Y);
                    return true;
                }

                m.Move(0, -gc.DefaultSpeed * dt);

                return false;
            }

            return false;
        }

        public void Update(float dt)
        {
            if (State != AnimationState.RUN) return;

            bool moveCompleted = MoveAndCompleted(dt, m1) && MoveAndCompleted(dt, m2);

            if (!moveCompleted) return;

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
