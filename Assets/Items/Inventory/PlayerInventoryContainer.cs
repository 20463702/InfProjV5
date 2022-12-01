using Items.ItemData;
using UnityEngine;
using UnityEngine.Events;

namespace Items.Inventory
{
    public class PlayerInventoryContainer : InventoryContainer
    {
        [SerializeField] protected int secondaryInventorySize;
        [field: SerializeField] public InventorySystem SecondaryInventorySystem { get; private set; }

        public static UnityAction<InventorySystem> OnPlayerInventoryDisplayReq;

        protected override void Awake()
        {
            base.Awake();

            SecondaryInventorySystem = new(secondaryInventorySize);
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.I))
                OnPlayerInventoryDisplayReq?.Invoke(SecondaryInventorySystem);
        }

        public bool AddToInventory(InventoryItemData data, int amount = 1)
        {
            if (InvSys.AddToInventory(data, amount))
                return true;
            else if (SecondaryInventorySystem.AddToInventory(data, amount))
                return true;
            return false;
        }
    }
}
