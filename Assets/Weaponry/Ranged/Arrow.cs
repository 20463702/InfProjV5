using UnityEngine;

namespace Weaponry.Ranged
{
    public class Arrow : MonoBehaviour
    {
        [HideInInspector] public float ArrowVelocity;
        [SerializeField] private Rigidbody2D _rb;
    
        private void FixedUpdate()
        {
            _rb.velocity = transform.up * ArrowVelocity;
        }
    }
}
