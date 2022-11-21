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

        protected new void Start()
        {
            PlayerRef = this;
            
            base.Start();

        }
        protected void Update()
        {
            Movement();
            Sprint();
        }

        protected override void Movement() =>
            Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));


        private void Sprint() => Speed = Input.GetKey(KeyCode.LeftShift) ? 6.5f : 4f;
    }
}
