using System;
using UnityEngine;

public class DialogueActivator : MonoBehaviour, Iinteractible
{
    [SerializeField] private DialougeObject _dialougeObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerMovement playerMovement))
        {
            playerMovement.Interactible = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerMovement playerMovement))
        {
            if (playerMovement.Interactible is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                playerMovement.Interactible = null;
            }
        }
    }

    public void Interact(PlayerMovement playerMovement)
    {
        playerMovement.DialougeUI.ShowDialouge(_dialougeObject);
    }
}