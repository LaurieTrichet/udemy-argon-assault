using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

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

    [Header("Ship movement based on horizontal and vertical positioning")]
    public float positionPitchFactor = -6.0f;
    public float positionYawFactor = -10.0f;

    [Header("Ship movement based on user inputs")]
    public float controlPitchFactor = -10.0f;
    public float controlRollFactor = -6.0f;

    public List<ParticleSystem> particleSystems = null;

    public float HorizontalOffset { get => horizontalOffset; set => horizontalOffset = value; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ProcessMovement();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControl = moveValue.y * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControl;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = controlRollFactor * moveValue.x;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessMovement()
    {
        if (shouldMove)
        {
            localPos = transform.localPosition;
            direction = new Vector2(xRange, yRange) * moveValue;
            var x = localPos.x + direction.x;
            var y = localPos.y + direction.y;
            direction = new Vector2(Mathf.Clamp(x, -xRange, xRange), Mathf.Clamp(y, -yRange, yRange));
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, direction, Time.deltaTime * speed);
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

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            particleSystems.ForEach(particleSystem => {
                particleSystem.Play();
            });
        } else if (context.canceled)
        {
            particleSystems.ForEach(particleSystem => {
                particleSystem.Stop();
                });
        }
    }

}
