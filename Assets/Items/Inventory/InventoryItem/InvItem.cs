using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
            menu.GetComponent<ItemMenu.PlrItemMenu>().Set(_itemRef);
        }

    }
}
