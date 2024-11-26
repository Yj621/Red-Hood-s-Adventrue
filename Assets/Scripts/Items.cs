using UnityEngine;

[CreateAssetMenu(fileName = "Items", menuName = "Scriptable Objects/Items")]


public class Items : ScriptableObject
{
    public enum ItemType
    {
        Gold,
        Exp,
        Potion
    }

    public ItemType type;
    public Sprite icon;
    public string itemName;
    public GameObject itemPrefab;
    public int price;

}
