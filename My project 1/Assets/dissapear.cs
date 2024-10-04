using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class dissapear : MonoBehaviour
{
    //WORKING ON DISSAPEARING PLATFORMS AT DIFFERENT TIMES
    Collider objectCollider;
    public Transform mainObject;
    int count;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        count = mainObject.childCount;
        objectCollider = GetComponent<Collider>();
        InvokeRepeating("ghostCall", 1.0f, 20.0f);
    }
    void ghostCall() {
        for (i=0; i < count; i++) 
        {
            ghost(transform.GetChild(i));
        }
        i = 0;
    }
    void ghost(Transform child) 
    {
        child.GetComponent<Renderer>().enabled = false;
        child.GetComponent<Collider>().isTrigger = true;
        //objectCollider.isTrigger = true;
        Invoke("deGhoster", 5);
    }
    void deGhoster() {
        for (int j = 0; j < count; j++)
        {
            deGhost(transform.GetChild(j));
        }
    }
    void deGhost(Transform child) 
    {
        child.GetComponent<Renderer>().enabled = true;
        child.GetComponent<Collider>().isTrigger = false;
        //objectCollider.isTrigger = false;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //InvokeRepeating("ghost", 5.0f, 5.0f);
        //Invoke("deGhost", 5);
    }
    
}
