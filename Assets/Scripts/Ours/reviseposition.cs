using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class reviseposition : MonoBehaviour
{
    // this code is used to revise the position and angle of VRcamera
    public Transform can;
    public Transform pesudocamera;
    public Transform motor;
    private Transform left;
    private Transform right;
    private GameObject ggleft;
    private GameObject ggright;
    private GameObject motorbike;
    private Rigidbody m_Rigidbody;
    public int humanstatus;
    public float gettomotor;
    public SteamVR_Input_Sources handType; // 1
    public SteamVR_Action_Boolean teleportAction; // 2
    public SteamVR_Action_Boolean grabAction; // 3
    private int changebreak = 0;
    public bool GetTeleportDown() // 1
    {
        return teleportAction.GetStateDown(handType);
    }

    public bool GetGrab() // 2
    {
        return grabAction.GetState(handType);
    }
    // Start is called before the first frame update
    void Start()
    {
        humanstatus = 0;
        left = transform.Find("lefthand");
        right = transform.Find("righthand");
        ggleft = GameObject.Find("glove-left");
        ggright = GameObject.Find("glove-right");
        m_Rigidbody = GameObject.Find("Truck").GetComponent<Rigidbody>();
        motorbike = GameObject.Find("Truck");
        ggleft.SetActive(false);
        ggright.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetGrab()&& changebreak==0)
        {
            Vector3 vtmp = can.position - pesudocamera.position;
            if(vtmp.sqrMagnitude<gettomotor)
            {
                changebreak = 1;
                StartCoroutine(killchange());
                humanstatus = 1 - humanstatus;
                if(humanstatus==0)
                {
                    left.Translate(new Vector3(0, 100, 0), Space.World);
                    right.Translate(new Vector3(0, 100, 0), Space.World);
                    m_Rigidbody.constraints = 0;
                    ggleft.SetActive(false);
                    ggright.SetActive(false);
                    print("Get off the motor");

                }  
                else
                {
                    left.Translate(new Vector3(0, -100, 0), Space.World);
                    right.Translate(new Vector3(0, -100, 0), Space.World);
                    StartCoroutine(changedamp());
                    m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ;
                    ggleft.SetActive(true);
                    ggright.SetActive(true);
                    print("Get on the motor");
                }
            }
            else
            {
                print("Too far, please get closer to the motor");
            }
        }
        if (humanstatus == 1)
        {
            Vector3 vtmp2 = transform.eulerAngles,vtmp3 = pesudocamera.eulerAngles;
            vtmp2.y = vtmp3.y;
            transform.eulerAngles = vtmp2;
            Vector3 vtmp1 = pesudocamera.position- can.position;
            transform.Translate(vtmp1, Space.World);
        }
    }

    private IEnumerator killchange()
    {
        yield return new WaitForSeconds(1);
        changebreak = 0;
    }

    private IEnumerator changedamp()
    {
        float tmpd = motorbike.GetComponent<EasySuspension>().getdamping();
        motorbike.GetComponent<EasySuspension>().setdamping(3f);
        yield return new WaitForSeconds(0.2f);
        motorbike.GetComponent<EasySuspension>().setdamping(tmpd);
    }
}
