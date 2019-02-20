using Math3TestGame.Models;
using Math3TestGame.Models.GameModels;
using Math3TestGame.Renders;
using Math3TestGame.Tools;
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

        private GameTimerModel gameTimerModel;
        private BonusPointsModel bonusPointsModel;

        private GameMatrix gameMatrix;
        private GameObjectFactory goFactory;

        private GameConfigs gc;

        private GameObject selected;

        public PlayController() : base(ControllerNames.Play)
        {
            Rectangle[,] regions = new Rectangle[8, 8];

            gc = GameConfigs.GetInstance();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    regions[i, j] = new Rectangle(gc.GetRealPoint(j + 1, i + 1), new Point(gc.RegionWidth, gc.RegionHeight));
                }
            }

            gameMatrix = new GameMatrix(regions, 8, 8);

            gameModel = new GameModel(gameMatrix);


            gameTimerModel = new GameTimerModel();
            bonusPointsModel = new BonusPointsModel();
            gameModel.Timer = gameTimerModel;
            gameModel.BonusPoints = bonusPointsModel;

            renderer = new PlayRenderer(gameModel);

            gameTimerModel.Start();
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
            gameModel.Update(dt);
        }
    }
}
