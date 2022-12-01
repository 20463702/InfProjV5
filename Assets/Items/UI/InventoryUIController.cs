using System;
using System.Diagnostics;
using Characters.PlayerCharacter;
using Items.Inventory;
using UnityEngine;

namespace Items.UI
{
    public class InventoryUIController : MonoBehaviour
    {
        [SerializeField] private DynamicInventoryDisplay chestInventoryPanel;
        [SerializeField] private DynamicInventoryDisplay playerInventoryPanel;

        private void Awake()
        {
            chestInventoryPanel.gameObject.SetActive(false);
            playerInventoryPanel.gameObject.SetActive(false);
        }
        
        private void OnEnable()
        {
            InventoryContainer.OnDynamicInventoryDisplayReq += DisplayInventory;
            PlayerInventoryContainer.OnPlayerInventoryDisplayReq += DisplayPlayerDynamicInventory;
        }

        private void OnDisable()
        {
            InventoryContainer.OnDynamicInventoryDisplayReq -= DisplayInventory;
            PlayerInventoryContainer.OnPlayerInventoryDisplayReq -= DisplayPlayerDynamicInventory;
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.Escape) && chestInventoryPanel.gameObject.activeInHierarchy)
                chestInventoryPanel.gameObject.SetActive(false);
            if (Input.GetKey(KeyCode.Escape) && playerInventoryPanel.gameObject.activeInHierarchy)
                playerInventoryPanel.gameObject.SetActive(false);
        }

        private void DisplayInventory(InventorySystem invSys)
        {
            chestInventoryPanel.gameObject.SetActive(true);
            chestInventoryPanel.RefreshDynamicInventory(invSys);
        }

        private void DisplayPlayerDynamicInventory(InventorySystem invSys)
        {
            playerInventoryPanel.gameObject.SetActive(true);
            playerInventoryPanel.RefreshDynamicInventory(invSys);
        }
    }
}
