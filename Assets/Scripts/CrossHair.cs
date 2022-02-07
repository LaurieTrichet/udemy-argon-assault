using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{


    public Camera followCamera = null;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ClampMovement();
    }

    private void ClampMovement()
    {
        var worldToViewportPosition = followCamera.WorldToViewportPoint(transform.position);

        var shouldOverride = false;
        if (worldToViewportPosition.x < 0)
        {
            worldToViewportPosition.x = 0;
            shouldOverride = true;
        }
        else if (worldToViewportPosition.x > 1)
        {
            worldToViewportPosition.x = 1;
            shouldOverride = true;
        }

        if (worldToViewportPosition.y < 0)
        {
            worldToViewportPosition.y = 0;
            shouldOverride = true;
        }
        else if (worldToViewportPosition.y > 1)
        {
            worldToViewportPosition.y = 1;
            shouldOverride = true;
        }

        if (shouldOverride)
        {
            transform.position = followCamera.ViewportToWorldPoint(worldToViewportPosition);
        }

    }
}
