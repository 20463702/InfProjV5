using Characters.PlayerCharacter;
using UnityEngine.UI;

namespace Items.Inventory.ItemMenu
{
    public class ExternalInventoryItemMenu : AbstractItemMenu
    {
        public new void Setup(Item iRef)
        {
            base.Setup(iRef);

            var buttons = GetComponentsInChildren<Button>();
            buttons[1].onClick.AddListener(TakeOne);
            buttons[2].onClick.AddListener(TakeAll);
        }

        private void TakeOne() =>
            PlayerCharacter.PlayerRef.Inventory.AddItem(ItemRef, transform.parent.GetComponent<InventorySystem>());

        private void TakeAll() =>
            PlayerCharacter.PlayerRef.Inventory.AddItem(ItemRef, transform.parent.GetComponent<InventorySystem>(), ItemRef.quantity);
    }
}
