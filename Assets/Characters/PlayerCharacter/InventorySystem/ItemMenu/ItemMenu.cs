using Items;
using UnityEngine;
using UnityEngine.UI;

namespace Characters.PlayerCharacter.InventorySystem.ItemMenu
{
    public class ItemMenu : MonoBehaviour
    {
        private Item _itemRef;
        private int _itemIndex;
        private PlayerCharacter _playerRef;
        private Button _btnDiscard;
        private Button _btnDiscardAll;
        private Button _btnCancel;
        
        public void Set(Item itemRef, PlayerCharacter playerRef)
        {
            _itemRef = itemRef;
            _playerRef = playerRef;
            
            // ReSharper disable once PossibleInvalidOperationException
            _itemIndex = (int)_playerRef.ItemIndexInInventory(_itemRef);

            var buttons = GetComponentsInChildren<Button>();
            _btnDiscard = buttons[0];
            _btnDiscardAll = buttons[1];
            _btnCancel = buttons[2];
            
            _btnDiscard.onClick.AddListener(DiscardOne);
            _btnDiscardAll.onClick.AddListener(DiscardAll);
            _btnCancel.onClick.AddListener(() => { DestroyImmediate(gameObject); });
        }

        private void DiscardOne()
        {
            if (_playerRef.InventoryItems[_itemIndex].quantity <= 1)
            {
                DiscardAll();
                return;
            }

            _playerRef.InventoryItems[_itemIndex].quantity--;
            _playerRef.inventoryRef.InvUpdate(_playerRef.InventoryItems);
            DestroyImmediate(gameObject);
        }

        private void DiscardAll()
        {
            _playerRef.InventoryItems.RemoveAt(_itemIndex);
            _playerRef.inventoryRef.InvUpdate(_playerRef.InventoryItems);
            DestroyImmediate(gameObject);
        }
    }
}
