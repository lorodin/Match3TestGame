using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math3TestGame.Models.Interfaces
{
    public interface IDynamic
    {
        void Update(int dt);
        DynamicState State { get; set; }
    }

    public enum DynamicState
    {
        STOP = 0,
        RUN = 1,
        END = 2
    }
}
