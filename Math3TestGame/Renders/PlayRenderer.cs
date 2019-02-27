using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math3TestGame.Models;
using Math3TestGame.Models.BonusEffects;
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
            if (gameModel.State == GameState.GAME_OVER) return;

            foreach(var control in gameModel.Controls)
            {
                if (control.Background != SpriteName.None)
                {
                    sbatch.Draw(tHelper.DefaultSpriteMap, control.Region, tHelper.GetTextureRegion(control.Background, control.SpriteAnimationStep), control.BackgroundColor, 0, Vector2.Zero, SpriteEffects.None, 0.3f);
                }
                if (!string.IsNullOrEmpty(control.Text))
                {
                    sbatch.DrawString(control.Font, control.Text, control.TextPosition, control.TextColor, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.5f);
                }
            }
            
            foreach(var go in gameModel.GameMatrix)
            {
                if (go.Visible) { 
                    sbatch.Draw(tHelper.DefaultSpriteMap, go.Region, tHelper.GetTextureRegion(go.SpriteName, go.SpriteAnimationStep), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
                    switch (go.Bonus)
                    {
                        case Models.GameModels.BonusEffect.BANG:
                            sbatch.Draw(tHelper.DefaultSpriteMap, go.Region, tHelper.GetTextureRegion(SpriteName.Bang, 0), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.3f);
                            break;
                        case Models.GameModels.BonusEffect.LINE_H:
                            sbatch.Draw(tHelper.DefaultSpriteMap, go.Region, tHelper.GetTextureRegion(SpriteName.LineH, 0), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.3f);
                            break;
                        case Models.GameModels.BonusEffect.LINE_V:
                            sbatch.Draw(tHelper.DefaultSpriteMap, go.Region, tHelper.GetTextureRegion(SpriteName.LineV, 0), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.3f);
                            break;
                    }
                }
                foreach(var be in go.BonusEffects)
                {
                    if (be.State == Models.Interfaces.DynamicState.END) continue;
                    switch (be.BonusType)
                    {
                        case Models.GameModels.BonusEffect.BANG:
                            sbatch.Draw(tHelper.DefaultSpriteMap, go.Region, tHelper.GetTextureRegion(SpriteName.BangEffect, be.AnimationStep), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.5f);
                            break;
                        case Models.GameModels.BonusEffect.LINE_H:
                            sbatch.Draw(tHelper.DefaultSpriteMap, go.Region, 
                                        tHelper.GetTextureRegion(SpriteName.LineHEffect, be.AnimationStep), 
                                        Color.White, 0, Vector2.Zero, 
                                        ((LineBonusEffect)be).Direction == LineBonusEffectDirection.LR ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 
                                        0.5f);
                            break;
                        case Models.GameModels.BonusEffect.LINE_V:
                            sbatch.Draw(tHelper.DefaultSpriteMap, go.Region,
                                        tHelper.GetTextureRegion(SpriteName.LineVEffect, be.AnimationStep),
                                        Color.White, 0, Vector2.Zero,
                                        ((LineBonusEffect)be).Direction == LineBonusEffectDirection.BT ? SpriteEffects.None : SpriteEffects.FlipVertically,
                                        0.5f);
                            break;
                    }
                }
            }
        }
    }
}
