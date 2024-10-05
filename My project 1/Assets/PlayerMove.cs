using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Jobs;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Vector3")]
    public Vector3 InputKey;
    public Vector3 transformOffset;
    Vector3 position;
    [Header("Transforms")]
    public Transform resetTransform;
    public Transform mainCam;
    [Header("Values")]
    public int upForce;
    public int forceDash;
    [Header("Bools")]
    public bool canMove = true;
    public bool lookDown = false;
    [Header("Scripts")]
    groundCheck scriptGround;
    [Header("Others")]
    public UnityEngine.UI.Slider healthBar;
    public Rigidbody rb;
    private float speed = 7.0f;
    float prePosY;
    public Quaternion saveRot;
    // Start is called before the first frame update
    void Start()
    {
        scriptGround = GetComponent<groundCheck>();
    }

    // Update handles healthbar, dashes, jump, inputkey reseting
    void Update()
    {
        InputKey = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //HEALTHBAR
        if (healthBar.value == 0.0f)
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
        if (scriptGround.grounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(0, upForce, 0, ForceMode.Impulse);
        }

        //DASH
        if (Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.F))
        {
            rb.AddForce(transform.right * forceDash, ForceMode.Impulse);
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.F))
        {
            rb.AddForce(-transform.right * forceDash, ForceMode.Impulse);
        }
    }
    void FixedUpdate() //Fixed Update calls the move character function continuously
    {
        if (canMove == true)
        {
            moveCharacter(InputKey);
        }
    }
    void moveCharacter(Vector3 Direction) //This function takes the characters rotatios plus its direction to make a new direction where the characetr will move to
    {
        Direction = rb.rotation * Direction;

        //SPRINT
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.W))
            {
                speed = Mathf.Lerp(speed, 26.0f, 0.05f);
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
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

        // if(!Physics.Linecast(transform.position, nextPos, out var ohit) ){
        //     rb.MovePosition(Vector3.Lerp(transform.position, nextPos, 0.5f));
        // }
        // else{
        //     Debug.Log("Now");
        // }
        // if(scriptGround.grounded == true){
        //     prePosY = transform.position.y;
        //     saveRot = transform.rotation;
        // }
        if (!Physics.SphereCast(transform.position, 0.3f, transform.forward, out var hit, 0.2f))
        {
            rb.MovePosition(Vector3.Lerp(transform.position, nextPos, Time.deltaTime * 20.0f));
            //rb.MovePosition(nextPos);
        }
        else
        {

            // Debug.Log("HIT");
            //Vector3 force = rb.GetAccumulatedForce();
            // rb.AddForce(new Vector3(-10f, -2 - force.y, 0.0f));

            //rb.MovePosition(Vector3.Lerp(transform.position, new Vector3 (transform.position.x, transform.position.y- 1.0f, transform.position.z), 0.3f));
            //transform.forward = -transform.up;
        }
        // Vector3 force = rb.GetAccumulatedForce();
        // Debug.Log(force.y);
        // if(force.y == 0.0f && scriptGround.grounded == false && Physics.SphereCast(transform.position, 0.5f, transform.forward, out hit, 0.2f))
        // {
        //     lookDown = true;

        // }

    }
}
