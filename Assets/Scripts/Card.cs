using System;
using UnityEngine;

namespace WarcardProto
{
    public class Card : MonoBehaviour
    {
        public int rank; // rank can be between 0 - 12 Ace is 12 , two is 0 
        public Suit suit; // suit can be between 0-3  
        SpriteRenderer spriteRenderer;

        private void Awake() 
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void ChangeModel(Sprite sprite, bool isActive)
        {
            spriteRenderer.sprite = sprite;
            gameObject.SetActive(isActive);
        }
    }
}
