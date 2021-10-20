using UnityEngine;

namespace WarcardProto
{
    public class AnimatorHandler : MonoBehaviour
    {
        Animator animator;

        private void Awake() 
        {
            animator = GetComponent<Animator>();
        }

        public void FlipCard()
        {
            gameObject.SetActive(true);
            animator.SetBool("flip", true);
        }
    }
}
