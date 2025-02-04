using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{

    public Button button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        button.GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick() {
        SceneManager.LoadScene("OpeningStreet");
        Debug.Log("here!");
    }
}
