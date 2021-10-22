using UnityEngine;
using UnityEngine.SceneManagement;

namespace WarcardProto
{
    public class GameHandler : MonoBehaviour
    {
        public DeckHandler player;
        public DeckHandler AI;
        public GameObject fightStart;
        public GameObject fightEffect;
        Deck mainDeck;

        SpriteHandler spriteHandler;
        bool warIsOn = false;
        [HideInInspector]
        public bool someoneWin = false;
        bool playerWin = false;
        [HideInInspector]
        public bool isStartDuel = false;
        bool isPressed = false;
        int countTurns = 0;

        private void Awake()
        {
            spriteHandler = GetComponent<SpriteHandler>();
            mainDeck = GetComponent<Deck>();
        }

        private void Start()
        {
            mainDeck.CreateNewDeck();
            mainDeck.Shuffle();
            DealCards();
        }

        void Update()
        {
            if (Input.GetMouseButtonUp(0) && !isStartDuel && !isPressed)
            {
                isStartDuel = !isStartDuel;
                isPressed = true;
            }

            if (isStartDuel)
            {
                if (!someoneWin && !warIsOn)
                {
                    PlayTurn();
                }
                if (someoneWin)
                {
                    WinState();
                }
                if (warIsOn)
                {
                    WarState();
                }
            }
        }

        private void WarState()
        {
            if (countTurns > 3)
            {
                isPressed = true;
                warIsOn = false;
                return;
            }
            bool isPressed2 = false;
            if (Input.GetMouseButtonDown(0) && !isPressed2)
            {
                PlayWar();
                if (isPressed)
                {
                    isPressed2 = true;
                    countTurns++;
                    fightStart.SetActive(false);
                    fightEffect.SetActive(false);
                }
            }
        }

        private void WinState()
        {
            bool isPressed2 = false;
            if (Input.GetMouseButtonDown(0) && !isPressed2)
            {
                spriteHandler.CardsGoBank(playerWin);
                if (isPressed)
                {
                    isPressed = false;
                    isPressed2 = true;
                }
            }
        }

        private void PlayWar()
        {
            ChangeDisplayCards();
            spriteHandler.War();
            mainDeck.AddCard(player.displayCard);
            mainDeck.AddCard(AI.displayCard);
            CheckEmpty();
        }

        private void PlayTurn()
        {
            ChangeDisplayCards();
            spriteHandler.ShowCards();
            CheckWin();
            CheckEmpty();
        }

        private void ChangeDisplayCards()
        {
            player.ChangeDisplayCard();
            AI.ChangeDisplayCard();
            spriteHandler.UpdateDisplayCards();
        }

        private void CheckEmpty()
        {
            if (player.IsEmpty())
            {
                player.EmptyBank();
                if (player.IsEmpty())
                {
                    WinGame(true);
                }
            }

            if (AI.IsEmpty())
            {
                AI.EmptyBank();
                if (AI.IsEmpty())
                {
                    WinGame(false);
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
                someoneWin = true;
                playerWin = true;
            }
            else
            {
                Reward(AI);
                someoneWin = true;
                playerWin = false;
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
            warIsOn = true;
            countTurns = 0;
            fightStart.SetActive(true);
            fightEffect.SetActive(true);
            fightEffect.GetComponent<ParticleSystem>().Play();
        }

        private void WinDuel()
        {
            someoneWin = true;
            countTurns = 0;
        }


        private void WinGame(bool AIWin)
        {
            if (AIWin)
            {
                Invoke(nameof(LoadAIWin), 1f);
            }
            else
            {
                Invoke(nameof(LoadPlayerWin), 1f);
            }
        }

        private void LoadAIWin()
        {
            SceneManager.LoadScene(1);
        }

        private void LoadPlayerWin()
        {
            SceneManager.LoadScene(2);
        }

    }
}
