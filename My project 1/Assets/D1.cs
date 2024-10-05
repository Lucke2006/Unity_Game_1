using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D1 : MonoBehaviour
{
    public float delay = 2.0f;
    public float switchTime = 10f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ghost", delay, switchTime);
    }

    void ghost()
    {
        gameObject.SetActive(false);
        Invoke("deghost", 5.0f);
    }
    void deghost()
    {
        gameObject.SetActive(true);
    }
}
