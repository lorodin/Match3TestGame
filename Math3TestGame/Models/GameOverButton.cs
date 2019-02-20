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
    public class GameOverButton : IDrawableModel
    {
        public Rectangle Rect { get; set; }
        public Rectangle Region { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public SpriteName SpriteName => throw new NotImplementedException();

        public int SpriteAnimationStep => throw new NotImplementedException();

        public SpriteAnimationState AnimationState { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private GameConfigs gc;
        private TextureHelper tHelper;
        

        public GameOverButton()
        {
            gc = GameConfigs.GetInstance();
            tHelper = TextureHelper.GetInstance();

            Rect = new Rectangle(gc.Center.X - gc.RegionWidth / 2, gc.Center.Y + gc.RegionHeight / 2, gc.RegionWidth, 20);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(tHelper.DefaultSpriteMap, Rect, tHelper.GetTextureRegion(SpriteName.GameOverButton, 0), Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
        }

        public void Update(int dt)
        {
            throw new NotImplementedException();
        }
    }
}
