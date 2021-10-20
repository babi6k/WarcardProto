﻿using System;
using UnityEngine;

namespace WarcardProto
{
    public class GameHandler : MonoBehaviour
    {
        public DeckHandler player;
        public DeckHandler AI;
        Deck mainDeck;

        SpriteHandler spriteHandler;

        private void Awake() 
        {
            spriteHandler = GetComponent<SpriteHandler>();
            mainDeck= GetComponent<Deck>();
        }

        private void Start() 
        {
            mainDeck.CreateNewDeck();
            mainDeck.Shuffle();
            DealCards();
        }

          void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                PlayTurn();
            }
        }

        private void PlayTurn()
        {
            player.ChangeDisplayCard();
            AI.ChangeDisplayCard();
            spriteHandler.ShowCards();
            CheckWin();
            CheckEmpty();
        }

        private void CheckEmpty()
        {
            if (player.IsEmpty())
            {
                player.EmptyBank();
                if (player.IsEmpty())
                {
                    Debug.Log("Player Lose");
                }
            }

            if (AI.IsEmpty())
            {
                AI.EmptyBank();
                if (AI.IsEmpty())
                {
                    Debug.Log("AI Lose");
                }
            }
        }

        private void DealCards()
        {
            for (int i = 0; i < 26; i++)
            {
                player.currentCards.AddCard(mainDeck.TakeCard());
                AI.currentCards.AddCard(mainDeck.TakeCard());
            }
            mainDeck.EmptyDeck();
        }

        private void CheckWin()
        {
            if (player.displayCard.rank == AI.displayCard.rank)
            {
                War();
            }
            else if (player.displayCard.rank > AI.displayCard.rank)
            {
                Reward(player);
            }
            else
            {
                Reward(AI);
            }
        }

        private void Reward(DeckHandler opponent)
        {
            mainDeck.AddCard(player.displayCard);
            mainDeck.AddCard(AI.displayCard);
            opponent.AddToBank(mainDeck);
            mainDeck.EmptyDeck();
        }

        private void War()
        {
            Debug.Log("Going to War");
        }
    }
}