using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public Transform target;
    float deltaZ;

    void Start()
    {

        deltaZ = transform.position.z - target.position.z;
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(transform.position.x, transform.position.y, target.position.z + deltaZ);

    }
}
