using System.Diagnostics;
using Characters.PlayerCharacter.InventorySystem;
using Items;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.PlayerCharacter
{
    public class PlayerCharacter : Character
    {
        [FormerlySerializedAs("_inventoryRef")] [SerializeField]
        public Inventory inventoryRef;
        private Stopwatch _invToggleSw = new();

        protected new void Start()
        {
            base.Start();

#region Inventory Setup
            inventoryRef = gameObject.GetComponentInChildren<Inventory>();
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
