using UnityEngine;

public class CamMove2 : MonoBehaviour
{
    Vector2 mouseLook;
    Vector2 smoothV;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;

    public Transform cameraRot;
    public GameObject player;
    //public float maxVerticalAngle = 60.0f; // Limit vertical rotation

    private Vector2 lastMousePos;
    private Quaternion update;
    void Start()
    {
        player = this.transform.parent.gameObject;
        lastMousePos = Input.mousePosition;   
    }

    // Update is called once per frame
    void Update()
    {
        // Get the mouse delta
        Vector2 mouseDelta = (Vector2)Input.mousePosition - lastMousePos;
        lastMousePos = Input.mousePosition;

        // Scale the mouse delta by sensitivity
        mouseDelta *= sensitivity * Time.deltaTime;

        // Quaternion update = Quaternion(mouseDelta.x, mouseDelta.y, 0, 0);   
    }
}
