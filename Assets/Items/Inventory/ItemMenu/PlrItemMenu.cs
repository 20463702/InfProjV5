using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;

using static Characters.PlayerCharacter.PlayerCharacter;

namespace Items.Inventory.ItemMenu
{
    public class PlrItemMenu : AbstractItemMenu
    {
        private int _itemIndex;
        
        /// <param name="iRef">Item reference</param>
        public new void Setup(Item iRef)
        {
            base.Setup(iRef);

            _itemIndex = (int)PlayerRef.Inventory.IndexOfItem(iRef);

            var buttons = GetComponentsInChildren<Button>();
            buttons[1].onClick.AddListener(DiscardOne);
            buttons[2].onClick.AddListener(DiscardAll);
        }

        private void DiscardOne()
        {
            if (PlayerRef.Inventory.Items[_itemIndex].quantity <= 1)
            {
                DiscardAll();
                return;
            }

            PlayerRef.Inventory.Items[_itemIndex].quantity--;
            PlayerRef.Inventory.InvUpdate();
            Destroy(gameObject);
        }

        private void DiscardAll()
        {
            PlayerRef.Inventory.Items.RemoveAt(_itemIndex);
            PlayerRef.Inventory.InvUpdate();
            Destroy(gameObject);
        }
    }
}
