using Math3TestGame.Models.Animations;
using Math3TestGame.Tools;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math3TestGame.Models.GameModels
{
    public delegate void Killed(GameObject go);

    public class GameObject
    {
        public GameObject Left { get; set; }
        public GameObject Right { get; set; }
        public GameObject Top { get; set; }
        public GameObject Bottom { get; set; }

        public Rectangle Region { get; set; }

        public bool Visible { get; set; } = true;

        public bool Selected { get; set; }

        public int hKilled { get; set; }

        public int vKilled { get; set; }

        public Point Value { get; set; }

        public event Killed OnKilled;

        public int SpriteAnimationStep { get; private set; }
        
        public SpriteName SpriteName { get; private set; }

        public SpriteAnimationState AnimationState { get; set; } = SpriteAnimationState.SHOW;

        public GameObject(Rectangle region, SpriteName spriteName, GameObject left = null, GameObject right = null, GameObject top = null, GameObject bottom = null)
        {
            Region = region;

            SpriteName = spriteName;

            Left = left;
            Right = right;
            Top = top;
            Bottom = bottom;

            Visible = true;

            if(Left != null)    Left.Right = this;
            if(Right != null)   Right.Left = this;
            if(Top != null)     Top.Bottom = this;
            if(Bottom != null)  Bottom.Top = this; 
        }
        
        public GameObject(GameObject item, SpriteName sprite):this(item.Region, sprite, item.Left, item.Right, item.Top, item.Bottom)
        {
        }

        public void Kill()
        {
            AnimationState = SpriteAnimationState.HIDE;
            Visible = false;
        }

        public void Move(int x, int y)
        {

        }

        public void Update(float dt)
        {

        }

        public override string ToString()
        {
            return ((int)SpriteName).ToString();
            //return hKilled.ToString();
        }
    }
    
    public enum BonusEffect
    {
        BANG = 0,
        LINE = 1
    }
    
    public enum PositionAnimationState
    {
        NONE = 0,
        MOVE = 1
    }

    public enum SpriteAnimationState
    {
        SHOW = 0,
        HIDE = 1,
        SELECT = 2
    }
}
