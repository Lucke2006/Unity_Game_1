using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{
    public player_movement movement;
    public Transform resetTransform;
    public Slider healthBar;
    void OnCollisionEnter (Collision collisioninfo) 
    {
        if (collisioninfo.collider.tag == "Obstacle" || collisioninfo.collider.tag == "ObstaGround")
        {
            healthBar.value -= 0.20f; 
        }
        if (healthBar.value == 0.0f)
        {
            movement.canMove = false;
        }
        if(collisioninfo.collider.tag == "InstaKill"){
            healthBar.value = 0.0f;
        }
        /*if (collisioninfo.collider.tag == "checkPoint") //checkpoint collisions
        {
            Debug.Log("Hit");
            resetTransform.position = GameObject.FindWithTag("checkPoint").transform.position; 
        }*/
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("checkPoint")) //checkpoint collisions
        {
            Debug.Log("Hit");
            resetTransform.position = other.transform.position; 
        }
    }

}
