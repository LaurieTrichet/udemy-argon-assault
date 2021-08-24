using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTarget : MonoBehaviour
{

    public GameObject enemyExplosion = null;
    public GameObject parent = null;
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("shot");
        Instantiate(enemyExplosion, this.transform.position, this.transform.rotation, parent.transform);
        Destroy(this.gameObject);
    }
}
