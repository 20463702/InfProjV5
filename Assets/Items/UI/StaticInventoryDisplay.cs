using System.Collections.Generic;
using Characters.PlayerCharacter;
using Items.Inventory;
using UnityEngine;
using UnityEngine.Serialization;

namespace Items.UI
{
    public class StaticInventoryDisplay : InventoryDisplay
    {
        [SerializeField] private InventoryContainer inventoryContainer;
        [SerializeField] private List<InventorySlotUI> slots;
        
        protected override void Start()
        {
            base.Start();

            InvSystem = inventoryContainer.InvSys;
            InvSystem.OnInventorySlotChange += UpdateSlot;
            
            AssignSlot(InvSystem);
        }
        
        public override void AssignSlot(InventorySystem invSys)
        {
            SlotDictionary = new Dictionary<InventorySlotUI, InventorySlot>();

            for (int i = 0, n = InvSystem.InventorySize; i < n; i++)
            {
                SlotDictionary.Add(slots[i], InvSystem.Slots[i]);
                slots[i].Init(InvSystem.Slots[i]);
            }
        }
    }
}
