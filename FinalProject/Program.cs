using System;
using System.Collections;
using System.Net.Security;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;


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
    class Card:IComparable
    {
        public Face face { get; set; }
        public Suit suit { get; set; }

        public Card(Face face, Suit suit)
        {
            this.face = face;
            this.suit = suit;
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }
            return this.face - (obj as Card).face;
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

            //if the rankings are different then we can simply send the diff
            if (this.ranking != groupOfCards.ranking)
            {
                return this.ranking - groupOfCards.ranking;
            }

            //get max cards, this works for all except Pairs and flush
            //if triplets we just need to find which one is highest
            if (this.ranking == Ranking.Triplets)
            {
                return this.card1.face - groupOfCards.card1.face;
            }

            //for straight flush, straight and flush we need max card

            //if straight flush, find the largest card and match
            if (this.ranking == Ranking.StraightFlush || this.ranking == Ranking.Straight)
            {
                Card firstGroupMaxCard = getMaxCard(this);
                Card secondGroupMaxCard = getMaxCard(groupOfCards);
                return firstGroupMaxCard.CompareTo(secondGroupMaxCard);
            }
            //if its pairs or highcards we might need to check each card
            if (this.ranking == Ranking.HighCard)
            {
                //check highest card first
                if(getMaxCard(this).CompareTo(getMaxCard(groupOfCards)) != 0)
                {
                    //if the max cards don't match
                    return getMaxCard(this).CompareTo(getMaxCard(groupOfCards));

                }

                if (getMidCard(this).CompareTo(getMidCard(groupOfCards)) != 0)
                {
                    //if the max cards don't match
                    return getMidCard(this).CompareTo(getMidCard(groupOfCards));

                }
                
                return getMinCard(this).CompareTo(getMinCard(groupOfCards));
            }
            //pairs
            if (this.ranking == Ranking.Pair)
            {
                //get the card that is a pair
                List<Card> list1 = getPairAndLonerFace(this);
                List<Card> list2 = getPairAndLonerFace(groupOfCards);

                if (list1.ElementAt(0).CompareTo(list2.ElementAt(0)) != 0)
                {
                    return list1.ElementAt(0).CompareTo(list2.ElementAt(0));
                }
                return list1.ElementAt(1).CompareTo(list2.ElementAt(1));
            }

            return this.ranking - groupOfCards.ranking;
        }

        /**
         * Return a List of card where 0 is the one of the pairs, and last index is loner card
         */
        public List<Card> getPairAndLonerFace(GroupOfCards groupOfCards)
        {
            List<Card> list = new List<Card>();

            if (groupOfCards.card1.CompareTo(groupOfCards.card2.face) ==0)
            {
                list.Add(groupOfCards.card1);
                list.Add(groupOfCards.card3);

            }

            if (groupOfCards.card1.CompareTo(groupOfCards.card3.face) == 0)
            {
                list.Add(groupOfCards.card1);
                list.Add(groupOfCards.card2);

            }
            if (groupOfCards.card2.CompareTo(groupOfCards.card3.face) == 0)
            {
                list.Add(groupOfCards.card2);
                list.Add(groupOfCards.card1);

            }


            return list;

        }

        public Card getMaxCard(GroupOfCards groupOfCards)
        {
            Card maxCard = groupOfCards.card1;
            if (maxCard.CompareTo(groupOfCards.card2)<0)
            {
                maxCard = groupOfCards.card2;
            }

            if (maxCard.CompareTo(groupOfCards.card3)< 0)
            {
                maxCard = groupOfCards.card3;
            }

            return maxCard;
        }

        public Card getMinCard(GroupOfCards groupOfCards)
        {
            Card minCard = groupOfCards.card1;
            if (minCard.CompareTo(groupOfCards.card2)> 0)
            {
                minCard = groupOfCards.card2;
            }

            if (minCard.CompareTo(groupOfCards.card3) > 0)
            {
                minCard = groupOfCards.card3;
            }

            return minCard;
        }

        public Card getMidCard(GroupOfCards groupOfCards)
        {
            if (groupOfCards.card1.CompareTo(getMaxCard(groupOfCards)) != 0 && groupOfCards.card1 != getMinCard(groupOfCards))
            {
                return groupOfCards.card1;
            }
            if (groupOfCards.card2.CompareTo(getMaxCard(groupOfCards)) != 0 && groupOfCards.card2 != getMinCard(groupOfCards))
            {
                return groupOfCards.card2;
            }
            return groupOfCards.card3;
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

                listOfGroupOfCards.Add(tempGroupOfCards);
            }


            





        }
    }



}

