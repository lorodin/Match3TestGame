using Math3TestGame.Models;
using Math3TestGame.Models.GameModels;
using Math3TestGame.Renders;
using Math3TestGame.Tools;
using Math3TestGame.UI;
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

        private int totalPoints = 0;
        private int game_time;

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
            };

            var pPoint = gc.GetRealPoint(1, 0.5f);
            var tPoint = gc.GetRealPoint(7.5f, 0.5f);

            lbPoints = new Label(gc.DefaultFont12, "Total points: 0", pPoint.X, pPoint.Y);
            lbTimer = new Label(gc.DefaultFont12, "Time: " + game_time, tPoint.X, tPoint.Y);

            gameModel.Controls.Add(lbPoints);
            gameModel.Controls.Add(lbTimer);
            
            renderer = new PlayRenderer(gameModel);
        }

        private void OnLineProgress(int i, int j)
        {
            
        }
        

        public override void MouseClick(int x, int y)
        {
            if (!gameModel.CanClientInput) return;

            foreach(var go in gameMatrix)
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

        }

        public override void Update(int dt)
        {
            if (gameModel.State == GameState.GAME_OVER)
            {
                ChangeController(ControllerNames.Start);
                return;
            }
            
            gameModel.Update(dt);

            var left_time = game_time - gameModel.SecondsOver;

            if (left_time <= 0) left_time = 0;

            lbTimer.Text = "Time: " + left_time;

            if (left_time == 0) gameModel.State = GameState.GAME_OVER;
        }
    }
}
