using System;
namespace PokerHandSorter.Models
{
    public class Card
    {
        public enum Suit
        {
            H,
            S,
            C,
            D
        }

        public enum FaceValue
        {
            Two = 2,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            T,
            J,
            Q,
            K,
            A
        }

        public Suit CardSuit { get; set; }
        public FaceValue CardValue { get; set; }
        public int CardValueInt { get; set; }

        public Card()
        { }
    }

    public static class CardExtensions
    {
        //change the numeral string to word & look up in the enum
        public static string NumberToWord(int number)
        {
            var numberWordArr = new string[] { "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
            return numberWordArr[number - 2];
        }
    }
}