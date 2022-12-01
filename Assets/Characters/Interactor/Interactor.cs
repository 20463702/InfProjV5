using System;
using Interfaces;
using UnityEngine;

namespace Characters.Interactor
{
    public class Interactor : MonoBehaviour
    {
        public Transform interactionPoint;
        public LayerMask interactionLayer;
        public float interactionPointRadius = 1f;
        public bool IsInteracting { get; private set; }

        public void Update()
        {
            // ReSharper disable once Unity.PreferNonAllocApi
            var colliders = Physics2D.OverlapCircleAll(interactionPoint.position, interactionPointRadius);

            if (Input.GetKey(KeyCode.E))
            {
                for (int i = 0, n = colliders.Length; i < n; i++)
                {
                    var interactable = colliders[i].GetComponent<IInteractable>();
                    if (interactable != null)
                        StartInteraction(interactable);
                }
            }
        }

        private void StartInteraction(IInteractable interactable)
        {
            interactable.Interact(this, out bool interactionSuccessful);
            IsInteracting = interactionSuccessful;
        }

        private void EndInteraction()
        {
            IsInteracting = false;
        }
    }
}
