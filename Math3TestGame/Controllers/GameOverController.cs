using Math3TestGame.Models;
using Math3TestGame.Renders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math3TestGame.Controllers
{
    public class GameOverController : Controller
    {
        private GameOverButton gameOverButton;
        public GameOverController() : base(ControllerNames.GameOver)
        {
            var gameOverModel = new GameOverModel();
            gameOverButton = new GameOverButton();

            gameOverModel.GameOverDialog = new GameOverDialogModel();
            gameOverModel.GameOverButton = gameOverButton;

            renderer = new GameOverRenderer(gameOverModel);
        }

        public override void MouseClick(int x, int y)
        {
            if (gameOverButton.Region.Contains(x, y)) ChangeController(ControllerNames.Start);
        }

        public override void MouseMove(int x, int y)
        {
            if (gameOverButton.Region.Contains(x, y))
            {
                gameOverButton.ButtonState = ButtonState.HOVER;
            }
            else
            {
                gameOverButton.ButtonState = ButtonState.NONE;
            }
        }

        public override void Update(int dt)
        {
            gameOverButton.Update(dt);
        }
    }
}
