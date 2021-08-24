using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreModifier", menuName = "ScriptableObjects/ScoreModifier")]
public class ScoreModifier : ScriptableObject
{
    [SerializeField] int scorePoints = 20;

    public int ScorePoints { get => scorePoints; set => scorePoints = value; }
}
