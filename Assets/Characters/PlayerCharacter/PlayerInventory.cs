using Items.Inventory;

namespace Characters.PlayerCharacter
{
    public class PlayerInventory : InventorySystem
    {
        protected new void Start()
        {
            base.Start();
            
            for (int i = 0, n = Panel.transform.childCount; i < n; i++)
                    Destroy(Panel.transform.GetChild(i).gameObject);
        }
    }
}
