using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static void PlayMusic(AudioSource music)
    {
        music.Play();
    }
}
