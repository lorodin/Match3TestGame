using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Math3TestGame.Models
{
    public class GameModel : IDrawableModel
    {
        public Rectangle Rect { get; set; }

        public IDrawableModel BonusPoints { get; set; }
        public IDrawableModel Timer { get; set; }

        public List<LineBonusEffect> Lines { get; set; }

        public IDrawableModel GameMatrix { get; set; }

        public bool GameOver { get; set; } = false;

        public GameOverDialogModel GameOverDialog { get; set; }

        public GameModel()
        {
        }

        public void Draw(SpriteBatch sb)
        {
            if (GameOver)
            {
                GameOverDialog.Draw(sb);
                return;
            }
            foreach (var l in Lines) l.Draw(sb);
            BonusPoints.Draw(sb);
            Timer.Draw(sb);
            GameMatrix.Draw(sb);
        }

        public void Update(int dt)
        {
            if (GameOver) return;
            BonusPoints.Update(dt);
            Timer.Update(dt);
            GameMatrix.Update(dt);
        }
    }
}
