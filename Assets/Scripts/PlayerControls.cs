using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    private const float DelayReloading = 1.0f;
    private float horizontalOffset = 1.0f;
    [Tooltip("how fast the ship moves depending on player's inputs")]
    public float speed = 10.0f;
    [Tooltip("how far the ship will move on the x axis")]
    public float xRange = 3.0f;
    [Tooltip("how far the ship will move on the y axis")]
    public float yRange = 3.0f;

    private bool shouldMove;
    private Vector2 direction;
    private Vector3 localPos;
    private Vector2 moveValue;

    public float HorizontalOffset { get => horizontalOffset; set => horizontalOffset = value; }

    public PlayerInput playerInput = null;
    public Camera camera = null;

    private void Start()
    {

    }

    void Update()
    {
        ProcessMovement();
    }

    private void ProcessMovement()
    {
        if (shouldMove)
        {

            localPos = transform.localPosition;

            direction = new Vector2(xRange, yRange) * moveValue;
            var x = localPos.x + direction.x;
            var y = localPos.y + direction.y;

            direction = new Vector2(x, y);

            var newPosition = Vector2.MoveTowards(transform.localPosition, direction, Time.deltaTime * speed);

            newPosition = camera.ScreenToViewportPoint(newPosition);
            Debug.Log(newPosition);
            newPosition = new Vector2(Mathf.Clamp01(newPosition.x) , Mathf.Clamp01(newPosition.y) );

            newPosition = camera.ViewportToScreenPoint(newPosition);
            Debug.Log(newPosition);

            localPos.x = newPosition.x;
            localPos.y = newPosition.y;
            transform.localPosition = localPos;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveValue = context.ReadValue<Vector2>();
        if (context.performed)
        {
            shouldMove = true;

        }
        if (context.canceled)
        {
            shouldMove = false;
        }
    }
}
