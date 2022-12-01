using Items.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Items.UI
{
    public class InventorySlotUI : MonoBehaviour
    {
        private Image _itemSprite;
        private TextMeshProUGUI _itemCount;
        public InventorySlot AssignedSlot { get; private set; }
        [SerializeField] //! DEBUG
        // ReSharper disable once InconsistentNaming
        private Button _button;
        public InventoryDisplay ParentDisplay { get; private set; }

        private void Awake()
        {
            _itemSprite = transform.GetChild(0).gameObject.GetComponent<Image>();
            _itemCount = GetComponentInChildren<TextMeshProUGUI>();
            _button = GetComponent<Button>();
            ParentDisplay = transform.parent.GetComponent<InventoryDisplay>();
            
            ClearSlot();
            _button.onClick.AddListener(OnUISlotClick);
        }

        /// <param name="s">Assigned inventory slot</param>
        public void Init(InventorySlot s)
        {
            AssignedSlot = s;
            UpdateUISlot(s);
        }

        public void ClearSlot()
        {
            AssignedSlot?.ClearSlot();
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
#if UNITY_EDITOR
                _itemCount.text = $"[{s.ItemData.id}] {s.StackSize}";
#else
                _itemCount.text = s.StackSize > 1 ? s.StackSize.ToString() : string.Empty;
#endif
            }
            else
                ClearSlot();
        }

        public void UpdateUISlot()
        {
            if (AssignedSlot != null)
                UpdateUISlot(AssignedSlot);
        }
        
        private void OnUISlotClick()
        {
            ParentDisplay.SlotClicked(this);
        }
    }
}
