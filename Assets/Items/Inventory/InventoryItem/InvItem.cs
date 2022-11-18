using System;
using Items.Inventory.ItemMenu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Debug = System.Diagnostics.Debug;

namespace Items.Inventory.InventoryItem
{
    public class InvItem : MonoBehaviour
    {
        private Item _itemRef;
        private Button _btn;
        [SerializeField]
        private GameObject menuPrefab;
        
        public void Set(Item i, GameObject itemMenuPrefab)
        {
            menuPrefab = itemMenuPrefab;
            _itemRef = i;
            _btn = GetComponent<Button>();
            _btn.onClick.AddListener(InstantiateMenu);
            
#if UNITY_EDITOR
            transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text =
                $"{_itemRef.name} ({_itemRef.quantity}) [{_itemRef.id}]";
#else
            transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text =
                $"{_itemRef.name} ({_itemRef.quantity})";
#endif
        }

        private void InstantiateMenu()
        {
            var menu = Instantiate(menuPrefab);
            menu.GetComponent<AbstractItemMenu>().Setup(_itemRef);

            if (menu.TryGetComponent<PlrItemMenu>(out var cPlr))
                cPlr.Setup(_itemRef);
            else if (menu.TryGetComponent<ExternalInventoryItemMenu>(out var cExt))
                cExt.Setup(_itemRef);
            else
                throw new Exception("ItemMenu component not found (InvItem.cs 43).");

            menu.transform.SetParent(transform.parent.parent);
        }
    }
}
