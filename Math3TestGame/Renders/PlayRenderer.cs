using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math3TestGame.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Math3TestGame.Renders
{
    public class PlayRenderer : IRenderer
    {
        private GameModel gameModel;

        public PlayRenderer(GameModel gm)
        {
            gameModel = gm;    
        }

        public void Draw(SpriteBatch sbatch)
        {
            gameModel.Draw(sbatch);
        }
    }
}
