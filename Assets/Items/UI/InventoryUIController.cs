using System;
using System.Diagnostics;
using Characters.PlayerCharacter;
using Items.Inventory;
using UnityEngine;

namespace Items.UI
{
    public class InventoryUIController : MonoBehaviour
    {
        [SerializeField] private DynamicInventoryDisplay inventoryPanel;

        private void Awake()
        {
            inventoryPanel.gameObject.SetActive(false);
        }
        
        private void OnEnable()
        {
            InventoryContainer.OnDynamicInventoryDisplayReq += DisplayInventory;
        }

        private void OnDisable()
        {
            InventoryContainer.OnDynamicInventoryDisplayReq -= DisplayInventory;
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.Escape) && inventoryPanel.gameObject.activeInHierarchy)
            {
                inventoryPanel.gameObject.SetActive(false);
            }
        }

        private void DisplayInventory(InventorySystem invSys)
        {
            inventoryPanel.gameObject.SetActive(true);
            inventoryPanel.RefreshDynamicInventory(invSys);
        }
    }
}
