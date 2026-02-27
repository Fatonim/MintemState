using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct ItemDrops
{
    public GameObject item;
    [Range(1, 100)]
    public int Amount;
}

[Serializable]
public class DropResources : MonoBehaviour
{
    public List<ItemDrops> Resource;

    public void ResourcesDrop()
    {
        foreach(ItemDrops res in Resource)
        {
            for (int i = 0; i < res.Amount; i++)
            {
                Instantiate(res.item, new Vector3 (transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            }
        }
    }
}
