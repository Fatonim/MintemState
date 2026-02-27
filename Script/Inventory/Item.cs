using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    never, 
    woodlog,
    plankwood,
    sword,
    axe,
    leaf,
    rope
}

public enum Category
{
    nothing,
    weapon,
    eat,
    block,
}

public enum CategoryOffset
{
    nothing,
    offset0,
    offset45
}

public class Item : MonoBehaviour
{
    public ItemType itemType;
    public GameObject icon;
    public Category category;
    public CategoryOffset categoryOffset;
}
