using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math3TestGame.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Math3TestGame.Models
{
    public delegate void Killed(GameObjectModel go);

    public class GameObjectModel : IDrawableModel
    {
        public GameObjectState State { get; set; } = GameObjectState.SHOW;
        public GameObjectBonus Bonus { get; set; } = GameObjectBonus.NONE;

        public event Killed OnKilled;

        public Rectangle Rect { get; set; }

        public Point NewPosition { get; set; }

        public SpriteName SpriteName { get; set; }

        protected GameConfigs gc;

        protected TextureHelper tHelper;

        protected int AnimationStep = 0;

        protected int ddt;

        protected int stepSelected = 1;

        public GameObjectModel(int i, int j, SpriteName sprName)
        {
            gc = GameConfigs.GetInstance();
            tHelper = TextureHelper.GetInstance();
            Rect = new Rectangle(gc.GetRealPoint(i, j), new Point(gc.RegionWidth, gc.RegionHeight));
            SpriteName = sprName;
        }

        public GameObjectModel(GameObjectModel g)
        {
            gc = GameConfigs.GetInstance();
            tHelper = TextureHelper.GetInstance();
            Rect = new Rectangle(g.Rect.X, g.Rect.Y, g.Rect.Width, g.Rect.Height);
            SpriteName = g.SpriteName;
        }
        
        public void Move(float x, float y)
        {
            Rect = new Rectangle(new Point(Rect.X + (int)x, Rect.Y + (int)y), new Point(gc.RegionWidth, gc.RegionHeight));//.Offset(x, y);
        }

        public virtual void Draw(SpriteBatch sb)
        {
            if (State != GameObjectState.KILLED)
                sb.Draw(gc.DefaultSpriteMap, Rect, tHelper.GetTextureRegion(SpriteName, AnimationStep), Color.White);
        }

        protected void SendKillAction()
        {
            if (OnKilled != null) OnKilled(this);
            OnKilled = null;
        }

        public virtual void Kill()
        {
            State = GameObjectState.HIDE;
        }

        public virtual void Update(int dt)
        {
            ddt += dt;
            if(ddt >= gc.ADTime)
            {
                ddt = 0;
                if(State == GameObjectState.NONE)
                {
                    AnimationStep = 0;
                }else if(State == GameObjectState.SHOW)
                {
                    AnimationStep--;
                    if(AnimationStep <= 0)
                    {
                        AnimationStep = 0;
                        State = GameObjectState.NONE;
                    }
                }else if(State == GameObjectState.HIDE)
                {
                    AnimationStep++;
                    if(AnimationStep >= 8)
                    {
                        AnimationStep = 8;
                        State = GameObjectState.KILLED;
                        SendKillAction();
                    }
                }else if(State == GameObjectState.SELECTED)
                {
                    AnimationStep += stepSelected;
                    if(AnimationStep <= 0)
                    {
                        AnimationStep = 0;
                        stepSelected = 1;
                    }else if(AnimationStep == 5)
                    {
                        stepSelected = -1;
                    }
                }
            }
        }
    }

    public enum GameObjectState
    {
        KILLED = 0,
        NONE = 3,
        SHOW = 1,
        HIDE = 2,
        SELECTED = 4
    }

    public enum GameObjectBonus
    {
        NONE = 0,
        LINE = 1,
        BOMB = 2
    }

    public enum LineBonusType
    {
        V = 0,
        H = 1
    }
}
