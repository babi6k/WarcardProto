using UnityEngine;

namespace WarcardProto
{
    public class InputHandler : MonoBehaviour
    {
        SpriteHandler spriteHandler;

        private void Awake()
        {
            spriteHandler = GetComponent<SpriteHandler>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                spriteHandler.ShowCards();
            }
        }
    }
}
