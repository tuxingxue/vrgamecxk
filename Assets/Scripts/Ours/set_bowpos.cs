using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class set_bowpos : MonoBehaviour
{
    public GameObject Bow;
    public Transform camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Bow.GetComponent<Valve.VR.InteractionSystem.ItemPackageSpawner>().bowstatus == 0)
        {
            Vector3 vtmp2 = transform.eulerAngles, vtmp3 = camera.eulerAngles;
            vtmp2.y = vtmp3.y;
            transform.eulerAngles = vtmp2;
            Vector3 vtmp1 = camera.position - transform.position;
            vtmp1.x = vtmp1.x - 0.5f;
            vtmp1.x = vtmp1.x - 0.1f;
            transform.Translate(vtmp1, Space.World);
        }
    }
}
