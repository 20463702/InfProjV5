using System;
using Characters.Enemy1;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Weaponry;
using Weaponry.Melee;

namespace Characters.PlayerCharacter
{
    public class PlayerCharacter : Character
    {
        /// <summary>
        ///     Global player ref.
        /// </summary>
        public static PlayerCharacter PlayerRef;
        
        public bool hasExternalInventoryOpen;
        [SerializeField] private Image healthBar;

        protected new void Start()
        {
            PlayerRef = this;

            base.Start(); //alle toekomstige zooi na dit zetten anders daantje de niet-neger bo os

            Inventory = gameObject.GetComponentInChildren<PlayerInventory>();

            InitHealth();
            
            //! delete dit
            Weapon = new MeleeWeapon(40f, 2f, 1f);
        }

        protected void Update()
        {
            base.Update();
            
            Movement();
            Sprint();
            InventoryToggle();
            UpdateHealthBar();
            if (health <= 0f)
                Respawn();
            if (Input.GetMouseButton(0))
                Attack();
        }

        protected override void Movement() =>
            Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        private void InventoryToggle()
        {
            if (hasExternalInventoryOpen || !Input.GetKey(KeyCode.I)) return;

            Inventory.panel.gameObject.SetActive(!Inventory.panel.gameObject.activeInHierarchy);
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

        private void Respawn()
        {
            transform.position = respawn.position;
            health = MaxHealth;
        }

        protected override void Attack()
        {
            Debug.Log(Weapon.DeltaTimeBetweenAttacks);
            if (Weapon.DeltaTimeBetweenAttacks <= 0)
            {
                if (Input.GetMouseButton(0))
                {
                    var colliders = Physics2D.OverlapCircleAll(transform.position, Weapon.Range);
                    for (int i = 0, n = colliders.Length; i < n; i++)
                    {
                        if (colliders[i].TryGetComponent<Enemy>(out var c) 
                            && Vector3.Distance(transform.position, colliders[i].transform.position) <= Weapon.Range)
                            c.TakeDamage(Weapon);
                    }
                    Weapon.ResetDeltaTime();
                }
            }
        }
    }
}
