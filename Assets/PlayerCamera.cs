using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Vector3 offset;

    void Start()
    {
        offset.x = 0;
        offset.y = 0;
        offset.z = -10;
    }

    void Update()
    {
        // if (Input.GetAxis("Mouse ScrollWheel") != 0f )
        // {
        //     offset.z += Input.GetAxis("Mouse ScrollWheel") * 3;
        // }
        
        transform.position = target.position + offset;
    }
}
