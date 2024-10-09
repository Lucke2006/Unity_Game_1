using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamReworked : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Game Objects")]
    GameObject prevGameObject = null;
    [Header("Transforms")]
    public Transform camMain;
    public Transform player;
    public Transform cameraRot;
    [Header("Vectors")]
    private Vector2 lastMousePos; 
    public Vector3 defaultOffset;
    public Vector3 targetOffset;
    Vector2 mouseDelta;
    [Header("Numbers")]
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;
    float rotXkeep = 0.0f;
    float rotZ = 0.0f;
    float rotY = 0.0f;
    public float lerpSpeed;
    float currentXrotation;
    Quaternion lowRot;
    Quaternion newRot;
    

    void Start()
    {
        camMain.position = player.position + defaultOffset;
        lastMousePos = Input.mousePosition;
        Cursor.lockState = CursorLockMode.Confined;
    }
    void FixedUpdate()
    {
        cameraRot.position = player.position;

        RaycastHit hit;
        var transformedOffsetCameraPoint = transform.TransformPoint(defaultOffset);
        if (Physics.Raycast(player.position, (transformedOffsetCameraPoint - player.position).normalized, out hit, UnityEngine.Vector3.Distance(transformedOffsetCameraPoint, player.position)))
        {
            GameObject hitObject = hit.collider.gameObject;
            prevGameObject = hitObject;
            targetOffset = transform.InverseTransformPoint(hit.point);
        }
        else
        {
            prevGameObject = null;
            targetOffset = defaultOffset;
        }

        camMain.localPosition = Vector3.Lerp(camMain.localPosition, targetOffset, lerpSpeed * Time.deltaTime);
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape)){
            Cursor.lockState = CursorLockMode.None;
        }
        // if(Input.GetKey(KeyCode.I)){
        //     mouseDelta.y = -mouseDelta.y;
        // } 
        //cameraRot.position = player.position;
        // Get the mouse delta
        mouseDelta = (Vector2)Input.mousePosition - lastMousePos;
        lastMousePos = Input.mousePosition;
        if(Input.GetKey(KeyCode.I)){
            mouseDelta.y *= -1.0f;
        }

        // Scale the mouse delta by sensitivity
        mouseDelta *= sensitivity * Time.deltaTime;

        // Update the camera rotation based on mouse delta
        //transform.Rotate(Vector3.up, mouseDelta.x, Space.World);
        // cameraRot.Rotate(Vector3.right, -mouseDelta.y, Space.Self);

        // //
        // cameraRot.Rotate(Vector3.up, mouseDelta.x, Space.World);
        //


        // if (cameraRot.localRotation.x <= 90.0f && cameraRot.localRotation.x < -10.0f)
        // {
        //     cameraRot.localRotation = Quaternion.Euler(rotXkeep, cameraRot.localRotation.y, cameraRot.localRotation.z); //this will keep the rotation from going to max on 0
        //     //cameraRot.localRotation = Quaternion.Euler(rotXkeep, cameraRot.eulerAngles.y, cameraRot.eulerAngles.z);
        // }

        // Quaternion currentrot = cameraRot.localRotation; //New rot quaternion equal to current CameraRot
        // float clampedXrot = Mathf.Clamp(cameraRot.rotation.x, -10.0f, 90.0f);   //clamped rot of x
        // Quaternion newRot = Quaternion.Euler(clampedXrot, currentrot.eulerAngles.y, currentrot.eulerAngles.z);  //new rot equal to quaternion including the new values

        //cameraRot.localRotation = newRot;
        if(cameraRot.localRotation.x < 90.0f)
        {
            lowRot = cameraRot.localRotation;
        }
        cameraRot.Rotate(Vector3.right, -mouseDelta.y, Space.Self);
        cameraRot.Rotate(Vector3.up, mouseDelta.x, Space.World);
        cameraRot.localRotation = Quaternion.Slerp(cameraRot.localRotation, Quaternion.Euler(cameraRot.eulerAngles.x, cameraRot.eulerAngles.y, rotZ), 0.5f); //This bounds y and z axis to 0
        // if(88.0f < cameraRot.eulerAngles.x && cameraRot.eulerAngles.x < 90.0f)
        // {
        //     cameraRot.localRotation = Quaternion.Euler(cameraRot.localRotation.x, 0.0f, 0.0f);
        // }

        //cameraRot.localRotation = Quaternion.Slerp(cameraRot.localRotation, newRot, 10.0f * Time.deltaTime);


        //cameraRot.localRotation = Quaternion.Euler(cameraRot.eulerAngles.x, cameraRot.eulerAngles.y, rotZ); //This bounds y and z axis to 0



        // RaycastHit hit;

        // //MOVE CAMERA WITH WALLS BEHIND
        // var transformedOffsetCameraPoint = transform.TransformPoint(defaultOffset);
        // if (Physics.Raycast(player.position, (transformedOffsetCameraPoint - player.position).normalized, out hit, UnityEngine.Vector3.Distance(transformedOffsetCameraPoint, player.position)))
        // {
        //     GameObject hitObject = hit.collider.gameObject;
        //     prevGameObject = hitObject;
        //     targetOffset = transform.InverseTransformPoint(hit.point);
        // }
        // else
        // {
        //     prevGameObject = null;
        //     targetOffset = defaultOffset;
        // }

        // camMain.localPosition = Vector3.Lerp(camMain.localPosition, targetOffset, lerpSpeed * Time.deltaTime);
    }
}
