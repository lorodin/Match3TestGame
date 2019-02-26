using Math3TestGame.Tools;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math3TestGame.Models.GameModels
{
    public class SimpleGameObject:AGameObject
    {
        public override Rectangle Region { get; set; }
        public override Point NewPosition { get; protected set; }
        public override int SpriteAnimationStep { get; protected set; }
        public override SpriteName SpriteName { get; protected set; }
        public override GameMatrix Parent { get; protected set; }

        public SimpleGameObject(Rectangle region, SpriteName spriteName, GameMatrix parent, AGameObject left = null, AGameObject right = null, AGameObject top = null, AGameObject bottom = null)
            :base(region, spriteName, parent, left, right, top, bottom)
        {
            
        }

        public SimpleGameObject(AGameObject item, SpriteName sprite) : this(item.Region, sprite, item.Parent, item.Left, item.Right, item.Top, item.Bottom)
        {
        }

        public override void Kill()
        {
            if (!CanKilled()) return;

            if (hKilled >= 3 && vKilled >= 3)
            {
                Parent.ReplaceItem(this, BonusEffect.BANG);
                return;
            }
            else if (Selected)
            {
                if (hKilled >= 5 || vKilled >= 5)
                {
                    Parent.ReplaceItem(this, BonusEffect.BANG);
                    return;
                }
                else if (hKilled >= 4 || vKilled >= 4)
                {
                    Parent.ReplaceItem(this, hKilled >= 4 ? BonusEffect.LINE_H : BonusEffect.LINE_V);
                    return;
                }
            }
            
            AnimationState = SpriteAnimationState.HIDE;
        }
    }
}
