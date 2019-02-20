using Math3TestGame.Models.GameModels;
using Math3TestGame.Tools;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math3TestGame.Models.Interfaces
{
    public interface IDrawableModel
    {
        Rectangle Region { get; set; }
        SpriteName SpriteName { get; }
        int SpriteAnimationStep { get; }
        SpriteAnimationState AnimationState { get; set; }
    }
}
