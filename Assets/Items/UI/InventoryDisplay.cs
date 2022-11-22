using System;
using System.Collections.Generic;
using Items.Inventory;
using Items.ItemData;
using UnityEngine;

namespace Items.UI
{
    public abstract class InventoryDisplay : MonoBehaviour
    {
        protected MouseItemData MouseInventoryItem;
        public InventorySystem InvSystem { get; protected set; }
        public Dictionary<InventorySlotUI, InventorySlot> SlotDictionary { get; protected set; }

        protected virtual void Start() {}

        public abstract void AssignSlot(InventorySystem invSys);

        protected virtual void UpdateSlot(InventorySlot updatedSlot)
        {
            foreach (var s in SlotDictionary)
                if (s.Value == updatedSlot)
                    s.Key.UpdateUISlot(updatedSlot);
        }

        public void SlotClicked(InventorySlotUI clickedSlot)
        {
            Debug.Log("click");
        }
    }
}
