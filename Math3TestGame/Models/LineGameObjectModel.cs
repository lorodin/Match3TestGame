using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math3TestGame.Models
{
    public class LineGameObjectModel:GameObjectModel
    {
        public LineBonusType LineType { get; private set; }
        public LineGameObjectModel(GameObjectModel g, LineBonusType type) : base(g)
        {
            Bonus = GameObjectBonus.LINE;
            LineType = type;
            State = GameObjectState.SHOW;
        }

        public override void Draw(SpriteBatch sb)
        {
            if (State != GameObjectState.KILLED)
            {
                if (State == GameObjectState.NONE || State == GameObjectState.SELECTED)
                    sb.Draw(gc.DefaultSpriteMap, Rect, 
                                                tHelper.GetTextureRegion(LineType == LineBonusType.H ? Tools.SpriteName.LineH : Tools.SpriteName.LineV, 0), 
                                                Color.White, 
                                                0, 
                                                Vector2.Zero, 
                                                SpriteEffects.None, 1);

                sb.Draw(gc.DefaultSpriteMap, Rect, tHelper.GetTextureRegion(SpriteName, AnimationStep), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
            }
        }
    }
}
