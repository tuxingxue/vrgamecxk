using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revise_position : MonoBehaviour
{
    // this code is used to revise the position and angle of VRcamera
    public Transform can;
    public Transform pesudocamera;
    private int humanstatus;
    // Start is called before the first frame update
    void Start()
    {
        humanstatus = 1;
        transform.position -= can.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(humanstatus==1)
        {
            Vector3 vtmp = pesudocamera.position;
            transform.position = vtmp-can.localPosition;
        }
    }
}
