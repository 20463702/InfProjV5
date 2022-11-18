using System.Diagnostics;
using UnityEngine;

namespace Characters.PlayerCharacter
{
    public class PlayerCharacter : Character
    {
        /// <summary>
        ///     Global player ref.
        /// </summary>
        public static PlayerCharacter PlayerRef;
        private readonly Stopwatch _invToggleSw = new();
        public bool hasExternalInventoryOpen;

        protected new void Start()
        {
            PlayerRef = this;
            
            base.Start();

            Inventory = gameObject.GetComponentInChildren<PlayerInventory>();
            _invToggleSw.Start();
        }
        protected void Update()
        {
            Movement();
            Sprint();
            InventoryToggle();
        }

        protected override void Movement() =>
            Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        private void InventoryToggle()
        {
            if (hasExternalInventoryOpen || !Input.GetKey(KeyCode.I) || _invToggleSw.ElapsedMilliseconds < 300) return;
            
            Inventory.panel.gameObject.SetActive(!Inventory.panel.gameObject.activeInHierarchy);
            _invToggleSw.Restart();
            hasExternalInventoryOpen = false;
        }

        private void Sprint() => Speed = Input.GetKey(KeyCode.LeftShift) ? 6.5f : 4f;
    }
}
