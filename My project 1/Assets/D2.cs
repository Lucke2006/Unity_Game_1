using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ghost", 3.0f, 10.0f);
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
