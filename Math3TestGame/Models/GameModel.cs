using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math3TestGame.Models.GameModels;
using Math3TestGame.Models.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Math3TestGame.Models
{
    public class GameModel 
    {
        public Rectangle Rect { get; set; }

        public BonusPointsModel BonusPoints { get; set; }

        public GameTimerModel Timer { get; set; }
        
        public bool GameOver { get; set; } = false;
        
        public GameMatrix GameMatrix { get; private set; }

        public bool CanClientInput { get; private set; } = false;

        public GameModel(GameMatrix gameMatrix)
        {
            GameMatrix = gameMatrix;
        }


        public void ClientSwapV(GameObject go1, GameObject go2)
        {
            GameMatrix.SwapV(go1, go2);
        }

        public void ClientSwapH(GameObject go1, GameObject go2)
        {
            GameMatrix.SwapH(go1, go2);
        }



        public void Update(int dt)
        {
            if (GameOver) return;

            BonusPoints.Update(dt);

            Timer.Update(dt);

            if(Timer.State == DynamicState.END)
            {
                GameOver = true;
                return;
            }

            bool isBusy = false;
            bool hasInvisible = false;

            foreach(var go in GameMatrix)
            {
                go.Update(dt);
                isBusy |= go.IsBusy();
                hasInvisible |= !go.Visible;
            }

            if (!isBusy)
            {
                if (hasInvisible)
                {
                    GameMatrix.Next(MatrixState.KILL);
                }
                else
                {
                    GameMatrix.Next();
                }
            }

            CanClientInput = !isBusy && GameMatrix.State == MatrixState.NONE;
        }
    }
}
