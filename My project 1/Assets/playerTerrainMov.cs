using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTerrainMov : MonoBehaviour
{
    public groundCheck scriptGround;
    public int rotationSpeed = 1;
    public bool grounded = false;
    public Transform playerVisual;
    public Transform player;
    
    void start()
    {
        scriptGround = GetComponent<groundCheck>();
    }
    void Update()
    { 
        if(scriptGround.grounded == true){
            RaycastHit hit;
            if(Physics.Raycast(playerVisual.position, Vector3.down.normalized, out hit))
            {
                if (hit.normal.y < 1.0f)
                {
                    Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                    Quaternion targetRotationY = Quaternion.FromToRotation(Vector3.forward, player.forward);
                    Quaternion finalRot = targetRotation * targetRotationY;
                    playerVisual.rotation = Quaternion.Slerp(playerVisual.rotation, finalRot, Time.deltaTime * rotationSpeed);
                }
                // cubeVisual.rotation = targetRotation * targetRotationY;
                // cubeVisual.up = hit.normal;
                
                //print(hit.distance);
            }
        }
        else{
            playerVisual.rotation = Quaternion.Slerp(playerVisual.rotation, player.rotation, 1.0f);
        }
    }
}
