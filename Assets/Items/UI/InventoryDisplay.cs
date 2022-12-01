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
        private MouseItemData _mouseInventoryItem;
        protected InventorySystem InvSystem { get; set; }
        protected Dictionary<InventorySlotUI, InventorySlot> SlotDictionary { get; set; }

        protected virtual void Start()
        {
            _mouseInventoryItem = GameObject.Find("MouseObject").GetComponent<MouseItemData>();
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
                _mouseInventoryItem.assignedSlot.ItemData == null)
            {
                if (isShiftPressed && uiSlot.AssignedSlot.SplitStack(out var halfStackSlot))
                {
                    _mouseInventoryItem.UpdateMouseSlot(halfStackSlot);
                    uiSlot.UpdateUISlot();
                }
                else
                {
                    _mouseInventoryItem.UpdateMouseSlot(uiSlot.AssignedSlot);
                    uiSlot.ClearSlot();
                }
            }
            else if (uiSlot.AssignedSlot.ItemData == null &&
                     _mouseInventoryItem.assignedSlot.ItemData !=
                     null)
            {
                uiSlot.AssignedSlot.AssignItem(_mouseInventoryItem.assignedSlot);
                uiSlot.UpdateUISlot();
                _mouseInventoryItem.ClearSlot();
            }
            else if (uiSlot.AssignedSlot.ItemData != null &&
                     _mouseInventoryItem.assignedSlot.ItemData !=
                     null)
            {
                bool isSameItem = uiSlot.AssignedSlot.ItemData.id == _mouseInventoryItem.assignedSlot.ItemData.id;

                if (isSameItem && uiSlot.AssignedSlot.RoomInStack(_mouseInventoryItem.assignedSlot.StackSize, out _))
                {
                    uiSlot.AssignedSlot.AssignItem(_mouseInventoryItem.assignedSlot);
                    uiSlot.UpdateUISlot();
                    _mouseInventoryItem.ClearSlot();
                }
                else if (isSameItem 
                         && !uiSlot.AssignedSlot.RoomInStack(_mouseInventoryItem.assignedSlot.StackSize, out var leftInStack))
                {
                    if (leftInStack < 1)
                        SwapSlotWithMouse(uiSlot);
                    else
                    {
                        int remainingOnMouse = _mouseInventoryItem.assignedSlot.StackSize - leftInStack;
                        uiSlot.AssignedSlot.AddToStack(leftInStack);
                        uiSlot.UpdateUISlot();

                        InventorySlot newItem = new(_mouseInventoryItem.assignedSlot.ItemData, remainingOnMouse);
                        _mouseInventoryItem.ClearSlot();
                        _mouseInventoryItem.UpdateMouseSlot(newItem);
                    }
                }
                else if (!isSameItem)
                    SwapSlotWithMouse(uiSlot);
            }
        }

        private void SwapSlotWithMouse(InventorySlotUI s)
        {
            InventorySlot clonedSlot = new(_mouseInventoryItem.assignedSlot.ItemData,
                _mouseInventoryItem.assignedSlot.StackSize);
            _mouseInventoryItem.ClearSlot();
            _mouseInventoryItem.UpdateMouseSlot(s.AssignedSlot);
            
            s.ClearSlot();
            s.AssignedSlot.AssignItem(clonedSlot);
            s.UpdateUISlot();
        }
    }
}
