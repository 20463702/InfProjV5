using Items.Inventory;

namespace Characters.PlayerCharacter
{
    public class PlayerInventory : InventorySystem
    {
        protected new void Start()
        {
            base.Start();
            
            for (int i = 0, n = panel.transform.childCount; i < n; i++)
                    Destroy(panel.transform.GetChild(i).gameObject);
        }
    }
}
