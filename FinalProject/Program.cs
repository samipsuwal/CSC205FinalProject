using System;
using System.Collections;
using System.Net.Security;


namespace Cards
{

    class Deck
    {
        private List<Card> cards;
        private const int numberOfCards = 52;
        private Face face;

        public Deck()
        {
            //initialize 52 cards
            initializeDeck();
        }

        public List<Card> getCards()
        {
            return cards;
        }

        private void initializeDeck()
        {
            cards = new List<Card>();

            Card card;

            //initialize faces
            
            Face [] faces = new Face []
            {
                Face.two, Face.three, Face.four, Face.five, Face.six, Face.seven, Face.eight, Face.nine, Face.ten, Face.Jester, Face.Queen, Face.King, Face.Ace
            };

            Suit[] suits = new Suit[]
            {
                Suit.club, Suit.diamond, Suit.hearts, Suit.spades
            };




            for (int faceCounter = 0; faceCounter < 13; faceCounter++)
            {

                for (int suitCounter = 0; suitCounter < 4; suitCounter++)
                {

                    card = new Card(faces[faceCounter], suits[suitCounter]);
                    cards.Add(card);
                }


            }
        }



    }
     class Card
    {
         public Face face { get; set; }
        public Suit suit { get; set; }


        public Card(Face face, Suit suit)
        {

            this.face = face;
            this.suit = suit;
        }


        public override string ToString()
        {
            return "your card is " + face + " of  " + suit;
        }
    }


    enum  Face
    {

        two = 2,
        three =3,
        four=4,
        five=5,
        six =6,
        seven=7,
        eight=8,
        nine=9,
        ten=10,
        Jester=11,
        Queen=12,
        King=13,
        Ace=14

    }

    enum Suit
    {
        diamond,
        spades,
        club,
        hearts
    }

    class Program
    {
        public static void Main(string[] args)
        {

            Deck deckA = new Deck();
            

            foreach (Card card in deckA.getCards())
            {
                Console.WriteLine(card);

            }


        }
    }
  


}

