using System;
using System.Collections;
using System.Net.Security;
using System.Collections.Generic;


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

            Face[] faces = new Face[]
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
            return "Your card is " + face + " of  " + suit;
        }
    }

    enum Face
    {
        two = 2,
        three = 3,
        four = 4,
        five = 5,
        six = 6,
        seven = 7,
        eight = 8,
        nine = 9,
        ten = 10,
        Jester = 11,
        Queen = 12,
        King = 13,
        Ace = 14
    }

    enum Suit
    {
        diamond,
        spades,
        club,
        hearts
    }

    enum Ranking
    {
        HighCard,
        Pair,
        Flush,
        Straight,
        StraightFlush,
        Triplets //Three of a kind
    }

    class GroupOfCards : IComparable
    {
        Card card1;
        Card card2;
        Card card3;
        Ranking ranking;

        public GroupOfCards(Card card1, Card card2, Card card3)
        {
            this.card1 = card1;
            this.card2 = card2;
            this.card3 = card3;
            figureRanking();
        }

        public void figureRanking()
        {
            if (isItTriple())
            {
                ranking = Ranking.Triplets;
            }
            else if (isItStraighFlush())
            {
                ranking = Ranking.StraightFlush;
            }
            else if (isItStraight())
            {
                ranking = Ranking.Straight;
            }
            else if (isItFlush())
            {
                ranking = Ranking.Flush;
            }
            else
            {
                ranking = Ranking.HighCard;
            }
        }


        public Boolean isItTriple()
        {
            if (card1.face == card2.face && card1.face == card3.face)
            {
                return true;
            }
            return false;
        }

        public Boolean isItStraighFlush()
        {
            if (isItStraight() && isItFlush())
            {
                return true;
            }
            return false;
        }

        public Boolean isItStraight()
        {

            //1->2->3
            if (card1.face + 1 == card2.face && card2.face + 1 == card3.face)
            {
                return true;
            }

            //1->3->2
            if (card1.face + 1 == card3.face && card3.face + 1 == card2.face)
            {
                return true;
            }


            //2->1->3
            if (card2.face + 1 == card1.face && card1.face + 1 == card3.face)
            {
                return true;
            }

            //2->3->1
            if (card2.face + 1 == card3.face && card3.face + 1 == card1.face)
            {
                return true;
            }

            //3->1->2
            if (card3.face + 1 == card1.face && card1.face + 1 == card2.face)
            {
                return true;
            }


            //3->2->1
            if (card3.face + 1 == card2.face && card2.face + 1 == card1.face)
            {
                return true;
            }

            return false;
        }

        public Boolean isItFlush()
        {
            if (card1.suit == card2.suit && card1.suit == card3.suit)
            {
                return true;
            }
            return false;
        }

        public Boolean isItPair()
        {
            //card1 ==card2
            if (card1.face == card2.face)
            {
                return true;
            }

            //card1==card3
            if (card1.face == card3.face)
            {
                return true;
            }

            //card2==card3
            if (card2.face == card2.face)
            {
                return true;
            }
            return false;
        }

        public int CompareTo(Object obj)
        {
            if (obj == null)
            {
                return 1;
            }
            GroupOfCards groupOfCards = obj as GroupOfCards;
            return this.ranking - groupOfCards.ranking;
        }
    }


    class Program
    {
        public static void Main(string[] args)
        {

            Deck deckA = new Deck();


            List<GroupOfCards> listOfGroupOfCards = new List<GroupOfCards>();
            //max of 17 players. 
            for (int i = 0; i < 6-1; i+=3)

            {
                Card card1 = deckA.getCards().ElementAt(i);//make this random
                Card card2 = deckA.getCards().ElementAt(i+1);//make this random
                Card card3 = deckA.getCards().ElementAt(i+2);//make this random
                deckA.getCards().RemoveAt(i);
                deckA.getCards().RemoveAt(i+1);
                deckA.getCards().RemoveAt(i+2);
                GroupOfCards tempGroupOfCards = new GroupOfCards(card1, card2, card3);

                listOfGroupOfCards.add(tempGroupOfCards);
            }


            





        }
    }



}

