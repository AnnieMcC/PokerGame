using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;

namespace PokerHandSorter.Models
{
    public class Game
    {
        private readonly string _dataFilePath;

        public Dictionary<int, Player> Players;
        public string[] Lines { get; set; }

        public Game(string dataFile, Dictionary<int, Player> players)
        {
            _dataFilePath = dataFile;
            Players = players;

            Lines = GetDataFromFile();
        }

        private string[] GetDataFromFile()
        {
            try
            {
                string[] lines = null;
                var path = Path.GetRelativePath(Directory.GetCurrentDirectory(), _dataFilePath);
                if (File.Exists(path))
                {
                    lines = File.ReadAllLines(_dataFilePath);
                }
                else
                {
                    throw new FileNotFoundException();
                }
                return lines;
            }
            catch (FileNotFoundException ex)
            {
                throw ex;
            }
        }
    }
}