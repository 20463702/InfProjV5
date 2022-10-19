using System;
using Characters.PlayerCharacter;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Items.UI.PickupUI
{
    public class ItemPickupUI : MonoBehaviour
    {
        private Button btnYes;
        private Button btnNo;
        [NonSerialized]
        public Item Item;
        [NonSerialized]
        public PlayerCharacter PlayerRef;

        private void Start()
        {
            var buttons = GetComponentsInChildren<Button>();

            if (buttons[0].name == "btnYes")
            {
                btnYes = buttons[0];
                btnNo = buttons[1];
            }
            else
            {
                btnYes = buttons[1];
                btnNo = buttons[0];
            }
            
    #if UNITY_EDITOR
            Debug.Log($"{btnYes}\n{btnNo}");
    #endif // UNITY_EDITOR

            btnNo.onClick.AddListener(() => { DestroyImmediate(this.gameObject); });
            btnYes.onClick.AddListener(PickUp);
        }

        private void PickUp()
        {
            PlayerRef.Inventory.Add(Item);
            Destroy(Item.gameObject);
            
#if UNITY_EDITOR
            foreach (var item in PlayerRef.Inventory)
                Debug.Log(item);
#endif // UNITY_EDITOR
            
            DestroyImmediate(this.gameObject);
        }
    }
}
