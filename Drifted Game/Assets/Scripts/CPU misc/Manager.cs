using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public TextMeshProUGUI scoreText;
    [SerializeField] public int score = 0;
    [SerializeField] private int totalCPUs = 3;




    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void AddScore(string cpuName)
    {
        score++;
        UpdateScoreText();
        Debug.Log($"Score updated: {score}/{totalCPUs}");
    }

    private void UpdateScoreText()
    {
        scoreText.text = $"{score}/{totalCPUs}";
    }

}