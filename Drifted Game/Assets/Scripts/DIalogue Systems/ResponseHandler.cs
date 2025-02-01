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
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;

    private DialogueUI dialogueUI;

    private List<GameObject> tempResponseButtons = new List<GameObject>();

    private void Start()
    {
        dialogueUI = GetComponent<DialogueUI>();
    }

    public void ShowResponses(Response[] responses)
    {
        float responseBoxHeight = 0;

        foreach (Response response in responses)
        {
            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.gameObject.SetActive(true);
            responseButton.GetComponent<TMP_Text>().text = response.ResponseText;
            responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response));

            tempResponseButtons.Add(responseButton);

            responseBoxHeight += responseButtonTemplate.sizeDelta.y;
        }

        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);

        responseBox.gameObject.SetActive(true);
    }

    private void OnPickedResponse(Response response)
    {

        responseBox.gameObject.SetActive(false);

        foreach (GameObject responseButton in tempResponseButtons)
        {
            Destroy(responseButton);
        }

        tempResponseButtons.Clear();

        dialogueUI.ShowDialogue(response.DialogueObject);

    }
}
