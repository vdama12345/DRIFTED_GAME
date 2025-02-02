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
    void Start() {
        playButton.GetComponent<Button>().onClick.AddListener(() => PlayerCheck());
        
    }
    void PlayerCheck() {
        if(PlayerManager.addPlayer(buttonText.text)) {
            select.SetActive(true);
        } else {
            select.SetActive(false);
        }
    }

}
