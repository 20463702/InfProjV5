using Characters.PlayerCharacter;
using Items.UI.PickupUI;
using Unity.VisualScripting;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Item : MonoBehaviour
    {
        protected BoxCollider2D BoxCollider;
        [SerializeField]
        private GameObject pickupUIPrefab;
        private PlayerCharacter _playerRef;
        private float _pickupRange;
        public int id;

        protected void Start()
        {
            this.BoxCollider = GetComponent<BoxCollider2D>();
            this._playerRef = GameObject.Find("Player").ConvertTo<PlayerCharacter>();
        }

        private void OnMouseDown()
        {
            if (Vector3.Distance(_playerRef.transform.position, this.transform.position) > 2)
                return;
        
            var gui = Instantiate(pickupUIPrefab);
            var ui = gui.GetComponent<ItemPickupUI>();
            ui.SetRefs(this, _playerRef);
        }
    }
}
