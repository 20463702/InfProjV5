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
        [SerializeField] private Transform attackPoint;
        private float _attackRange = 0.5f;
        [SerializeField] private LayerMask enemyLayers;
        private int _attackDamage = 40;
        private float _timeBetweenAttack;
        private float _startTimeBetweenAttack;
        public AbstractWeapon weapon;

        protected new void Start()
        {
            PlayerRef = this;

            base.Start(); //alle toekomstige zooi na dit zetten anders daantje de niet-neger bo os

            Inventory = gameObject.GetComponentInChildren<PlayerInventory>();

            InitHealth();
            
            //! delete dit
            weapon = new MeleeWeapon(69f, 42069f);
        }

        protected void Update()
        {
            Movement();
            Sprint();
            InventoryToggle();
            UpdateHealthBar();
            if (health <= 0f)
                Respawn();
            if (Input.GetMouseButton(0))
                MeleeAttack();
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

        private void MeleeAttack()
        {
            if (_timeBetweenAttack <= 0)
            { // dan val je aan
                if (Input.GetMouseButton(0))
                {
                    // Common Job LLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL
                    var enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, weapon.Range, Enemy.Layer);
                    Debug.Log(enemiesToDamage.Length);
                    for (int i = 0, n = enemiesToDamage.Length; i < n; i++)
                    {
                        Debug.Log(enemiesToDamage[i]);
                        enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(weapon);
                    }
                }
                _timeBetweenAttack = _startTimeBetweenAttack;
            }
            else
            {
                _timeBetweenAttack -= Time.deltaTime; 
            }
        }
    }
}
