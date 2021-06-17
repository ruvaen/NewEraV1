using UnityEngine;
using Mirror;

public class Item : NetworkBehaviour, IInteractable
{
    [SerializeField] private ItemObject itemObject;
    [SerializeField] private int amount;
    private InventoryObject inventoryObject;
    private bool debug;
    public ItemObject ItemObject { get { return itemObject; } }
    public InventoryObject InventoryObject { set { inventoryObject = value; } }
    private void Update()
    {
        debug = hasAuthority;
    }
    public void Interact()
    {
        Debug.Log("Interacting with " + itemObject.ItemName);
        if (inventoryObject != null)
        {
            inventoryObject.AddItem(itemObject, amount);
            CmdDestroy();
        }
    }
    public void TriggerEnterOn()
    {
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }
    public void TriggerExitOn()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
    #region Server
    [Command]
    private void CmdDestroy()
    {
        Destroy(this.gameObject);
    }
    #endregion

}
