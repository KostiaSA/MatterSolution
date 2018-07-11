using MatterSolution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApp
{
    public class TestHandParser
    {
        public string Text1 = @"PokerStars Hand #172702447363:  Hold'em No Limit ($0.05/$0.10 USD) - 2017/07/06 21:33:38 ET
Table 'Amanda II' 6-max Seat #6 is the button
Seat 1: lexon3 ($10 in chips)
Seat 2: cezarmaxim ($9.81 in chips)
Seat 3: Dracyus ($10.53 in chips)
Seat 4: Gerafont ($12.66 in chips)
Seat 5: gogonoway ($9.51 in chips)
Seat 6: XtZ1 ($10.10 in chips)
lexon3: posts small blind $0.05
cezarmaxim: posts big blind $0.10
*** HOLE CARDS ***
Dracyus: raises $0.20 to $0.30
Gerafont: raises $0.60 to $0.90
gogonoway: folds
XtZ1: folds
lexon3: folds
cezarmaxim: folds
Dracyus: calls $0.60
*** FLOP *** [3d 6s 8c]
Dracyus: checks
Gerafont: bets $0.93
Dracyus: calls $0.93
*** TURN *** [3d 6s 8c] [5c]
Dracyus: checks
Gerafont: checks
*** RIVER *** [3d 6s 8c 5c] [Qc]
Dracyus: checks
Gerafont: checks
*** SHOW DOWN ***
Dracyus: shows [Tc Td] (a pair of Tens)
Gerafont: mucks hand
Dracyus collected $3.64 from pot
*** SUMMARY ***
Total pot $3.81 | Rake $0.17
Board [3d 6s 8c 5c Qc]
Seat 1: lexon3 (small blind) folded before Flop
Seat 2: cezarmaxim (big blind) folded before Flop
Seat 3: Dracyus showed [Tc Td] and won ($3.64) with a pair of Tens
Seat 4: Gerafont mucked
Seat 5: gogonoway folded before Flop (didn't bet)
Seat 6: XtZ1 (button) folded before Flop (didn't bet)";

        public string Text2 = @"PokerStars Hand #172705239374:  Hold'em No Limit ($0.05/$0.10 USD) - 2017/07/06 23:52:12 ET
Table 'Adriana' 6-max Seat #4 is the button
Seat 1: Snejinka93 ($14.35 in chips)
Seat 2: granmephisto ($10.14 in chips)
Seat 3: NMKidman ($33.83 in chips)
Seat 4: CheburaLLLKa ($10.05 in chips)
Seat 6: alexmr2709 ($10 in chips)
alexmr2709: posts small blind $0.05
Snejinka93: posts big blind $0.10
*** HOLE CARDS ***
granmephisto: raises $0.20 to $0.30
NMKidman: calls $0.30
CheburaLLLKa: folds
alexmr2709: calls $0.25
Snejinka93: folds
*** FLOP *** [4h Tc 5d]
alexmr2709: checks
granmephisto: bets $1
NMKidman: calls $1
alexmr2709: folds
*** TURN *** [4h Tc 5d] [Ah]
granmephisto: bets $1.50
NMKidman: folds
Uncalled bet ($1.50) returned to granmephisto
granmephisto collected $2.86 from pot
*** SUMMARY ***
Total pot $3 | Rake $0.14
Board [4h Tc 5d Ah]
Seat 1: Snejinka93 (big blind) folded before Flop
Seat 2: granmephisto collected ($2.86)
Seat 3: NMKidman folded on the Turn
Seat 4: CheburaLLLKa (button) folded before Flop (didn't bet)
Seat 6: alexmr2709 (small blind) folded on the Flop";

        public void Test1()
        {
            var parser = new TextHandParser();
            parser.Text = Text1;
            parser.Parse();
            Console.WriteLine("Готово");
            Console.ReadKey();
        }
    }
}