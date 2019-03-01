using Math3TestGame.Models.BonusEffects;
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
    public delegate void Killed(AGameObject go);

    public abstract class AGameObject
    {
        public AGameObject Left { get; set; }
        public AGameObject Right { get; set; }
        public AGameObject Top { get; set; }
        public AGameObject Bottom { get; set; }

        public abstract Rectangle Region { get; set; }

        public abstract Point NewPosition { get; protected set; }

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
                    SpriteAnimationStep = 0;
                }
            }
        }

        public int hKilled { get; set; }

        public int vKilled { get; set; }

        public Point Value { get; set; }

        public event Killed OnKilled;

        public abstract int SpriteAnimationStep { get; protected set; }

        public abstract SpriteName SpriteName { get; protected set; }

        public PositionAnimationState Moving { get; private set; }

        public SpriteAnimationState AnimationState { get; set; } = SpriteAnimationState.SHOW;

        protected GameObjectFactory gFactory;

        public List<IBonusEffect> BonusEffects { get; private set; } = new List<IBonusEffect>();

        public BonusEffect Bonus { get; set; } = BonusEffect.NONE;

        private int ddt = 0;

        protected GameConfigs gc;

        private int selectedDirection = 1;

        public abstract GameMatrix Parent { get; protected set; }

        public AGameObject(Rectangle region, SpriteName spriteName, GameMatrix parent, AGameObject left = null, AGameObject right = null, AGameObject top = null, AGameObject bottom = null)
        {
            Region = region;

            this.Parent = parent;

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
        
        public AGameObject(AGameObject item, SpriteName sprite):this(item.Region, sprite, item.Parent, item.Left, item.Right, item.Top, item.Bottom)
        {
        }


        
        public void NewLife()
        {
            if (Left != null) Region = new Rectangle(Left.NewPosition.X + gc.RegionWidth, Left.NewPosition.Y, gc.RegionWidth, gc.RegionHeight);
            else if (Right != null) Region = new Rectangle(Right.NewPosition.X - gc.RegionWidth, Right.NewPosition.Y, gc.RegionWidth, gc.RegionHeight);

            Parent.ReplaceItem(this);
        }

        private void KillLeftLine()
        {
            if (Left == null) return;
            Left.Kill(new LineBonusEffect(LineBonusEffectDirection.RL));
        }

        private void KillRightLine()
        {
            if (Right == null) return;
            Right.Kill(new LineBonusEffect(LineBonusEffectDirection.LR));
        }

        private void KillTopLine()
        {
            if (Top == null) return;
            Top.Kill(new LineBonusEffect(LineBonusEffectDirection.BT));
        }

        private void KillBottomLine()
        {
            if (Bottom == null) return;
            Bottom.Kill(new LineBonusEffect(LineBonusEffectDirection.TB));
        }

        public void SetBonus(IBonusEffect bEffect)
        {
            BonusEffects.Add(bEffect);
        }

        public void Kill(IBonusEffect bEffect)
        {
            //if (BonusEffects.Count != 0) return;
            var finded = BonusEffects.FindAll((b) => {
                var bb = b.BonusType == bEffect.BonusType;
                return bb;
            });

            if (finded.Count == 0)
            {
                BonusEffects.Add(bEffect);
                if(bEffect.BonusType == BonusEffect.BANG)
                {
                    AudioHelper.GetInstance().Play(SongName.BANG);
                }
                if (bEffect.BonusType == BonusEffect.LINE_H || bEffect.BonusType == BonusEffect.LINE_V)
                {
                    ((LineBonusEffect)bEffect).BeforeLastStep += (effect) =>
                    {
                        switch (effect.Direction)
                        {
                            case LineBonusEffectDirection.LR:
                                KillRightLine();
                                break;
                            case LineBonusEffectDirection.RL:
                                KillLeftLine();
                                break;
                            case LineBonusEffectDirection.TB:
                                KillBottomLine();
                                break;
                            case LineBonusEffectDirection.BT:
                                KillTopLine();
                                break;
                        }
                    };
                }

                Kill();
            }
        }

        public abstract void Kill();
        
        protected bool CanKilled()
        {
            try {
                return AnimationState == SpriteAnimationState.NONE && Visible;// && BonusEffects.Count == 0;
            } catch (Exception e)
            {
                return false;
            }
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

        private SearchModel CountNear(SearchModel search, AGameObject next, Direction direction)
        {
            if (next == null) return search;

            if(search.SpriteName == next.SpriteName)
            {
                search.Count++;
                return CountNear(search, direction == Direction.TOP ? next.Top : 
                                         direction == Direction.BOTTOM ? next.Bottom : 
                                         direction == Direction.LEFT ? next.Left : 
                                         next.Right, 
                                         direction);

            }else if(search.SpriteName == SpriteName.GameObject6)
            {
                search.SpriteName = next.SpriteName;
                search.Count++;
                return CountNear(search, direction == Direction.TOP ? next.Top : 
                                         direction == Direction.BOTTOM ? next.Bottom : 
                                         direction == Direction.LEFT ? next.Left : 
                                         next.Right, 
                                         direction);
            }

            return search;
        }

        public bool HasTurn()
        {


            var sl = new SearchModel
            {
                Count = 0,
                SpriteName = SpriteName
            };

            var st = new SearchModel
            {
                Count = 0,
                SpriteName = SpriteName
            };

            var sr = new SearchModel
            {
                Count = 0,
                SpriteName = SpriteName
            };

            var sb = new SearchModel
            {
                Count = 0,
                SpriteName = SpriteName
            };


            var slt = new SearchModel
            {
                Count = 0,
                SpriteName = SpriteName
            };

            var slb = new SearchModel
            {
                Count = 0,
                SpriteName = SpriteName
            };

            if (Left != null)
            {
                slt = CountNear(slt, Left.Top, Direction.TOP);
                slb = CountNear(slb, Left.Bottom, Direction.BOTTOM);
                
                if (slt.SpriteName == slb.SpriteName)
                {
                    int totalCount = slt.Count + slb.Count + 1;
                    if(totalCount >= 3)
                    {
                        BonusEffects.Add(new HelpBonusEffect(Direction.LEFT));
                        SetDefaultState();
                        return true;
                    }
                }

                sl = CountNear(sl, Left.Left, Direction.LEFT);

                if(sl.Count + 1 >= 3)
                {
                    BonusEffects.Add(new HelpBonusEffect(Direction.LEFT));
                    return true;
                }
            }

            var srt = new SearchModel
            {
                Count = 0,
                SpriteName = SpriteName
            };

            var srb = new SearchModel
            {
                Count = 0,
                SpriteName = SpriteName
            };

            if (Right != null)
            {
                srt = CountNear(srt, Right.Top, Direction.TOP);
                srb = CountNear(srb, Right.Bottom, Direction.BOTTOM);

                if(srt.SpriteName == srb.SpriteName)
                {
                    int totalCount = srt.Count + srb.Count + 1;
                    if(totalCount >= 3)
                    {
                        BonusEffects.Add(new HelpBonusEffect(Direction.RIGHT));
                       return true;
                    }
                }

                sr = CountNear(sr, Right.Right, Direction.RIGHT);
                if(sr.Count + 1 >= 3)
                {
                    BonusEffects.Add(new HelpBonusEffect(Direction.RIGHT));
                    return true;
                }
            }


            var stl = new SearchModel
            {
                Count = 0,
                SpriteName = SpriteName
            };

            var str = new SearchModel
            {
                Count = 0,
                SpriteName = SpriteName
            };

            if(Top != null)
            {
                stl = CountNear(stl, Top.Left, Direction.LEFT);
                str = CountNear(str, Top.Right, Direction.RIGHT);

                if(stl.SpriteName == str.SpriteName)
                {
                    int totalCount = stl.Count + str.Count + 1;
                    if(totalCount >= 3)
                    {
                        BonusEffects.Add(new HelpBonusEffect(Direction.TOP));
                        return true;
                    }
                }

                st = CountNear(st, Top.Top, Direction.TOP);
                if(st.Count >= 3)
                {
                    BonusEffects.Add(new HelpBonusEffect(Direction.TOP));
                    return true;
                }
            }

            var sbl = new SearchModel
            {
                Count = 0,
                SpriteName = SpriteName
            };

            var sbr = new SearchModel
            {
                Count = 0,
                SpriteName = SpriteName
            };

            if(Bottom != null)
            {
                sbl = CountNear(sbl, Bottom.Left, Direction.LEFT);
                sbr = CountNear(sbr, Bottom.Right, Direction.RIGHT);
                if(sbl.SpriteName == sbr.SpriteName)
                {
                    int totalCount = sbl.Count + sbr.Count + 1;
                    if(totalCount >= 3)
                    {
                        BonusEffects.Add(new HelpBonusEffect(Direction.BOTTOM));
                        return true;
                    }
                }

                sb = CountNear(sb, Bottom.Bottom, Direction.BOTTOM);

                if(sb.Count + 1 >= 3)
                {
                    BonusEffects.Add(new HelpBonusEffect(Direction.BOTTOM));
                    return true;
                }
            }


            if(sbl.Count + 1 >= 3 || sbr.Count + 1 >= 3)
            {
                BonusEffects.Add(new HelpBonusEffect(Direction.BOTTOM));
                return true;
            }

            if(stl.Count + 1 >= 3 || str.Count + 1 >= 3)
            {
                BonusEffects.Add(new HelpBonusEffect(Direction.TOP));
                return true;
            }

            if (slt.Count + 1 >= 3 || slb.Count + 1 >= 3)
            {
                BonusEffects.Add(new HelpBonusEffect(Direction.LEFT));
                return true;
            }

            if(srt.Count + 1 >= 3 || srb.Count + 1 >= 3)
            {
                BonusEffects.Add(new HelpBonusEffect(Direction.RIGHT));
                return true;
            }

            return false;
        }

        public void SetDefaultState()
        {
            AnimationState = SpriteAnimationState.NONE;
            SpriteAnimationStep = 0;
        }

        public bool IsBusy()
        {
            return Moving != PositionAnimationState.NONE || (AnimationState != SpriteAnimationState.NONE && AnimationState != SpriteAnimationState.SELECT) || BonusEffects.FindAll(b => b.BonusType != BonusEffect.HELP).Count != 0;
        }
        
        public void ClearHelp()
        {
            BonusEffects.RemoveAll(b => b.BonusType == BonusEffect.HELP);
        }

        private void BangNear()
        {
            AudioHelper.GetInstance().Play(SongName.BANG);

            if (Left != null)
            {
                if (Left.Top != null) Left.Top.Kill(new BangBonusEffect());
                Left.Kill(new BangBonusEffect());
            }
            if (Top != null)
            {
                if (Top.Right != null) Top.Right.Kill(new BangBonusEffect());
                Top.Kill(new BangBonusEffect());
            }
            if (Right != null)
            {
                if (Right.Bottom != null) Right.Bottom.Kill(new BangBonusEffect());
                Right.Kill(new BangBonusEffect());
            }
            if (Bottom != null)
            {
                if (Bottom.Left != null) Bottom.Left.Kill(new BangBonusEffect());
                Bottom.Kill();
            }

            AnimationState = SpriteAnimationState.HIDE;
        }

        protected void SetBonusEffect(IBonusEffect effect)
        {
            BonusEffects.Add(effect);
        }

        public void Update(int dt)
        {
            if (Moving == PositionAnimationState.NONE && AnimationState == SpriteAnimationState.NONE && BonusEffects.Count == 0) return;

            if(Moving == PositionAnimationState.MOVE)
            {
                UpdatePosition(dt);
                return;
            }

            if(BonusEffects.Count != 0)
            {
                int countEnd = 0;

                for (int i = 0; i < BonusEffects.Count; i++) {
                    BonusEffects[i].Update(dt);
                    if(BonusEffects[i].State == DynamicState.END)
                    {
                        switch (BonusEffects[i].BonusType)
                        {
                            case BonusEffect.WAIT_BANG:
                                BangNear();
                                break;
                        }
                    }
                    countEnd += BonusEffects[i].State == DynamicState.END || BonusEffects[i].State == DynamicState.STOP ? 1 : 0;
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
                    if(SpriteAnimationStep >= 9)
                    {
                        SpriteAnimationStep = 9;
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
        LINE_H = 3,
        WAIT_BANG = 4,
        MULTICOLOR = 5,
        HELP = 6,
    }
    
    public enum Direction
    {
        TOP = 0,
        BOTTOM = 1,
        RIGHT = 2,
        LEFT = 3
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
