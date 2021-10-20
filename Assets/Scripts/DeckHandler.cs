using UnityEngine;

namespace WarcardProto
{
    public class DeckHandler : MonoBehaviour
    {
        public Deck currentCards;
        public Deck bankCards;
        public Card displayCard;

        public void AddToBank(Deck wonCards)
        {
            bankCards.AddCards(wonCards.cards);
        }

        public void ChangeDisplayCard()
        {
            displayCard = currentCards.TakeCard();
        }

        public void AddCards(Deck newCards)
        {
            currentCards.AddCards(newCards.cards);
        }
    }
}
