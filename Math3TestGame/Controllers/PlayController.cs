using Math3TestGame.Models;
using Math3TestGame.Models.GameModels;
using Math3TestGame.Renders;
using Math3TestGame.Tools;
using Math3TestGame.UI;
using Math3TestGame.UI.Dialogs;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math3TestGame.Controllers
{
    public class PlayController : Controller
    {
        private GameModel gameModel;
        
        private GameMatrix gameMatrix;

        private GameConfigs gc;

        private AGameObject selected;

        private Label lbTimer;
        private Label lbPoints;

        private SimpleButton pauseButton;

        private int totalPoints = 0;
        private int game_time;

        private PauseDialog pauseDialog;
        private WinDialog winDialog;
        private GameOverDialog gameOverDialog;

        public List<ADialog> Dialogs { get; private set; } 

        private ADialog CurrentDialog
        {
            get
            {
                return Dialogs.Find(d => d.DialogState == DialogState.SHOW);
            }
        }
        
        public PlayController() : base(ControllerNames.Play)
        {
            gc = GameConfigs.GetInstance();

            game_time = gc.GameTime;

            Point size = new Point(8, 8);

            switch (gc.GameType)
            {
                case GameType.G6x6:
                    size = new Point(6, 6);
                    break;
                case GameType.G6x8:
                    size = new Point(6, 8);
                    break;
                case GameType.G8x9:
                    size = new Point(8, 9);
                    break;
            }


            Rectangle[,] regions = new Rectangle[size.Y, size.X];

            int offsetX = (10 - size.X) / 2;
            int offsetY = size.Y == 6 ? 2 : 1;
        
            for (int i = 0; i < size.X; i++)
            {
                for (int j = 0; j < size.Y; j++)
                {
                    regions[j, i] = new Rectangle(gc.GetRealPoint(i + offsetX, j + offsetY), new Point(gc.RegionWidth, gc.RegionHeight));
                }
            }

            gameMatrix = new GameMatrix(regions, size.Y, size.X);

            gameModel = new GameModel(gameMatrix);

            gameMatrix.OnItemKilled += () =>
            {
                lbPoints.Text = "Total points: " + (totalPoints++);

                if (totalPoints >= gc.MaxPoints)
                {
                    gameModel.State = GameState.WIN;
                    winDialog.Show();
                }
            };

            var pPoint = gc.GetRealPoint(1, 0.3f);
            var tPoint = gc.GetRealPoint(7.5f, 0.3f);

            lbPoints = new Label(gc.DefaultFont12, "Total points: 0", pPoint.X, pPoint.Y);
            lbTimer = new Label(gc.DefaultFont12, "Time: " + game_time, tPoint.X, tPoint.Y);
            pauseButton = new SimpleButton(gc.DefaultFont12, "| |", gc.Center.X - gc.RegionWidth / 2, 5, gc.RegionWidth - 10, gc.RegionHeight - 10, Color.White);

            gameModel.Controls.Add(lbPoints);
            gameModel.Controls.Add(lbTimer);
            gameModel.Controls.Add(pauseButton);

            Dialogs = new List<ADialog>();

            pauseDialog = new PauseDialog(gc.Center.X - gc.RegionWidth * 3, gc.Center.Y - 5 * gc.RegionHeight / 2, gc.RegionWidth * 6, gc.RegionHeight * 5);
            winDialog = new WinDialog(gc.Center.X - gc.RegionWidth * 3, gc.Center.Y - 2 * gc.RegionHeight, gc.RegionWidth * 6, gc.RegionHeight * 3);
            gameOverDialog = new GameOverDialog(gc.Center.X - gc.RegionWidth * 3, gc.Center.Y - 2 * gc.RegionHeight, gc.RegionWidth * 6, gc.RegionHeight * 3);

            Dialogs.Add(pauseDialog);
            Dialogs.Add(winDialog);
            Dialogs.Add(gameOverDialog);

            renderer = new PlayRenderer(Dialogs, gameModel);
        }


        public override void MouseClick(int x, int y)
        {
            var current = CurrentDialog;

            if(current != null)
            {
                var result = current.MouseClick(x, y);
                switch (result)
                {
                    case PlayDialogResult.EXIT:
                        gc.CurrentGame.Exit();
                        break;
                    case PlayDialogResult.MAIN_MENU:
                        ChangeController(ControllerNames.Start);
                        break;
                    case PlayDialogResult.RESTART:
                        ChangeController(ControllerNames.Play);
                        break;
                    case PlayDialogResult.RESUME:
                        gameModel.State = GameState.PLAY;
                        current.Hide();
                        break;
                }
                return;
            }
            
            if(pauseButton.Region.Contains(x, y))
            {
                gameModel.State = GameState.PAUSE;
                pauseDialog.Show();
                return;
            }

            if (!gameModel.CanClientInput) return;

            foreach (var go in gameMatrix)
            {
                if(go.Region.Contains(x, y))
                {
                    if (selected != null)
                    {
                        if(selected.Left == go || selected.Right == go)
                        {
                            gameModel.ClientSwapH(selected, go);
                            selected = null;
                            return;
                        }else if(selected.Top == go || selected.Bottom == go)
                        {
                            gameModel.ClientSwapV(selected, go);
                            selected = null;
                            return;
                        }

                        selected.Selected = false;
                        selected = null;
                    }
                    
                    selected = go;
                    selected.Selected = true;
                }    
            }
        }

        public override void MouseMove(int x, int y)
        {
            var current = CurrentDialog;

            if (current != null)
            {
                current.MouseMove(x, y);
                return;
            }

            if (pauseButton.Region.Contains(x, y)) pauseButton.ButtonState = ButtonState.HOVER;
            else pauseButton.ButtonState = ButtonState.NONE;
        }

        public override void Update(int dt)
        {
            foreach(var dialog in Dialogs)
            {
                if (dialog.DialogState == DialogState.HIDE) continue;
                dialog.Update(dt);
                return;
            }
            
            gameModel.Update(dt);
            
            var left_time = game_time - gameModel.SecondsOver;

            if (left_time <= 0) left_time = 0;

            lbTimer.Text = "Time: " + left_time;

            if (left_time == 0)
            {
                gameModel.State = GameState.GAME_OVER;
                gameOverDialog.Show();
            }
        }
    }
}
