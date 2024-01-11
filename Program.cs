using System;
using System.Collections.Generic;

namespace UnoNew
{
    class Card
    {
        public static string[] specialcolorcards = 
        {
            "Skip",
            "Reverse",
            "+2",
        };
        public static string[] specialcards = 
        {
            "Wild Card",
            "+4",
        };
        public static string[] colors = 
        {
            "red",
            "blue",
            "green",
            "yellow",
        };
        public string color;
        public string value;
        public Card(string c, string v)
        {
            color = c;
            value = v;
        }

        public static List<Card> BuildDeck()
        {
            List<Card> AllCards = new List<Card>();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    AllCards.Add(new Card(colors[i] ,Convert.ToString(j)));
                }
                for (int j = 0; j < 3; j++)
                {
                    AllCards.Add(new Card(colors[i] ,specialcolorcards[j]));
                }
                for (int j = 1; j < 10; j++)
                {
                    AllCards.Add(new Card(colors[i] ,Convert.ToString(j)));
                }
                for (int j = 0; j < 3; j++)
                {
                    AllCards.Add(new Card(colors[i] ,specialcolorcards[j]));
                }
                for (int j = 0; j < 2; j++)
                {
                    AllCards.Add(new Card("", specialcards[j]));
                }
            }
            return AllCards;
        }
        
        public static List<Card> ShuffleCard(List<Card> AllCards)
        {
            List<Card> ShuffledDeck = new List<Card>();
            Random numGen = new Random();
            int totalCards = AllCards.Count;

            while (ShuffledDeck.Count < totalCards)
            {
                int addedCard = numGen.Next(0, AllCards.Count - 1);
                ShuffledDeck.Add(AllCards[addedCard]);
                AllCards.RemoveAt(addedCard);
            }
            return ShuffledDeck;
        }
        
        public static string PrintCard(Card card)
        {
            return (card.value + ", " + card.color);
        }
        
        public static string PrintCard(string prefix, Card card)
        {
            return (prefix + card.value + ", " + card.color);
        }
        
        public static string PrintCard(Card card, string suffix)
        {
            return (card.value + ", " + card.color + suffix);
        }
        
        public static string PrintCard(string prefix, Card card, string suffix)
        {
            return (prefix + card.value + ", " + card.color + suffix);
        }

        public static bool doCardsFit(Card card1, Card card2)
        {
            return (card1.color == card2.color) || (card1.value == card2.value);
        }

        public static bool isSpecial(Card card)
        {
            bool foundSpecial = false;
            for (int i = 0; i < specialcolorcards.Length; i++)
            {
                if (specialcolorcards[i] == card.value)
                {
                    foundSpecial = true;
                }
            }
            for (int i = 0; i < specialcards.Length; i++)
            {
                if (specialcards[i] == card.value)
                {
                    foundSpecial = true;
                }
            }
            return foundSpecial;
        }
        public static bool validColor(Card card)
        {
            return (card.color == colors[0]) || (card.color == colors[1]) || (card.color == colors[2]) || (card.color == colors[3]);
        }
        public static void ChangeColors(Card card)
        {
            if (card.color == "Red")
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (card.color == "Yellow")
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else if (card.color == "Green")
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (card.color == "Blue")
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public static void ResetColors()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static bool isNoColor(Card card)
        {
            return ((card.value == specialcards[0]) || (card.value == specialcards[1]));
        }
    }
    class Player
    {
        public string name;
        public List<Card> hand = new List<Card>();
        public Player(string n, List<Card> h)
        {
            name = n;
            hand = h;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int selected;
            int settingsselected;
                
            string[] mainmenu = 
            {
                "Play",
                "Settings",
                "Rules",
                "Cards",
                "Close",
            };
                
            string[] settingsmenu =
            {
                "Starting Cards",
                "Players",
                "Time Limit",
            };
                
            string[] settingsstartstats =
            {
                "7",
                "4",
                "Coming Soon",
            };
            
            string[] settingsstats =
            {
                settingsstartstats[0],
                settingsstartstats[1],
                settingsstartstats[2],
            };
            bool cont = true;
            while (cont == true)
            {
                Console.WriteLine("Welcome to Uno:");
                for (int i = 0; i < mainmenu.Length; i++)
                {
                    Console.WriteLine((i + 1) + ". " + mainmenu[i]);
                }
                selected = Convert.ToInt32(Console.ReadLine()) - 1;

                if (selected == 0) //selected == "Play"
                {
                    Console.WriteLine("Creating Game. . .");
                    
                    List<Player> players = new List<Player>();
                    Console.WriteLine("Asking Names for " + settingsstats[1] + " players.");
                    
                    for (int i = 0; i < Convert.ToInt32(settingsstats[1]); i++)
                    {
                        Console.WriteLine("Name of " + (i + 1) + ". player:");
                        List<Card> testCards = new List<Card>();
                        players.Add(new Player(Console.ReadLine(), testCards));
                    }

                    Console.WriteLine("Shuffling Deck. . .");

                    List<Card> cardDeck = Card.ShuffleCard(Card.BuildDeck()); 
                    
                    Console.WriteLine("Done!\nPassing out Cards. . .");

                    for (int k = 0; k < Convert.ToInt32(settingsstats[0]); k++)
                    {
                        for (int i = 0; i < Convert.ToInt32(settingsstats[1]); i++)
                        {
                            players[i].hand.Add(cardDeck[0]);
                            cardDeck.Remove(cardDeck[0]);
                        }
                    }
                    Console.WriteLine("Done!");
                    
                    List<Card> discardPile = new List<Card>();   
                    discardPile.Add(cardDeck[0]);
                    cardDeck.Remove(cardDeck[0]);
                    
                    while (Card.isSpecial(discardPile[discardPile.Count - 1]))
                    {
                        discardPile.Add(cardDeck[0]);
                        cardDeck.Remove(cardDeck[0]);
                        Console.WriteLine("There was a special card on the discard pile. . .");
                    }
                    
                    int j = 0;
                    bool playerWith0Cards = false;
                    while (playerWith0Cards == false)
                    {
                        
                        if (j >= players.Count)
                        {
                            j -= players.Count;
                        }

                        Console.WriteLine("The card on the discard pile is " + Card.PrintCard(discardPile[discardPile.Count - 1]));
                        Console.WriteLine("It is " + players[j].name + "'s Turn");
                        
                        bool successMove = false;

                        if (Card.isSpecial(discardPile[discardPile.Count - 1])) //Checks if Card is special
                        {
                            if (discardPile[discardPile.Count - 1].value == "Skip") //if "Skip Card"
                            {
                                Console.WriteLine(players[j].name + " needs to skip this round");
                                successMove = true;
                            }

                            if (discardPile[discardPile.Count - 1].value == "+2") //if "+2"
                            {
                                Console.WriteLine(players[j].name + " needs to draw 2 cards");
                                players[j].hand.Add(cardDeck[0]);
                                cardDeck.Remove(cardDeck[0]);
                                players[j].hand.Add(cardDeck[0]);
                                cardDeck.Remove(cardDeck[0]);
                            }
                            if (discardPile[discardPile.Count - 1].value == "+4") //if "+4"
                            {
                                Console.WriteLine(players[j].name + " needs to draw 4 cards");
                                players[j].hand.Add(cardDeck[0]);
                                cardDeck.Remove(cardDeck[0]);
                                players[j].hand.Add(cardDeck[0]);
                                cardDeck.Remove(cardDeck[0]);
                                players[j].hand.Add(cardDeck[0]);
                                cardDeck.Remove(cardDeck[0]);
                                players[j].hand.Add(cardDeck[0]);
                                cardDeck.Remove(cardDeck[0]);
                            }
                        }

                        while (successMove == false)
                        {
                            Console.WriteLine("Actions:");
                            for (int i = 0; i < players[j].hand.Count; i++)
                            {
                                Console.WriteLine((i + 1) + " - " + Card.PrintCard(players[j].hand[i]));
                            }
                            Console.WriteLine((players[j].hand.Count + 1) + " - Draw Card");
                            
                            int selectedMove = Convert.ToInt32(Console.ReadLine()) - 1;
                            if (selectedMove == players[j].hand.Count) //selectedMove == "Draw Card"
                            {
                                Console.WriteLine("You chose: Draw Card");
                                Console.WriteLine("You drew: " + Card.PrintCard(cardDeck[0]));
                                players[j].hand.Add(cardDeck[0]);
                                cardDeck.Remove(cardDeck[0]);
                                if (Card.doCardsFit(players[j].hand[players[j].hand.Count - 1], discardPile[discardPile.Count - 1]))
                                {
                                    Console.WriteLine("This Card is playable. Do you want to play it? (yes/no)");
                                    if (Console.ReadLine() == "yes")
                                    {
                                        discardPile.Add(players[j].hand[players[j].hand.Count - 1]);
                                        players[j].hand.Remove(players[j].hand[players[j].hand.Count - 1]);
                                    }
                                }
                                successMove = true;
                            }
                            else //selectedMove == "Play Card"
                            {
                                Console.WriteLine("You chose: " + Card.PrintCard(players[j].hand[selectedMove]));
                                
                                if (Card.doCardsFit(players[j].hand[selectedMove], discardPile[discardPile.Count - 1]) || Card.isNoColor(players[j].hand[selectedMove])) //continue here
                                {
                                    Console.WriteLine("This move fits");     
                                    
                                    discardPile.Add(players[j].hand[selectedMove]);
                                    players[j].hand.Remove(players[j].hand[selectedMove]);
                                    successMove = true;
                                }
                                
                                else
                                {
                                    Console.WriteLine("Your card can't be played.");
                                }
                            } //End of Move

                            if (players[j].hand.Count == 1) //if player has 1 Card left
                            {
                                Console.WriteLine(players[j].name + ": Uno!");
                            }
                            else if (players[j].hand.Count == 0) //if player has 0 Cards left
                            {
                                Console.WriteLine(players[j].name + "Uno, Uno!");
                                Console.WriteLine("The Winner is " + players[j].name + "!");
                                playerWith0Cards = true;
                            }
                        } //End of Turn-While
                        j++;

                        if (playerWith0Cards == false)
                        {
                            if (Card.isNoColor(discardPile[discardPile.Count - 1]))
                            {
                                if (discardPile[discardPile.Count - 1].value == "Wild Card")
                                {
                                    bool validColor = false;
                                    while (validColor)
                                    {
                                        Console.WriteLine("What Color do you choose:");
                                        validColor = Card.validColor(discardPile[discardPile.Count - 1]);
                                        if (validColor)
                                        {
                                            discardPile[discardPile.Count - 1].color = Console.ReadLine();
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid color. Try again:");
                                        }
                                    }
                                }
                                else if (discardPile[discardPile.Count - 1].value == "+4")
                                {
                                    bool validColor = false;
                                    while (validColor)
                                    {
                                        Console.WriteLine("What Color do you choose:");
                                        validColor = Card.validColor(discardPile[discardPile.Count - 1]);
                                        if (validColor)
                                        {
                                            discardPile[discardPile.Count - 1].color = Console.ReadLine();
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid color. Try again:");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                
                else if (selected == 1) //selected == "Settings"
                {
                    bool leavesettings = false;
                    while (leavesettings == false)
                    {
                        Console.WriteLine("Settings:");
                        for (int i = 0; i < settingsmenu.Length; i++)
                        {
                            Console.WriteLine((i + 1) + ". " + settingsmenu[i] + " - " + settingsstats[i]);
                        }
                        Console.WriteLine("4. Back to Main Menu");
                        Console.WriteLine("Choose any number to change:");
                        settingsselected = Convert.ToInt32(Console.ReadLine()) - 1;

                        if (settingsselected == 0) //settingsselected == "Starting Cards"
                        {
                            bool acceptednewstartingcard = false;
                            while (acceptednewstartingcard == false)
                            {
                                Console.WriteLine("Choose new Starting Cards:");
                                int newstartingcards = Convert.ToInt32(Console.ReadLine());
                                if (newstartingcards * Convert.ToInt32(settingsstats[1]) > 64)
                                {
                                    Console.WriteLine("Not enough cards for every player. Please choose lower amount of cards or players.");
                                    acceptednewstartingcard = false;
                                }
                                
                                else
                                {
                                    Console.WriteLine("New amount of Starting Cards is: " + newstartingcards);
                                    settingsstats[0] = Convert.ToString(newstartingcards);
                                    acceptednewstartingcard = true;
                                }
                            }
                            selected = 1;
                        }
                        else if (settingsselected == 1) //settingsselected == "Players"
                        {
                            bool acceptednewplayers = false;
                            while (acceptednewplayers == false)
                            {
                                Console.WriteLine("Choose new Players:");
                                int newplayers = Convert.ToInt32(Console.ReadLine());
                                
                                if (newplayers * Convert.ToInt32(settingsstats[0]) > 64)
                                {
                                    Console.WriteLine("Not enough cards for every player. Please choose lower amount of cards or players.");
                                    acceptednewplayers = false;
                                }
                                
                                else
                                {
                                    Console.WriteLine("New amount of Players is: " + newplayers);
                                    settingsstats[1] = Convert.ToString(newplayers);
                                    acceptednewplayers = true;
                                }
                            }
                            selected = 1;
                        }
                        else if (settingsselected == 2) //settingsselected == "Time Limit"
                        {
                            Console.WriteLine("Coming Soon!");
                            selected = 1;
                        }
                        
                        else if (settingsselected == 3) //settingsselected == "Back to Main Menu"
                        {
                            leavesettings = true;
                        }
                    }
                }
                
                else if (selected == 2) //selected == "Rules"
                {
                    Console.WriteLine("Rules:");
                    Console.WriteLine("\nEvery Player starts with " + settingsstats[0] + " cards.\nThe first card of the Deck will be the Starting Card.\nA random player will be selected the starting player.\nThis player can then choose to either play a card on the discard pile or drraw a card from the deck.\nThe card that you want to play must either match the color or the number of the card on the discard pile.\nCheck out the Cards Menu for the function of all the other cards.\nIf the deck has no cards left, all cards on the discard pile except for the last one get turned around and make up a new deck.\nThe player that has played all their cards first, wins.");
                }
                
                else if (selected == 3) //selected == "Cards"
                {
                    Console.WriteLine("Cards:");
                    //Number Cards
                    Console.WriteLine("\nNumber Cards:\nNumber Cards are Cards that have a number and a color. You can play these on every card with the same number or color.\n");

                    //Skip Cards
                    Console.WriteLine("Skip Cards:\nSkip Cards lets the next player skip his turn.\nIt can be played on the same color or on another Skip Card.\n");

                    //Reverse Cards
                    Console.WriteLine("Reverse Cards:\nReverse Cards basically turn around the direction of how the game gets played,\nIt can be placed on the same color or on another Reverse Card.\n");

                    //Switch Cards
                    Console.WriteLine("Switch Card:\nSwitch your cards with a different player.\nCan be placed on the same color or on another Reverse Card.\n");

                    //+2 Cards
                    Console.WriteLine("+2 Cards:\n+2 Cards make, that the next player needs to draw 2 cards.\nThey can be placed on the same color and on +2 or +4 Cards.\n");

                    //+4 Cards
                    Console.WriteLine("+4 Cards:\n+4 Cards make, that the next player needs to draw 4 cards.\nYou can also choose a new color.\nIf placed on another +2 or +4 Card, add the amount off cards drawn together.\nThey can be placed on all colors and on +2 and +4 Cards.\n");

                    //WildCard
                    Console.WriteLine("Wild Cards:\nWild Cards let you choose the next color to be played.\nIt can be placed on any color but not on other Wild Cards or +4 Cards.\n");
                }
                
                else if (selected == 4) //selected == "Close"
                {
                    cont = false;
                }
            }    
        }
        static string GetStringInput(string prefix)
        {
            Console.WriteLine(prefix);
            return Console.ReadLine();
        }
    }
}