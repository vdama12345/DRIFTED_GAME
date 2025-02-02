using UnityEngine;

[System.Serializable]
public class Response
{
    [SerializeField] private string responseText;
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private MonoBehaviour postDialogueScript; // Optional script

    public string ResponseText => responseText;
    public DialogueObject DialogueObject => dialogueObject;
    public MonoBehaviour PostDialogueScript => postDialogueScript;
}