using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTarget : MonoBehaviour
{

    public GameObject enemyExplosion = null;
    public GameObject enemyImpact = null;
    public GameObject parent = null;

    public ScoreModifier scoreModifier = null;
    private ScoreBoard scoreBoard = null;

    public int health = 5;

    private void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }
    //private void OnParticleCollision(GameObject other)
    //{
    //    Debug.Log("shot "+ other.name);

    //    //ProcessHit();
    //}


        private void ProcessHit()
    {
        if ( health-- > 0)
        {
            scoreBoard.UpdateScore(scoreModifier.ScorePoints);
            Debug.Log("damage");
            CreateImpactVFX();
           
        }
        else
        {
            Debug.Log("dead");
            RemoveFromGame();
        }
    }

    private void RemoveFromGame()
    {
        CreateExplosionVFX();

        Destroy(this.gameObject);
    }

    private void CreateExplosionVFX()
    {
        Instantiate(enemyExplosion, this.transform.position, this.transform.rotation, parent.transform);
    }   
    
    private void CreateImpactVFX()
    {
        Instantiate(enemyImpact, this.transform.position, this.transform.rotation, parent.transform);
    }
}
