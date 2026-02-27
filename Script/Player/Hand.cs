using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private Item hand_item;

    public CameraScript camera;
    public BagInventory bag;
    public hand_attack hand_attack;
    public Player player;

    [HideInInspector] public SpriteRenderer sprite;
    [HideInInspector] public int SlotSelected = -1;

    public void OnClickSlot(int index)
    {
        if (SlotSelected != -1)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
        if (SlotSelected != index)
        {
            if (bag.GetItemInSlotID(index) != null && bag.GetItemInSlotID(index).category != Category.nothing)
            {
                SlotSelected = index;
                Instantiate(bag.GetItemInSlotID(index), transform);
                foreach (Transform child in transform)
                {
                    hand_item = child.GetComponent<Item>();
                    child.GetComponent<BoxCollider2D>().enabled = false;
                    if (child.GetComponent<SpriteRenderer>() != null)
                        sprite = child.GetComponent<SpriteRenderer>();
                    else
                    {
                        foreach (Transform child2 in child)
                        {
                            sprite = child2.GetComponent<SpriteRenderer>();
                        }
                    }
                    if (hand_item.categoryOffset == CategoryOffset.offset0)
                    {
                        GetComponent<rot_Weapons>().offsetAngle = 0;
                    } else if (hand_item.categoryOffset == CategoryOffset.offset45)
                    {
                        GetComponent<rot_Weapons>().offsetAngle = 45;
                    }
                    player.offsetAngle();
                }
            }
            else SlotSelected = -1;
        }
    }

    public void Attack()
    {
        if (SlotSelected != -1 && hand_item.category == Category.weapon)
        {
            if (hand_item.itemType == ItemType.sword)
            {
                hand_item.GetComponent<sword>().camera = camera;
                hand_item.GetComponent<sword>().StartAttack();
            } else if (hand_item.itemType == ItemType.axe)
            {
                hand_item.GetComponent<axe>().camera = camera;
                hand_item.GetComponent<axe>().StartAttack();
            }
        } else
        {
            hand_attack.StartAttack();
        }
    }

    public CameraScript Get_Camera()
    {
        return camera;
    }
}
