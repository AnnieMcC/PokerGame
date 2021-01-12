using System;
using System.Collections.Generic;
using PokerHandSorter.Models;
using PokerHandSorter.Services;
using Xunit;

namespace PokerHandSorter.Tests
{
    public class UnitTest2
    {
        const string fileName = "poker-hands.txt";

        public Dictionary<int, Player> Players { get; set; }
        public Game Game { get; set; }
        public PokerGameService PokerGameService { get; set; }

        public UnitTest2()
        {
            Players = new Dictionary<int, Player>();
            for (int i = 0; i < 2; i++)
            {
                Players.Add(i, new Player() { PlayerName = $"Player {i + 1}" });
            }
        }

        [Fact]
        public void TestCreateGame()
        {
            var game = new Game("poker-hands.txt", Players);
            Assert.NotNull(game);
        }

        //    //[Fact]
        //    //public void TestCreatePlayers()
        //    //{
        //    //    var game = new Game("txt", 2);
        //    //    game.CreatePlayers();
        //    //    Assert.Collection(game._players,
        //    //        item => Assert.Equal("Player1", item.Key),
        //    //        item => Assert.Equal("Player2", item.Key));
        //    //}

        [Theory,
            InlineData("4D 6S 9H QH QC 3D 6D 7H QD QS"), // 2 x Pair, same rank
            InlineData("5D 8C 9S JS AC 2C 5C 7D 8S QH"), // High card Ace
            InlineData("2H 2D 4C 4D 4S 3C 3D 3S 9S 9D"), // Full house w 3 x 4s
            InlineData("4H 7C 4S 8D 8C 3D JH 3H 5D AS")]
        public void Player1Wins(string hand)
        {
            string[] lines = new string[] { hand };
            Game = new Game(fileName, Players);
            Game.Lines = lines;

            PokerGameService = new PokerGameService(Game);
            PokerGameService.PlayGame();

            var expected = "Player 1: 1\nPlayer 2: 0\n";
            var result = PokerGameService.OutputResult();
            Assert.Equal(expected, result);
        }

        [Theory,
            InlineData("9H 9C 6S 7S KD 2C 3S 9S 9D AD"), // 2 x Pair, same rank
            InlineData("2D 9C AS AH AC 3D 6D 7D TD QD"), // 3 of a kind
            InlineData("2D 2C 2S AH AC QC QH 6H 6S 6D"), // 2 x Full House
            InlineData("QC QH 6H 6S 6D KC TC QC JC AC") // full house & flush*
            ] // royal flush
        public void Player2Wins(string hand)
        {
            string[] lines = new string[] { hand };
            Game = new Game(fileName, Players);
            Game.Lines = lines;

            PokerGameService = new PokerGameService(Game);
            PokerGameService.PlayGame();

            var expected = "Player 1: 0\nPlayer 2: 1\n";
            var result = PokerGameService.OutputResult();
            Assert.Equal(expected, result);
        }

        [Theory,
            InlineData("AH TC 6S 2S 2D TC 9S 5S 2D 2D"), // 2 x Pair, same rank
            InlineData("AH AC KS KS 6D JC JS 5S 4D 4D")] // 2 x Pair, same rank
        public void Player1Wins2Pairs(string hand)
        {
            string[] lines = new string[] { hand };
            Game = new Game(fileName, Players);
            Game.Lines = lines;

            PokerGameService = new PokerGameService(Game);
            PokerGameService.PlayGame();

            var expected = "Player 1: 1\nPlayer 2: 0\n";
            var result = PokerGameService.OutputResult();
            Assert.Equal(expected, result);
        }
    }
}
