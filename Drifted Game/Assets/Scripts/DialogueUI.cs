using UnityEngine;
using TMPro;

public class DialougeUI : MonoBehaviour
{
  [SerializeField] private GameObject dialogueBox;
  [SerializeField] private TMP_text textLabel;
  [SerializeField] private DialogueObject testDialogue;

  private TypewriterEffect typewriterEffect;
  
  private void Start()
  {
    typewriterEffect=GetComponent<TypewriterEffect>();
    CloseDialogueBox();
    ShowDialogue(testDialogue);
  }

  public void ShowDialouge(DialogueObject dialogueObject)
  {
    dialogueBox.SetActive(true);
    StartCoroutine(StepthroughDialogue(dialogueObject));
  }

  private IEnumerator StepthroughDialogue(DialogueObject dialogueObject)
  {

    foreach (string dialogue in dialogueObject Dialogue)
    {
      yield return typewriterEffect.Run(dialogue, textLabel);
      yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
    }

    CloseDialogueBox();
  }

  private void CloseDialogueBox()
  {
    dialogueBox.SetActive(false);
    textLabel.text = string.Empty;
  }
}


