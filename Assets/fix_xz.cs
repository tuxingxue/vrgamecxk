using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fix_xz : MonoBehaviour
{
    public Transform can;
    public GameObject Camerarig;
    float z;
    // Start is called before the first frame update
    void Start()
    {
        z = transform.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        int status = Camerarig.GetComponent<set_position>().humanstatus;
        if(status==1)
        {
            Vector3 v = transform.eulerAngles;
            z = can.localEulerAngles.z;
            v.z = z;
            transform.eulerAngles = v;
        }
        else
        {
           /* Vector3 v = transform.eulerAngles;
            v.z = z;
            transform.eulerAngles = v;*/
        }
    }
}
