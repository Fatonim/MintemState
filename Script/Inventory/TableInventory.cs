using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public struct AmountItem
{
    public Item item;
    [Range(1, 100)]
    public int Amount;
}

[Serializable]
public struct ListCraft
{
    public List<AmountItem> Materials;
    public Item Result;
}

[Serializable]
public class TableInventory : MonoBehaviour
{
    public List<ListCraft> Crafts;
    public GameObject[] tableSlotsObject;
    public BagInventory bagInv;
    public Text needMaterials;
    public GameObject[] iconMaterials;

    private int tableCraftSelected = -1;

    private void Start()
    {
        for (int i = 0; i < Crafts.Count; i++)
        {
            Instantiate(Crafts[i].Result.icon, tableSlotsObject[i].transform);
        }
    }

    public void OnClickCraftSlot(int index)
    {
        if (tableCraftSelected != index && tableCraftSelected != -1)
        {
            foreach (GameObject iconMat in iconMaterials)
            {
                foreach (Transform iconMatPos in iconMat.transform)
                {
                    Destroy(iconMatPos.gameObject);
                }
            }
        }
        needMaterials.text = "Cost:";
        tableCraftSelected = index;
        for (int i = 0; i < Crafts[index].Materials.Count; i++)
        {
            needMaterials.text += "\n" + bagInv.GetCountItem(Crafts[index].Materials[i].item) + "/" + Crafts[index].Materials[i].Amount;
            Instantiate(Crafts[index].Materials[i].item.icon, iconMaterials[i].transform);
        }
    }

    public void UpdateTable()
    {
        if (tableCraftSelected != -1)
        {
            int index = tableCraftSelected;
            needMaterials.text = "Cost:";
            for (int i = 0; i < Crafts[index].Materials.Count; i++)
            {
                needMaterials.text += "\n" + bagInv.GetCountItem(Crafts[index].Materials[i].item) + "/" + Crafts[index].Materials[i].Amount;
            }
        }
    }

    private bool CanCraft(int index)
    {
        for (int i = 0; i < Crafts[index].Materials.Count; i++)
        {
            if (bagInv.GetCountItem(Crafts[index].Materials[i].item) < Crafts[index].Materials[i].Amount)
                return false;
        }

        return true;
    }

    public void Crafting()
    {
        int index = tableCraftSelected;
        if (index != -1)
        {
            if (CanCraft(index))
            {
                for (int i = 0; i < Crafts[index].Materials.Count; i++)
                {
                    int slotIndex = bagInv.IDSlotWithItem(Crafts[index].Materials[i].item);
                    Slots slot = bagInv.bagSlots[slotIndex];
                    slot.Set_Count(slot.Get_Count() - Crafts[index].Materials[i].Amount);
                    bagInv.Update_countItem_text(slotIndex, slot.Get_Count());
                }
                bagInv.AddItem(Crafts[index].Result, false);
                UpdateTable();
                Debug.Log("123");
            }
            else Debug.Log("не хватает ресурсов");
        }
        else Debug.Log("не выбран слот");
    }
}
