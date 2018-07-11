using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatterSolution
{
    public enum PlayerPos { UTG, MP, CO, BTN, SB, BB };

    public enum PlayerActionType { Fold, Check, Call, Bet, Raise, Limp, SB, BB, Bet3, CallBet3, Squeeze, CallSqueeze, Bet4, CallBet4, Bet5, CallBet5 };

    public enum PreflopState { None, WasLimp, WasRaise, WasRaiseCall, WasBet3, WasBet3Call, WasSqueeze, WasCallSqueeze, WasBet4, WasBet4Call,  WasBet5, WasBet5Call };

    public enum PosflopState { None, WasCheck, WasBet, WasBetCall, WasRaise,  WasRaiseCall };

    public enum Street { Preflop, Flop, Turn, River };

    public class HandEngine
    {
        public HandEngine()
        {

        }
    }
}
