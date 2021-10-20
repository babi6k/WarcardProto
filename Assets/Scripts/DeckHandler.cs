using System;
using UnityEngine;

namespace WarcardProto
{
    public class DeckHandler : MonoBehaviour
    {
        public Deck currentCards;
        public Deck bankCards;
        public Transform displayCardParent;
        public Transform mainDeckCardsHolder;

        [HideInInspector]
        public Card displayCard;

        public void AddToBank(Deck wonCards)
        {
            bankCards.AddCards(wonCards.cards);
        }

        public void ChangeDisplayCard()
        {
            if (displayCard != null)
            {
                for (int i = 0; i < displayCardParent.childCount; i++)
                {
                    displayCardParent.GetChild(i).gameObject.SetActive(false);
                    displayCardParent.GetChild(i).SetParent(mainDeckCardsHolder);
                }
            }
            Card newCard = currentCards.TakeCard();
            newCard.transform.SetParent(displayCardParent);
            displayCard = newCard;
            newCard.transform.position = displayCardParent.position;
        }

        public void AddCards(Deck newCards)
        {
            currentCards.AddCards(newCards.cards);
        }

        public bool IsEmpty()
        {
            return currentCards.cards.Count <= 0;
        }

        public void EmptyBank()
        {
            currentCards.AddCards(bankCards.TakeAllCards());
        }
    }
}
