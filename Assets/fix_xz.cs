using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fix_xz : MonoBehaviour
{
    float z;
    // Start is called before the first frame update
    void Start()
    {
        z = transform.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = transform.eulerAngles;
        v.z = transform.Find("Camera").localEulerAngles.z;
        transform.eulerAngles = v;
    }
}
