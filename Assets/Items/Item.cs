using Characters.PlayerCharacter;
using Items.UI.PickupUI;
using Unity.VisualScripting;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Item : MonoBehaviour
    {
        [SerializeField]
        private GameObject pickupUIPrefab;
        private float _pickupRange;
        public byte quantity = 1;
        public byte id;

        private void Start()
        {
            this._pickupRange = 2f;
        }
        
        private void OnMouseDown()
        {
            if (Vector3.Distance(PlayerCharacter.PlayerRef.transform.position, transform.position) > _pickupRange)
                return;
        
            var gui = Instantiate(pickupUIPrefab);
            var ui = gui.GetComponent<ItemPickupUI>();
            ui.SetRefs(this);
            PlayerCharacter.PlayerRef.inventoryRef.InvUpdate(PlayerCharacter.PlayerRef.InventoryItems);
        }
    }
}
