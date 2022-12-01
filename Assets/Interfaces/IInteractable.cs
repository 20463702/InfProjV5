using Characters.Interactor;
using UnityEngine;
using UnityEngine.Events;

namespace Interfaces
{
    public interface IInteractable
    {
        public UnityAction<IInteractable> OnInteractionComplete { get; set; }

        public void Interact(Interactor i, out bool interactionSuccessful);
        public void EndInteraction();
    }
}
