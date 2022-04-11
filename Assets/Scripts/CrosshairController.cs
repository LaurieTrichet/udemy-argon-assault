using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    private float horizontalOffset = 1.0f;
    [Tooltip("how fast the crosshair moves depending on player's inputs")]
    public float speed = 10.0f;
    public float step = 10.0f;

    public PlayerMovementController ship = null;
    public float HorizontalOffset { get => horizontalOffset; set => horizontalOffset = value; }

    public Camera targetCamera = null;

    public GameObject aimTracker = null;
    private Vector3 aimTrackerPosition = Vector3.zero;

    private Vector2 direction = Vector2.zero;

    private void Start()
    {    
        //aimTracker.transform.localPosition = aimTrackerPosition;
    }

    void FixedUpdate()
    {
        ClampMovement();
        ProcessMovement();
    }

    private void ClampMovement()
    {
        var worldToViewportPosition = targetCamera.WorldToViewportPoint(aimTracker.transform.position);

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

      
        aimTrackerPosition = targetCamera.ViewportToWorldPoint(worldToViewportPosition);
       
    }

    private void ProcessMovement()
    {
        direction = targetCamera.WorldToScreenPoint(aimTrackerPosition);
        //direction.x = Mathf.Floor(direction.x);
        //direction.y = Mathf.Floor(direction.y);

        transform.position = direction;
    }



}
