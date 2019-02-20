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
    public class StartController:Controller
    {
        private PlayButtonModel playButton;
        public StartController() : base(ControllerNames.Start)
        {
            playButton = new PlayButtonModel();

            renderer = new StartRenderer(playButton);
        }
        
        public override void MouseClick(int x, int y)
        {
            if (playButton.Region.Contains(x, y)) ChangeController(ControllerNames.Play);
        }

        public override void MouseMove(int x, int y)
        {
            if(playButton.Region.Contains(x, y))
            {
                playButton.PlayButtonState = PlayButtonState.HOVER;
            }
            else
            {
                playButton.PlayButtonState = PlayButtonState.NONE;
            }
        }

        public override void Update(int dt)
        {
            playButton.Update(dt);
        }
    }
}
