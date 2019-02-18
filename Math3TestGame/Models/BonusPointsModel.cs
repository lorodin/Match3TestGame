using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Math3TestGame.Models
{
    public class BonusPointsModel : IDrawableModel
    {
        public int Points { get; set; } = 0;

        public Rectangle Rect { get; set; }

        private GameConfigs gc;
        private Vector2 position;

        public BonusPointsModel()
        {
            gc = GameConfigs.GetInstance();
            Rect = new Rectangle(gc.GetRealPoint(1, 0.5f), new Point(0, 0));
            position = new Vector2(Rect.X, Rect.Y);
        }

        public void Draw(SpriteBatch sb)
        {
            sb.DrawString(gc.DefaultFont, "Total points: " + Points, position, Color.White);
        }

        public void Update(int dt)
        {
        }
    }
}
