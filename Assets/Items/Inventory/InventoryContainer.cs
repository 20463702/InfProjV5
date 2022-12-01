using Items.ItemData;
using UnityEngine;
using UnityEngine.Events;

namespace Items.Inventory
{
    public class InventoryContainer : MonoBehaviour
    {
        [SerializeField] private int inventorySize;
        [field: SerializeField] public InventorySystem InvSys { get; protected set; }
        
        public static UnityAction<InventorySystem> OnDynamicInventoryDisplayReq;

        protected virtual void Awake()
        {
            InvSys = new(inventorySize);
        }
    }
}
