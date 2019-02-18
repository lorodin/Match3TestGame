using Math3TestGame.Models;
using Math3TestGame.Renders;
using Math3TestGame.Tools;
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
        private GameObjectsMatrix gMatrix;
        private Animator animator;

        private IAnimation killItemsAnimation;
        private IAnimation swapAnimation;
        private IAnimation createNewItems;
        private IAnimation dropDownAnimation;
        private IAnimation bonusedAnimation;

        private List<LineBonusEffect> lines = new List<LineBonusEffect>();

        private bool game_over = false;

        public PlayController() : base(ControllerNames.Play)
        {
            gameModel = new GameModel();
            gameModel.Lines = lines;
            var timer = new GameTimerModel();
            var bonusPoints = new BonusPointsModel();
            gMatrix = new GameObjectsMatrix();
            gMatrix.OnKilled += GMatrix_OnKilled;
            gameModel.Timer = timer;
            gameModel.BonusPoints = bonusPoints;
            gameModel.GameMatrix = gMatrix;

            timer.OnTimerEnded += Timer_OnTimerEnded;

            renderer = new PlayRenderer(gameModel);

            animator = new Animator();

            killItemsAnimation = new AnimationKillItems();

            createNewItems = new AnimationShowNewItems();

            bonusedAnimation = new AnimationShowNewItems();

            dropDownAnimation = new AnimationMoveItems();

            gameModel.GameOverDialog = new GameOverDialogModel();

            gameModel.GameOverDialog.OnClickButton += () =>
            {
                ChangeController(ControllerNames.Start);
            };

            var endAnimation = new EndAnimation();

            var swapBackAnimation = new AnimationSwapObjects();

            swapAnimation = new AnimationSwapObjects();

            swapBackAnimation.OnNext(() =>
            {
                gMatrix.SwapItems();
                gMatrix.SwapModel = null;
                return new EndAnimation();
            });


            swapAnimation.OnNext(() =>
            {
                gMatrix.SwapItems();

                if (!gMatrix.GoodSwap())
                {
                    return swapBackAnimation.Start(new List<GameObjectModel>
                    {
                        gMatrix.GetObject(gMatrix.SwapModel.I1, gMatrix.SwapModel.J1),
                        gMatrix.GetObject(gMatrix.SwapModel.I2, gMatrix.SwapModel.J2)
                    });
                }

                gMatrix.HideItems();

                gMatrix.SwapModel = null;

                return killItemsAnimation.Start(gMatrix.HiddenItems);
            });

            killItemsAnimation.OnNext(() =>
            {
                if(lines.Count != 0)
                {
                    gMatrix.HideItems();
                    return killItemsAnimation.Start(gMatrix.HiddenItems);
                }
                if(gMatrix.Bonused.Count != 0)
                {
                    gMatrix.ConvertBonusedToItems();
                    return bonusedAnimation.Start(gMatrix.Bonused);
                }

                gMatrix.MoveObjects();

                if(gMatrix.MovingObjects.Count != 0)
                {
                    return dropDownAnimation.Start(gMatrix.MovingObjects);
                }

                gMatrix.CreateEmpty();

                if(gMatrix.NewItems.Count != 0)
                {
                    return createNewItems.Start(gMatrix.NewItems);
                }

                return endAnimation;
            });

            bonusedAnimation.OnNext(() =>
            {
                gMatrix.MoveObjects();

                if(gMatrix.MovingObjects.Count != 0)
                {
                    return dropDownAnimation.Start(gMatrix.MovingObjects);
                }

                gMatrix.CreateEmpty();

                if(gMatrix.NewItems.Count != 0)
                {
                    return createNewItems.Start(gMatrix.NewItems);
                }

                return endAnimation;
            });

            dropDownAnimation.OnNext(() =>
            {
                gMatrix.HideItems();

                if(gMatrix.HiddenItems.Count != 0)
                {
                    return killItemsAnimation.Start(gMatrix.HiddenItems);
                }
                
                gMatrix.CreateEmpty();

                if(gMatrix.NewItems.Count != 0)
                {
                    return createNewItems.Start(gMatrix.NewItems);
                }

                return endAnimation;
            });

            createNewItems.OnNext(() =>
            {
                gMatrix.HideItems();

                if (gMatrix.HiddenItems.Count != 0)
                {
                    return killItemsAnimation.Start(gMatrix.HiddenItems);
                }

                gMatrix.CreateEmpty();

                if(gMatrix.NewItems.Count != 0)
                {
                    return createNewItems.Start(gMatrix.NewItems);
                }

                return endAnimation;
            });

            timer.Start();
        }

        private void GMatrix_OnKilled(GameObjectModel go)
        {
            ((BonusPointsModel)gameModel.BonusPoints).Points += 10;
            switch (go.Bonus)
            {
                case GameObjectBonus.BOMB:
                    Debug.WriteLine("Bomb killed");
                    break;
                case GameObjectBonus.LINE:
                    switch(((LineGameObjectModel)go).LineType)
                    {
                        case LineBonusType.H:
                            var ll = new LineBonusEffect(Direction.LEFT, go.Rect.X, go.Rect.Y);
                            var rl = new LineBonusEffect(Direction.RIGHT, go.Rect.X, go.Rect.Y);

                            ll.OnLineProgress += OnLineProgress;
                            rl.OnLineProgress += OnLineProgress;

                            lines.Add(ll);
                            lines.Add(rl);


                            break;
                        case LineBonusType.V:
                            var ul = new LineBonusEffect(Direction.UP, go.Rect.X, go.Rect.Y);
                            var bl = new LineBonusEffect(Direction.BOTTOM, go.Rect.X, go.Rect.Y);

                            ul.OnLineProgress += OnLineProgress;
                            bl.OnLineProgress += OnLineProgress;

                            lines.Add(ul);
                            lines.Add(bl);

                            break;
                    }
                    
                    break;
            }
        }

        private void OnLineProgress(int i, int j)
        {
            gMatrix.KillItem(i, j);
        }

        private void Timer_OnTimerEnded()
        {
            gameModel.GameOver = true;
        }

        public override void MouseClick(int x, int y)
        {
            if (gameModel.GameOver)
            {
                gameModel.GameOverDialog.Click(x, y);
                return;
            }
            if (animator.State != AnimatorState.Free || lines.Count != 0) return;
            

            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    var g = gMatrix.GetObject(i, j);

                    if (g.Rect.Contains(x, y))
                    {
                        if(gMatrix.SelectedPoint != null && gMatrix.SelectedItem != null)
                        {
                            var di = Math.Abs(i - gMatrix.SelectedPoint.I);
                            var dj = Math.Abs(j - gMatrix.SelectedPoint.J);
                            
                            if (di == 1 ^ dj == 1 && di <= 1 && dj <= 1)
                            {

                                gMatrix.SwapModel = new SwapModel
                                {
                                    I1 = gMatrix.SelectedPoint.I,
                                    J1 = gMatrix.SelectedPoint.J,
                                    I2 = i,
                                    J2 = j
                                };

                                gMatrix.UnselectItem();

                                animator.RunAnimation(swapAnimation, new List<GameObjectModel>
                                {
                                    gMatrix.GetObject(gMatrix.SwapModel.I1, gMatrix.SwapModel.J1), gMatrix.GetObject(gMatrix.SwapModel.I2, gMatrix.SwapModel.J2)
                                });
                                
                                return;
                            }
                        }

                        gMatrix.SelectItem(i, j);

                        return;
                    }
                }
            }
        }

        public override void MouseMove(int x, int y)
        {

        }
        bool finded = false;
        public override void Update(int dt)
        {
            gameModel.Update(dt);

            if (gameModel.GameOver) return;

            if (!gMatrix.ReadyToPlay) return;

            int endedLines = 0;

            foreach(var l in lines)
            {
                l.Update(dt);
                endedLines += l.State == BonusState.HIDE ? 1 : 0;
            }

            if(endedLines == lines.Count)
            {
                lines.Clear();
            }

            animator.Update(dt);

            if (animator.State == AnimatorState.Free)
            {
                gMatrix.HideItems();

                if(gMatrix.HiddenItems.Count != 0)
                {
                    animator.RunAnimation(killItemsAnimation, gMatrix.HiddenItems);
                    return;
                }

                gMatrix.MoveObjects();

                if (gMatrix.MovingObjects.Count != 0)
                {
                    animator.RunAnimation(dropDownAnimation, gMatrix.MovingObjects);
                    return;
                }

                if (gMatrix.Bonused.Count != 0)
                {
                    animator.RunAnimation(bonusedAnimation, gMatrix.Bonused);
                }

                gMatrix.CreateEmpty();

                if (gMatrix.NewItems.Count != 0)
                {
                    animator.RunAnimation(createNewItems, gMatrix.NewItems);
                    return;
                }
            }
        }
    }
}
