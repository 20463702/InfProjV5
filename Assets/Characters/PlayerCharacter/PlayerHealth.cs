using UnityEngine;
using UnityEngine.UI;

namespace Characters.PlayerCharacter
{
    public class PlayerHealth : MonoBehaviour
    {
        public float health;
        public float maxHealth;
        public Image healthBar;

        public GameObject player;
        public Transform respawn;
        void Start()
        {
            maxHealth = health;
        }
    
        void Update()
        {
            healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0f, 1f);   
        }

        private void Death()
        {
        
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                player.transform.position = respawn.position;
             
            }
        }
    }
}
