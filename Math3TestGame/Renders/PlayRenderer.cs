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
    public class PlayRenderer : IRenderer
    {
        private GameModel gameModel;

        private GameConfigs gc;
        private TextureHelper tHelper;

        public PlayRenderer(GameModel gm)
        {
            gameModel = gm;
            gc = GameConfigs.GetInstance();
            tHelper = TextureHelper.GetInstance();

        }

        public void Draw(SpriteBatch sbatch)
        {
            if (gameModel.GameOver) return;

            sbatch.DrawString(gc.DefaultFont, "Total points: " + gameModel.BonusPoints.Points, new Vector2(gameModel.BonusPoints.Region.X, gameModel.BonusPoints.Region.Y), Color.White);
            sbatch.DrawString(gc.DefaultFont, "Time: " + gameModel.Timer.StrSec, new Vector2(gameModel.Timer.Region.X, gameModel.Timer.Region.Y), Color.White);


            foreach(var go in gameModel.GameMatrix)
            {
                //sb.Draw(tHelper.DefaultSpriteMap, Rect, tHelper.GetTextureRegion(SpriteName.GameOverButton, 0), Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
                if (!go.Visible) continue;

                sbatch.Draw(tHelper.DefaultSpriteMap, go.Region, tHelper.GetTextureRegion(go.SpriteName, go.SpriteAnimationStep), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
            }
        }
    }
}
