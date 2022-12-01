using System;
using UnityEngine;

namespace Items.Inventory.Chest
{
    [Serializable]
    public struct ChestSaveData
    {
        public InventorySystem inventorySystem;
        public Vector3 position;
        public Quaternion rotation;

        public ChestSaveData(InventorySystem invSys, Vector3 pos, Quaternion rot)
        {
            inventorySystem = invSys;
            position = pos;
            rotation = rot;
        }
    }
}