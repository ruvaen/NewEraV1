using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Object", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    [SerializeField] private List<InventorySlot> inventory = new List<InventorySlot>();
    public void AddItem(ItemObject _item, int _amount)
    {
        bool _hasItem = false;
        foreach (InventorySlot _inventorySlot in inventory)
        {
            if (_inventorySlot.Item == _item)
            {
                _inventorySlot.AddAmount(_amount);
                _hasItem = true;
                break;
            }
        }
        if (!_hasItem)
        {
            inventory.Add(new InventorySlot(_item, _amount));
        }
    }
    public void ClearInventory()
    {
        inventory.Clear();
    }

}


[System.Serializable]
public class InventorySlot
{
    private ItemObject item;
    private int amount;
    public float Amount { get { return amount; } }
    public ItemObject Item { get { return item; } }
    public InventorySlot(ItemObject _item,int _amount)
    {
        item = _item;
        amount = _amount;
    }
    public void AddAmount(int _amount)
    {
        amount += _amount;
    }
}
