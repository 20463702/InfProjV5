using Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Characters.PlayerCharacter.InventorySystem.InventoryItem
{
    public class InvItem : MonoBehaviour
    {
        private PlayerCharacter _playerRef;
        private Item _itemRef;
        private Button _btn;
        [SerializeField]
        private GameObject menuPrefab;
        
        public void Set(Item i, PlayerCharacter plr)
        {
            _itemRef = i;
            _playerRef = plr;
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
            menu.GetComponent<ItemMenu.ItemMenu>().Set(_itemRef, _playerRef);
            // menu.transform.localPosition = new Vector3(0, 0, 0);
        }

    }
}
