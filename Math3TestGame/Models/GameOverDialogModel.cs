using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math3TestGame.Models.GameModels;
using Math3TestGame.Models.Interfaces;
using Math3TestGame.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Math3TestGame.Models
{
    public delegate void ClickButton();

    public class GameOverDialogModel : IDrawableModel
    {
        public event ClickButton OnClickButton;
        public Rectangle Rect { get; set; }
        public Rectangle Region { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public SpriteName SpriteName => throw new NotImplementedException();

        public int SpriteAnimationStep => throw new NotImplementedException();

        public SpriteAnimationState AnimationState { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private GameConfigs gc;

        private TextureHelper tHelper;

        private GameOverButton gOButton;
        public GameOverDialogModel()
        {
            gc = GameConfigs.GetInstance();
            tHelper = TextureHelper.GetInstance();
            Rect = new Rectangle(gc.Center.X - gc.RegionWidth * 3 / 2, gc.Center.Y - gc.RegionHeight / 2, gc.RegionWidth * 3, gc.RegionHeight * 2);
            gOButton = new GameOverButton();
        }

        public void Click(int x, int y)
        {
            if(gOButton.Rect.Contains(x, y))
            {
                if (OnClickButton != null) OnClickButton();
            }
        }


        public void Draw(SpriteBatch sb)
        {
            gOButton.Draw(sb);
            sb.Draw(tHelper.DefaultSpriteMap, Rect, tHelper.GetTextureRegion(SpriteName.GameOverBG, 0), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
        }

        public void Update(int dt)
        {

        }
    }
}
