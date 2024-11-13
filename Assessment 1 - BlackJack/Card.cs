using System;
using System.Collections.Generic;
using System.Text;

namespace Assessment_1___BlackJack
{
    class Card
    {
        public string Suit { get; set; }

        public string Type { get; set; }

        public int Value { get; set; }

        public Card (string suit, string type, int value)
        {
            Suit = suit;
            Type = type;
            Value = value;
        }

        public override string ToString()
        {
            return "The " + Type + " of " + Suit + " worth " + Value;
        }
    }
}
