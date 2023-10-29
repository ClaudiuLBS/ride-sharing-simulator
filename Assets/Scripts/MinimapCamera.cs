using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    Transform target;

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(90, target.rotation.eulerAngles.y, 0);
        transform.position = new Vector3(target.position.x, 50, target.position.z);
    }

}
