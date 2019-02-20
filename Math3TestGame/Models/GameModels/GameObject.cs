using Math3TestGame.Models.Animations;
using Math3TestGame.Models.Interfaces;
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

        public Point NewPosition { get; private set; }

        public bool Visible { get; set; } = true;
        private bool selected = false;

        public bool Selected {
            get {
                return selected;
            } set
            {
                selected = value;

                if (selected)
                {
                    AnimationState = SpriteAnimationState.SELECT;
                }
                else
                {
                    AnimationState = SpriteAnimationState.NONE;
                }
            }
        }

        public int hKilled { get; set; }

        public int vKilled { get; set; }

        public Point Value { get; set; }

        public event Killed OnKilled;

        public int SpriteAnimationStep { get; private set; }
        
        public SpriteName SpriteName { get; private set; }

        public PositionAnimationState Moving { get; private set; }

        public SpriteAnimationState AnimationState { get; set; } = SpriteAnimationState.SHOW;

        private GameObjectFactory gFactory;

        public List<IDynamic> BonusEffects { get; private set; } = new List<IDynamic>();

        private int ddt = 0;

        private GameConfigs gc;

        private int selectedDirection = 1;

        public GameObject(Rectangle region, SpriteName spriteName, GameObject left = null, GameObject right = null, GameObject top = null, GameObject bottom = null)
        {
            Region = region;

            NewPosition = new Point(region.X, region.Y);

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

            AnimationState = SpriteAnimationState.SHOW;
            SpriteAnimationStep = 8;

            gFactory = GameObjectFactory.GetInstance();
            gc = GameConfigs.GetInstance();
        }
        
        public GameObject(GameObject item, SpriteName sprite):this(item.Region, sprite, item.Left, item.Right, item.Top, item.Bottom)
        {
        }


        public void NewLife()
        {
            Visible = true;

            AnimationState = SpriteAnimationState.SHOW;
            Moving = PositionAnimationState.NONE;

            SpriteName = gFactory.RandomSpriteName();

            SpriteAnimationStep = 8;

            Region = new Rectangle(NewPosition, new Point(gc.RegionWidth, gc.RegionHeight));

            if(Left != null)
            {
                Region = new Rectangle(Left.NewPosition.X + gc.RegionWidth, Left.NewPosition.Y, gc.RegionWidth, gc.RegionHeight);
            }else if(Right != null)
            {
                Region = new Rectangle(Right.NewPosition.X - gc.RegionWidth, Right.NewPosition.Y, gc.RegionWidth, gc.RegionHeight);
            }

            /*if(hKilled >= 3 && vKilled >= 3)
            {
                
            }*/

            hKilled = 0;
            vKilled = 0;
        }

        public void Kill()
        {
            AnimationState = SpriteAnimationState.HIDE;
        }
        

        private void KillComplete()
        {

        }

        public void MoveH()
        {
            AnimationState = SpriteAnimationState.NONE;
            if (Top != null)
                NewPosition = new Point(Top.NewPosition.X, Top.NewPosition.Y + gc.RegionHeight);

            else if (Bottom != null)
                NewPosition = new Point(Bottom.NewPosition.X, Bottom.NewPosition.Y - gc.RegionHeight);

            SpriteAnimationStep = 0;
            Moving = PositionAnimationState.MOVE;
        }

        public void MoveV()
        {
            //if (newPosition.X == x && newPosition.Y == y) return;
            AnimationState = SpriteAnimationState.NONE;
            if (Left != null)
                NewPosition = new Point(Left.NewPosition.X + gc.RegionWidth, Left.NewPosition.Y);

            else if (Right != null)
                NewPosition = new Point(Right.NewPosition.X - gc.RegionWidth, Right.NewPosition.Y);

            SpriteAnimationStep = 0;
            Moving = PositionAnimationState.MOVE;
        }

        private void UpdatePosition(int dt)
        {
            if(NewPosition.Y > Region.Y)
            {
                if(Region.Y + (int)(gc.DefaultSpeed * dt) >= NewPosition.Y)
                {
                    Region = new Rectangle(NewPosition.X, NewPosition.Y, gc.RegionWidth, gc.RegionHeight);
                    Moving = PositionAnimationState.NONE;
                }
                else
                {
                    Region = new Rectangle(Region.X, Region.Y + (int)(gc.DefaultSpeed * dt), gc.RegionWidth, gc.RegionHeight);
                }
            }else if(NewPosition.Y < Region.Y)
            {
                if(Region.Y - (int)(gc.DefaultSpeed * dt) <= NewPosition.Y)
                {
                    Region = new Rectangle(NewPosition.X, NewPosition.Y, gc.RegionWidth, gc.RegionHeight);
                    Moving = PositionAnimationState.NONE;
                }
                else
                {
                    Region = new Rectangle(Region.X, Region.Y - (int)(gc.DefaultSpeed * dt), gc.RegionWidth, gc.RegionHeight);
                }
            }else if(NewPosition.X > Region.X)
            {
                if (Region.X + (int)(gc.DefaultSpeed * dt) >= NewPosition.X)
                {
                    Region = new Rectangle(NewPosition.X, NewPosition.Y, gc.RegionWidth, gc.RegionHeight);
                    Moving = PositionAnimationState.NONE;
                }
                else
                {
                    Region = new Rectangle(Region.X + (int)(gc.DefaultSpeed * dt), Region.Y, gc.RegionWidth, gc.RegionHeight);
                }
            }else if(NewPosition.X < Region.X)
            {
                if(Region.X - (int)(gc.DefaultSpeed * dt) <= NewPosition.X)
                {
                    Region = new Rectangle(NewPosition.X, NewPosition.Y, gc.RegionWidth, gc.RegionHeight);
                    Moving = PositionAnimationState.NONE;
                }
                else
                {
                    Region = new Rectangle(Region.X - (int)(gc.DefaultSpeed * dt), Region.Y, gc.RegionWidth, gc.RegionHeight);
                }
            }
            else
            {
                Moving = PositionAnimationState.NONE;
            }
        }


        public bool IsBusy()
        {
            return Moving != PositionAnimationState.NONE || (AnimationState != SpriteAnimationState.NONE && AnimationState != SpriteAnimationState.SELECT) || BonusEffects.Count != 0;
        }

        public void Update(int dt)
        {
            if (Moving == PositionAnimationState.NONE && AnimationState == SpriteAnimationState.NONE) return;

            if(Moving == PositionAnimationState.MOVE)
            {
                UpdatePosition(dt);
                return;
            }

            if(BonusEffects.Count != 0)
            {
                int countEnd = 0;
                foreach(var b in BonusEffects)
                {
                    b.Update(dt);
                    countEnd += b.State == DynamicState.END || b.State == DynamicState.STOP ? 1 : 0;
                }

                if (countEnd == BonusEffects.Count) BonusEffects.Clear();
            }


            ddt += dt;

            if(ddt >= gc.ADTime)
            {
                ddt = 0;
                if(AnimationState == SpriteAnimationState.SELECT)
                {
                    SpriteAnimationStep += selectedDirection;
                    if(SpriteAnimationStep <= 0 || SpriteAnimationStep == 5)
                    {
                        selectedDirection *= -1;
                    }
                    if (SpriteAnimationStep < 0) SpriteAnimationStep = 1;
                    return;
                }else if(AnimationState == SpriteAnimationState.SHOW)
                {
                    SpriteAnimationStep--;
                    if(SpriteAnimationStep <= 0)
                    {
                        SpriteAnimationStep = 0;
                        AnimationState = SpriteAnimationState.NONE;
                    }
                    return;
                }
                else if(AnimationState == SpriteAnimationState.HIDE)
                {
                    SpriteAnimationStep++;
                    if(SpriteAnimationStep == 8)
                    {
                        SpriteAnimationStep = 8;
                        AnimationState = SpriteAnimationState.NONE;
                        Visible = false;
                    }
                }
            }
        }

        public override string ToString()
        {
            return ((int)SpriteName).ToString();
        }
    }
    
    public enum BonusEffect
    {
        NONE = 0,
        BANG = 1,
        LINE_V = 2,
        LINE_H = 3
    }
    
    public enum PositionAnimationState
    {
        NONE = 0,
        MOVE = 1
    }

    public enum SpriteAnimationState
    {
        NONE = 0,
        SHOW = 1,
        HIDE = 2,
        SELECT = 3
    }
}
