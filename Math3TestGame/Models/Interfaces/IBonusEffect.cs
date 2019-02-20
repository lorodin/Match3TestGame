using Math3TestGame.Models.GameModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math3TestGame.Models.Interfaces
{
    public interface IBonusEffect:IDynamic
    {
        BonusEffect BonusType { get; }
        int AnimationStep { get; }
    }
}
