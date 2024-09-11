using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visualMove : MonoBehaviour
{

    private Vector2 lastMousePos;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;
    void Start()
    {
        lastMousePos = Input.mousePosition;
    }

    // Update is called once per frame
    void fixedUpdate()
    {
         Vector2 mouseDelta = (Vector2)Input.mousePosition - lastMousePos;
        lastMousePos = Input.mousePosition;

        // Scale the mouse delta by sensitivity
        mouseDelta *= sensitivity * Time.deltaTime;

        // Update the camera rotation based on mouse delta
        transform.Rotate(Vector3.up, mouseDelta.x, Space.World);
    }
}
