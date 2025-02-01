using UnityEngine;
using Systems.Collections;
using Systems.Collections.Generic;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]

public class DialogueObject : ScriptableObject
{
  [SerializeField] [TextArea] private string[] dialogue;

  public string[] Dialogue => dialogue;



}


