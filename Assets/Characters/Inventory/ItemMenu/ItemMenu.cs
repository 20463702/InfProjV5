using Items;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;

namespace Characters.Inventory.ItemMenu
{
    public class ItemMenu : MonoBehaviour
    {
        private Item _itemRef;
        private int _itemIndex;
        private TMP_Text _text;

        private void Start()
        {
            _text = transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        }
        
        public void Set(Item iRef)
        {
            _itemRef = iRef;
            
            // ReSharper disable once PossibleInvalidOperationException
            _itemIndex = (int)((Character)PlayerCharacter.PlayerCharacter.PlayerRef).Inventory.IndexOfItem(_itemRef);

            var buttons = GetComponentsInChildren<Button>();
            buttons[0].onClick.AddListener(DiscardOne);
            buttons[1].onClick.AddListener(DiscardAll);
            buttons[2].onClick.AddListener(() => { Destroy(gameObject); });
            
#if UNITY_EDITOR
            transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text =
                $"{_itemRef.name} ({_itemRef.quantity}) [{_itemRef.id}]";
#else
            _text.text = $"{_itemRef.name} ({_itemRef.quantity})";
#endif
        }

        private void DiscardOne()
        {
            if (((Character)PlayerCharacter.PlayerCharacter.PlayerRef).Inventory.Items[_itemIndex].quantity <= 1)
            {
                DiscardAll();
                return;
            }

            ((Character)PlayerCharacter.PlayerCharacter.PlayerRef).Inventory.Items[_itemIndex].quantity--;
            PlayerCharacter.PlayerCharacter.PlayerRef.Inventory.InvUpdate();
            Destroy(gameObject);
        }

        private void DiscardAll()
        {
            ((Character)PlayerCharacter.PlayerCharacter.PlayerRef).Inventory.Items.RemoveAt(_itemIndex);
            PlayerCharacter.PlayerCharacter.PlayerRef.Inventory.InvUpdate();
            Destroy(gameObject);
        }
    }
}
