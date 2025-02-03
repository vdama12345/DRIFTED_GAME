using System.Collections;
using UnityEngine;

public class Players : MonoBehaviour
{
    [SerializeField] private GameObject Stella;
    [SerializeField] private GameObject Luna;
    [SerializeField] private GameObject Rudy;
    [SerializeField] private AudioSource music;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //MusicManager.PlayMusic(music);
        ArrayList active = PlayerManager.getList();
        for(int i = 0; i < active.Count; i++) {
            Debug.Log("i ran");
            switch (active[i]) {
                case "Stella":
                    Stella.SetActive(true);
                    break;
                case "Luna":
                    Luna.SetActive(true);
                    break;
                case "Rudy":
                    Rudy.SetActive(true);
                    break;
            }
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
