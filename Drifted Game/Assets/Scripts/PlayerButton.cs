using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject select;
    [SerializeField] private TMPro.TextMeshProUGUI buttonText;
    [SerializeField] private GameObject nextPlayer;
    [SerializeField] private GameObject previousPlayer;
    void Start() {
        playButton.GetComponent<Button>().onClick.AddListener(() => PlayerCheck());
        
    }
    void PlayerCheck() {
        if(PlayerManager.addPlayer(buttonText.text)) {
            select.SetActive(true);
            if(!buttonText.text.Equals("Bruno")) {
                nextPlayer.SetActive(true);
            }   
            if(!buttonText.text.Equals("Stella")) {
                previousPlayer.SetActive(false);
            }
        } else {
            select.SetActive(false);
            if(!buttonText.text.Equals("Bruno")) {
                nextPlayer.SetActive(false);
            } 
            if(!buttonText.text.Equals("Stella")) {
                previousPlayer.SetActive(true);
            }
        }
    }

}
