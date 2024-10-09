using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dis : MonoBehaviour
{
    // Start is called before the first frame update
    public float displacement;
    void Start()
    {
        InvokeRepeating("ghost", displacement, 10.0f);
    }

    void ghost(){
        gameObject.SetActive(false);
        Invoke("deghost", 5.0f);
    }
    void deghost(){
        gameObject.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
