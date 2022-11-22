using System.Collections.Generic;
using System.Linq;
using Items.ItemData;
using UnityEngine;
using UnityEngine.Events;

namespace Items.Inventory
{
    [System.Serializable]
    public class InventorySystem
    {
        [field: SerializeField] public List<InventorySlot> Slots { get; private set; }
        public int InventorySize => Slots.Count;
        public UnityAction<InventorySlot> OnInventorySlotChange;

        /// <param name="s">size</param>
        public InventorySystem(int s)
        {
            Slots = new(s);
            for (int i = 0; i < s; i++)
                Slots.Add(new InventorySlot());
        }

        /// <param name="i">item</param>
        /// <param name="a">amount</param>
        /// <returns>success?</returns>
        public bool AddToInventory(InventoryItemData i, int a = 1)
        {
            if (ContainsItem(i, out var invSlots)) //? Does item exist?
            {
                foreach (var s in invSlots)
                {
                    if (s.RoomInStack(a, out _))
                    {
                        s.AddToStack(a);
                        OnInventorySlotChange?.Invoke(s);
                        return true;
                    }
                }
            }
            if (HasFreeSlot(out var freeSlot)) //? Gets first available slot.
            {
                freeSlot.UpdateSlot(i, a);
                OnInventorySlotChange?.Invoke(freeSlot);
                return true;
            }

            return false;
        }

        public bool ContainsItem(InventoryItemData iData, out List<InventorySlot> invSlot)
        {
            invSlot = Slots.Where(i => i.ItemData == iData).ToList();
            return invSlot != null;
        }

        public bool HasFreeSlot(out InventorySlot freeSlot)
        {
            freeSlot = Slots.FirstOrDefault(i => i.ItemData == null);
            return freeSlot != null;
        }
    }
}
