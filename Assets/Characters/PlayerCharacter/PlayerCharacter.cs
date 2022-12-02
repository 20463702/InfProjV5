using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Weaponry;
using Weaponry.Melee;

namespace Characters.PlayerCharacter
{
    public class PlayerCharacter : Character
    {
        /// <summary>Global player ref</summary>
        public static PlayerCharacter PlayerRef;

        [SerializeField] private float sprintPlayerSpeed;
        [SerializeField] private Image healthBar;
        
        protected override void Start()
        {
            PlayerRef = this;

            base.Start();

            InitHealth();
            
            sprintPlayerSpeed = sprintPlayerSpeed != 0f ? sprintPlayerSpeed : 6.5f;
        }

        protected override void Update()
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
        
        private void Sprint() => Speed = Input.GetKey(KeyCode.LeftShift) ? sprintPlayerSpeed : Speed;
        
        private void InitHealth()
        {
            MaxHealth = Health;
        }

        private void UpdateHealthBar() =>
            healthBar.fillAmount = Mathf.Clamp(Health / MaxHealth, 0, 1);
    }
}
