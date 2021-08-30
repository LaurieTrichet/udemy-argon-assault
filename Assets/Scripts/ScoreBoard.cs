using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    public TMPro.TMP_Text scoreText = null;

    private int score = 0;

    public int Score { get => score; set => score = value; }

    private void Start()
    {
        UpdateUI();
        
    }
    public void UpdateScore(int points)
    {
        score += points;
        UpdateUI();
        Debug.Log("score: " + score);
    }

    private void UpdateUI()
    {
        scoreText.SetText(score.ToString());
    }
}
