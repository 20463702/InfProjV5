using System.Diagnostics;
using Characters.PlayerCharacter.InventorySystem;
using Items;
using UnityEngine;

namespace Characters.PlayerCharacter
{
    public class PlayerCharacter : Character
    {
        [SerializeField]
        private GameObject inventoryUIPrefab;
        private Inventory inventoryRef;
        private Stopwatch InvToggleSw = new();

        protected new void Start()
        {
            base.Start();

#region Inventory Setup
            this.inventoryRef = this.gameObject.GetComponentInChildren<Inventory>();
            this.inventoryRef.gameObject.SetActive(false);
            this.InvToggleSw.Start();
#endregion Inventory Setup
        }
        protected void Update()
        {
            this.Movement();
            this.InventoryToggle();
        }

        protected override void Movement() =>
            this.Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        private void InventoryToggle()
        {
            if (Input.GetKey(KeyCode.I) && this.InvToggleSw.ElapsedMilliseconds >= 500)
            {
                this.inventoryRef.gameObject.SetActive(!this.inventoryRef.gameObject.activeInHierarchy);
                this.InvToggleSw.Restart();
            }
        }

        public override Item GiveItem(Item i)
        {
            this.InventoryItems.Add(i);
            this.inventoryRef.InvUpdate(this.InventoryItems);
            
            return i;
        }
    }
}
