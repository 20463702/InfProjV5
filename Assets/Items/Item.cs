using Characters.PlayerCharacter;
using Items.UI.PickupUI;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Item : MonoBehaviour
    {
        [SerializeField]
        private GameObject pickupUIPrefab;
        private const byte PickupRange = 2;
        public byte quantity = 1;
        public byte id;

        private void OnMouseDown()
        {
            if (Vector3.Distance(PlayerCharacter.PlayerRef.transform.position, transform.position) > PickupRange)
                return;
        
            var gui = Instantiate(pickupUIPrefab);
            var ui = gui.GetComponent<ItemPickupUI>();
            ui.SetRefs(this);
        }
    }
}
