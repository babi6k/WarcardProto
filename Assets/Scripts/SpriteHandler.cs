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
        public float showCardTime = 0.5f;
        public float flipTime = 0.2f;
        public float bankCardTime = 1f;
        public float warCardTime = 0.5f;
        public AnimatorHandler playerAnimatorHandler;
        public AnimatorHandler AIanimatorHandler;

        Card playerCurrentCard;
        Card AICurrentCard;

        /*The list is populated with the cards going from clubs to diamond see Suit enum for ref
        for example jack of hearts will be suit = 1 , rank = 9 
        */

        private Sprite GetCardModel(Suit suit, int rank)
        {
            return cardModels[(int)suit * 13 + rank];
        }
        //Animation with coroutines as everything should be according to time
        IEnumerator AnimateFlip()
        {
            playerAnimatorHandler.FlipCard();
            AIanimatorHandler.FlipCard();
            yield return new WaitForSeconds(showCardTime);
            ResetAnimatorHandlers();
            HideAnimatorHandlers();
            ChangeCards();
        }

        IEnumerator AnimateWar()
        {
            ResetAnimatorHandlers();
            HideAnimatorHandlers();
            playerAnimatorHandler.War();
            AIanimatorHandler.War();
            yield return new WaitForSeconds(warCardTime);
            playerCurrentCard.gameObject.SetActive(false);
            AICurrentCard.gameObject.SetActive(false);
            yield return new WaitForSeconds(warCardTime);
        }

        IEnumerator AnimateCardsBank(bool playerWin)
        {
            playerAnimatorHandler.GoToBank(playerWin);
            AIanimatorHandler.GoToBank(playerWin);
            yield return new WaitForSeconds(flipTime);
            playerCurrentCard.gameObject.SetActive(false);
            AICurrentCard.gameObject.SetActive(false);
            yield return new WaitForSeconds(bankCardTime);
            ResetAnimatorHandlers();
            //HideAnimatorHandlers();
            yield return new WaitForSeconds(bankCardTime);
        }

        private void ResetAnimatorHandlers()
        {
            playerAnimatorHandler.transform.position = displayPlayerCard.position;
            playerAnimatorHandler.transform.rotation = displayPlayerCard.rotation;
            playerAnimatorHandler.transform.localScale = new Vector3(1, 1, 1);
            AIanimatorHandler.transform.position = displayAICard.position;
            AIanimatorHandler.transform.rotation = displayAICard.rotation;
            AIanimatorHandler.transform.localScale = new Vector3(1, 1, 1);
        }

        private void HideAnimatorHandlers()
        {
            playerAnimatorHandler.gameObject.SetActive(false);
            AIanimatorHandler.gameObject.SetActive(false);
        }

        //Getting the child from the card holder and assigning a ref for the card 
        public void UpdateDisplayCards()
        {
            playerCurrentCard = displayPlayerCard.GetComponentInChildren<Card>();
            AICurrentCard = displayAICard.GetComponentInChildren<Card>();
            playerCurrentCard.gameObject.SetActive(true);
            playerCurrentCard.gameObject.SetActive(true);
        }

        public void CardsGoBank(bool playerWin)
        {
            //FlipCard();
            playerCurrentCard.GetComponent<SpriteRenderer>().sprite = coverCardPlayer;
            AICurrentCard.GetComponent<SpriteRenderer>().sprite = coverCardAI;
            StartCoroutine(AnimateCardsBank(playerWin));
        }

        public void War()
        {
            StartCoroutine(AnimateWar());
            ShowCoverCards();
        }

        public void ShowCards()
        {
            StartCoroutine(AnimateFlip());
        }

        private void ChangeCards()
        {
            playerCurrentCard.ChangeModel(GetCardModel(playerCurrentCard.suit, playerCurrentCard.rank), true);
            AICurrentCard.ChangeModel(GetCardModel(AICurrentCard.suit, AICurrentCard.rank), true);
        }

        public void ShowCoverCards()
        {
            playerCurrentCard.ChangeModel(coverCardPlayer, true);
            AICurrentCard.ChangeModel(coverCardAI, true);
        }
    }
}
