using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reviseion_position : MonoBehaviour
{
    public Transform can;

    // Start is called before the first frame update
    void Start()
    {
        transform.position -= can.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //if()
        //
        
    }
}
