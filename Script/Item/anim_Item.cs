using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_Item : MonoBehaviour
{
    public Animator anim;
    public float TimeToFlashing;
    public float lifeTime;

    private float MoveTime;
    private float ItemMoveX;
    private Vector3 posEnd;
    private float ItemMoveY;

    private void Awake()
    {
        ItemMoveX = 3;
        MoveTime = 5;
        ItemMoveY = Random.Range(-7, 7);
        if (ItemMoveX * ItemMoveY > 9)
        {
            float dif = ItemMoveY / 9;
            ItemMoveY /= dif;
            ItemMoveX /= dif;
        }
        posEnd = transform.position + new Vector3(ItemMoveX / 4, ItemMoveY / 4, 0);
        Invoke("DestroyItem", lifeTime);
        Invoke("Flashing", TimeToFlashing);
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, posEnd, MoveTime / 1000f);
    }

    private void DestroyItem()
    {
        Destroy(gameObject);
    }

    private void Flashing()
    {
        anim.SetTrigger("End");
    }
}
