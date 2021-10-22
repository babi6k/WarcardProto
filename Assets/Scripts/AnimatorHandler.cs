using UnityEngine;

namespace WarcardProto
{
    public class AnimatorHandler : MonoBehaviour
    {
        public ParticleSystem winCardsEffect;

        Animator animator;
        GameHandler gameHandler;

        bool flipFlag = false;
        bool bankFlag = false;
        bool warFlag = false;
        bool playerWinFlag = false;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            gameHandler = FindObjectOfType<GameHandler>();

        }

        private void Update()
        {
            if (flipFlag)
            {
                animator.SetBool("flip", true);
                flipFlag = false;
            }
            else if (bankFlag)
            {
                if (playerWinFlag)
                {
                    animator.SetBool("bankPlayer", true);
                }
                else
                {
                    animator.SetBool("bankAI", true);
                }
                bankFlag = false;
            }
            else if (warFlag)
            {
                animator.SetBool("war", true);
                warFlag = false;
            }

        }

        public void FlipCard()
        {
            gameObject.SetActive(true);
            flipFlag = true;
        }

        public void GoToBank(bool playerWin)
        {
            gameObject.SetActive(true);
            playerWinFlag = playerWin;
            bankFlag = true;
        }

        public void War()
        {
            gameObject.SetActive(true);
            warFlag = true;
        }

        //Animation Event Methods
        public void Hide()
        {
            gameObject.SetActive(false);
        }
        public void FinishDuel()
        {
            gameHandler.isStartDuel = false;
            gameHandler.someoneWin = false;
        }
    }
}
