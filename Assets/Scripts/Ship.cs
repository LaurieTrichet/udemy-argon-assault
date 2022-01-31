using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour
{
    private const float DelayReloading = 1.0f;
    private float horizontalOffset = 1.0f;
    [Tooltip("how fast the ship moves depending on player's inputs")]
    public float speed = 10.0f;     
    [Tooltip("how fast the ship rotates depending on player's inputs")]
    public float yawnSpeed = 10.0f;    

    [Header("Ship movement based on user inputs")]

    public float controlRollFactor = -6.0f;

    [Header("Add lasers here")]
    public List<ParticleSystem> lasers = null;

    [Header("The object which the ship looks toward")]
    public Transform directionVisualIndicator = null;

    public float rayLength = 10f;

    public float HorizontalOffset { get => horizontalOffset; set => horizontalOffset = value; }

    public Camera camera = null;

    public PlayerInput playerInput = null;

    public ParticleSystem particleExplosion = null;
    public GameObject model = null;
    private BoxCollider boxCollider = null;

    private bool shouldMove;
    private Vector2 moveValue;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        ProcessMovement();
        ProcessRotation();

    }

    void OnDrawGizmos()
    {
        Vector3 direction = transform.TransformDirection(Vector3.forward) * rayLength;
        //Gizmos.DrawRay(new Ray(transform.position, direction));

        var a = transform.position;
        var b = a + direction;
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(a, direction);
        Gizmos.color = Color.red;
        //Gizmos.DrawLine(a, b);
    }

    private void ProcessRotation()
    {
        directionVisualIndicator.localPosition = new Vector3(moveValue.x, moveValue.y, directionVisualIndicator.localPosition.z) ;
        var lookRotation = Quaternion.LookRotation(directionVisualIndicator.localPosition);
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, lookRotation, yawnSpeed * Time.deltaTime);
        float roll = controlRollFactor * moveValue.x;
        var localRotation = transform.localRotation.eulerAngles;
        float rollLerped = Mathf.LerpAngle(localRotation.z, roll, speed * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(localRotation.x, localRotation.y, rollLerped);
    }

    private void ProcessMovement()
    {
        if (shouldMove)
        {
            Vector3 position = transform.localPosition;
            float singleStep = speed * Time.deltaTime;
            Vector3 targetDirection = position + new Vector3(moveValue.x, moveValue.y, 0) * singleStep;
            transform.localPosition = new Vector3(targetDirection.x, targetDirection.y, position.z);

            ClampMovement();
        }
    }

    private void ClampMovement()
    {
        var worldToViewportPosition = this.camera.WorldToViewportPoint(transform.position);
        worldToViewportPosition.x = Mathf.Clamp01(worldToViewportPosition.x);
        worldToViewportPosition.y = Mathf.Clamp01(worldToViewportPosition.y);
        transform.position = this.camera.ViewportToWorldPoint(worldToViewportPosition);
        //Debug.Log(transform.position);
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
            lasers.ForEach(particleSystem => {
                particleSystem.Play();
            });
        } else if (context.canceled)
        {
            lasers.ForEach(particleSystem => {
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
