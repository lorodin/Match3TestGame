using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Math3TestGame.Models
{
    public class BangGameObjectModel:GameObjectModel
    {
        public BangGameObjectModel(GameObjectModel g) : base(g)
        {
            Bonus = GameObjectBonus.BOMB;
            State = GameObjectState.SHOW;
        }

        public override void Kill()
        {
            base.Kill();
            Debug.WriteLine("Bomb killed");
        }

        private float bombTimer = 0;

        public override void Update(int dt)
        {
            ddt += dt;

            if (State == GameObjectState.HIDE && ddt >= gc.ADTime)
            {
                bombTimer += dt;
                AnimationStep++;

                if (AnimationStep >= 8)
                {
                    AnimationStep = 8;
                }

                if(bombTimer >= gc.BOMB_TIME)
                {
                    State = GameObjectState.KILLED;
                    AnimationStep = 8;
                    SendKillAction();
                }
            }

            if (ddt >= gc.ADTime)
            {
                ddt = 0;
                if (State == GameObjectState.NONE)
                {
                    AnimationStep = 0;
                }
                else if (State == GameObjectState.SHOW)
                {
                    AnimationStep--;
                    if (AnimationStep <= 0)
                    {
                        AnimationStep = 0;
                        State = GameObjectState.NONE;
                    }
                }
                else if (State == GameObjectState.SELECTED)
                {
                    AnimationStep += stepSelected;
                    if (AnimationStep <= 0)
                    {
                        AnimationStep = 0;
                        stepSelected = 1;
                    }
                    else if (AnimationStep == 5)
                    {
                        stepSelected = -1;
                    }
                }
            }
        }
    

        public override void Draw(SpriteBatch sb)
        {
            if(State != GameObjectState.KILLED)
            {
                if(State == GameObjectState.NONE || State == GameObjectState.SELECTED)
                {
                    sb.Draw(gc.DefaultSpriteMap, Rect, tHelper.GetTextureRegion(Tools.SpriteName.Bang, 0), Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
                }
                    

                sb.Draw(gc.DefaultSpriteMap, Rect, tHelper.GetTextureRegion(SpriteName, AnimationStep), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
            }
        }
    }
}
