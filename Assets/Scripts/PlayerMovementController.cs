using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovementController : MonoBehaviour
{
    private const float DelayReloading = 1.0f;
    private float horizontalOffset = 1.0f;
    [Tooltip("how fast the ship moves depending on player's inputs")]
    public float speed = 10.0f;

    [Header("Ship movement based on user inputs")]
    public float rotationSpeed = 10;

    public float controlRollFactor = -6.0f;
    public float rollSpeed = 10;

    [Header("Add lasers here")]
    public List<ParticleSystem> lasers = null;

    [Header("The object which the ship looks toward")]
    public Transform directionVisualIndicator = null;

    public float rayLength = 10f;

    public float HorizontalOffset { get => horizontalOffset; set => horizontalOffset = value; }

    public Camera followCamera = null;

    public PlayerInput playerInput = null;

    public ParticleSystem particleExplosion = null;
    public GameObject model = null;
    private BoxCollider boxCollider = null;

    private bool shouldMove;
    private Vector2 moveValue;

    private Bounds shipBorder;
    private Vector2 shipSize = Vector2.zero;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        shipBorder = boxCollider.bounds;
        shipSize.x = shipBorder.size.x;
        shipSize.y = shipBorder.size.y;
    }

    void FixedUpdate()
    {
        var deltaTime = Time.fixedDeltaTime;
        if (shouldMove)
        {
            ProcessMovement(deltaTime);
        }
        ProcessRotation(deltaTime);
    }
    private void ProcessMovement(float deltaTime)
    {
        Vector3 position = transform.localPosition;
        float singleStep = speed * deltaTime;
        Vector3 targetDirection = position + new Vector3(moveValue.x, moveValue.y, 0) * singleStep;
        transform.localPosition = new Vector3(targetDirection.x, targetDirection.y, position.z);

        ClampMovement();
    }
    private void ClampMovement()
    {

        //tempPosition.x = transform.position.x - shipSize.x;
        //tempPosition.y = transform.position.y - shipSize.y;
        //tempPosition = transform.position;
        //var worldToViewportPosition = followCamera.WorldToViewportPoint(tempPosition);
        var worldToViewportPosition = followCamera.WorldToViewportPoint(transform.position);
        worldToViewportPosition.x = Mathf.Clamp01(worldToViewportPosition.x);
        worldToViewportPosition.y = Mathf.Clamp01(worldToViewportPosition.y);
        transform.position = followCamera.ViewportToWorldPoint(worldToViewportPosition);
        //Debug.Log(transform.position);
    }

    private void ProcessRotation(float deltaTime)
    {
        YawPlane(deltaTime);
        RollPlane(deltaTime);
    }

    private void YawPlane(float deltaTime)
    {
        directionVisualIndicator.localPosition = new Vector3(moveValue.x, moveValue.y, directionVisualIndicator.localPosition.z);

        var lookRotation = Quaternion.LookRotation(directionVisualIndicator.localPosition);
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, lookRotation, rotationSpeed * deltaTime);
    }

    private void RollPlane(float deltaTime)
    {
        float rollAngle = controlRollFactor * moveValue.x;
        var localEulerRotation = transform.localEulerAngles;

        float rollLerped = Mathf.LerpAngle(localEulerRotation.z, rollAngle, rollSpeed * deltaTime);
        transform.localRotation = Quaternion.Euler(localEulerRotation.x, localEulerRotation.y, rollLerped);
    }


    private Vector2 tempPosition = Vector2.zero;

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
            lasers.ForEach(particleSystem =>
            {
                particleSystem.Play();
            });
        }
        else if (context.canceled)
        {
            lasers.ForEach(particleSystem =>
            {
                particleSystem.Stop();
            });
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);

        var explosionInstance = Instantiate(particleExplosion, this.transform.position, this.transform.rotation);
        explosionInstance.Play();
        this.playerInput.enabled = false;
        boxCollider.enabled = false;
        model.SetActive(false);
        StartCoroutine(RestartScene());
    }

    IEnumerator RestartScene()
    {
        yield return new WaitForSecondsRealtime(DelayReloading);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
