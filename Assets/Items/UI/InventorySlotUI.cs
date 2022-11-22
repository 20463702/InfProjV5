using Items.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Items.UI
{
    public class InventorySlotUI : MonoBehaviour
    {
        private Image _itemSprite;
        private TextMeshProUGUI _itemCount;
        private InventorySlot _assignedSlot;
        [SerializeField] //! DEBUG
        // ReSharper disable once InconsistentNaming
        private Button _button;
        public InventoryDisplay ParentDisplay { get; private set; }

        private void Awake()
        {
            _itemSprite = GetComponentInChildren<Image>();
            _itemCount = GetComponentInChildren<TextMeshProUGUI>();
            _button = GetComponent<Button>();
            ParentDisplay = transform.parent.GetComponent<InventoryDisplay>();
            
            ClearSlot();
            _button.onClick.AddListener(OnUISlotClick);
        }

        /// <param name="s">Assigned inventory slot</param>
        public void Init(InventorySlot s)
        {
            _assignedSlot = s;
            UpdateUISlot(s);
        }

        public void ClearSlot()
        {
            _assignedSlot?.ClearSlot();
            _itemSprite.sprite = null;
            _itemSprite.color = Color.clear;
            _itemCount.text = string.Empty;
        }

        public void UpdateUISlot(InventorySlot s)
        {
            if (s.ItemData != null)
            {
                _itemSprite.sprite = s.ItemData.icon;
                _itemSprite.color = Color.white;
                _itemCount.text = s.StackSize > 1 ? s.StackSize.ToString() : string.Empty;
            }
            else
                ClearSlot();
        }

        public void UpdateUISlot()
        {
            if (_assignedSlot != null)
                UpdateUISlot(_assignedSlot);
        }
        
        private void OnUISlotClick()
        {
            ParentDisplay.SlotClicked(this);            
        }
    }
}
