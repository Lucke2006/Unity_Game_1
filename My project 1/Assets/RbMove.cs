using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography;
using JetBrains.Annotations;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.AssetImporters;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Assertions.Must;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class RbMove : MonoBehaviour
{
    [Header("Vector3")]
    public Vector3 InputKey;
    public Vector3 transformOffset;
    Vector3 position;
    [Header("Transforms")]
    public Transform resetTransform;
    public Transform mainCam;
    public Transform playerVisuals;
    [Header("Bools")]
    public bool canMove = true;
    bool canSideRot = false;
    [Header("Values")]
    public int upForce;
    public int forceDash;
    float rotX = 0.0f; //for fixing rotation in sprint
    float rotZ = 0.0f; //same as above
    [Header("Scripts")]
    public groundCheck scriptGround;
    [Header("Others")]
    public UnityEngine.UI.Slider healthBar;
    public Rigidbody rb;
    private float speed = 7.0f;

    // Start is called before the first frame update
    void start()
    {
        scriptGround = GetComponent<groundCheck>();
    }
    void Update()
    {
        InputKey = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        //HEALTHBAR
        if(healthBar.value == 0.0f)
        {   
            canMove = false;
            if (Input.GetKeyDown("r"))
            {
                transform.position = resetTransform.position + transformOffset; //position when r is pressed
                rb.velocity = Vector3.zero; //velocity set to 0 when r is pressed
                canMove = true;
                transform.eulerAngles = Vector3.zero;   //Player spawns without rotation
                healthBar.value = 100.0f;   //resets healthbar value
            }
        }

        //JUMP
        if(scriptGround.grounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(0, upForce, 0, ForceMode.Impulse);
        }

        //DASH
        if(Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.F))
        {
            rb.AddForce(transform.right * forceDash, ForceMode.Impulse);
        }
        else if(Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.F))
        {
            rb.AddForce(-transform.right * forceDash, ForceMode.Impulse);
        }
        
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //MOVE
        if(canMove == true)
        { 
            moveCharacter(InputKey);
        }
        
        void cameraPlayerMov()
        {
            if (Input.GetKey(KeyCode.W))
            {
                playerVisuals.rotation = Quaternion.Slerp(playerVisuals.rotation, transform.rotation, Time.deltaTime * 4.0f);
                rb.rotation = Quaternion.Slerp(rb.rotation, Quaternion.Euler(rotX, mainCam.localEulerAngles.y, rotZ), 0.1f); //change camera player direction to camera
            }
            //WORKING HERE 05/24/24
            //Notes: Make something so that when S is pressed, the character looks back, and siderot for when its pressed with A or D
            //More Notes: perhaps make the side rot work with ground check movement, and back side rot as well
            //Side Rotation
            //walking backwards doesnt work well, its backwards of where you are facing
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            {
                Debug.Log("Here");
                playerVisuals.rotation = Quaternion.Slerp(playerVisuals.rotation, transform.rotation, 0.5f);
            }
            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            {
                //playerVisuals.rotation = Quaternion.Slerp(playerVisuals.rotation, transform.rotation, 0.5f);
                playerVisuals.localRotation = Quaternion.Slerp(playerVisuals.localRotation, Quaternion.Euler(0.0f, 45.0f, 0.0f), Time.deltaTime * 5.0f);          
            }
            else if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            {
                playerVisuals.localRotation = Quaternion.Slerp(playerVisuals.localRotation, Quaternion.Euler(0.0f, -45.0f, 0.0f), Time.deltaTime * 5.0f);
            }
            else
            {
                //playerVisuals.rotation = Quaternion.Slerp(playerVisuals.rotation, transform.rotation, Time.deltaTime * 4.0f); //Marked out on 09/23/2024 for the purpose of back rot, its oon forward movement now
            }

            // Back Rotation


            //WORKING HERE: 09/18/2024
            if (Input.GetKey(KeyCode.S))
            {
                InputKey = -InputKey;
                rb.rotation = Quaternion.Slerp(rb.rotation, Quaternion.Euler(rotX, mainCam.localEulerAngles.y, rotZ), 0.1f);
                playerVisuals.localRotation = Quaternion.Slerp(playerVisuals.localRotation, Quaternion.Euler(rotX, 180.0f + mainCam.rotation.y, rotZ), 0.5f);
                
                //playerVisuals.rotation = Quaternion.Slerp(rb.rotation, Quaternion.Euler(rotX, 180.0f + mainCam.localEulerAngles.y, rotZ), 0.1f);
                //playerVisuals.forward = -mainCam.forward; //WORKING HERE 09/12/2024


                //transform.rotation = playerVisuals.rotation;
                //playerVisuals.localRotation = Quaternion.Slerp(playerVisuals.localRotation, Quaternion.Euler(playerVisuals.localEulerAngles.x, 180.0f, playerVisuals.localEulerAngles.z), 1.0f);
            }

            if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            {
                Debug.Log("Here");
                playerVisuals.rotation = transform.rotation;
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
            {
                playerVisuals.localRotation = Quaternion.Slerp(playerVisuals.localRotation, Quaternion.Euler(0.0f, -225.0f, 0.0f), 0.5f);
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
            {
                playerVisuals.localRotation = Quaternion.Slerp(playerVisuals.localRotation, Quaternion.Euler(0.0f, 225.0f, 0.0f), 0.5f);
            }

            //
        }
        cameraPlayerMov();
    }

    void moveCharacter( Vector3 Direction)
    {
        Direction = rb.rotation * Direction;

        //SPRINT
        if(Input.GetKey(KeyCode.LeftShift))
        {
            if(Input.GetKey(KeyCode.W))
            {
                speed = Mathf.Lerp(speed, 26.0f, 0.05f);
            }
            else if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
            {
                speed = Mathf.Lerp(speed, 18.0f, 0.05f);
            }
        }
        else
        {
            speed = Mathf.Lerp(speed, 13.0f, 0.03f);
        }
        

        //ACTUAL MOVE

        Vector3 nextPos = rb.position + Direction * speed * Time.deltaTime;

        //DONT GO THROUGH OBJECTS
        if(!Physics.Linecast(transform.position, nextPos, out var ohit)){
            rb.MovePosition(Vector3.Lerp(transform.position, nextPos, 0.5f));
        }


    }
}
