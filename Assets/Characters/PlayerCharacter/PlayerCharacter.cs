using System.Diagnostics;
using UnityEngine;

namespace Characters.PlayerCharacter
{
    public class PlayerCharacter : Character
    {
        /// <summary>Global player ref</summary>
        public static GameObject PlayerRef;

        [SerializeField] private float basePlayerSpeed;
        [SerializeField] private float sprintPlayerSpeed;
        
        protected new void Start()
        {
            PlayerRef = gameObject;
            
            base.Start();
            
            basePlayerSpeed = basePlayerSpeed != 0f ? basePlayerSpeed : 4.0f;
            sprintPlayerSpeed = sprintPlayerSpeed != 0f ? sprintPlayerSpeed : 6.5f;
        }
        
        protected void Update()
        {
            Sprint();
            Movement();
        }

        protected override void Movement() =>
            Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        private void Sprint() => Speed = Input.GetKey(KeyCode.LeftShift) ? sprintPlayerSpeed : basePlayerSpeed;
    }
}
