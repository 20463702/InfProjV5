using Items.Inventory;
using Items.ItemData;
using UnityEngine;
// ReSharper disable Unity.InefficientPropertyAccess

namespace Items
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class ItemPickup : MonoBehaviour
    {
        public InventoryItemData itemData;
        private BoxCollider2D _boxCollider;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            if (TryGetComponent(out _spriteRenderer))
                _spriteRenderer.sprite = itemData.icon;
            transform.localScale /= 3;

            _boxCollider = GetComponent<BoxCollider2D>();
            _boxCollider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D c)
        {
            var inventory = c.transform.GetComponent<PlayerInventoryContainer>();
            if (!inventory) return;

            if (inventory.AddToInventory(itemData))
                Destroy(gameObject);
        }
    }
}
