using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TypewriterEffect : MonoBehaviour
{
  [SerializeField] private float typewriterSpeed = 50f;

  public Coroutine Run()
  {
    return StartCoroutine(routine:TypeText(textToType, textLabel));
  }
  
  private IEnumerator TypeText()
  {

    textLabel.text = string.Empty;

    float t = 0;
    int charIndex = 0;

    while (charIndex < textToType.Length)
    {
      t += Time.deltaTime;
      charIndex = MAthf.FloorToInt(t);
      charIndex = Mathf.Clamp(value.charIndex, min:0, max:textToType.Length);

      textLabel.text = textToType.Substring(0, charIndex);

      yield return null;
    }

    textLabel.text = textToType;

  }
}