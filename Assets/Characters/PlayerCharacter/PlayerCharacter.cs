using System.Diagnostics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

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
        [SerializeField] private Image healthBar;
        
        protected new void Start()
        {
            PlayerRef = this;
            
            base.Start(); //alle toekomstige zooi na dit zetten anders daantje boos

            Inventory = gameObject.GetComponentInChildren<PlayerInventory>();
            _invToggleSw.Start();
            
            InitHealth();
        }
        protected void Update()
        {
            Movement();
            Sprint();
            InventoryToggle();
            UpdateHealthBar();
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

        private void InitHealth()
        {
            MaxHealth = health;
        }

        private void UpdateHealthBar()
        {
            healthBar.fillAmount = Mathf.Clamp(health / MaxHealth, 0, 1);
        }

    }
}
