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

            base.Start(); //alle toekomstige zooi na dit zetten anders daantje de niet-neger (maar wel faggot) boos

            InitHealth();
            
            //! delete dit
            Weapon = new MeleeWeapon(40f, 2f, 0.5f);
        }

        protected void Update()
        {
            base.Update();
            
            Movement();
            Sprint();
            UpdateHealthBar();
            if (Input.GetMouseButton(0))
                Attack<Enemy.Enemy>();
        }

        protected override void Movement() =>
            Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

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
