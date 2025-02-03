using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] AudioSource music;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MusicManager.PlayMusic(music);
    }
}
