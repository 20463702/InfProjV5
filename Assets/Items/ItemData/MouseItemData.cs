using System.Collections.Generic;
using Items.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Items.ItemData
{
    public class MouseItemData : MonoBehaviour
    {
        private Image _itemSprite;
        private TextMeshProUGUI _itemCount;
        public InventorySlot assignedSlot;
        private bool _isItemDataNotNull;
        private int _uiLayer;

        private void Start()
        {
            _uiLayer = LayerMask.NameToLayer("UI");
        }
        
        private void Awake()
        {
            _isItemDataNotNull = false;
            _itemSprite = GetComponentInChildren<Image>();
            _itemCount = GetComponentInChildren<TextMeshProUGUI>();

            _itemSprite.color = Color.clear;
            _itemCount.text = string.Empty;
        }

        private void Update()
        {
            if (_isItemDataNotNull)
                transform.position = Input.mousePosition;
        }

        public void ClearSlot()
        {
            assignedSlot.ClearSlot();
            _itemCount.text = string.Empty;
            _itemSprite.color = Color.clear;
            _itemSprite.sprite = null;
        }

        public void UpdateMouseSlot(InventorySlot s)
        {
            _isItemDataNotNull = true;
            assignedSlot.AssignItem(s);
            _itemSprite.sprite = s.ItemData.icon;
#if UNITY_EDITOR
            _itemCount.text = $"[{s.ItemData.id}] {s.StackSize}";
#else
            _itemCount.text = s.StackSize > 1 ? s.StackSize.ToString() : string.Empty;
#endif
            _itemSprite.color = Color.white;
        }
    }
}
