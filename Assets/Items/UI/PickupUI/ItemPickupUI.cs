using System;
using Characters.PlayerCharacter;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Items.UI.PickupUI
{
    public class ItemPickupUI : MonoBehaviour
    {
        private Button _btnYes;
        private Button _btnNo;
        [field: NonSerialized]
        public Item ItemRef { get; private set; }
        [field: NonSerialized]
        public PlayerCharacter PlayerRef { get; private set; }

        public void SetRefs(Item itemRef, PlayerCharacter playerRef)
        {
            ItemRef = itemRef;
            PlayerRef = playerRef;
        }

        private void Start()
        {
            var buttons = GetComponentsInChildren<Button>();

            _btnYes = buttons[0];
            _btnNo = buttons[1];
            
            _btnNo.onClick.AddListener(() => { DestroyImmediate(gameObject); });
            _btnYes.onClick.AddListener(PickUp);
        }

        private void PickUp()
        {
            PlayerRef.GiveItem(ItemRef, null);
            
            DestroyImmediate(gameObject);

            ItemRef.gameObject.SetActive(false);
        }
    }
}
