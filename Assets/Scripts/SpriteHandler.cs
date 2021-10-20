using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WarcardProto
{
    public class SpriteHandler : MonoBehaviour
    {
        [Header("Models")]
        public List<Sprite> cardModels;
        public Sprite coverCardPlayer;
        public Sprite coverCardAI;

        [Header("Display card positions")]
        public Transform displayPlayerCard;
        public Transform displayAICard;

        [Header("Animations cards")]
        public float waitAnimationTime = 0.5f;
        public AnimatorHandler animatorHandler;

        /*The list is populated with the cards going from clubs to diamond see Suit enum for ref
        for example jack of hearts will be suit = 1 , rank = 9 
        */

        private Sprite GetCardModel(Suit suit, int rank)
        {
            return cardModels[(int)suit * 13 + rank];
        }

        IEnumerator AnimateCards()
        {
            animatorHandler.FlipCard();
            yield return new WaitForSeconds(waitAnimationTime);
            animatorHandler.gameObject.SetActive(false);
            ChangeCards();
        }


        public void ShowCards()
        {
            StartCoroutine(AnimateCards());
        }

        private void ChangeCards()
        {
            Card playerCard = displayPlayerCard.GetComponentInChildren<Card>();
            Card AICard = displayAICard.GetComponentInChildren<Card>();
            playerCard.ChangeModel(GetCardModel(playerCard.suit, playerCard.rank), true);
            AICard.ChangeModel(GetCardModel(AICard.suit, AICard.rank), true);
        }

        public void ShowCoverCards()
        {
            Card playerCard = displayPlayerCard.GetComponentInChildren<Card>();
            Card AICard = displayAICard.GetComponentInChildren<Card>();
            playerCard.ChangeModel(coverCardPlayer, true);
            AICard.ChangeModel(coverCardAI, true);
        }
    }
}
