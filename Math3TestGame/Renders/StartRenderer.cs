using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math3TestGame.Models.Interfaces;
using Math3TestGame.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Math3TestGame.Renders
{
    public class StartRenderer : IRenderer
    {
        private IDrawableModel playButton;

        private TextureHelper tHelper;

        public StartRenderer(IDrawableModel playButton)
        {
            this.playButton = playButton;
            tHelper = TextureHelper.GetInstance();
        }

        public void Draw(SpriteBatch sbatch)
        {
            sbatch.Draw(tHelper.DefaultSpriteMap, playButton.Region, tHelper.GetTextureRegion(playButton.SpriteName, playButton.SpriteAnimationStep), Color.White);
        }
    }
}
