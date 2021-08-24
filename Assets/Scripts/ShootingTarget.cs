using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTarget : MonoBehaviour
{

    public GameObject enemyExplosion = null;
    public GameObject parent = null;

    public ScoreModifier scoreModifier = null;
    private ScoreBoard scoreBoard = null;

    private void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("shot");
        ProcessHit();

        RemoveFromGame();
    }

    private void ProcessHit()
    {
        scoreBoard.UpdateScore(scoreModifier.ScorePoints);
    }

    private void RemoveFromGame()
    {
        Instantiate(enemyExplosion, this.transform.position, this.transform.rotation, parent.transform);

        Destroy(this.gameObject);
    }
}
