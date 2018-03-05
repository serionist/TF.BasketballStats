using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TF.BasketballStats
{
    public enum GameEventType:int
    {
        QuarterStart,
        QuarterStop,
        TimerStop,
        TimerStart,
        TimeoutStart,
        TimeoutEnd,
        PlayerEnter,
        PlayerLeave,
        Fault,
        ForcedFault,
        NearTwoPointAttempt,
        NearTwoPointMade,
        MidTwoPointAttempt,
        MidTwoPointMade,
        ThreePointAttempt,
        ThreePointMade,
        OffensiveRebound,
        DefensiveRebound,
        Assist,
        Block,
        Steal,
        FreeThrowAttempt,
        FreeThrowMade,
        Turnover,
        GameEnd
    }
   
}
