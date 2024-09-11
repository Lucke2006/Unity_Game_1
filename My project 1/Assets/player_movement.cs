
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class player_movement : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody rb;

    public float forwardForce = 1000f;
    public float leftForce = -1000f;
    public float rightForce = 1000f;
    public float backForce = -1000f;
    public float upForce = 1000f;

    public bool grounded = false;

    public bool canMove = true;

    public Transform resetTransform;
    public Vector3 transformOffset;

    public float curRotY = 0;
    public Slider healthBar;
    float counter1 = 0.0f;


    // Update is called once per frame
    void Update()
    {   
        if(healthBar.value == 0.0f)
        {    
            if (Input.GetKeyDown("r"))
            {
                transform.position = resetTransform.position + transformOffset; //position when r is pressed
                rb.velocity = Vector3.zero; //velocity set to 0 when r is pressed
                canMove = true;
                transform.eulerAngles = Vector3.zero;   //Player spawns without rotation
                healthBar.value = 100.0f;   //resets healthbar value
            }
        }

        if(healthBar.value > 0.0f)
        {
            if(Input.GetKeyDown("r"))
            {
                transform.eulerAngles = Vector3.zero;
            }
        }

        //rb.AddForce(0, 0, forwardForce * Time.deltaTime);
        if (canMove != true)
        {
            return;
        }


        // if(Input.GetKey(KeyCode.RightArrow)){
        //     transform.Rotate(new Vector3(0, 90 * Time.deltaTime, 0));
        // }
        // if(Input.GetKey(KeyCode.LeftArrow))
        // {
        //     transform.Rotate(new Vector3(0, -90 * Time.deltaTime, 0));
        // }


        if (grounded == true)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rb.AddForce(transform.forward * forwardForce * Time.deltaTime);
                // counter1 += forwardForce;
                // rb.AddForce(0, 0, forwardForce * Time.deltaTime);
            }
            //test
            // if(!Input.GetKey(KeyCode.W))
            //     {
            //         print(counter1);
            //         counter1 = 0.0f;
            //     }
            //test
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddForce(transform.right * leftForce * Time.deltaTime);
                //curRotY = Mathf.Lerp(curRotY, -15f, Time.deltaTime);

            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.AddForce(transform.right * rightForce * Time.deltaTime);

            }
            if (Input.GetKey(KeyCode.S))
            {
                rb.AddForce(transform.forward * backForce * Time.deltaTime);

            }
        }

        if (Input.GetKeyDown("space"))
        {
            Debug.Log("SPACE");

            if (grounded)
            {

                rb.AddForce(0, upForce, 0, ForceMode.Impulse);
                grounded = false;
                Debug.Log("JUMP");
            }
        }


        /*if (Input.GetKey(KeyCode.RightArrow))
        {
            curRotY = Mathf.Lerp(curRotY, 360f, Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            curRotY = Vector3.Slerp(curRotY, -360f, Time.deltaTime);
        }*/
        
        //transform.eulerAngles = new Vector3(0, curRotY, 0);
    }

    void OnCollisionEnter(Collision collisioninfo)
    {
        if (collisioninfo.transform.gameObject.tag == "Ground" || collisioninfo.transform.gameObject.tag == "ObstaGround")
        {
            grounded = true;
        }
    }
}
