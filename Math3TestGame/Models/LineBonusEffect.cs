using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math3TestGame.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Math3TestGame.Models
{

    public delegate void LineProgress(int i, int j);

    public class LineBonusEffect : IDrawableModel
    {
        public event LineProgress OnLineProgress;
        public Rectangle Rect { get; set; }
        
        public Point StartPosition { get; private set; }

        private GameConfigs gc;
        private TextureHelper tHelper;

        public BonusState State { get; set; } = BonusState.SHOW;

        public Direction Direction { get; private set; }

        private int stepAnimation = 0;

        private int ddt = 0;

        private int directionAnimation = 1;
        

        public LineBonusEffect(Direction direction, int x, int y)
        {
            gc = GameConfigs.GetInstance();
            tHelper = TextureHelper.GetInstance();
            Direction = direction;

            Rect = new Rectangle(x, y, gc.RegionWidth, gc.RegionHeight);

            StartPosition = new Point(x, y);//, Rect.Width, Rect.Height);
        }

        public void Draw(SpriteBatch sb)
        {
            if (State == BonusState.HIDE) return;

            sb.Draw(gc.DefaultSpriteMap, Rect, tHelper.GetTextureRegion(SpriteName.FireBall, stepAnimation), Color.White, 0, Vector2.Zero, SpriteEffects.None, 1); //, 0, Vector2.Zero, SpriteEffects.None, 11
        }

        public void Update(int dt)
        {
            if (State == BonusState.HIDE) return;

            ddt += dt;

            switch (Direction)
            {
                case Direction.BOTTOM:
                    Rect = new Rectangle(new Point(Rect.X, (int)(Rect.Y + dt * gc.DefaultSpeed / 2)), new Point(gc.RegionWidth, gc.RegionHeight));
                    break;
                case Direction.UP:
                    Rect = new Rectangle(new Point(Rect.X, (int)(Rect.Y - dt * gc.DefaultSpeed / 2)), new Point(gc.RegionWidth, gc.RegionHeight));
                    break;
                case Direction.RIGHT:
                    Rect = new Rectangle(new Point((int)(Rect.X + dt * gc.DefaultSpeed / 2), Rect.Y), new Point(gc.RegionWidth, gc.RegionHeight));
                    break;
                case Direction.LEFT:
                    Rect = new Rectangle(new Point((int)(Rect.X - dt * gc.DefaultSpeed / 2), Rect.Y), new Point(gc.RegionWidth, gc.RegionHeight));
                    break;
            }

            int dx = Math.Abs(StartPosition.X - Rect.X);
            int dy = Math.Abs(StartPosition.Y - Rect.Y);

            if (dx != 0 && dx % gc.RegionWidth == 0)
            {
                if(OnLineProgress != null)
                {
                    var pos = gc.GetIndexes(Rect.X, Rect.Y);
                    OnLineProgress(pos.X - 1, pos.Y - 1);
                    if(pos.X - 1 <= 0 || pos.X - 1 >= 7)
                    {
                        State = BonusState.HIDE;
                    }
                }
            }

            if(dy != 0 && dy % gc.RegionHeight == 0)
            {
                if(OnLineProgress != null)
                {
                    var pos = gc.GetIndexes(Rect.X, Rect.Y);
                    OnLineProgress(pos.X - 1, pos.Y - 1);
                    if (pos.Y - 1 <= 0 || pos.Y - 1 >= 7)
                    {
                        State = BonusState.HIDE;
                    }
                }
            }

            if(ddt >= gc.ADTime)
            {
                ddt = 0;

                stepAnimation += directionAnimation;

                if(stepAnimation > 5)
                {
                    stepAnimation = 4;
                    directionAnimation *= -1;
                }

                if(stepAnimation < 0)
                {
                    stepAnimation = 0;
                    directionAnimation *= -1;
                }
            }
        }
    }


    public enum BonusState
    {
        SHOW = 0,
        HIDE = 1
    }

    public enum Direction
    {
        LEFT = 0,
        RIGHT = 1,
        UP = 2,
        BOTTOM = 3
    }
}
