using Math3TestGame.Tools;
using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math3TestGame.Models.GameModels
{
    public delegate void KilledItem();

    public class GameMatrix:IEnumerable<AGameObject>
    {
        private AGameObject first;
        private int w = 10;
        private int h = 10;

        private Random rnd;

        public MatrixState State { get; private set; } = MatrixState.NONE;

        private GameObjectFactory gFactory;
        
        public IEnumerator<AGameObject> GetEnumerator()
        {
            var current = first;
            while(current != null)
            {
                var left = current;
                while(left != null)
                {
                    yield return left;
                    left = left.Right;
                }
                current = current.Bottom;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public event KilledItem OnItemKilled;

        public GameMatrix()
        {
            rnd = new Random();

            AGameObject left = null;
            AGameObject current = null;

            gFactory = GameObjectFactory.GetInstance();

            for (int i = 0; i < w; i++)
            {
                current = gFactory.GetGameObject(Rectangle.Empty, this, null, null, current);

                current.Value = new Point(i, 0);

                left = current;
                
                if (i == 0) first = current;

                for(int j = 1; j < h; j++)
                {
                    var top = (left.Top != null && left.Top.Right != null) ? left.Top.Right : null;

                    var ni = gFactory.GetGameObject(Rectangle.Empty, this, left, null, top);

                    ni.Value = new Point(i, j);

                    left = ni;
                }
            }
        }
        
        public GameMatrix(Rectangle[,] regions, int w, int h)
        {
            rnd = new Random();

            this.w = w;
            this.h = h;

            AGameObject left = null;
            AGameObject current = null;

            gFactory = GameObjectFactory.GetInstance();

            for (int i = 0; i < w; i++)
            {
                current = gFactory.GetGameObject(regions[i, 0], this, null, null, current);
                
                left = current;

                if (i == 0) first = current;

                for(int j = 1; j < h; j++)
                {
                    var top = (left.Top != null && left.Top.Right != null) ? left.Top.Right : null;

                    var ni = gFactory.GetGameObject(regions[i, j], this, left, null, top);

                    ni.Value = new Point(i, j);

                    left = ni;
                }
            }
        }

       


        private void FindKilled()
        {
            State = MatrixState.NONE;

            var current = first;

            while (current != null)
            {
                var left = current;
                var marker = current;

                while(marker != null)
                {
                    int count = 0;

                    while(left != null && left.SpriteName == marker.SpriteName)
                    {
                        count++;
                        left = left.Right;
                    }

                    while(marker != left)
                    {
                        marker.hKilled = count;

                        if (count >= 3)
                        {
                            State = MatrixState.KILL;

                            marker.Kill();
                        }

                        marker = marker.Right;
                    }
                }
                current = current.Bottom;
            }

            current = first;

            while(current != null)
            {
                var top = current;
                var marker = current;

                while(marker != null)
                {
                    int count = 0;

                    while(top != null && top.SpriteName == marker.SpriteName)
                    {
                        count++;
                        top = top.Bottom;
                    }

                    while(marker != top)
                    {
                        marker.vKilled = count;

                        if (count >= 3)
                        {
                            State = MatrixState.KILL;
                            marker.Kill();
                        }

                        marker = marker.Bottom;
                    }
                }

                current = current.Right;
            }
        }

        private void DropDownItems()
        {
            State = MatrixState.NONE;

            var current = first;
            
            while (current != null)
            {
                var bottom = current;
                int rowKilled = 0;
                while (bottom != null)
                {
                    var marker = bottom;
                    while(marker != null && !marker.Visible && marker.Top != null && marker.Top.Visible)
                    {
                        SwapV(marker, marker.Top);
                        rowKilled++;
                        State = MatrixState.DROP_DOWN;
                    }
                        
                    bottom = bottom.Bottom;
                }

                while (current.Top != null) current = current.Top;

                current = current.Right;
            }
        }

        private void CreateNewItems()
        {
            State = MatrixState.NONE;

            var current = first;

            while(current != null)
            {
                var top = current;
               
                while(top != null && !top.Visible)
                {
                    top.NewLife();
                    State = MatrixState.CREATE;
                    top = top.Bottom;
                }
                
                current = current.Right;
            }
        }
        
        public void ReplaceItem(AGameObject replaced, BonusEffect bonus = BonusEffect.NONE)
        {
            AGameObject ni = null;

            switch (bonus)
            {
                case BonusEffect.LINE_V:
                    ni = gFactory.GetGameObject(replaced, LineType.V);
                    break;
                case BonusEffect.LINE_H:
                    ni = gFactory.GetGameObject(replaced, LineType.H);
                    break;
                default:
                    ni = gFactory.GetGameObject(replaced, bonus);
                    break;
            }

            if (replaced.Left == null && replaced.Top == null) first = ni;

            if (OnItemKilled != null) OnItemKilled();
        }
        
        public void TestSwap()
        {
            SwapV(first.Bottom.Bottom, first.Bottom.Bottom.Bottom);
        }
        
        public void TestHSwap()
        {
            SwapH(first.Bottom.Right.Right, first.Bottom.Right.Right.Right);
        }

        public void SwapH(AGameObject go1, AGameObject go2)
        {
            if (go1 == null || go2 == null) return;

            var left = go1.Right == go2 ? go1 : go2;

            if (left == null) return;

            var right = go1.Left == go2 ? go1 : go2;

            if (right == null) return;

            var ll = left.Left;
            var lt = left.Top;
            var lb = left.Bottom;

            var rr = right.Right;
            var rt = right.Top;
            var rb = right.Bottom;

            if (ll != null) ll.Right = right;
            if (lt != null) lt.Bottom = right;
            if (lb != null) lb.Top = right;

            if (rr != null) rr.Left = left;
            if (rt != null) rt.Bottom = left;
            if (rb != null) rb.Top = left;

            left.Top = rt;
            left.Bottom = rb;
            left.Right = rr;

            right.Top = lt;
            right.Bottom = lb;
            right.Left = ll;

            right.Right = left;
            left.Left = right;

            left.MoveH();
            right.MoveH();

            if (first == left) first = right;
        }

        public void SwapV(AGameObject go1, AGameObject go2)
        {
            if (go1 == null || go2 == null) return;

            var top = go1.Bottom == go2 ? go1 : go2;

            if (top == null) return;

            var bottom = go1.Top == go2 ? go1 : go2;

            if (bottom == null) return;

            var tt = top.Top;
            var tl = top.Left;
            var tr =top.Right;

            var bb = bottom.Bottom;
            var bl = bottom.Left;
            var br = bottom.Right;

            if (tt != null) tt.Bottom = bottom;
            if (tl != null) tl.Right = bottom;
            if (tr != null) tr.Left = bottom;

            if (bb != null) bb.Top = top;
            if (bl != null) bl.Right = top;
            if (br != null) br.Left = top;


            top.Top = bottom;
            top.Bottom = bb;
            top.Left = bl;
            top.Right = br;

            bottom.Bottom = top;
            bottom.Top = tt;
            bottom.Left = tl;
            bottom.Right = tr;

            top.MoveV();
            bottom.MoveV();

            if (top == first) first = bottom;
            else if (bottom == first) first = top;
        }

        public void Next(MatrixState step)
        {
            this.State = step;
            Next();
        }

        public void Next()
        {
            switch (State)
            {
                case MatrixState.NONE:
                    FindKilled();
                    //if (State == MatrixState.NONE) DropDownItems();
                    if (State == MatrixState.NONE) CreateNewItems();
                    break;
                case MatrixState.KILL:
                    DropDownItems();
                    if (State == MatrixState.NONE) CreateNewItems();
                    //if (State == MatrixState.NONE) FindKilled();
                    break;
                case MatrixState.DROP_DOWN:
                    CreateNewItems();
                    //if (State == MatrixState.NONE) DropDownItems();
                    break;
                case MatrixState.CREATE:
                    FindKilled();
                    break;
            }
        }



        #region DEBUG

        public string GetItemsPositions()
        {
            string result = "";

            AGameObject current = first;

            while (current != null)
            {
                if (current == null) break;

                result += "(" + current.NewPosition.X +" " +current.NewPosition.Y + ") ";

                var left = current;

                AGameObject right;

                while ((right = left.Right) != null)
                {
                    result += "(" + right.NewPosition.X + " " + right.NewPosition.Y + ") ";

                    left = right;
                }

                current = current.Bottom;

                result += "\r\n";
            }

            return result;
        }

        public string GetKilledItems()
        {
            string result = "";

            AGameObject current = first;

            while (current != null)
            {
                if (current == null) break;

                result += (int)current.AnimationState + " ";

                var left = current;

                AGameObject right;

                while ((right = left.Right) != null)
                {
                    result += (int)right.AnimationState + " ";

                    left = right;
                }

                current = current.Bottom;

                result += "\r\n";
            }

            return result;
        }

        public string GetVMatrix()
        {
            string result = "";

            AGameObject current = first;

            while (current != null)
            {
                if (current == null) break;

                result += current.vKilled + " ";

                var left = current;

                AGameObject right;

                while ((right = left.Right) != null)
                {
                    result += right.vKilled + " ";

                    left = right;
                }

                current = current.Bottom;

                result += "\r\n";
            }

            return result;
        }

        public string GetHMatrix()
        {
            string result = "";

            AGameObject current = first;

            while (current != null)
            {
                if (current == null) break;

                result += current.hKilled + " ";

                var left = current;

                AGameObject right;

                while ((right = left.Right) != null)
                {
                    result += right.hKilled + " ";

                    left = right;
                }

                current = current.Bottom;

                result += "\r\n";
            }

            return result;
        }

        public override string ToString()
        {
            string result = "";

            AGameObject current = first;

            while(current != null)
            {
                if (current == null) break;

                result += (current.Visible ? current.ToString() : "0") + " ";

                var left = current;

                AGameObject right;

                while((right = left.Right) != null)
                {
                    result += (right.Visible ? right.ToString() : "0") + " ";

                    left = right;
                }

                current = current.Bottom;

                result += "\r\n";
            }

            return result;
        }

        #endregion
    }

    public enum MatrixState
    {
        NONE      = 0,
        KILL      = 1,
        DROP_DOWN = 2,
        CREATE    = 3
    }
}
