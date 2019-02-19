using Math3TestGame.Tools;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math3TestGame.Models.GameModels
{
    public class GameMatrix
    {
        private GameObject first;
        private int w = 8;
        private int h = 8;

        private Random rnd;

        public MatrixState State { get; private set; } = MatrixState.NONE;

        public GameMatrix()
        {
            rnd = new Random();

            GameObject left = null;
            GameObject current = null;

            for (int i = 0; i < w; i++)
            {
                current = new GameObject(Rectangle.Empty, rndSpriteName(), null, null, current);

                current.Value = new Point(i, 0);

                left = current;
                
                if (i == 0) first = current;

                for(int j = 1; j < h; j++)
                {
                    var top = (left.Top != null && left.Top.Right != null) ? left.Top.Right : null;

                    var ni = new GameObject(Rectangle.Empty, rndSpriteName(), left, null, top);

                    ni.Value = new Point(i, j);

                    left = ni;
                }
            }
        }

        private SpriteName rndSpriteName()
        {
            switch(rnd.Next(1, 6))
            {
                case 1: return SpriteName.GameObject1;
                case 2: return SpriteName.GameObject2;
                case 3: return SpriteName.GameObject3;
                case 4: return SpriteName.GameObject4;
            }

            return SpriteName.GameObject5;
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
                    if(top == current)
                    {
                        top = CreateNewItem(top);
                        current = top;
                    }
                    else
                    {
                        top = CreateNewItem(top);
                    }
                    State = MatrixState.CREATE;
                    top = top.Bottom;
                }

                //while (current.Top != null) current = current.Top;
                current = current.Right;
            }
        }

        private GameObject CreateNewItem(GameObject item)
        {
            item = new GameObject(item, rndSpriteName());
            if (item.Left == null && item.Top == null) first = item;
            return item;
        }

        public void TestSwap()
        {
            SwapV(first.Bottom.Bottom, first.Bottom.Bottom.Bottom);
        }

        public void SwapV(GameObject go1, GameObject go2)
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

            top.Move(bottom.Region.X, bottom.Region.Y);
            bottom.Move(top.Region.X, top.Region.Y);

            if (top == first) first = bottom;
        }

        public void Next()
        {
            switch (State)
            {
                case MatrixState.NONE:
                    FindKilled();
                    break;
                case MatrixState.KILL:
                    DropDownItems();
                    break;
                case MatrixState.DROP_DOWN:
                    CreateNewItems();
                    break;
                case MatrixState.CREATE:
                    FindKilled();
                    break;
            }
        }





        public string GetKilledItems()
        {
            string result = "";

            GameObject current = first;

            while (current != null)
            {
                if (current == null) break;

                result += (int)current.AnimationState + " ";

                var left = current;

                GameObject right;

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

            GameObject current = first;

            while (current != null)
            {
                if (current == null) break;

                result += current.vKilled + " ";

                var left = current;

                GameObject right;

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

            GameObject current = first;

            while (current != null)
            {
                if (current == null) break;

                result += current.hKilled + " ";

                var left = current;

                GameObject right;

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

            GameObject current = first;

            while(current != null)
            {
                if (current == null) break;

                result += (current.Visible ? current.ToString() : "0") + " ";

                var left = current;

                GameObject right;

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
    }

    public enum MatrixState
    {
        NONE      = 0,
        KILL      = 1,
        DROP_DOWN = 2,
        CREATE    = 3
    }
}
