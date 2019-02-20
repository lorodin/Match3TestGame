using Math3TestGame.Models.GameModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math3TestGame.Models
{
    public class SwapModel
    {
        public AGameObject GameObject1 { get; set; }
        public AGameObject GameObject2 { get; set; }
        public SwapDirection Direction { get; set; }
    }

    public enum SwapDirection
    {
        VERTICAL = 0,
        HORIZONTAL = 1
    }
}
