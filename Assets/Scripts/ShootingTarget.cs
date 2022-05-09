using System;
using UnityEngine;

public class ShootingTarget : MonoBehaviour
{
    private const string Tag = "SpawnAtRuntime";
    public GameObject enemyExplosion = null;
    public GameObject enemyImpact = null;
    public GameObject parent = null;

    public ScoreModifier scoreModifier = null;

    private LaserParticleController[] particleControllers = null;

    public IntGameEvent onDeathEvent = null;

    public int health = 5;

    private void Start()
    {
        particleControllers = GetComponents<LaserParticleController>();
        var spawnAtRuntime = GameObject.FindWithTag(Tag);
        parent = spawnAtRuntime ?? CreateSpawnAtRuntime();
    }


    private GameObject CreateSpawnAtRuntime()
    {
        var parent = new GameObject(Tag);
        parent.tag = Tag;
        return parent;
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("shot " + other.name);

        ProcessHit();
    }

    private void ProcessHit()
    {
        if ( health-- > 0)
        {
            Debug.Log("damage");
            CreateImpactVFX();
        }
        else
        {
            Debug.Log("dead");
            onDeathEvent?.Trigger(scoreModifier.ScorePoints);
            
            RemoveFromGame();
        }
    }

    private void RemoveFromGame()
    {
        CreateExplosionVFX();
        foreach(var particleController in particleControllers)
        {
            particleController.StopWatchingForShootingTarget(this);
        }

        Destroy(gameObject);
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
