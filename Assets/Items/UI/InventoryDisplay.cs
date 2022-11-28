using System;
using System.Collections.Generic;
using Items.Inventory;
using Items.ItemData;
using JetBrains.Annotations;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace Items.UI
{
    public abstract class InventoryDisplay : MonoBehaviour
    {
        protected MouseItemData mouseInventoryItem;
        public InventorySystem InvSystem { get; protected set; }
        public Dictionary<InventorySlotUI, InventorySlot> SlotDictionary { get; protected set; }

        protected virtual void Start()
        {
            mouseInventoryItem = GameObject.Find("MouseObject").GetComponent<MouseItemData>();
        }

        public abstract void AssignSlot(InventorySystem invSys);

        protected void UpdateSlot(InventorySlot updatedSlot)
        {
            foreach (var s in SlotDictionary)
                if (s.Value == updatedSlot)
                    s.Key.UpdateUISlot(updatedSlot);
        }

        public void SlotClicked(InventorySlotUI uiSlot)
        {
            bool isShiftPressed = Input.GetKey(KeyCode.LeftShift);
            
            if (uiSlot.AssignedSlot.ItemData != null &&
                mouseInventoryItem.assignedSlot.ItemData == null)
            {
                if (isShiftPressed && uiSlot.AssignedSlot.SplitStack(out var halfStackSlot))
                {
                    mouseInventoryItem.UpdateMouseSlot(halfStackSlot);
                    uiSlot.UpdateUISlot();
                }
                else
                {
                    mouseInventoryItem.UpdateMouseSlot(uiSlot.AssignedSlot);
                    uiSlot.ClearSlot();
                }
            }
            else if (uiSlot.AssignedSlot.ItemData == null &&
                     mouseInventoryItem.assignedSlot.ItemData !=
                     null)
            {
                uiSlot.AssignedSlot.AssignItem(mouseInventoryItem.assignedSlot);
                uiSlot.UpdateUISlot();
                mouseInventoryItem.ClearSlot();
            }
            else if (uiSlot.AssignedSlot.ItemData != null &&
                     mouseInventoryItem.assignedSlot.ItemData !=
                     null)
            {
                bool isSameItem = uiSlot.AssignedSlot.ItemData.id == mouseInventoryItem.assignedSlot.ItemData.id;

                if (isSameItem && uiSlot.AssignedSlot.RoomInStack(mouseInventoryItem.assignedSlot.StackSize, out _))
                {
                    uiSlot.AssignedSlot.AssignItem(mouseInventoryItem.assignedSlot);
                    uiSlot.UpdateUISlot();
                    mouseInventoryItem.ClearSlot();
                }
                else if (isSameItem 
                         && !uiSlot.AssignedSlot.RoomInStack(mouseInventoryItem.assignedSlot.StackSize, out var leftInStack))
                {
                    if (leftInStack < 1)
                        SwapSlotWithMouse(uiSlot);
                    else
                    {
                        int remainingOnMouse = mouseInventoryItem.assignedSlot.StackSize - leftInStack;
                        uiSlot.AssignedSlot.AddToStack(leftInStack);
                        uiSlot.UpdateUISlot();

                        InventorySlot newItem = new(mouseInventoryItem.assignedSlot.ItemData, remainingOnMouse);
                        mouseInventoryItem.ClearSlot();
                        mouseInventoryItem.UpdateMouseSlot(newItem);
                    }
                }
                else if (!isSameItem)
                    SwapSlotWithMouse(uiSlot);
            }
        }

        private void SwapSlotWithMouse(InventorySlotUI s)
        {
            InventorySlot clonedSlot = new(mouseInventoryItem.assignedSlot.ItemData,
                mouseInventoryItem.assignedSlot.StackSize);
            mouseInventoryItem.ClearSlot();
            mouseInventoryItem.UpdateMouseSlot(s.AssignedSlot);
            
            s.ClearSlot();
            s.AssignedSlot.AssignItem(clonedSlot);
            s.UpdateUISlot();
        }
    }
}
