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

    [Header("Add lasers here")]
    public List<ParticleSystem> lasers = null;

    [Header("The object which the ship looks toward")]
    public Transform reticle = null;

    public float HorizontalOffset { get => horizontalOffset; set => horizontalOffset = value; }



    public PlayerInput playerInput = null;

    public ParticleSystem particleExplosion = null;
    public GameObject model = null;
    private BoxCollider boxCollider = null;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        ProcessMovement();
        //ProcessRotation();
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
            //Debug.Log(moveValue);
            //reticle.transform.position = Vector2.MoveTowards(reticle.transform.position, moveValue, speed);
            //reticle.transform = reticle.transform.position.move
            // Determine which direction to rotate towards
            //Vector3 targetDirection = reticle.position - transform.position;

            //// The step size is equal to speed times frame time.
            //float singleStep = speed * Time.deltaTime;

            //// Rotate the forward vector towards the target direction by one step
            //Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            //// Draw a ray pointing at our target in
            //Debug.DrawRay(transform.position, newDirection, Color.red);

            //// Calculate a rotation a step closer to the target and applies rotation to this object
            //transform.rotation = Quaternion.LookRotation(newDirection);

            // The step size is equal to speed times frame time.
            //var step = speed * Time.deltaTime;

            //// Rotate our transform a step closer to the target's.
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, step);
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
