using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : ScriptableObject
{
    public int itemID;
    public string itemName;
    [TextArea(5, 10)]
    public string itemDescription;
    public Sprite itemIcon;
    public int itemStacksize;
}
