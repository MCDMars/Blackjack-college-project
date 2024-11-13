using System;
using System.Collections.Generic;
using System.Text;

namespace Assessment_1___BlackJack
{
    class Player
    {
        public string Name { get; set; }

        public int HandValue { get; set; }

        public bool IsBust { get; set; }
        
        public bool IsTurn { get; set; }

        public List<Card> Hand { get; set; } = new List<Card>();

        // Empty constructor for actual players
        public Player()
        {
        }

        // Parameter constructor for dealer
        public Player (string name)
        {
            Name = name;
        }
    }
}
