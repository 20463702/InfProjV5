using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Items.ItemData
{
    public class MouseItemData : MonoBehaviour
    {
        private Image _itemSprite;
        private TextMeshProUGUI _itemCount;

        private void Awake()
        {
            _itemSprite = GetComponentInChildren<Image>();
            _itemCount = GetComponentInChildren<TextMeshProUGUI>();

            _itemSprite.color = Color.clear;
            _itemCount.text = string.Empty;
        }
    }
}
