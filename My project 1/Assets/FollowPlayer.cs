
using UnityEngine;


public class FollowPlayer : MonoBehaviour
{
    public Transform cameraRot;
    public Vector3 offset;
    //public Transform Rotate;

    void Update()
    {
        transform.position = cameraRot.position + offset;
    }
}
