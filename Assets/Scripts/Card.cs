using System;
using UnityEngine;
using System.Collections.Generic;

namespace WarcardProto
{
    public class Card : MonoBehaviour
    {
        public Suit suit; // suit can be between 0-3  
        public int rank; // rank can be between 0 - 12 Ace is 12 , two is 0 

        SpriteRenderer spriteRenderer;

        public Card(int newSuit, int newRank)
        {
            rank = newRank;
            suit = (Suit)newSuit;
        }

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void ChangeModel(Sprite sprite, bool isActive)
        {
            gameObject.SetActive(isActive);
            spriteRenderer.sprite = sprite;
        }
    }
}
