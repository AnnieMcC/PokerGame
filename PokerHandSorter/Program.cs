using System;
using System.Collections.Generic;
using PokerHandSorter.Models;
using PokerHandSorter.Services;

namespace PokerHandSorter
{
    class Program
    {
        const int numberOfPlayers = 2;
        const string fileName = "poker-hands.txt";

        private static void Main()
        {
            try
            {
                Console.WriteLine("Press Enter to start the poker game...");
                Console.ReadLine();

                var players = new Dictionary<int, Player>();
                for (int i = 0; i < numberOfPlayers; i++)
                {
                    players.Add(i, new Player() { PlayerName = $"Player {i + 1}" });
                }

                var game = new Game(fileName, players);

                var pokerGameService = new PokerGameService(game);
                pokerGameService.PlayGame();

                Console.WriteLine(pokerGameService.OutputResult());
              
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.UtcNow}: Error in Main: {ex.Message}");
            }
        }
    }
}