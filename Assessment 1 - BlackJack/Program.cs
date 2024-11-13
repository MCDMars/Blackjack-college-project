using System;
using System.Collections.Generic;
using System.Linq;

namespace Assessment_1___BlackJack
{
    class Program
    {
        // Error handling bool
        static bool isValid;

        static void Main(string[] args)
        {
            // Bool for pretty much the entire main program, will stop the program when true
            bool exit = false;
            // Menu selector
            int menuSelector;

            // Bool to check if game is still being played
            bool gameBool = true;

            // Generate a dealer
            Player dealer = new Player("Dealer");


            while (exit != true)
            {
                Console.Clear();
                // Just ASCII art for the menu
                Console.WriteLine("######  #          #     #####  #    #       #    #     #####  #    #");
                Console.WriteLine("#     # #         # #   #     # #   #        #   # #   #     # #   #  ");
                Console.WriteLine("#     # #        #   #  #       #  #         #  #   #  #       #  #  ");
                Console.WriteLine("######  #       #     # #       ###          # #     # #       ###    ");
                Console.WriteLine("#     # #       ####### #       #  #   #     # ####### #       #  #   ");
                Console.WriteLine("#     # #       #     # #     # #   #  #     # #     # #     # #   # ");
                Console.WriteLine("######  ####### #     #  #####  #    #  #####  #     #  #####  #    # ");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("1. Play");
                Console.WriteLine("2. Exit");
                Console.WriteLine();
                Console.Write("What is your selection:  ");

                isValid = int.TryParse(Console.ReadLine(), out menuSelector);

                // I gotta make an error handling class with this method or something, I use it so much
                while (isValid != true || menuSelector < 1 || menuSelector > 2)
                {
                    Console.WriteLine("Invalid choice!");
                    Console.Write("What is your selection:  ");
                    isValid = int.TryParse(Console.ReadLine(), out menuSelector);
                }

                switch (menuSelector)
                {
                    // Play the game
                    case 1:
                        Console.Clear();
                        // Get the players
                        List<Player> playerList = GeneratePlayers();
                        Console.Clear();
                        // Get how many decks to play with
                        List<Card> playingDeck = GetPlayingDeck();
                        
                        while (gameBool)
                        {
                            Console.Clear();
                            gameBool = BlackJack(playerList, playingDeck, dealer);
                        }
                        break;

                    // Exit the game
                    case 2:
                        Console.WriteLine("Goodbye!");
                        Console.ReadKey();
                        exit = true;
                        break;
                }
            }
            
        }

        // It aint pretty, but it does generate 52 card decks quite handily
        static List<Card> AddDeck(int decks)
        {
            List<Card> result = new List<Card>();

            // Adds as many decks as are passed through as a required deck
            for (int d = 0; d < decks; d++)
            {
                for (int i = 0; i < 4; i++)
                {
                    // Adds spades section of deck
                    if (i == 0)
                    {
                        result.Add(new Card("spades", "ace", 11));
                        result.Add(new Card("spades", "two", 2));
                        result.Add(new Card("spades", "three", 3));
                        result.Add(new Card("spades", "four", 4));
                        result.Add(new Card("spades", "five", 5));
                        result.Add(new Card("spades", "six", 6));
                        result.Add(new Card("spades", "seven", 7));
                        result.Add(new Card("spades", "eight", 8));
                        result.Add(new Card("spades", "nine", 9));
                        result.Add(new Card("spades", "ten", 10));
                        result.Add(new Card("spades", "jack", 10));
                        result.Add(new Card("spades", "queen", 10));
                        result.Add(new Card("spades", "king", 10));
                    }
                    if (i == 1)
                    {
                        result.Add(new Card("hearts", "ace", 11));
                        result.Add(new Card("hearts", "two", 2));
                        result.Add(new Card("hearts", "three", 3));
                        result.Add(new Card("hearts", "four", 4));
                        result.Add(new Card("hearts", "five", 5));
                        result.Add(new Card("hearts", "six", 6));
                        result.Add(new Card("hearts", "seven", 7));
                        result.Add(new Card("hearts", "eight", 8));
                        result.Add(new Card("hearts", "nine", 9));
                        result.Add(new Card("hearts", "ten", 10));
                        result.Add(new Card("hearts", "jack", 10));
                        result.Add(new Card("hearts", "queen", 10));
                        result.Add(new Card("hearts", "king", 10));
                    }
                    if (i == 2)
                    {
                        result.Add(new Card("clubs", "ace", 11));
                        result.Add(new Card("clubs", "two", 2));
                        result.Add(new Card("clubs", "three", 3));
                        result.Add(new Card("clubs", "four", 4));
                        result.Add(new Card("clubs", "five", 5));
                        result.Add(new Card("clubs", "six", 6));
                        result.Add(new Card("clubs", "seven", 7));
                        result.Add(new Card("clubs", "eight", 8));
                        result.Add(new Card("clubs", "nine", 9));
                        result.Add(new Card("clubs", "ten", 10));
                        result.Add(new Card("clubs", "jack", 10));
                        result.Add(new Card("clubs", "queen", 10));
                        result.Add(new Card("clubs", "king", 10));
                    }
                    if (i == 3)
                    {
                        result.Add(new Card("diamonds", "ace", 11));
                        result.Add(new Card("diamonds", "two", 2));
                        result.Add(new Card("diamonds", "three", 3));
                        result.Add(new Card("diamonds", "four", 4));
                        result.Add(new Card("diamonds", "five", 5));
                        result.Add(new Card("diamonds", "six", 6));
                        result.Add(new Card("diamonds", "seven", 7));
                        result.Add(new Card("diamonds", "eight", 8));
                        result.Add(new Card("diamonds", "nine", 9));
                        result.Add(new Card("diamonds", "ten", 10));
                        result.Add(new Card("diamonds", "jack", 10));
                        result.Add(new Card("diamonds", "queen", 10));
                        result.Add(new Card("diamonds", "king", 10));
                    }
                }
            }

            return result;
        }

        // Deals next card in the deck to the player
        static void DealCard(int count, Player player, List<Card> deck)
        {
            player.Hand.Add(deck[count]);
        }

        // method to check if a player has gone bust, also changes aces to try to make the player not go bust if at all possible
        static bool BustCheck(Player player)
        {
            // Check for aces (specifically, a card worth 11), change to 1 if an ace is found, subtract 10 from the hand value, and break the loop if this brings the total below 21
            foreach (Card card in player.Hand)
            {
                if (card.Value == 11)
                {
                    card.Value = 1;
                    player.HandValue -= 10;
                    // If an ace is changed, and this has brought the hand below 21 break the foreach loop before messing with more aces, as well as let the player know
                    if (player.HandValue < 21)
                    {
                        Console.WriteLine("Aces were changed!");
                        break;
                    }
                }
            }

            // If changing aces worked, return false. Otherwise, return true
            if (player.HandValue <= 21)
                return false;

            else
            {
                Console.WriteLine("{0}. Bust!", player.HandValue);
                return true;
            }
        }

        // Intro sequence for getting player list
        static List<Player> GeneratePlayers()
        {
            // Bool to see if player is happy with name, and error handling char
            char playerHappy;
            // Player count int
            int playerCount;

            // Result list
            List<Player> result = new List<Player>();

            Console.Write("Hi, thanks for playing! But first, how many of you are there (4 max) :   ");

            isValid = int.TryParse(Console.ReadLine(), out playerCount);

            // I gotta make an error handling class with this method or something, I use it so much
            while (isValid != true || playerCount < 1 || playerCount > 4)
            {
                Console.WriteLine("Invalid choice!");
                Console.Write("How many of you are there (4 max) :   ");
                isValid = int.TryParse(Console.ReadLine(), out playerCount);
            }

            // Fill the list with as many players as playerCount
            for (int i = 0; i < playerCount; i++)
            {
                result.Add(new Player());
            }

            Console.WriteLine("Great! There's {0} of you. Plenty for a quality game.", playerCount);

            // Gather names
            Console.WriteLine("Now, if I could get your names, please.");
            foreach (Player player in result)
            {
                Console.Write("Player {0}, what's your name? :   ", result.IndexOf(player) + 1);
                player.Name = Console.ReadLine();

                Console.Write("So your name is {0}, is that right (y/n)? :   ", player.Name);
                isValid = char.TryParse(Console.ReadLine(), out playerHappy);

                while (isValid != true || playerHappy != 'y' && playerHappy != 'n')
                {
                    Console.WriteLine("Invalid choice!");
                    Console.Write("So your name is {0}, is that right (y/n)? :   ", player.Name);
                    isValid = char.TryParse(Console.ReadLine(), out playerHappy);
                }

                while (playerHappy == 'n')
                {
                    Console.WriteLine("Ok, sorry, we'll try again");
                    Console.Write("Player {0}, what's your name? :   ", result.IndexOf(player) + 1);
                    player.Name = Console.ReadLine();

                    Console.Write("So your name is {0}, is that right (y/n) :   ", player.Name);
                    isValid = char.TryParse(Console.ReadLine(), out playerHappy);

                    while (isValid != true || playerHappy != 'y' && playerHappy != 'n')
                    {
                        Console.WriteLine("Invalid choice!");
                        Console.Write("So your name is {0}, is that right (y/n) :   ", player.Name);
                        isValid = char.TryParse(Console.ReadLine(), out playerHappy);
                    }
                }
            }

            Console.WriteLine("Great! Seems I have all of you now.");
            return result;
        }

        // Get the number of decks to shuffle in and generate a playing deck
        static List<Card> GetPlayingDeck()
        {
            // Number of full 52 card decks to shuffle into playing deck
            int deckCount;

            Console.WriteLine("So now that you're all here, last order of business, how many decks should we shuffle into the playing deck (max 5)? :   ");
            isValid = int.TryParse(Console.ReadLine(), out deckCount);

            while (isValid != true || deckCount < 1 || deckCount > 5)
            {
                Console.WriteLine("Invalid entry!");
                Console.WriteLine("How many decks should we shuffle into the playing deck (max 5)? :   ");
                isValid = int.TryParse(Console.ReadLine(), out deckCount);
            }

            // Add decks to the playing deck
            List<Card> result = AddDeck(deckCount);

            // Flavour text before returning the shuffled list
            Console.WriteLine("Alright then. That's all shuffled up. I reckon we're ready to play!");
            Console.ReadLine();

            return result;
        }

        // Fisher-Yates shuffling algorithm
        public static List<T> Shuffle<T>(List<T> list)
        {
            Random rand = new Random();
            for (int i = 0; i < list.Count; i++)
            {
                int k = rand.Next(0, i);
                T value = list[k];
                list[k] = list[i];
                list[i] = value;
            }
            return list;
        }


        // The bulk of the code, the BlackJack game itself
        static bool BlackJack(List<Player> players, List<Card> deck, Player dealer)
        {
            // First of all, shuffle the deck
            deck = Shuffle(deck);

            // This char decides if the game will reset or end entirely
            char playAgain;
            
            foreach (Player player in players)
            {
                // Reset the turns and going bust
                player.IsTurn = true;
                player.IsBust = false;
                // Clear players hands
                player.Hand.Clear();
                // Reset value of hand
                player.HandValue = 0;
            }

            // Do the same for the dealer
            dealer.IsBust = false;
            dealer.Hand.Clear();
            dealer.HandValue = 0;
            dealer.IsTurn = true;

            // Reset the card count
            int cardCount = 0;

            // Player choice for their turns
            int playerChoice;
            

            // Deals out initial hands
            for (int i = 0; i < 2; i++)
            {
                foreach (Player player in players)
                {
                    DealCard(cardCount, player, deck);
                    player.HandValue += player.Hand.Last().Value;
                    cardCount++;
                }
                DealCard(cardCount, dealer, deck);
                dealer.HandValue += dealer.Hand.Last().Value;
                cardCount++;
            }

            Console.WriteLine("Dealer has ??? and {0}", dealer.Hand[1].ToString());

            foreach (Player player in players)
            {
                Console.WriteLine("{0}, you're up!", player.Name);
                Console.WriteLine("You have {0} and {1}", player.Hand[0].ToString(), player.Hand[1].ToString());

                if (player.HandValue == 21)
                {
                    Console.WriteLine("BlackJack!");
                    player.IsTurn = false;
                }

                // Players turn if no blackjack occurs
                while (player.IsTurn)
                {
                    // Give the choice
                    Console.WriteLine("Your hand is worth {0}", player.HandValue);
                    Console.WriteLine("1.Hit");
                    Console.WriteLine("2.Stick");
                    isValid = int.TryParse(Console.ReadLine(), out playerChoice);

                    // Error handle the choice
                    while (isValid != true || playerChoice > 2 || playerChoice < 1)
                    {
                        Console.WriteLine("Invalid entry, try again");
                        Console.WriteLine("1.Hit");
                        Console.WriteLine("2.Stick");
                        isValid = int.TryParse(Console.ReadLine(), out playerChoice);
                    }

                    // Program the outcome

                    // If hit, deal another card. Will basically deal a card, write out what the card is and what it's worth, then add the card value
                    if (playerChoice == 1)
                    {
                        DealCard(cardCount, player, deck);
                        Console.WriteLine(player.Hand.Last().ToString());
                        player.HandValue += player.Hand.Last().Value;
                        cardCount++;

                        // Also, if hitting causes the hand value to go over 21, check for aces, and if no way to go below 21, go bust and end turn, see method below
                        if (player.HandValue > 21)
                        {
                            player.IsBust = BustCheck(player);

                            // End turn immediately if player has gone bust
                            if (player.IsBust)
                                player.IsTurn = false;
                        }
                    }

                    // Will just end the loop with the current hand
                    if (playerChoice == 2)
                    {
                        player.IsTurn = false;
                    }

                }
            }

            // Reveal the dealers full hand
            Console.WriteLine("Dealers hand revealed");
            Console.WriteLine("Dealer has {0} and {1}", dealer.Hand[0].ToString(), dealer.Hand[1].ToString());

            if (dealer.HandValue == 21)
            {
                Console.WriteLine("BlackJack!");
                dealer.IsTurn = false;
            }

            // Dealers turn
            while (dealer.IsTurn)
            {
                // Reveal the full hand
                Console.WriteLine("Dealers hand revealed");
                Console.WriteLine("Dealer has {0} and {1}", dealer.Hand[0].ToString(), dealer.Hand[1].ToString());
                // Dealer will keep hitting until value is at least 17, then stick
                Console.WriteLine("Dealers hand is worth {0}", dealer.HandValue);

                // If hit, deal another card. Will basically deal a card, write out what the card is and what it's worth, then add the card value
                while (dealer.HandValue < 17)
                {
                    Console.WriteLine("Dealer hits!");
                    DealCard(cardCount, dealer, deck);
                    Console.WriteLine(dealer.Hand.Last().ToString());
                    dealer.HandValue += dealer.Hand.Last().Value;
                    cardCount++;

                    // Also, if hitting causes the hand value to go over 21, check for aces, and if no way to go below 21, go bust and end turn, see method below
                    if (dealer.HandValue > 21)
                    {
                        dealer.IsBust = BustCheck(dealer);

                        // End turn immediately if player has gone bust
                        if (dealer.IsBust)
                            dealer.IsTurn = false;
                    }
                }

                // Will just end the loop with the current hand
                if (dealer.HandValue >= 17 && dealer.IsBust == false)
                {
                    Console.WriteLine("Dealer sticks!");
                    dealer.IsTurn = false;
                }

            }

            // Tally up results, baring in mind, while there's more players, it's each player against the dealer, so if the dealer goes bust after a player goes bust, that's a draw for that player
            Console.WriteLine("Let's see how everyone did...");
            foreach (Player player in players)
            {
                if ((player.IsBust == true && dealer.IsBust == false) || (player.IsBust == false && player.HandValue < dealer.HandValue && dealer.IsBust == false))
                {
                    Console.WriteLine("{0} loses, house wins", player.Name);
                }

                if ((player.IsBust == true && dealer.IsBust == true) || (player.HandValue == dealer.HandValue))
                {
                    Console.WriteLine("Draw!");
                }

                if ((player.IsBust == false && dealer.IsBust == true) || (player.IsBust == false && player.HandValue > dealer.HandValue && dealer.IsBust == false))
                {
                    Console.WriteLine("{0} wins!", player.Name);
                }

            }

            // Check for playing again or not
            Console.Write("Play again? (y/n) :   ");
            isValid = char.TryParse(Console.ReadLine(), out playAgain);

            while (isValid != true || playAgain != 'y' && playAgain != 'n')
            {
                Console.WriteLine("Invalid Entry!");
                Console.Write("Play again? (y/n) :   ");
                isValid = char.TryParse(Console.ReadLine(), out playAgain);
            }

            if (playAgain == 'y')
            {
                Console.WriteLine("Ok! We'll go again. Press any key to continue.");
                Console.ReadKey();
                return true;
            }

            else
            {
                Console.WriteLine("Thanks very much for playing. See you next time! Press any key to continue.");
                Console.ReadKey();
                return false;
            }
        }
    }
}
