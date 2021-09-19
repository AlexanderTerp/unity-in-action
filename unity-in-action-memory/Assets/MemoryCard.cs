using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{

    [SerializeField] private GameObject cardBack;
    [SerializeField] private SpriteRenderer cardSymbolRenderer;
    [SerializeField] private SceneController controller;

    public int Id { get; private set; }

    public void SetCard(int id, Sprite image)
    {
        Id = id;
        cardSymbolRenderer.sprite = image;
    }

    public void OnMouseDown()
    {
        if (cardBack.activeSelf)
        {
            cardBack.SetActive(false);
        }
    }
}
