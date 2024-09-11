using System.Collections;
using UnityEngine;

public class CamCollision : MonoBehaviour
{    // Update is called once per frame
    public Transform camPos;
    public Transform playerPos;
    void Update()
    {

        //origin, orientation

        RaycastHit hit;
        if (Physics.Raycast(playerPos.position, (camPos.position - playerPos.position).normalized, out hit, UnityEngine.Vector3.Distance(camPos.position, playerPos.position)))
        {
            
            Debug.Log("HIT");
        }
    }
}
