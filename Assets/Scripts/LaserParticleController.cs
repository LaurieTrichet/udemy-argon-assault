using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserParticleController : MonoBehaviour
{
    ParticleSystem laserParticleSystem;

    void Start()
    {
        laserParticleSystem = GetComponent<ParticleSystem>();

        StartWatchingForAllShootingTargets();
    }

    private void StartWatchingForAllShootingTargets()
    {
        var colliders = FindObjectsOfType<ShootingTarget>();
        foreach (var collider in colliders)
        {
            laserParticleSystem.trigger.AddCollider(collider);
        }
    }

    public void StopWatchingForShootingTarget(ShootingTarget shootingTarget)
    {
        laserParticleSystem.trigger.RemoveCollider(shootingTarget);
    }
}
