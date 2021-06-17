using UnityEngine;
using UnityEditor;
using Mirror;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Player player;
    private InventoryObject inventoryObject;
    public InventoryObject InventoryObject { get { return inventoryObject; } }

    private void Start()
    {
        Debug.Log(player.playerID);
        if (!GetComponent<NetworkIdentity>().isLocalPlayer) return;
        inventoryObject = ScriptableObject.CreateInstance<InventoryObject>();
        Debug.Log("Assets/Scriptable Objects/Inventory/" + player.playerID + " Inventory.asset");
        AssetDatabase.CreateAsset(inventoryObject, "Assets/Scriptable Objects/Inventory/"+player.playerID+" Inventory.asset");
        AssetDatabase.SaveAssets();
        if (inventoryObject != null)
        {
            inventoryObject.ClearInventory();
        }
    }
    private void OnDestroy()
    {
        AssetDatabase.DeleteAsset("Assets/Scriptable Objects/Inventory/" + player.playerID + " Inventory.asset");
    }

}
