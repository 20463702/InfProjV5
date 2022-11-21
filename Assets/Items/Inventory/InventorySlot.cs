using UnityEngine;

namespace Items.Inventory
{
    [System.Serializable]
    public class InventorySlot
    {
        [field: SerializeField] public InventoryItemData ItemData { get; private set; }
        [field: SerializeField] public int StackSize { get; private set; }

        /// <summary>
        ///     Standard constructor.
        /// </summary>
        /// <param name="iData">Item data</param>
        /// <param name="a">Amount</param>
        public InventorySlot(InventoryItemData iData, int a)
        {
            ItemData = iData;
            StackSize = a;
        }

        /// <summary>Default slot config.</summary>
        public InventorySlot() => ClearSlot();

        public void ClearSlot()
        {
            ItemData = null;
            StackSize = -1;
        }

        public void UpdateSlot(InventoryItemData iData, int a)
        {
            ItemData = iData;
            StackSize = a;
        }
        
        /// <param name="a">Amount to be added to the stack</param>
        /// <param name="r">Remaining stack capacity</param>
        /// <returns>true if amount less than available capacity</returns>
        public bool RoomInStack(int a, out int r)
        {
            r = ItemData.maxStackSize - StackSize;
            return StackSize + a <= ItemData.maxStackSize;
        }

        /// <param name="a">Amount</param>
        public void AddToStack(int a)
        {
            StackSize += a;
        }
        
        /// <param name="a">Amount</param>
        public void RemoveFromStack(int a)
        {
            StackSize -= a;
        }
    }
}
