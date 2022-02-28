using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private float horizontalOffset = 1.0f;
    [Tooltip("how fast the ship moves depending on player's inputs")]
    public float speed = 10.0f;
    public float step = 10.0f;

    public Ship ship = null;
    public float HorizontalOffset { get => horizontalOffset; set => horizontalOffset = value; }

    public Camera targetCamera = null;

    public GameObject aimTracker = null;

    public float rayLength = 10f;

    void Update()
    {
        ProcessMovement();
    }

    void OnDrawGizmos()
    {
        Vector3 direction = transform.TransformDirection(Vector3.forward) * rayLength;
        var a = transform.position;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(a, direction);
    }
    private void ProcessMovement()
    {
        var shipPosition = aimTracker.transform.position;
        var direction = targetCamera.WorldToScreenPoint(shipPosition);
        var newPosition = direction;
        transform.position = new Vector2(newPosition.x, newPosition.y);
    }

}
