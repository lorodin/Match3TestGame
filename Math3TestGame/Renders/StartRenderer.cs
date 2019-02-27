using System;
using System.Collections.Generic;
using Math3TestGame.Tools;
using Math3TestGame.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Math3TestGame.Renders
{
    public class StartRenderer : IRenderer
    {
        private TextureHelper tHelper;

        private List<AUIControl> controls;

        private GameConfigs gc;

        public StartRenderer(List<AUIControl> controls)
        {
            this.controls = controls;
            tHelper = TextureHelper.GetInstance();
            gc = GameConfigs.GetInstance();
        }

        public void Draw(SpriteBatch sbatch)
        {
            foreach (var control in controls)
            {
                if (control.Background != SpriteName.None)
                { 
                    sbatch.Draw(tHelper.DefaultSpriteMap, 
                                control.Region, 
                                tHelper.GetTextureRegion(control.Background, control.SpriteAnimationStep), 
                                control.BackgroundColor, 0, Vector2.Zero, SpriteEffects.None, 0.3f);
                }
                if (!string.IsNullOrEmpty(control.Text))
                    sbatch.DrawString(control.Font, 
                                      control.Text, 
                                      control.TextPosition, 
                                      control.TextColor, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.5f);
            }
        }
    }
}
