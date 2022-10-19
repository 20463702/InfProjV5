using Enums;
using UnityEngine;

namespace Characters.PlayerCharacter
{
    public class PlayerCharacter : Character
    {
        protected void Update()
        {
            this.Movement();
        }

        protected override void Movement() =>
            this.Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}
