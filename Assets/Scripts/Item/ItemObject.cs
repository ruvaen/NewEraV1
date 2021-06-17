using UnityEngine;

public enum ItemType { Default, Equipment }
public abstract class ItemObject : ScriptableObject
{
    [SerializeField] protected string itemName;
    [SerializeField] protected  GameObject prefab;
    [SerializeField] protected ItemType itemType;
    [SerializeField] protected string description;

    public string ItemName { get { return itemName; } }
}
