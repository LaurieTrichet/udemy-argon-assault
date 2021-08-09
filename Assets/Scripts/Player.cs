using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    public float HorizontalOffset = 1.0f;
    public float speed = 10.0f;
    public float xRange = 3.0f;
    public float yRange = 3.0f;
    private bool shouldMove;
    Vector2 direction;
    Vector3 localPos;
    private Vector2 moveValue;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
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
        Debug.Log(moveValue + " , " + context.phase);
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
            Debug.Log("fire");
        }
    }

}
