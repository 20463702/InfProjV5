using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "Inventory System/Inventory Item")]
    public class InventoryItemData : ScriptableObject
    {
        public int id;
        public string displayName;
        [TextArea(4, 4)]
        public string description;
        public Sprite icon;
        public int maxStackSize;
    }
}
