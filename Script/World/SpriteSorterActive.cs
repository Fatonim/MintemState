using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSorterActive : MonoBehaviour
{
    public float offset;
    public GameObject[] objects;
    public Hand hand;
    private SpriteRenderer spriteRenderer;
    private List<SpriteRenderer> sprites = new List<SpriteRenderer>();

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        foreach (GameObject obj in objects)
        {
            sprites.Add(obj.GetComponent<SpriteRenderer>());
        } 
    }


    private void LateUpdate()
    {
        int sort = -(int)((transform.position.y + offset) * 100);
        spriteRenderer.sortingOrder = sort;
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.sortingOrder = sort;
        }
        if (hand != null && hand.SlotSelected != -1)
            hand.sprite.sortingOrder = sort;
    }
}
