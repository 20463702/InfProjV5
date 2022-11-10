using System.Diagnostics;
using Items.Inventory;
using Items;
using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.PlayerCharacter
{
    public class PlayerCharacter : Character
    {
        public static PlayerCharacter PlayerRef;
        private Stopwatch _invToggleSw = new();

        protected new void Start()
        {
            PlayerRef = this;
            
            base.Start();

#region Inventory Setup
            Inventory = gameObject.GetComponentInChildren<PlayerInventory>();
            _invToggleSw.Start();
#endregion Inventory Setup
        }
        protected void Update()
        {
            Movement();
            InventoryToggle();
        }

        protected override void Movement() =>
            Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        private void InventoryToggle()
        {
            if (Input.GetKey(KeyCode.I) && _invToggleSw.ElapsedMilliseconds >= 500)
            {
                Inventory.Panel.gameObject.SetActive(!Inventory.Panel.gameObject.activeInHierarchy);
                _invToggleSw.Restart();
            }
        }
    }
}
