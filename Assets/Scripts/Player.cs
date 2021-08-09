using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Vector2 direction = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

     public void OnMove(InputAction.CallbackContext context)
    {
            var value = context.ReadValue<Vector2>();
            Debug.Log(value);
        if (context.performed)
        {
   
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
