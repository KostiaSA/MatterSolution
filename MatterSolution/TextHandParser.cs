using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatterSolution
{

    public enum ParserActionType { Fold, Check, Call, Raise, BB, SB, Unbet };
    public enum ParserRoom { PokerStars, Poker888 };

    public class ParserAction
    {
        public TextHandParser Parser;
        public Street Street;
        public HandPlayer Player;
        public ParserActionType Action;
        public decimal Stavka;
        //public decimal Stack;
        //public decimal Pot;
    }

    public class TextHandParser
    {
        public string Text;
        public string HandId;
        public int ButtonSeat;
        public string Board;
        public ParserRoom Room;

        public List<HandPlayer> Players = new List<HandPlayer>();
        public List<ParserAction> Actions = new List<ParserAction>();

        public void Parse()
        {
            //HandId = "";
            //Players.Clear();
            //Actions.Clear();
            Street street = Street.Preflop;
            Decimal TotalPot;
            Decimal Rake;

            var lines = Text.Split('\n');

            if (lines[0].StartsWith("PokerStars Hand #"))
            {
                Room = ParserRoom.PokerStars;
                var words = lines[0].Split(new[] { "PokerStars Hand #", ":  Hold'em No Limit" }, StringSplitOptions.RemoveEmptyEntries);
                HandId = words[0];
            }
            else
                throw new Exception("неизвестный Room " + lines[0]);

            foreach (string line in lines)
            {
                if (line.EndsWith(" is the button"))
                {
                    var words = line.Split(new[] { "6-max Seat #", " is the button" }, StringSplitOptions.RemoveEmptyEntries);
                    ButtonSeat = Int16.Parse(words.Last());
                }
                else
                if (line.StartsWith("Seat ") && line.EndsWith("in chips)"))
                {
                    string seatStr = Convert.ToString(line[5]);
                    HandPlayer player = new HandPlayer();
                    player.Seat = Int16.Parse(seatStr);
                    var shortLine = line.Substring(8);
                    var words = shortLine.Split(new[] { " ($", " in chips)" }, StringSplitOptions.RemoveEmptyEntries);
                    player.Name = words[0];
                    player.StartStack = Decimal.Parse(words[1]);
                    Players.Add(player);
                }
                else
                if (line.Contains(": posts small blind $"))
                {
                    var words = line.Split(new[] { ": posts small blind $" }, StringSplitOptions.RemoveEmptyEntries);
                    var action = new ParserAction();
                    action.Parser = this;
                    action.Street = street;
                    action.Player = Players.Find(p => p.Name == words[0]);
                    action.Action = ParserActionType.SB;
                    action.Stavka = Decimal.Parse(words[1]);
                    action.Player.WinAmount -= action.Stavka;
                    Actions.Add(action);
                }
                else
                if (line.Contains(": posts big blind $"))
                {
                    var words = line.Split(new[] { ": posts big blind $" }, StringSplitOptions.RemoveEmptyEntries);
                    var action = new ParserAction();
                    action.Parser = this;
                    action.Street = street;
                    action.Player = Players.Find(p => p.Name == words[0]);
                    action.Action = ParserActionType.BB;
                    action.Stavka = Decimal.Parse(words[1]);
                    action.Player.WinAmount -= action.Stavka;
                    Actions.Add(action);
                }
                else
                if (line.Contains(": raises $"))
                {
                    var words = line.Split(new[] { ": raises $", " to $" }, StringSplitOptions.RemoveEmptyEntries);
                    var action = new ParserAction();
                    action.Parser = this;
                    action.Street = street;
                    action.Player = Players.Find(p => p.Name == words[0]);
                    action.Action = ParserActionType.Raise;
                    action.Stavka = Decimal.Parse(words[1]);
                    action.Player.WinAmount -= action.Stavka;
                    Actions.Add(action);
                }
                else
                if (line.Contains(": bets $"))
                {
                    var words = line.Split(new[] { ": bets $"}, StringSplitOptions.RemoveEmptyEntries);
                    var action = new ParserAction();
                    action.Parser = this;
                    action.Street = street;
                    action.Player = Players.Find(p => p.Name == words[0]);
                    action.Action = ParserActionType.Raise;
                    action.Stavka = Decimal.Parse(words[1]);
                    action.Player.WinAmount -= action.Stavka;
                    Actions.Add(action);
                }
                else
                if (line.StartsWith("Uncalled bet ($"))
                {
                    var words = line.Split(new[] { "Uncalled bet ($", ") returned to " }, StringSplitOptions.RemoveEmptyEntries);
                    var action = new ParserAction();
                    action.Parser = this;
                    action.Street = street;
                    action.Player = Players.Find(p => p.Name == words[1]);
                    action.Action = ParserActionType.Unbet;
                    action.Stavka = -Decimal.Parse(words[0]);
                    action.Player.WinAmount -= action.Stavka;
                    Actions.Add(action);
                }
                else
                if (line.Contains(": calls $"))
                {
                    var words = line.Split(new[] { ": calls $" }, StringSplitOptions.RemoveEmptyEntries);
                    var action = new ParserAction();
                    action.Parser = this;
                    action.Street = street;
                    action.Player = Players.Find(p => p.Name == words[0]);
                    action.Action = ParserActionType.Call;
                    action.Stavka = Decimal.Parse(words[1]);
                    action.Player.WinAmount -= action.Stavka;
                    Actions.Add(action);
                }
                else
                if (line.EndsWith(": checks"))
                {
                    var words = line.Split(new[] { ": checks" }, StringSplitOptions.RemoveEmptyEntries);
                    var action = new ParserAction();
                    action.Parser = this;
                    action.Street = street;
                    action.Player = Players.Find(p => p.Name == words[0]);
                    action.Action = ParserActionType.Check;
                    action.Stavka = 0;
                    Actions.Add(action);
                }
                else
                if (line.EndsWith(": folds"))
                {
                    var words = line.Split(new[] { ": folds" }, StringSplitOptions.RemoveEmptyEntries);
                    var action = new ParserAction();
                    action.Parser = this;
                    action.Street = street;
                    action.Player = Players.Find(p => p.Name == words[0]);
                    action.Action = ParserActionType.Fold;
                    action.Stavka = 0;
                    Actions.Add(action);
                }
                else
                if (line.StartsWith("*** FLOP *** ["))
                {
                    var words = line.Split(new[] { "*** FLOP *** [","]" }, StringSplitOptions.RemoveEmptyEntries);
                    street = Street.Flop;
                    Board = words[0].Replace(" ","");
                }
                else
                if (line.StartsWith("*** TURN *** ["))
                {
                    var words = line.Split(new[] { "*** TURN *** [" }, StringSplitOptions.RemoveEmptyEntries);
                    street = Street.Flop;
                    Board = words[0].Replace(" ", "").Replace("[", "").Replace("]", "");
                }
                else
                if (line.StartsWith("*** RIVER *** ["))
                {
                    var words = line.Split(new[] { "*** RIVER *** [" }, StringSplitOptions.RemoveEmptyEntries);
                    street = Street.Flop;
                    Board = words[0].Replace(" ", "").Replace("[", "").Replace("]", "");
                }
                else
                if (line.Contains(": shows ["))
                {
                    var words = line.Split(new[] { ": shows [", "] (" }, StringSplitOptions.RemoveEmptyEntries);
                    var player = Players.Find(p => p.Name == words[0]);
                    player.PocketPairStr= words[1].Replace(" ", "");
                }
                else
                if (line.StartsWith("Total pot $"))
                {
                    var words = line.Split(new[] { "Total pot $", " | Rake $" }, StringSplitOptions.RemoveEmptyEntries);
                    TotalPot = Decimal.Parse(words[0]);
                    Rake = Decimal.Parse(words[1]);
                }
                else
                if (line.Contains(" collected $") && line.Contains(" from pot"))
                {
                    var words = line.Split(new[] { " collected $", " from pot" }, StringSplitOptions.RemoveEmptyEntries);
                    var player = Players.Find(p => p.Name == words[0]);
                    player.WinAmount += Decimal.Parse(words[1].Replace(" ", ""));
                }


            }

            decimal checkSum = Players.Sum(x => x.WinAmount);

        }
    }
}
