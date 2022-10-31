using Items;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;

namespace Characters.PlayerCharacter.InventorySystem.ItemMenu
{
    public class ItemMenu : MonoBehaviour
    {
        private Item _itemRef;
        private int _itemIndex;
        private Button _btnDiscard;
        private Button _btnDiscardAll;
        private Button _btnCancel;
        private TMP_Text _text;

        private void Start()
        {
            _text = transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        }
        
        public void Set(Item iRef)
        {
            _itemRef = iRef;
            
            // ReSharper disable once PossibleInvalidOperationException
            _itemIndex = (int)PlayerCharacter.PlayerRef.ItemIndexInInventory(_itemRef);

            var buttons = GetComponentsInChildren<Button>();
            _btnDiscard = buttons[0];
            _btnDiscardAll = buttons[1];
            _btnCancel = buttons[2];
            
#if UNITY_EDITOR
            transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text =
                $"{_itemRef.name} ({_itemRef.quantity}) [{_itemRef.id}]";
#else
            _text.text = $"{_itemRef.name} ({_itemRef.quantity})";
#endif
            
            _btnDiscard.onClick.AddListener(DiscardOne);
            _btnDiscardAll.onClick.AddListener(DiscardAll);
            _btnCancel.onClick.AddListener(() => { Destroy(gameObject); });
        }

        private void DiscardOne()
        {
            if (PlayerCharacter.PlayerRef.InventoryItems[_itemIndex].quantity <= 1)
            {
                DiscardAll();
                return;
            }

            PlayerCharacter.PlayerRef.InventoryItems[_itemIndex].quantity--;
            PlayerCharacter.PlayerRef.inventoryRef.InvUpdate(PlayerCharacter.PlayerRef.InventoryItems);
            Destroy(gameObject);
        }

        private void DiscardAll()
        {
            PlayerCharacter.PlayerRef.InventoryItems.RemoveAt(_itemIndex);
            PlayerCharacter.PlayerRef.inventoryRef.InvUpdate(PlayerCharacter.PlayerRef.InventoryItems);
            Destroy(gameObject);
        }
    }
}
