using Characters.Interactor;
using Interfaces;
using UnityEngine.Events;

namespace Items.Inventory.Chests
{
    public class ChestInventory : InventoryContainer, IInteractable
    {
        public UnityAction<IInteractable> OnInteractionComplete { get; set; }
        
        public void Interact(Interactor i, out bool interactionSuccessful)
        {
            OnDynamicInventoryDisplayReq?.Invoke(InvSys);
            interactionSuccessful = true;
        }

        public void EndInteraction()
        {
        }
    }
}
