using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Animations;

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
    float rotationX;
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
        rotationX = Input.GetAxis("Horizontal") * 10.0f;
        rotationX *= Time.deltaTime;
        if (Input.GetKey(KeyCode.W))
        {
            targetRotationY = Quaternion.FromToRotation(Vector3.forward, player.forward); //used for direction on tilted surfaces
            playerVisuals.rotation = Quaternion.Slerp(playerVisuals.rotation, transform.rotation, rotationSpeed * Time.deltaTime);
            rb.rotation = Quaternion.Slerp(rb.rotation, Quaternion.Euler(rotX, mainCam.localEulerAngles.y, rotZ), 0.1f); //change camera player direction to camera
        }
                
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
            
            
            //playerVisuals.localRotation = Quaternion.Slerp(playerVisuals.rotation, Quaternion.Euler(playerVisuals.rotation.x, 180*rotationX, playerVisuals.rotation.z), 0.5f);
            newForward = (transform.forward + transform.right).normalized; //Diagonal the z and x vectors       
        }
        else if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            //playerVisuals.localRotation = Quaternion.Slerp(playerVisuals.localRotation, Quaternion.Euler(0.0f, -45.0f, 0.0f), 0.5f);
            //playerVisuals.forward = Vector3.Lerp(playerVisuals.forward, (transform.forward - transform.right).normalized, 0.5f); //lerps to diagonal vector
            newForward = (transform.forward - transform.right).normalized;
        }




        //MOVE SIDEWAYS

        //ORIGINAL
        // if((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))
        // {
        //     transform.right = mainCam.right;                        //right vector of transform is aligned with right vector of maincam
        //     
        // }

        //TURNING SIDE WALK
        // if(Input.GetKey(KeyCode.A) && !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))
        // {
        //     transform.right = mainCam.right;                        //right vector of transform is aligned with right vector of maincam
        //     playerVisuals.forward = Vector3.Lerp(playerVisuals.forward, -transform.right, 40.0f*Time.deltaTime);
        // }
        // if(Input.GetKey(KeyCode.D) && !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))
        // {
        //     transform.right = mainCam.right;                        //right vector of transform is aligned with right vector of maincam
        //     playerVisuals.forward = Vector3.Lerp(playerVisuals.forward, transform.right, 40.0f*Time.deltaTime);
        // }

        //REGULAR SIDE WALK
        if((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))
        {
            transform.right = mainCam.right;                        //right vector of transform is aligned with right vector of maincam    
            newForward = transform.forward;
        }
        
        
        
        // Back Rotation


        //WORKING HERE: 09/18/2024
        if (Input.GetKey(KeyCode.S))
        {
            targetRotationY = Quaternion.FromToRotation(Vector3.forward, -player.forward);
            scriptMove.InputKey = -scriptMove.InputKey;
            rb.rotation = Quaternion.Slerp(rb.rotation, Quaternion.Euler(rotX, mainCam.localEulerAngles.y, rotZ), 0.1f);
            playerVisuals.localRotation = Quaternion.Slerp(playerVisuals.localRotation, Quaternion.Euler(rotX, 180.0f + mainCam.rotation.y, rotZ), rotationSpeed*Time.deltaTime);
            
        }

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            Debug.Log("Here");
            newForward = -transform.forward;
            //playerVisuals.forward = Vector3.Lerp(playerVisuals.forward, -transform.forward, rotationSpeed*Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            //playerVisuals.forward = Vector3.Lerp(playerVisuals.forward, (transform.right - transform.forward).normalized, 0.5f); //turns with the diagonal vector
            newForward = (transform.right - transform.forward).normalized; //turns the player diagonally
            //playerVisuals.localRotation = Quaternion.Slerp(playerVisuals.localRotation, Quaternion.Euler(0.0f, -225.0f, 0.0f), 0.5f);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            newForward = (-transform.forward - transform.right).normalized; //turns the player diagonally
            //playerVisuals.forward = Vector3.Lerp(playerVisuals.forward, (-transform.forward - transform.right).normalized, 0.5f); //turns with the diagonal vector
            //playerVisuals.localRotation = Quaternion.Slerp(playerVisuals.localRotation, Quaternion.Euler(0.0f, 225.0f, 0.0f), 0.5f);
        }
        playerVisuals.forward = Vector3.Lerp(playerVisuals.forward, newForward, rotationSpeed*Time.deltaTime);

        //ADJUST TO TERRAIN
        if(scriptGround.grounded == true){
            RaycastHit hit;
            if(Physics.Raycast(playerVisuals.position, Vector3.down.normalized, out hit))
            {
                if (hit.normal.y < 1.0f && hit.normal.y > 0.75f)
                {
                    //WORKING HERE 09/27/2024
                    //problem when walking back on tilted objects
                    Quaternion targetRotation = Quaternion.FromToRotation(playerVisuals.up, hit.normal);
                    //Quaternion targetRotationY = Quaternion.FromToRotation(Vector3.forward, -player.forward);
                    Quaternion finalRot = targetRotation * targetRotationY;
                    playerVisuals.rotation = Quaternion.Slerp(playerVisuals.rotation, finalRot, rotationSpeed * Time.deltaTime);
                }
            }
        }
    }
    void FixedUpdate()
    {
        
    }
}
