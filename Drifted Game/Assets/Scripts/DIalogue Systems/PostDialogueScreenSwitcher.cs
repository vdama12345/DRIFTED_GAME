using UnityEngine;
using UnityEngine.SceneManagement;

public class PostDialogueScreenSwitcher : MonoBehaviour
{

    [SerializeField] private string sceneName;

    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
