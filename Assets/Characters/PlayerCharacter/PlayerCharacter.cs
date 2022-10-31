using System.Diagnostics;
using Characters.Inventory;
using Items;
using UnityEngine;

namespace Characters.PlayerCharacter
{
    public class PlayerCharacter : Character
    {
        public static PlayerCharacter PlayerRef;
        
        [SerializeField]
        public Inventory.InventorySystem inventoryRef;
        private Stopwatch _invToggleSw = new();

        protected new void Start()
        {
            PlayerRef = this;
            
            base.Start();

#region Inventory Setup
            inventoryRef = gameObject.GetComponentInChildren<Inventory.InventorySystem>();
            inventoryRef.gameObject.SetActive(false);
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
                inventoryRef.gameObject.SetActive(!inventoryRef.gameObject.activeInHierarchy);
                _invToggleSw.Restart();
            }
        }

        public override Item GiveItem(Item i, Character other)
        {
            _ = base.GiveItem(i, other);
            inventoryRef.InvUpdate(InventoryItems);
            
            return i;
        }
    }
}
