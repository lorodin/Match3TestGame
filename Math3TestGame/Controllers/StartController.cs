using Math3TestGame.Models;
using Math3TestGame.Renders;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math3TestGame.Controllers
{
    public class StartController:Controller, IRenderer
    {
        private PlayButtonModel playButton;
        public StartController() : base(ControllerNames.Start)
        {
            renderer = this;
            playButton = new PlayButtonModel();
        }

        public void Draw(SpriteBatch sbatch)
        {
            playButton.Draw(sbatch);
        }

        public override void MouseClick(int x, int y)
        {
            if (playButton.Rect.Contains(x, y)) ChangeController(ControllerNames.Play);
        }

        public override void MouseMove(int x, int y)
        {
            if(playButton.Rect.Contains(x, y))
            {
                playButton.State = PlayButtonState.HOVER;
            }
            else
            {
                playButton.State = PlayButtonState.NONE;
            }
        }

        public override void Update(int dt)
        {
            playButton.Update(dt);
        }
    }
}
