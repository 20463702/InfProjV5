using System;
using Characters.PlayerCharacter;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Items.Inventory.ItemMenu
{
    public abstract class AbstractItemMenu : MonoBehaviour
    {
        protected Item ItemRef;
        private TMP_Text _text;

        private void Awake()
        {
            _text = transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        }

        /// <param name="iRef">Item reference</param>
        public void Setup(Item iRef)
        {
            ItemRef = iRef;

#if UNITY_EDITOR
            _text!.text = $"{ItemRef.name} ({ItemRef.quantity}) [{ItemRef.id}]";
#else
            _text.text = $"{_itemRef.name} ({_itemRef.quantity})";
#endif
            
            var buttons = GetComponentsInChildren<Button>();
            buttons[0].onClick.AddListener(() => { Destroy(gameObject); });
        }
    }
}
