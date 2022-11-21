using UnityEngine;
using UnityEngine.Events;

namespace Items.Inventory
{
    public class InventoryContainer : MonoBehaviour
    {
        [SerializeField] private int inventorySize;
        [field: SerializeField] public InventorySystem Inventory { get; protected set; }
        
        public static UnityAction<InventorySystem> OnDynamicInventoryDisplayReq;

        private void Awake()
        {
            Inventory = new(inventorySize);
        }
    }
}
