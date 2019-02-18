using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math3TestGame.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Math3TestGame.Models
{
    public class GameObjectsMatrix : IDrawableModel
    {
        public event Killed OnKilled;

        public Rectangle Rect { get; set; }

        private GameObjectModel[,] gObjects;

        private Random rnd = new Random();

        public GameObjectModel SelectedItem { get; private set; }

        public SwapModel SwapModel { get; set; }

        private int[,] vM = new int[8, 8];
        private int[,] hM = new int[8, 8];

        public bool ReadyToPlay { get; private set; } = false;

        private GameConfigs gc;

        public SelectedPoint SelectedPoint { get; private set; }

        public GameObjectModel lastSelection;

        public List<GameObjectModel> Bonused { get; set; } = new List<GameObjectModel>();

        public List<GameObjectModel> HiddenItems { get; set; } = new List<GameObjectModel>();

        public List<GameObjectModel> MovingObjects { get; set; } = new List<GameObjectModel>();

        public List<GameObjectModel> NewItems { get; set; } = new List<GameObjectModel>();

        public List<GameObjectModel> Bangs { get; set; } = new List<GameObjectModel>();

        public GameObjectsMatrix()
        {
            gc = GameConfigs.GetInstance();
            gObjects = new GameObjectModel[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    gObjects[i, j] = new GameObjectModel(i + 1, j + 1, rndSpriteName());
                    gObjects[i, j].OnKilled += onKilled;
                }
            }
        }

        public void SelectItem(int i, int j)
        {
            if (SelectedItem != null) SelectedItem.State = GameObjectState.NONE;
            gObjects[i, j].State = GameObjectState.SELECTED;
            lastSelection = SelectedItem;
            SelectedItem = gObjects[i, j];
            SelectedPoint = new SelectedPoint
            {
                I = i,
                J = j
            };
        }
        
        private void onKilled(GameObjectModel go)
        {
            if(go.Bonus == GameObjectBonus.BOMB)
            {
                var p = gc.GetIndexes(go.Rect.X, go.Rect.Y);
                

                for(var i = p.X - 2; i < 8 && i <= p.X ; i++)
                {
                    if (i < 0) continue;
                    for(var j = p.Y - 2; j < 8 && j <= p.Y ; j++)
                    {
                        if (j < 0) continue;
                        if (gObjects[i, j].State != GameObjectState.HIDE && gObjects[i, j].State != GameObjectState.KILLED) gObjects[i, j].Kill();
                    }
                }
            }
            if (OnKilled != null) OnKilled(go);
        }
        
        public void KillItem(int i, int j)
        {
            if (i > 7 || i < 0 || j > 7 || j < 0) return;
            if (gObjects[i, j].State == GameObjectState.HIDE || gObjects[i, j].State == GameObjectState.KILLED) return;
            gObjects[i, j].Kill();
        }

        public void HideItems()
        {
            HiddenItems.Clear();
            Bonused.Clear();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (gObjects[i, j].State != GameObjectState.NONE) continue;
                    int hk = j + 1;
                    while (hk < 8 && gObjects[i, j].SpriteName == gObjects[i, hk].SpriteName) hk++;
                    int l = hk - j;
                    for (; j < hk; j++) hM[i, j] = l;
                    if (j == hk) j--;
                }
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (gObjects[i, j].State != GameObjectState.NONE) continue;
                    int vk = j + 1;
                    while (vk < 8 && gObjects[j, i].SpriteName == gObjects[vk, i].SpriteName) vk++;
                    int l = vk - j;
                    for (; j < vk; j++) vM[j, i] = l;
                    if (j == vk) j--;
                }
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (gObjects[i, j].State == GameObjectState.KILLED) continue;

                    if (gObjects[i, j].State == GameObjectState.HIDE)
                    {
                        HiddenItems.Add(gObjects[i, j]);
                        continue;
                    }

                    // Ставим где должен находится бонус

                    if (vM[i, j] >= 3 && hM[i, j] >= 3)
                    {
                        Bonused.Add(new BangGameObjectModel(gObjects[i, j]));

                    }else if (SwapModel != null && (SwapModel.I1 == i && SwapModel.J1 == j || SwapModel.I2 == i && SwapModel.J2 == j))
                    {
                        if(vM[i, j] >= 5 || hM[i, j] >= 5)
                        {
                            Bonused.Add(new BangGameObjectModel(gObjects[i, j]));
                        }
                        else if(vM[i, j] >= 4 || hM[i,j] >= 4)
                        {
                            //Bonused.Add(new BangGameObjectModel(gObjects[i, j]));
                            Bonused.Add(new LineGameObjectModel(gObjects[i, j], vM[i, j] >= 4 ? LineBonusType.H : LineBonusType.V));
                        }
                    }

                    if (vM[i, j] >= 3)
                    {
                        gObjects[i, j].Kill();
                        HiddenItems.Add(gObjects[i, j]);
                    }
                    else if (hM[i, j] >= 3)
                    {
                        gObjects[i, j].Kill();
                        HiddenItems.Add(gObjects[i, j]);
                    }

                    /*if (gObjects[i, j].Bonus == GameObjectBonus.BOMB)
                    {
                        Debug.WriteLine("Bomb finded: " + i + " " + j + "; vM:" + vM[i, j] + "; hM:" + hM[i, j]);
                    }*/
                }
            }
        }
        
        public void SetItem(GameObjectModel g)
        {
            var p = gc.GetIndexes(g.Rect.X, g.Rect.Y);
            g.OnKilled += onKilled;
            gObjects[p.X - 1, p.Y - 1] = g;
        }
        
        private bool isGoodPoint(int i, int j)
        {
            int h = 1;
            int v = 1;

            int l = i - 1;
            int r = i + 1;

            int u = j - 1;
            int b = j + 1;

            while(l >= 0 && gObjects[l, j].SpriteName == gObjects[i, j].SpriteName)
            {
                h++;
                l--;
            }

            while (r < 8 && gObjects[r, j].SpriteName == gObjects[i, j].SpriteName)
            {
                h++;
                r++;
            }
            
            while(u >= 0 && gObjects[i, u].SpriteName == gObjects[i, j].SpriteName)
            {
                u--;
                v++;
            }

            while(b < 8 && gObjects[i, b].SpriteName == gObjects[i, j].SpriteName)
            {
                b++;
                v++;
            }

            return h >= 3 || v >= 3;
        }

        public bool GoodSwap()
        {
            return isGoodPoint(SwapModel.I1, SwapModel.J1) || isGoodPoint(SwapModel.I2, SwapModel.J2);
        }

        public void MoveObjects()
        {
            MovingObjects.Clear();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 7; j >= 0; j--)
                {
                    if (gObjects[i, j].State != GameObjectState.KILLED) continue;

                    int k = j - 1;

                    while (k >= 0 && gObjects[i, k].State == GameObjectState.KILLED) k--;

                    if (k == -1) continue;

                    //if (gObjects[i, k].State == GameObjectState.KILLED) continue;

                    var b = gObjects[i, k];

                    for (int l = k; l < j; l++) gObjects[i, l] = gObjects[i, l + 1];//  m[l, i] = m[l + 1, i];

                    gObjects[i, j] = b;

                    if(b.State == GameObjectState.KILLED)
                    {
                        int z = 10;
                    }

                    gObjects[i, j].NewPosition = gc.GetRealPoint(i + 1, j + 1);

                    MovingObjects.Add(gObjects[i, j]);
                }
            }
        }

        public void ConvertBonusedToItems()
        {
            foreach(var b in Bonused)
            {
                SetItem(b);
            }
        }

        public void CreateEmpty()
        {
            NewItems.Clear();
            
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (gObjects[i, j].State != GameObjectState.KILLED) break;

                    gObjects[i, j] = new GameObjectModel(i + 1, j + 1, rndSpriteName());
                    gObjects[i, j].OnKilled += onKilled;

                    NewItems.Add(gObjects[i, j]);
                }
            }
        }

        private void WriteMatrix()
        {
            Debug.WriteLine("Write matrix");
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    Debug.Write(gObjects[i, j].State + " ");
                }
                Debug.WriteLine("");
            }
        }

        public GameObjectModel GetObject(int i, int j)
        {
            return gObjects[i, j];
        }

        public void SwapItems()
        {
            var b = gObjects[SwapModel.I1, SwapModel.J1];
            gObjects[SwapModel.I1, SwapModel.J1] = gObjects[SwapModel.I2, SwapModel.J2];
            gObjects[SwapModel.I2, SwapModel.J2] = b;
        }


        private SpriteName rndSpriteName()
        {
            int s = rnd.Next(0, 5);
            switch (s)
            {
                case 0:
                    return SpriteName.GameObject1;
                case 1:
                    return SpriteName.GameObject2;
                 case 2:
                    return SpriteName.GameObject3;
                 case 3:
                    return SpriteName.GameObject4;
            }
            return SpriteName.GameObject5;
        }

        public void UnselectItem()
        {
            if (SelectedItem == null) return;
            SelectedItem.State = GameObjectState.NONE;
            lastSelection = SelectedItem;
            //SelectedPoint = null;
            SelectedItem = null;
        }

        public void Draw(SpriteBatch sb)
        {
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    gObjects[i, j].Draw(sb);
                }
            }
        }

        public void Update(int dt)
        {
            if (!ReadyToPlay)
            {
                bool r = true;
                for(int i = 0; i < 8; i++)
                {
                    for(int j = 0; j < 8; j++)
                    {
                        r &= gObjects[i, j].State != GameObjectState.SHOW;
                        if (!r) break;
                    }
                    if (!r) break;
                }
                ReadyToPlay = r;
            }

            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    gObjects[i, j].Update(dt);
                }
            }
        }
    }
}
