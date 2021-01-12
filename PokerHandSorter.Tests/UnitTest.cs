using System;
using Xunit;
using PokerHandSorter.Services;
using PokerHandSorter.Models;

namespace PokerHandSorter.Tests
{
    //public class UnitTest1
    //{
    //    private PokerGameService _sorterService { get; set; }
    //    private Game _game { get; set; }

    //    //public UnitTest1()
    //    //{
    //    //    //_sorterService = new SorterService();
    //    //}


    //    //[Theory, InlineData("poker-hands.txt")]
    //    //public void TestGetDataFromFileTrue(string filePath)
    //    //{
    //    //    var game = new Game(filePath, 2);
    //    //    Assert.NotEmpty(game.GetDataFromFile());
    //    //}

    //    //[Theory, InlineData("poker-hands.txt")]
    //    //public void TestGetDataEquals500(string filePath)
    //    //{
    //    //    var game = new Game(filePath, 2);
    //    //    Assert.Equal(500, game.GetDataFromFile().Length);
    //    //}


    //  //  [Theory]
    //    //[InlineData("6C 8S 3S TS 4S")]      
    //    //[InlineData("2S KD TH 9D AH")]       
    //    //[InlineData("AH 6S KS 8D 5D")]
    //    //public void TestWinnerPlayer1(string handInput)
    //    //{
    //    //    var player1 = "Player 1";
    //    //    Assert.Equal(player1, _sorterService.CountWinningHands(handInput));
    //    //}

    //    //[Theory]
    //    //[InlineData("AD 5H 8D 5C 2H")] // pair
    //    //public void HasPairTrue(string handInput)
    //    //{
    //    //    var player1 = "Player 1";
    //    //    Assert.Equal(player1, _sorterService.CountWinningHands(handInput));
    //    //}

    //    //[Theory]
    //    //[InlineData("2S AD 7H 2C AC")] // 2 pair
    //    //public void HasTwoPairsTrue(string handInput)
    //    //{
    //    //    var player1 = "Player 1";
    //    //    Assert.Equal(player1, _sorterService.CountWinningHands(handInput));
    //    //}

    //    //[Theory]
    //    //[InlineData("KC QC 6C 6D 6H")] // 3 of a kind
    //    //public void HasThreeOfAKindTrue(string handInput)
    //    //{
    //    //    var player1 = "Player 1";
    //    //    Assert.Equal(player1, _sorterService.CountWinningHands(handInput));
    //    //}

    //    //[Theory]
    //    //[InlineData("6C 5H 8S 4H 7S")] //straight
    //    //public void HasStraightTrue(string handInput)
    //    //{
    //    //    var player1 = "Player 1";
    //    //    Assert.Equal(player1, _sorterService.CountWinningHands(handInput));
    //    //}

    //    //[Theory]
    //    //[InlineData("8C QC 4C AC JC")] // flush
    //    //public void HasFlushTrue(string handInput)
    //    //{
    //    //    var player1 = "Player 1";
    //    //    Assert.Equal(player1, _sorterService.CountWinningHands(handInput));
    //    //}

    //    //[Theory]
    //    //[InlineData("AS AC 3C 3H 3S")] // full house
    //    //public void HasFullHouseTrue(string handInput)
    //    //{
    //    //    var player1 = "Player 1";
    //    //    Assert.Equal(player1, _sorterService.CountWinningHands(handInput));
    //    //}

    //    //[Theory]
    //    //[InlineData("TH TS TD 8H TC")] //4 of a kind
    //    //public void HasFourOfAKindTrue(string handInput)
    //    //{
    //    //    var player1 = "Player 1";
    //    //    Assert.Equal(player1, _sorterService.CountWinningHands(handInput));
    //    //}

    //    //[Theory]
    //    //[InlineData("2C 3C 6C 5C 4C")] // straight flush
    //    //public void HasStraightFlushTrue(string handInput)
    //    //{
    //    //    var player1 = "Player 1";
    //    //    Assert.Equal(player1, _sorterService.CountWinningHands(handInput));
    //    //}

    //    //[Theory]
    //    //[InlineData("KC TC QC JC AC")] // royal flush
    //    //public void HasRoyalFlushTrue(string handInput)
    //    //{
    //    //    var player1 = "Player 1";
    //    //    Assert.Equal(player1, _sorterService.CountWinningHands(handInput));
    //    //}
    //}
}
