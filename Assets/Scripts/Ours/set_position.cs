using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class set_position : MonoBehaviour
{
    // this code is used to revise the position and angle of VRcamera
    public Transform can;
    public Transform pesudocamera;
    public Transform motor;
    public GameObject Bow;
    private Valve.VR.InteractionSystem.Hand hand1, hand2;
    //private Transform left;
    //private Transform right;
    public float speed;
    private float aspeed;
    private GameObject ggleft;
    private GameObject ggright;
    private GameObject fakeleft;
    private GameObject fakeright;
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
        speed = 0f;
        aspeed = 0f;
        //left = transform.Find("lefthand");
        //right = transform.Find("righthand");
        ggleft = GameObject.Find("LeftHand");
        ggright = GameObject.Find("RightHand");
        fakeleft = GameObject.Find("vr_glove_left_model_slim(Clone)");
        fakeright = GameObject.Find("vr_glove_right_model_slim(Clone)");
        m_Rigidbody = GameObject.Find("Truck").GetComponent<Rigidbody>();
        motorbike = GameObject.Find("Truck");
        ggleft.SetActive(true);
        ggright.SetActive(true);
        fakeleft.SetActive(false);
        fakeright.SetActive(false);
        hand1 = GameObject.Find("LeftHand").GetComponent<Valve.VR.InteractionSystem.Hand>();
        hand2 = GameObject.Find("RightHand").GetComponent<Valve.VR.InteractionSystem.Hand>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetGrab() && changebreak == 0 && Bow.GetComponent<Valve.VR.InteractionSystem.ItemPackageSpawner>().bowstatus==0)
        {
            Vector3 vtmp = can.position - pesudocamera.position;
            if (vtmp.sqrMagnitude < gettomotor)
            {
                changebreak = 1;
                StartCoroutine(killchange());
                humanstatus = 1 - humanstatus;
                if (humanstatus == 0)
                {
                    fakeleft.SetActive(false);
                    fakeright.SetActive(false);
                    m_Rigidbody.constraints = 0;
                    ggleft.SetActive(true);
                    ggright.SetActive(true);
                    print("Get off the motor");

                }
                else
                {
                    fakeleft.SetActive(true);
                    fakeright.SetActive(true);
                    StartCoroutine(changedamp());
                    m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ;
                    ggleft.SetActive(false);
                    ggright.SetActive(false);
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
            Vector3 vtmp2 = transform.eulerAngles, vtmp3 = pesudocamera.eulerAngles;
            vtmp2.y = vtmp3.y;
            transform.eulerAngles = vtmp2;
            Vector3 vtmp1 = pesudocamera.position - can.position;
            transform.Translate(vtmp1, Space.World);
            speed = vtmp1.magnitude/Time.deltaTime;
            if(speed>3f)
            {
                hand1.TriggerHapticPulse(0.01f, 100f, speed * 0.0015f);
                hand2.TriggerHapticPulse(0.01f, 100f, speed * 0.0015f);
            }
            float xangle;
            if(Mathf.Abs(motor.eulerAngles.x)>180f)
            {
                xangle = 360f - Mathf.Abs(motor.eulerAngles.x);
            }
            else
            {
                xangle = Mathf.Abs(motor.eulerAngles.x);
            }
            if(speed > 0.03f && xangle > 8f)
            {
                hand1.TriggerHapticPulse(0.05f, 100f, speed * 0.03f);
                hand2.TriggerHapticPulse(0.05f, 100f, speed * 0.03f);
            }
        }
        else
        {
            speed = 0;
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

    public float getspeed()
    {
        //Vector3 vtmp1 = pesudocamera.position - can.position;
        //float speed = vtmp1.magnitude / Time.deltaTime;
        if (humanstatus == 1 && speed > 3f)
        {
            hand1.TriggerHapticPulse(0.03f, 50f, speed * 0.05f);
            hand2.TriggerHapticPulse(0.03f, 50f, speed * 0.05f);
        }
        return speed;
    }

}

