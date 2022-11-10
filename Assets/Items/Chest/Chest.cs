using Characters.PlayerCharacter;
using Items.Inventory;
using UnityEngine;

namespace Items.Chest
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Chest : MonoBehaviour
    {
        private const byte InteractionRange = 2;
        public InventorySystem Inventory { get; private set; }

        private void Start()
        {
            Inventory = GetComponentInChildren<InventorySystem>();
        }

        private void OnMouseDown()
        {
            if (Vector3.Distance(PlayerCharacter.PlayerRef.transform.position, transform.position) > InteractionRange)
                return;
            
            Inventory.Panel.SetActive(true);
            PlayerCharacter.PlayerRef.Inventory.Panel.SetActive(true);
        }
    }
}
