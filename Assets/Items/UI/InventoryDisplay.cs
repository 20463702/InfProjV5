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
        private MouseItemData MouseInventoryItem;
        protected InventorySystem InvSystem { get; set; }
        protected Dictionary<InventorySlotUI, InventorySlot> SlotDictionary { get; set; }

        protected virtual void Start()
        {
            MouseInventoryItem = GameObject.Find("MouseObject").GetComponent<MouseItemData>();
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
                MouseInventoryItem.assignedSlot.ItemData == null)
            {
                if (isShiftPressed && uiSlot.AssignedSlot.SplitStack(out var halfStackSlot))
                {
                    MouseInventoryItem.UpdateMouseSlot(halfStackSlot);
                    uiSlot.UpdateUISlot();
                }
                else
                {
                    MouseInventoryItem.UpdateMouseSlot(uiSlot.AssignedSlot);
                    uiSlot.ClearSlot();
                }
            }
            else if (uiSlot.AssignedSlot.ItemData == null &&
                     MouseInventoryItem.assignedSlot.ItemData !=
                     null)
            {
                uiSlot.AssignedSlot.AssignItem(MouseInventoryItem.assignedSlot);
                uiSlot.UpdateUISlot();
                MouseInventoryItem.ClearSlot();
            }
            else if (uiSlot.AssignedSlot.ItemData != null &&
                     MouseInventoryItem.assignedSlot.ItemData !=
                     null)
            {
                bool isSameItem = uiSlot.AssignedSlot.ItemData.id == MouseInventoryItem.assignedSlot.ItemData.id;

                if (isSameItem && uiSlot.AssignedSlot.RoomInStack(MouseInventoryItem.assignedSlot.StackSize, out _))
                {
                    uiSlot.AssignedSlot.AssignItem(MouseInventoryItem.assignedSlot);
                    uiSlot.UpdateUISlot();
                    MouseInventoryItem.ClearSlot();
                }
                else if (isSameItem 
                         && !uiSlot.AssignedSlot.RoomInStack(MouseInventoryItem.assignedSlot.StackSize, out var leftInStack))
                {
                    if (leftInStack < 1)
                        SwapSlotWithMouse(uiSlot);
                    else
                    {
                        int remainingOnMouse = MouseInventoryItem.assignedSlot.StackSize - leftInStack;
                        uiSlot.AssignedSlot.AddToStack(leftInStack);
                        uiSlot.UpdateUISlot();

                        InventorySlot newItem = new(MouseInventoryItem.assignedSlot.ItemData, remainingOnMouse);
                        MouseInventoryItem.ClearSlot();
                        MouseInventoryItem.UpdateMouseSlot(newItem);
                    }
                }
                else if (!isSameItem)
                    SwapSlotWithMouse(uiSlot);
            }
        }

        private void SwapSlotWithMouse(InventorySlotUI s)
        {
            InventorySlot clonedSlot = new(MouseInventoryItem.assignedSlot.ItemData,
                MouseInventoryItem.assignedSlot.StackSize);
            MouseInventoryItem.ClearSlot();
            MouseInventoryItem.UpdateMouseSlot(s.AssignedSlot);
            
            s.ClearSlot();
            s.AssignedSlot.AssignItem(clonedSlot);
            s.UpdateUISlot();
        }
    }
}
