using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;
    public string playerID;
    [SerializeField] private NetworkIdentity networkIdentity;
    private IInteractable targetInteractable;
    private GameObject collidedTarget;

    private void Update()
    {
        if (!isLocalPlayer) return;
        if (Input.GetKeyDown(KeyCode.E) && targetInteractable!=null && collidedTarget!=null)
        {
            CmdAssignAuthority(collidedTarget);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isLocalPlayer) return;
        if (other.CompareTag("Item"))
        {
            Item _item = other.gameObject.GetComponent<Item>();          
            if (_item != null)
            {
                _item.TriggerEnterOn();
                _item.InventoryObject = playerInventory.InventoryObject;
                targetInteractable = _item;
                collidedTarget = other.gameObject;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!isLocalPlayer) return;
        if (other.CompareTag("Item"))
        {
            Item _item = other.gameObject.GetComponent<Item>();
            if (_item != null)
            {
                _item.TriggerExitOn();
                _item.InventoryObject = null;
                targetInteractable = null;
                collidedTarget = null;
            }
        }
    }
    [TargetRpc]
    private void RpcOnAuthorityAssigned(GameObject _object)
    {
        targetInteractable.Interact();
    }
    [Command]
    private void CmdAssignAuthority(GameObject _object)
    {
        _object.GetComponent<NetworkIdentity>().AssignClientAuthority(networkIdentity.connectionToClient);
        RpcOnAuthorityAssigned(_object);
    }
}
