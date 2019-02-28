using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math3TestGame.Models.GameModels;
using Math3TestGame.Models.Interfaces;
using Math3TestGame.UI;
using Math3TestGame.UI.Dialogs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Math3TestGame.Models
{
    public class GameModel 
    {
        public Rectangle Rect { get; set; }

        public GameState State { get; set; }
        
        public GameMatrix GameMatrix { get; private set; }

        public bool CanClientInput { get; private set; } = false;

        private SwapModel clientSwapModel;

        public List<AUIControl> Controls { get; private set; } = new List<AUIControl>();
        
        public GameModel(GameMatrix gameMatrix)
        {
            GameMatrix = gameMatrix;
        }
        
        public void ClientSwapV(AGameObject go1, AGameObject go2)
        {
            GameMatrix.SwapV(go1, go2);
            clientSwapModel = new SwapModel
            {
                GameObject1 = go1,
                GameObject2 = go2,
                Direction = SwapDirection.VERTICAL
            };
        }

        public void ClientSwapH(AGameObject go1, AGameObject go2)
        {
            GameMatrix.SwapH(go1, go2);
            clientSwapModel = new SwapModel
            {
                GameObject1 = go1,
                GameObject2 = go2,
                Direction = SwapDirection.HORIZONTAL
            };
        }

        private int timeOver = 0;

        public int SecondsOver
        {
            get
            {
                return (timeOver - timeOver % 1000) / 1000;
            }
        }

        public void Update(int dt)
        {
            if(State == GameState.PAUSE)
            {
                return;
            }

            timeOver += dt;
            

            foreach(var control in Controls)
            {
                control.Update(dt);
            }
            

            if (State == GameState.GAME_OVER) return;


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

                    if (clientSwapModel != null && GameMatrix.State == MatrixState.NONE)
                    {
                        switch (clientSwapModel.Direction)
                        {
                            case SwapDirection.HORIZONTAL:
                                GameMatrix.SwapH(clientSwapModel.GameObject1, clientSwapModel.GameObject2);
                                break;
                            case SwapDirection.VERTICAL:
                                GameMatrix.SwapV(clientSwapModel.GameObject1, clientSwapModel.GameObject2);
                                break;
                        }
                    }

                    if (clientSwapModel != null)
                    {
                        clientSwapModel = null;
                    }
                }
            }

            CanClientInput = !isBusy && GameMatrix.State == MatrixState.NONE;
        }
    }

    public enum GameState
    {
        PLAY = 0,
        PAUSE = 1,
        WIN = 2,
        GAME_OVER = 3
    }
}
