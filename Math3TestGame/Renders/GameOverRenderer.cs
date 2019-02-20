using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math3TestGame.Models;
using Math3TestGame.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Math3TestGame.Renders
{
    public class GameOverRenderer : IRenderer
    {
        private GameOverModel goModel;

        private TextureHelper tHelper;

        public GameOverRenderer(GameOverModel model)
        {
            goModel = model;
            tHelper = TextureHelper.GetInstance();
        }

        public void Draw(SpriteBatch sbatch)
        {
            sbatch.Draw(tHelper.DefaultSpriteMap, goModel.GameOverDialog.Region, tHelper.GetTextureRegion(goModel.GameOverDialog.SpriteName, 0), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0f);
            sbatch.Draw(tHelper.DefaultSpriteMap, goModel.GameOverButton.Region, tHelper.GetTextureRegion(goModel.GameOverButton.SpriteName, goModel.GameOverButton.SpriteAnimationStep), Color.White, 0, Vector2.Zero, SpriteEffects.None, 1f);

        }
    }
}
