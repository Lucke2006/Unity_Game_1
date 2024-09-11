using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class groundCheck : MonoBehaviour
{
    private Vector3 origin;
    public float radius;
    public float maxDistance;
    private float currentHitDistance;

    private Vector3 direction;
    public LayerMask layerMask;
    public bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        origin = transform.position;
        direction = -transform.up;

        RaycastHit hit;
        if(Physics.SphereCast(origin, radius, direction, out hit, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal))
        {
            grounded = true;         
        }
        else{
            grounded = false;
        }
    }
    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + direction * currentHitDistance);
        Gizmos.DrawWireSphere(origin + direction * currentHitDistance, radius);
    }
}
