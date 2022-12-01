// ReSharper disable Unity.InefficientPropertyAccess
using Characters.Interactor;
using Interfaces;
using SaveLoadSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Items.Inventory.Chest
{
    [RequireComponent(typeof(UniqueId))]
    public class ChestInventory : InventoryContainer, IInteractable
    {
        public UnityAction<IInteractable> OnInteractionComplete { get; set; }

        protected override void Awake()
        {
            base.Awake();

            SaveLoad.OnLoadGame += LoadInventory;
        }

        private void Start()
        {
            ChestSaveData chestData = new(InvSys, transform.position, transform.rotation);

            SaveGameManager.data.chestDictionary.Add(GetComponent<UniqueId>().ID, chestData);
        }

        private void LoadInventory(SaveData data)
        {
            if (data.chestDictionary.TryGetValue(GetComponent<UniqueId>().ID, out var chestData))
            {
                InvSys = chestData.inventorySystem;
                transform.position = chestData.position;
                transform.rotation = chestData.rotation;
            }
        }
        
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
