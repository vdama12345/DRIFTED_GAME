using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DialogueActivator : MonoBehaviour, IInteractable
{

    [SerializeField] private DialogueObject dialogueObject;

    public void UpdateDialogueObject(DialogueObject dialogueObject)
    {
        this.dialogueObject = dialogueObject;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player 1") && other.TryGetComponent(out PlayerController player))
        {
            player.Interactable = this;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player 1") && other.TryGetComponent(out PlayerController player))
        {

            if (player.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                player.Interactable = null;
            }

        }
    }
    public void Interact(PlayerController player)
    {

        if (TryGetComponent(out DialogueResponseEvents responseEvents))
        {
            player.DialogueUI.AddResponseEvents(responseEvents.Events);
        }
        player.DialogueUI.ShowDialogue(dialogueObject);
    }

}
