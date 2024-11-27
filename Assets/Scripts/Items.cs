using UnityEngine;

[CreateAssetMenu(fileName = "Items", menuName = "Scriptable Objects/Items")]


public class Items : ScriptableObject
{
    public enum ItemType
    {
        Coin,
        Exp,
        Potion
    }

    public ItemType itemType;
    public Sprite itemIcon;
    public string itemName;
    public GameObject itemPrefab;
    //public int itemPrice;

}
