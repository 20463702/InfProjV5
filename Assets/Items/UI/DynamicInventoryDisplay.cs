using System;
using System.Linq;
using Items.Inventory;
using Unity.VisualScripting;
using UnityEngine;

namespace Items.UI
{
    public class DynamicInventoryDisplay : InventoryDisplay
    {
        [SerializeField] protected InventorySlotUI invSlotPrefab;
        
        public void RefreshDynamicInventory(InventorySystem invSys)
        {
            ClearSlots();
            InvSystem = invSys;
            if (InvSystem != null) 
                InvSystem.OnInventorySlotChange += UpdateSlot;
            AssignSlot(invSys);
        }
    
        public override void AssignSlot(InventorySystem invSys)
        {
            SlotDictionary = new();

            if (invSys == null)
                return;

            for (int i = 0, n = invSys.InventorySize; i < n; i++)
            {
                var uiSlot = Instantiate(invSlotPrefab, transform);
                SlotDictionary.Add(uiSlot, invSys.Slots[i]);
                uiSlot.Init(invSys.Slots[i]);
                uiSlot.UpdateUISlot();
            }
        }

        private void ClearSlots()
        {
            foreach (var t in transform.Cast<Transform>())
            {
                Destroy(t.gameObject);
            }
            
            if (SlotDictionary != null)
                SlotDictionary.Clear();
        }

        private void OnDisable()
        {
            if (InvSystem != null)
                InvSystem.OnInventorySlotChange -= UpdateSlot;
        }
    }
}
