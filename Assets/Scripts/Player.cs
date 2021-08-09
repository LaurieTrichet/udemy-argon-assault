using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    public float HorizontalOffset = 1.0f;
    public float speed = 10.0f;
    private bool shouldMove;
    Vector2 direction;
    Vector3 localPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (shouldMove)
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, direction, Time.deltaTime * speed);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<Vector2>();
        Debug.Log(value + " , " + context.phase);
        if (context.performed)
        {
            shouldMove = true;
            localPos = transform.localPosition;
            direction =  new Vector2(localPos.x, localPos.y) + value * HorizontalOffset;
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
