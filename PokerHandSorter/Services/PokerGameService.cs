using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokerHandSorter.Models;

namespace PokerHandSorter.Services
{
    public class PokerGameService : IPokerGameService
    {
        private readonly Game _game;

        public PokerGameService(Game game)
        {
            _game = game;
        }

        public void PlayGame()
        {
            try
            {
                //each line represents a hand
                foreach (var line in _game.Lines)
                {
                    var hand = new Hand(_game.Players, line);
                    hand.EvaluatePlayerHands();

                    // get the winner of the hand
                    // check number of distinct handRankings
                    // if == number of players, they have different scores - get the max for winner
                    if (hand.PlayerHands.Select(ph => new { ph.HandRanking }).Distinct().Count() == _game.Players.Count())
                    {
                        var pl = hand.PlayerHands.OrderByDescending(ph => (int)ph.HandRanking).FirstOrDefault().Player;
                        pl.HandsWon++;
                    }
                    // if there is a "tie" (same HandRanking)
                    // && hand ranking is only "highCard" (1)
                    // find the highest single card
                    else if (hand.PlayerHands.FirstOrDefault().HandRanking == Hand.HandRanking.HighCard)
                    {
                        Player highCardPlayer = null;

                        for (int i = 0; i < 5; i++) //loop through cards
                        {
                            int? highCardValue = null;
                            for (int j = 0; j < hand.PlayerHands.Count(); j++) //loop through hands
                            {
                                var value = hand.PlayerHands[j].PlayerCards[i].CardValueInt;

                                //if there's a tie, break out, no winner
                                if (highCardValue.HasValue && value == highCardValue)
                                {
                                    highCardPlayer = null;
                                    break;
                                }

                                if (highCardValue == null || value > highCardValue)
                                {
                                    highCardValue = value;
                                    highCardPlayer = hand.PlayerHands[j].Player;
                                }

                                if (j == hand.PlayerHands.Count() - 1)
                                {
                                    break;
                                }
                            }
                            if (highCardPlayer != null)
                            {
                                break;
                            }
                        }

                        highCardPlayer.HandsWon++;
                    }
                    // tie of handRanking (not "highCard")
                    // take highest of ranked cards
                    else
                    {
                        // if handRanking is same AND ComboHighCard is same
                        if (hand.PlayerHands.Select(ph => new { ph.CombinationHighCard }).Distinct().Count() == 1)
                        {
                            var highest = hand.PlayerHands.Max(c => c.PlayerCards.FirstOrDefault().CardValueInt);

                            var pl = hand.PlayerHands.Where(pc => pc.PlayerCards.FirstOrDefault().CardValueInt == highest)
                                                    .Select(pc => pc.Player).FirstOrDefault();
                            pl.HandsWon++;
                        }
                        else
                        {
                            var pl = hand.PlayerHands.OrderByDescending(ph => ph.CombinationHighCard).FirstOrDefault().Player;
                            pl.HandsWon++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string OutputResult()
        {
            var sb = new StringBuilder();
            foreach (var player in _game.Players)
            {
                sb.AppendLine(string.Format("{0}: {1}", player.Value.PlayerName, player.Value.HandsWon));
            }
            return sb.ToString();
        }        
    }
}
