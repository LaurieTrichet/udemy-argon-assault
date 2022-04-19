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

    void FixedUpdate()
    {
        ClampMovement();
        ProcessMovement();
    }

    private void ClampMovement()
    {
        aimTrackerPosition = targetCamera.WorldToViewportPoint(aimTracker.transform.position);
        aimTrackerPosition.x = Mathf.Clamp01(aimTrackerPosition.x);
        aimTrackerPosition.y = Mathf.Clamp01(aimTrackerPosition.y);
        aimTrackerPosition = targetCamera.ViewportToWorldPoint(aimTrackerPosition);
    }

    private void ProcessMovement()
    {
        direction = targetCamera.WorldToScreenPoint(aimTrackerPosition);
        transform.position = direction;
    }

}
