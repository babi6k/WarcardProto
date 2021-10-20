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


        /*The list is populated with the cards going from clubs to diamond see Suit enum for ref
        for example jack of hearts will be suit = 1 , rank = 9 
        */

        public Sprite GetCardModel(Suit suit, int rank)
        {
            return cardModels[(int)suit * 13 + rank];
        }

        public void ShowCards()
        {
            currentPlayerCard.ChangeModel(GetCardModel(currentPlayerCard.suit, currentPlayerCard.rank), true);
            currentAICard.ChangeModel(GetCardModel(currentAICard.suit, currentAICard.rank), true);
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
