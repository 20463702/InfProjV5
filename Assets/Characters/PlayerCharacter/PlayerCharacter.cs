using System.Diagnostics;
using UnityEngine;
using Weaponry.Ranged;

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
        [SerializeField] private Transform _hand;
        
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
            RotateHand();
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

        private void RotateHand()
        {
            float angle = RangedCombatUtility.AngleTowardsMouse(_hand.position);
            _hand.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        }
    }
}
