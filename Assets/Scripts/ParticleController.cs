using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    ParticleSystem particleSystem;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();

        var colliders = FindObjectsOfType<ShootingTarget>();


        foreach( var collider in colliders)
        {
            particleSystem.trigger.AddCollider(collider);
        }
    }

    public void RemoveObject(ShootingTarget shootingTarget)
    {
        particleSystem.trigger.RemoveCollider(shootingTarget);
    }
}
