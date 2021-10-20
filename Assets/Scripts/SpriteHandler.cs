using System.Collections.Generic;
using UnityEngine;

namespace WarcardProto
{
    public class SpriteHandler : MonoBehaviour
    {
        public List<Sprite> cardModels;
        public Card currentPlayerCard;
        public Card currentAICard;
        public Sprite coverCardPlayer;
        public Sprite coverCardAI;
        public Transform displayPlayerCard;
        public Transform displayAICard;

        /*The list is populated with the cards going from clubs to diamond see Suit enum for ref
        for example jack of hearts will be suit = 1 , rank = 9 
        */

        private Sprite GetCardModel(Suit suit, int rank)
        {
            return cardModels[(int)suit * 13 + rank];
        }

        public void ShowCards()
        {
            Card playerCard = displayPlayerCard.GetComponentInChildren<Card>();
            Card AICard = displayAICard.GetComponentInChildren<Card>();
            playerCard.ChangeModel(GetCardModel(playerCard.suit, playerCard.rank), true);
            AICard.ChangeModel(GetCardModel(AICard.suit, AICard.rank), true);
        }

        public void DisableCards()
        {
            currentPlayerCard.ChangeModel(coverCardPlayer, false);
            currentAICard.ChangeModel(coverCardAI, false);
        }

        public void ShowCoverCards()
        {
            currentPlayerCard.ChangeModel(coverCardPlayer, true);
            currentAICard.ChangeModel(coverCardAI, true);
        }
    }
}
