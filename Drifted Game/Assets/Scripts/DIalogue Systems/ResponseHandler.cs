using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using System.Collections.Generic;

public class ResponseHandler : MonoBehaviour
{
    [SerializeField] private GameObject option1;
    [SerializeField] private GameObject option2;
    [SerializeField] private GameObject responseBox1;
    [SerializeField] private GameObject responseBox2;

    private ResponseEvent[] responseEvents;
    private DialogueUI dialogueUI;

    private List<GameObject> tempResponseButtons = new List<GameObject>();

    private void Start()
    {
        dialogueUI = GetComponent<DialogueUI>();
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        this.responseEvents = responseEvents;
    }


    public void ShowResponses(Response[] responses)
    {
        ClearResponseListeners(); // Clear previous listeners

        responseBox1.SetActive(true);
        responseBox2.SetActive(true);

        option1.GetComponent<TMP_Text>().text = responses[0].ResponseText;
        option1.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(responses[0], 0));

        option2.GetComponent<TMP_Text>().text = responses[1].ResponseText;
        option2.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(responses[1], 1));
    }

    private void ClearResponseListeners()
    {
        option1.GetComponent<Button>().onClick.RemoveAllListeners();
        option2.GetComponent<Button>().onClick.RemoveAllListeners();
    }



    private void OnPickedResponse(Response response, int responseIndex)
    {

        responseBox1.gameObject.SetActive(false);
        responseBox2.gameObject.SetActive(false);


        if (responseEvents != null && responseIndex <= responseEvents.Length)
        {
            responseEvents[responseIndex].OnPickedResponse?.Invoke();
        }

        responseEvents = null;

        if (response.DialogueObject)
        {
            dialogueUI.ShowDialogue(response.DialogueObject);
        } else
        {
            dialogueUI.CloseDialogueBox();
        }

        dialogueUI.ShowDialogue(response.DialogueObject);

        response.InvokePostDialogueEvent();
    }
}
