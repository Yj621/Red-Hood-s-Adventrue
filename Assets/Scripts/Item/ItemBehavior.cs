using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    public Items itemData;
    
    public Items.ItemType GetItemType()
    {
        return itemData.itemType;
    }

    public string GetItemName()
    {
        return itemData.itemName;
    }
}
