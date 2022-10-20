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
        private Inventory _inventoryRef;
        private Stopwatch _invToggleSw = new();

        protected new void Start()
        {
            base.Start();

#region Inventory Setup
            this._inventoryRef = this.gameObject.GetComponentInChildren<Inventory>();
            this._inventoryRef.gameObject.SetActive(false);
            this._invToggleSw.Start();
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
            if (Input.GetKey(KeyCode.I) && this._invToggleSw.ElapsedMilliseconds >= 500)
            {
                this._inventoryRef.gameObject.SetActive(!this._inventoryRef.gameObject.activeInHierarchy);
                this._invToggleSw.Restart();
            }
        }

        public override Item GiveItem(Item i)
        {
            this.InventoryItems.Add(i);
            this._inventoryRef.InvUpdate(this.InventoryItems);
            
            return i;
        }
    }
}
