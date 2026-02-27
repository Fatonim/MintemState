using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slots
{
    Item item;
    int count;

    public Slots(Item item, int count)
    {
        this.item = item;
        this.count = count;
    }

    public Item Get_Item()
    {
        return item;
    }
    public void Count_Update()
    {
        count++;
    }
    public int Get_Count()
    {
        return count;
    }
    public void Set_Count(int value)
    {
        count = value;
    }
    public void Set_Item(Item item)
    {
        this.item = item;
    }
}


public class BagInventory : MonoBehaviour
{
    public GameObject[] bagSlotsObject;
    public Text[] countItem_text;
    public TableInventory tableInv;

    public List<Slots> bagSlots = new List<Slots>();

    public void AddItem(Item item, bool delete)
    {
        int index = 0;
        int empty_id = -1;
        foreach (Slots slot in bagSlots)
        {
            if (empty_id == -1)
            {
                if (slot.Get_Item().itemType == ItemType.never)
                {
                    empty_id = index;
                }
            }
            if (item.itemType == slot.Get_Item().itemType)
            {
                slot.Count_Update();
                Update_countItem_text(index, slot.Get_Count());
                if (delete) Destroy(item.gameObject);
                tableInv.UpdateTable();
                return;
            }
            index++;
        }
        if (empty_id != -1)
        {
            bagSlots[empty_id].Set_Item(item);
            bagSlots[empty_id].Set_Count(1);
            Update_countItem_text(empty_id, 1);
            Instantiate(item.icon, bagSlotsObject[empty_id].transform);
            if (delete) Destroy(item.gameObject);
            tableInv.UpdateTable();
            return;
        }
        bagSlots.Add(new Slots(item, 1));
        Update_countItem_text(index, 1);
        Instantiate(item.icon, bagSlotsObject[bagSlots.Count - 1].transform);
        if (delete) Destroy(item.gameObject);
        tableInv.UpdateTable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Item")
        {
            AddItem(other.GetComponent<Item>(), true);
        }
    }

    public void Update_countItem_text(int index, int count)
    {
        if (count == 0)
        {
            bagSlots[index].Get_Item().itemType = ItemType.never;
            Icon iconObj = bagSlotsObject[index].GetComponentInChildren<Icon>();
            Destroy(iconObj.gameObject);
            countItem_text[index].text = "";
        }
        else countItem_text[index].text = "" + count;
    }

    public int GetCountItem(Item item)
    {
        int count = 0;
        foreach (Slots slot in bagSlots)
        {
            if (item.itemType == slot.Get_Item().itemType)
            {
                count = slot.Get_Count();
                break;
            }
        }
        return count;
    }

    public int IDSlotWithItem(Item item)
    {
        for (int i = 0; i < bagSlots.Count; i++)
        {
            if (item.itemType == bagSlots[i].Get_Item().itemType)
            {
                return i;
            }
        }
        return 0;
    }

    public Item GetItemInSlotID(int index)
    {
        foreach (Transform child in bagSlotsObject[index].transform)
        {
            return child.GetComponent<Icon>().item;
        }
        return null;
    }
}
