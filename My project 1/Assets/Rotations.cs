using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotations : MonoBehaviour
{
    [Header("Transforms")]
    public Transform mainCam;
    public Transform playerVisuals;
    public Transform player;
    [Header("Scripts")]
    PlayerMove scriptMove;
    groundCheck scriptGround;

    [Header("Others")]
    public Rigidbody rb;
    Quaternion targetRotationY;
    [Header("Values")]
    float rotX = 0.0f; //for fixing rotation in sprint
    float rotZ = 0.0f; //same as above
    public float rotationSpeed = 0.1f;
    void Start()
    {
        scriptMove = GetComponent<PlayerMove>();
        scriptGround = GetComponent<groundCheck>();
    }

    // Update is called once per frame
    void Update()
    {   
        // if(scriptMove.lookDown == true){
        //     transform.forward = -transform.up;
        //     playerVisuals.rotation = scriptMove.saveRot;
        //     scriptMove.lookDown = false;
        // }
        if (Input.GetKey(KeyCode.W))
        {
            targetRotationY = Quaternion.FromToRotation(Vector3.forward, player.forward); //used for direction on tilted surfaces
            playerVisuals.rotation = Quaternion.Slerp(playerVisuals.rotation, transform.rotation, 0.5f);
            rb.rotation = Quaternion.Slerp(rb.rotation, Quaternion.Euler(rotX, mainCam.localEulerAngles.y, rotZ), 0.1f); //change camera player direction to camera
        }
        //WORKING HERE 05/24/24
                
        Vector3 newForward = playerVisuals.forward; //used for turning to diagonal angles
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            Debug.Log("Here");
            // playerVisuals.forward = Vector3.Lerp(playerVisuals.forward, transform.forward, 0.5f);
            newForward = transform.forward;
            //playerVisuals.rotation = Quaternion.Slerp(playerVisuals.rotation, transform.rotation, 0.5f);
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            //playerVisuals.rotation = Quaternion.Slerp(playerVisuals.rotation, transform.rotation, 0.5f);
            //playerVisuals.localRotation = Quaternion.Slerp(playerVisuals.localRotation, Quaternion.Euler(0.0f, 45.0f, 0.0f), 0.5f);
            newForward = (transform.forward + transform.right).normalized; //Diagonal the z and x vectors
            // playerVisuals.forward = Vector3.Lerp(playerVisuals.forward, (transform.forward + transform.right).normalized, 0.5f); //lerps to diagonal vector        
        }
        else if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            //playerVisuals.localRotation = Quaternion.Slerp(playerVisuals.localRotation, Quaternion.Euler(0.0f, -45.0f, 0.0f), 0.5f);
            //playerVisuals.forward = Vector3.Lerp(playerVisuals.forward, (transform.forward - transform.right).normalized, 0.5f); //lerps to diagonal vector
            newForward = (transform.forward - transform.right).normalized;
        }

        playerVisuals.forward = Vector3.Lerp(playerVisuals.forward, newForward, 0.5f);
        //MOVE SIDEWAYS
        if((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))
        {
            transform.right = mainCam.right;                        //right vector of transform is aligned with right vector of maincam
        }

        // Back Rotation


        //WORKING HERE: 09/18/2024
        if (Input.GetKey(KeyCode.S))
        {
            targetRotationY = Quaternion.FromToRotation(Vector3.forward, -player.forward);
            scriptMove.InputKey = -scriptMove.InputKey;
            rb.rotation = Quaternion.Slerp(rb.rotation, Quaternion.Euler(rotX, mainCam.localEulerAngles.y, rotZ), 0.1f);
            playerVisuals.localRotation = Quaternion.Slerp(playerVisuals.localRotation, Quaternion.Euler(rotX, 180.0f + mainCam.rotation.y, rotZ), 0.5f);
            
        }

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            Debug.Log("Here");
            playerVisuals.forward = Vector3.Lerp(playerVisuals.forward, -transform.forward, 0.5f);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            playerVisuals.forward = Vector3.Lerp(playerVisuals.forward, (transform.right - transform.forward).normalized, 0.5f); //turns with the diagonal vector
            //playerVisuals.localRotation = Quaternion.Slerp(playerVisuals.localRotation, Quaternion.Euler(0.0f, -225.0f, 0.0f), 0.5f);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            playerVisuals.forward = Vector3.Lerp(playerVisuals.forward, (-transform.forward - transform.right).normalized, 0.5f); //turns with the diagonal vector
            //playerVisuals.localRotation = Quaternion.Slerp(playerVisuals.localRotation, Quaternion.Euler(0.0f, 225.0f, 0.0f), 0.5f);
        }

        //ADJUST TO TERRAIN
        if(scriptGround.grounded == true){
            RaycastHit hit;
            if(Physics.Raycast(playerVisuals.position, Vector3.down.normalized, out hit))
            {
                if (hit.normal.y < 1.0f)
                {
                    //WORKING HERE 09/27/2024
                    //problem when walking back on tilted objects
                    Quaternion targetRotation = Quaternion.FromToRotation(playerVisuals.up, hit.normal);
                    //Quaternion targetRotationY = Quaternion.FromToRotation(Vector3.forward, -player.forward);
                    Quaternion finalRot = targetRotation * targetRotationY;
                    playerVisuals.rotation = Quaternion.Slerp(playerVisuals.rotation, finalRot, rotationSpeed);
                }
            }
        }
    }
    void FixedUpdate()
    {
        
    }
}
