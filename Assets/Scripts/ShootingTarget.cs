using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTarget : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("shot");
        Destroy(this.gameObject);
    }
}
