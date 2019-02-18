using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math3TestGame.Models
{
    public interface IDrawableModel
    {
        Rectangle Rect { get; set; }
        void Draw(SpriteBatch sb);
        void Update(int dt);
    }
}
