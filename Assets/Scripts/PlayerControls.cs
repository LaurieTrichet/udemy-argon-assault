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
    public float step = 10.0f;

    private bool shouldMove;
    private Vector3 localPos;

    public Ship ship = null;
    public float HorizontalOffset { get => horizontalOffset; set => horizontalOffset = value; }

    public Camera camera = null;
    private Canvas canvasParent = null;
    public RectTransform playableAreaRectTransform = null;
    private float cursorOffsetWidth = 0;
    private float cursorOffsetHeight = 0;



    public float rayLength = 10f;

    private void Start()
    {
        canvasParent = GetComponentInParent<Canvas>();
        var rect = GetComponent<RectTransform>();
        cursorOffsetWidth = rect.rect.width / 2;
        cursorOffsetHeight = rect.rect.height / 2;
    }

    void Update()
    {
        ProcessMovement();
    }



    void OnDrawGizmos()
    {
        Vector3 direction = transform.TransformDirection(Vector3.forward) * rayLength;
        //Gizmos.DrawRay(new Ray(transform.position, direction));

        var a = transform.position;
        //Gizmos.color = Color.blue;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(a, direction);
        //Gizmos.DrawLine(a, b);
    }
    private void ProcessMovement()
    {

        var shipPosition = ship.transform.position;
        Vector3 direction = ship.transform.TransformDirection(Vector3.forward) * rayLength;
        direction = camera.WorldToScreenPoint(direction);
        //var direction = camera.WorldToScreenPoint(shipPosition);
        Debug.Log(direction);
        //localPos = transform.localPosition;

        //direction = new Vector2(step, step) * shipPosition * canvasParent.scaleFactor;
        //var x = localPos.x + direction.x;
        //var y = localPos.y + direction.y;

        //direction = new Vector2(x, y);

        //var newPosition = Vector2.MoveTowards(localPos, direction, Time.deltaTime * speed);
        var newPosition = direction;

            //var minX = -playableAreaRectTransform.rect.width / 2 + cursorOffsetWidth;
            //var maxX = playableAreaRectTransform.rect.width / 2 - cursorOffsetWidth;
            //var minY = -playableAreaRectTransform.rect.height / 2 + cursorOffsetHeight;
            //var maxY = playableAreaRectTransform.rect.height / 2 - cursorOffsetHeight;

            //newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            //newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

            //transform.localPosition = newPosition;
            transform.position = new Vector2(newPosition.x, newPosition.y);
        
    }

}
