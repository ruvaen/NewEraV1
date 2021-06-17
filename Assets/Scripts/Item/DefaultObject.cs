using UnityEngine;

[CreateAssetMenu(fileName = "New Default Item Object", menuName = "Inventory System/Items/Default")]
public class DefaultObject : ItemObject
{
    private void Awake()
    {
        itemType = ItemType.Default;
    }
}
