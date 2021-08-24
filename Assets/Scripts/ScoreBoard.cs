using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{

    private int score = 0;

    public int Score { get => score; set => score = value; }

    public void UpdateScore(int points)
    {
        score += points;
        Debug.Log("score: " + score);
    }
}
