using UnityEngine;
using System.Collections.Generic;

namespace WarcardProto
{
    public class Deck : MonoBehaviour
    {
        public List<Card> cards = new List<Card>();
        public Card cardPrefab;
        public Transform cardsParent;

        public void CreateNewDeck()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    Card newCard = Instantiate(cardPrefab);
                    newCard.transform.parent = cardsParent;
                    newCard.suit = (Suit)i;
                    newCard.rank = j;
                    cards.Add(newCard);
                }
            }
        }

        public Card TakeCard()
        {
            Card topCard = cards[0];
            cards.RemoveAt(0);
            return topCard;
        }

        public List<Card> TakeAllCards()
        {
            List<Card> bankCards = new List<Card>();
            foreach (var card in cards)
            {
                bankCards.Add(card);
            }
            cards.Clear();
            return bankCards;
        }

        public void AddCards(List<Card> winningCards)
        {
            foreach (var card in winningCards)
            {
                cards.Add(card);
            }
        }

        public void AddCard(Card newCard)
        {
            cards.Add(newCard);
        }

        public void Shuffle()
        {
            List<Card> shuffledCards = new List<Card>();
            for (int i = 0; i < cards.Count; i++)
            {
                Card currentCard = cards[i];
                int randomNumber = Random.Range(0,cards.Count);
                cards[i] = cards[randomNumber];
                cards[randomNumber] = currentCard;
            }  
        }

        public void EmptyDeck()
        {
            cards.Clear();
        }
    }
}
