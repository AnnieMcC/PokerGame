using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerHandSorter.Models
{
    public struct PlayerHand
    {
        public Player Player { get; set; }
        public Card[] PlayerCards { get; set; }

        public Hand.HandRanking HandRanking { get; set; }
        public int CombinationHighCard { get; set; }
    }

    public class Hand
    {
        public enum HandRanking
        {
            HighCard = 1,
            Pair,
            TwoPairs,
            ThreeOfAKind,
            Straight,
            Flush,
            FullHouse,
            FourOfAKind,
            StraightFlush,
            RoyalFlush
        }

        private readonly Dictionary<int, Player> _players;
        private readonly string _line;

        public List<PlayerHand> PlayerHands { get; set; }

        //.ctor
        public Hand(Dictionary<int, Player> players, string line)
        {
            _players = players;
            _line = line;
        }

        internal void EvaluatePlayerHands()
        {
            PlayerHands = new List<PlayerHand>();
            foreach (var player in _players)
            {
                var playerHand = new PlayerHand()
                {
                    Player = player.Value,
                    PlayerCards = GetPlayerCards(player.Key) // ordered
                };
                int highCard = 0;
                playerHand.HandRanking = EvaluateHand(playerHand.PlayerCards, ref highCard);
                playerHand.CombinationHighCard = highCard;

                PlayerHands.Add(playerHand);
            }    
        }

        private Card[] GetPlayerCards(int key)
        {
            var strItemsArr = key == 0 ? _line.Split(" ").Take(5) : _line.Split(" ").TakeLast(5);

            var playerCardsList = new List<Card>();

            string cardValueStr, cardSuitStr;

            foreach (var item in strItemsArr)
            {
                cardValueStr = (int.TryParse(item.Substring(0, 1), out int cardNumber)) ? CardExtensions.NumberToWord(cardNumber) : item.Substring(0, 1);
                cardSuitStr = item.Substring(1, 1);
                playerCardsList.Add(new Card
                {
                    CardValue = (Card.FaceValue)Enum.Parse(typeof(Card.FaceValue), cardValueStr),
                    CardValueInt = (int)(Card.FaceValue)Enum.Parse(typeof(Card.FaceValue), cardValueStr),
                    CardSuit = (Card.Suit)Enum.Parse(typeof(Card.Suit), cardSuitStr)
                });
            }

            return playerCardsList.OrderByDescending(c => c.CardValueInt).ToArray();
        }

        public HandRanking EvaluateHand(Card[] playerCards, ref int highCard)
        {
            int highestComboCard = 0;
            if (HasRoyalFlush(playerCards))
            {
                highCard = (int)Card.FaceValue.A;
                return HandRanking.RoyalFlush;
            }
            if (HasStraightFlush(playerCards, ref highestComboCard))
            {
                highCard = highestComboCard;
                return HandRanking.StraightFlush;
            }
            if (HasFourOfAKind(playerCards, ref highestComboCard))
            {
                highCard = highestComboCard;
                return HandRanking.FourOfAKind;
            }
            if (HasFullHouse(playerCards, ref highestComboCard))
            {
                highCard = highestComboCard;
                return HandRanking.FullHouse;
            }
            if (HasFlush(playerCards, ref highestComboCard))
            {
                highCard = highestComboCard;
                return HandRanking.Flush;
            }
            if (HasStraight(playerCards, ref highestComboCard))
            {
                highCard = highestComboCard;
                return HandRanking.Straight;
            }
            if (HasThreeOfAKind(playerCards, ref highestComboCard))
            {
                highCard = highestComboCard;
                return HandRanking.ThreeOfAKind;
            }
            if (HasTwoPairs(playerCards, ref highestComboCard))
            {
                highCard = highestComboCard;
                return HandRanking.TwoPairs;
            }
            if (HasPair(playerCards, ref highestComboCard))
            {
                highCard = highestComboCard;
                return HandRanking.Pair;
            }

            highCard = playerCards.Max(c => c.CardValueInt);
            return HandRanking.HighCard;
        }


        //========================================================
        private bool HasRoyalFlush(Card[] playerCards)
        {
            var grouped = playerCards.GroupBy(c => c.CardSuit)
                                     .Select(grp => new
                                     {
                                         key = grp.Key,
                                         count = grp.Count()
                                     });
            var ordered = playerCards.OrderBy(c => c.CardValueInt);

            bool hasAce = playerCards.Any(c => c.CardValue == Card.FaceValue.A);
            bool hasStraight = !ordered.Select((i, j) => i.CardValueInt - j).Distinct().Skip(1).Any();
            bool hasFlush = grouped.Any(a => a.count == 5);

            return hasStraight && hasFlush && hasAce;
        }

        private bool HasStraightFlush(Card[] playerCards, ref int highestComboCard)
        {
            var grouped = playerCards.GroupBy(c => c.CardSuit)
                                     .Select(grp => new
                                     {
                                         key = grp.Key,
                                         count = grp.Count()
                                     });

            var ordered = playerCards.OrderBy(c => c.CardValueInt);

            bool hasStraight = !ordered.Select((i, j) => i.CardValueInt - j).Distinct().Skip(1).Any();
            bool hasFlush = grouped.Any(a => a.count == 5);

            highestComboCard = ordered.Max(c => c.CardValueInt);
            return hasStraight && hasFlush;           
        }

        private bool HasFourOfAKind(Card[] playerCards, ref int highestComboCard)
        {
            var grouped = playerCards.GroupBy(pl => pl.CardValue)
                                  .Select(grp => new
                                  {
                                      key = grp.Key,
                                      count = grp.Count()
                                  }).Where(a => a.count == 4);

            highestComboCard = (int)grouped.Select(a => a.key).FirstOrDefault();
            return grouped.Any();
        }

        private bool HasFullHouse(Card[] playerCards, ref int highestComboCard)
        {
            var grouped = playerCards.GroupBy(pl => pl.CardValue)
                                  .Select(grp => new
                                  {
                                      key = grp.Key,
                                      count = grp.Count()
                                  });

            highestComboCard = (int)grouped.Where(a => a.count == 3)
                                            .Select(a => a.key).FirstOrDefault();
            return grouped.Any(a => a.count == 3) && grouped.Any(a => a.count == 2);
        }

        private bool HasFlush(Card[] playerCards, ref int highestComboCard)
        {
            var grouped = playerCards.GroupBy(c => c.CardSuit)
                                     .Select(grp => new
                                     {
                                         key = grp.Key,
                                         count = grp.Count()
                                     });

            highestComboCard = playerCards.OrderByDescending(c => c.CardValueInt).FirstOrDefault().CardValueInt;
            return grouped.Any(a => a.count == 5);
        }

        private bool HasStraight(Card[] playerCards, ref int highestComboCard)
        {
            var ordered = playerCards.OrderBy(c => c.CardValueInt);

            highestComboCard = playerCards.OrderByDescending(c => c.CardValueInt).FirstOrDefault().CardValueInt;
            return !ordered.Select((i, j) => i.CardValueInt - j).Distinct().Skip(1).Any();
        }

        private bool HasThreeOfAKind(Card[] playerCards, ref int highestComboCard)
        {
            var grouped = playerCards.GroupBy(c => c.CardValue)
                                  .Select(grp => new
                                  {
                                      key = grp.Key,
                                      count = grp.Count()
                                  }).Where(a => a.count == 3);

            highestComboCard = (int)grouped.Select(a => a.key).FirstOrDefault();
            return grouped.Any();
        }

        private bool HasTwoPairs(Card[] playerCards, ref int highestComboCard)
        {
            var grouped = playerCards.GroupBy(c => c.CardValueInt)
                                  .Select(grp => new
                                  {
                                      key = grp.Key,
                                      count = grp.Count()
                                  }).Where(grp => grp.count == 2);

            if (grouped.Any())
            {
                highestComboCard = grouped.Max(k => k.key);
                return true;
            }
            return false;
        }

        private bool HasPair(Card[] playerCards, ref int highestComboCard)
        {
            var grouped = playerCards.GroupBy(c => c.CardValue)
                                  .Select(grp => new
                                  {
                                      key = grp.Key,
                                      count = grp.Count()
                                  }).Where(a => a.count == 2);

            highestComboCard = (int)grouped.Select(a => a.key).FirstOrDefault();
            return grouped.Any();
        }
    }
}
